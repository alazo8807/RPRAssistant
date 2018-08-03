using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RPRAssistant.Models
{
	[Table("Persons")]
	public class Person
	{
		[Key]
		public int Id { get; set; }

		public string Name { get; set; }

		public string EmailAddress { get; set; }
	}
}