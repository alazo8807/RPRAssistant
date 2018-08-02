using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;

namespace RPRAssistant.Core.Helpers
{
	public static class SerializationHelper
	{
		public static object JsonParse(string value, Type type)
		{
			return JsonConvert.DeserializeObject(value, type);
		}

		public static object JsonParse(string value)
		{
			object result;
			try
			{
				result = JsonConvert.DeserializeObject(value);
			}
			catch (Exception)
			{
				result = null;
			}
			return result;
		}

		public static T JsonParse<T>(string value)
		{
			return JsonConvert.DeserializeObject<T>(value);
		}

		public static void JsonPopulate(string value, object target)
		{
			JsonConvert.PopulateObject(value, target);
		}

		public static string JsonStringify(object val)
		{
			return JsonConvert.SerializeObject(val, 0);
		}

		public static string JsonStringifyIgnoreNullValues(object val)
		{
			var formatting = Newtonsoft.Json.Formatting.None;
			var jsonSerializerSettings = new JsonSerializerSettings
			{
				StringEscapeHandling = StringEscapeHandling.EscapeHtml,
				NullValueHandling = NullValueHandling.Ignore,
				ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
				PreserveReferencesHandling = PreserveReferencesHandling.Objects
			};

			return JsonConvert.SerializeObject(val, formatting, jsonSerializerSettings);
		}

		public static object DeserializeFromXmlDataContract(string xml, Type typeToDeserializeInto)
		{
			object result;
			using (StringReader stringReader = new StringReader(xml))
			{
				result = DeserializeFromXmlDataContract(XmlReader.Create(stringReader, GetXmlReaderSettingsForDeserializeFromXmlDataContruct()), typeToDeserializeInto);
			}

			return result;
		}

		internal static object DeserializeFromXmlDataContract(XmlReader reader, Type typeToDeserializeInto)
		{
			return GetDataContractSerializer(typeToDeserializeInto).ReadObject(reader);
		}

		private static DataContractSerializer GetDataContractSerializer(Type type)
		{
			return new DataContractSerializer(type);
		}

		private static XmlReaderSettings GetXmlReaderSettingsForDeserializeFromXmlDataContruct()
		{
			return new XmlReaderSettings { CheckCharacters = false };
		}

		public static string SerializeToXmlDataContract(object obj)
		{
			if (obj == null)
				throw new ArgumentNullException("obj");

			return SerializeToXmlDataContract(obj, false);
		}

		public static string SerializeToXmlDataContract(object obj, bool omitHeader)
		{
			if (obj == null)
				throw new ArgumentNullException("obj");

			return SerializeToXmlDataContract(obj, obj.GetType(), omitHeader, false);
		}

		public static string SerializeToXmlDataContract(object obj, Type type, bool omitHeader)
		{
			return SerializeToXmlDataContract(obj, type, omitHeader, false);
		}

		public static string SerializeToXmlDataContract(object obj, Type type, bool omitHeader, bool makeXmlReadable)
		{
			var stringBuilder = new StringBuilder();
			var xmlWriterSettings = new XmlWriterSettings
			{
				CheckCharacters = false,
				OmitXmlDeclaration = omitHeader
			};

			if (makeXmlReadable)
			{
				xmlWriterSettings.Indent = true;
				xmlWriterSettings.IndentChars = "\t";
			}

			XmlWriter xmlWriter = XmlWriter.Create(stringBuilder, xmlWriterSettings);
			GetDataContractSerializer(type).WriteObject(xmlWriter, obj);
			xmlWriter.Close();

			return stringBuilder.ToString();
		}

		/// <summary>
		/// Find value for given tag in a JSON string
		/// </summary>
		/// <param name="json">String for JSON source</param>
		/// <param name="value">String of the property name (tag) that we want value for</param>
		/// <returns>Object for the value element of the JSON pair</returns>
		public static object FindValue(string json, string value)
		{
			object result = null;

			using (JsonTextReader reader = new JsonTextReader(new StringReader(json)))
			{
				while (reader.Read())
				{
					if (reader.Value != null)
					{
						if (reader.TokenType.ToString() == "PropertyName" && reader.Value.ToString() == value)
						{
							// next value after the tag is the value that we want 
							reader.Read();

							return reader.Value;
						}
					}
				}
			}

			// tag not found
			return result;
		}

		public static IList<T> JsonParseFile<T>(string filePath)
		{
			try
			{
				var jsonText = File.ReadAllText(filePath);
				var result = JsonConvert.DeserializeObject<IList<T>>(jsonText);
				return result;
			}
			catch (Exception ex)
			{
				return null;
			}
		}
		
	}
}
