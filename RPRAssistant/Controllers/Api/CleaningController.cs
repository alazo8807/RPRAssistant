using RPRAssistant.Core.Dtos;
using RPRAssistant.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace RPRAssistant.Controllers.Api
{
	public class CleaningController : ApiController
	{
		public CleaningController()
		{

		}

		public IHttpActionResult GetCleaningSched()
		{
			var file = @"
			[
				{
					""PersonId"": ""Ale"", 
					""Date"": ""2018 -07-30T18:44:56.2727464-04:00""
				},
				{
					""PersonId"": ""Fer"",
					""Date"": ""2018 -07-06T18:44:56.2727464-04:00""
				},
				{
					""PersonId"": ""Mau"",
					""Date"": ""2018 -07-07T18:44:56.2727464-04:00""
				}
			]";

			var sched = SerializationHelper.JsonParse<List<CleaningSchedDto>>(file);
			
			return Ok(sched);
		}

		public IHttpActionResult GetCleaningSchedForSpecificDay(DateTime date)
		{
			var file = @"
			{
				""PersonId"": ""Ale"", 
				""Date"": ""2018 -07-30T18:44:56.2727464-04:00""
			}";

			CleaningSchedDto sched = SerializationHelper.JsonParse<CleaningSchedDto>(file);

			return Ok(sched.PersonId);
		}

		[HttpGet]
		public IHttpActionResult SendScheduleNotification(DateTime date)
		{
			var file = @"
			{
				""PersonId"": ""Ale"", 
				""Date"": ""2018 -07-30T18:44:56.2727464-04:00"",
				""EmailedFlag"": ""True""
			}";

			CleaningSchedDto sched = SerializationHelper.JsonParse<CleaningSchedDto>(file);

			var person = sched.PersonId;

			string[] emailList = { @"alazo8807@gmail.com" };
			string body = string.Format("Hello RPR!, This week is {0}'s cleaning turn. Have a good week!" + "\r\n", person);
			string footer = string.Format("Please do not reply to this email. This is an automatic notification service.");

			var result = EmailHelper.SendSMTPWithGmail(emailList, string.Format("{0}{1}", body, footer));

			//SendEmail(person)

			return Ok(result);
		}
	}
}
