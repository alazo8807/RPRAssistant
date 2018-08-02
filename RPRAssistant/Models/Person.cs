using System.ComponentModel.DataAnnotations;

namespace RPRAssistant.Models
{
	public class Person
	{
		[Key]
		public int Id { get; set; }

		public string Name { get; set; }

		public string EmailAddress { get; set; }
	}
}