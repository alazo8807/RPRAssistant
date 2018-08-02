using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RPRAssistant.Models
{
	public class CleaningSchedule
	{
		[Key]
		public int Id { get; set; }

		public int PersonId { get; set; }

		[ForeignKey("PersonId")]
		public Person Person { get; set; }

		public DateTime SchedDate { get; set; }

		public bool EmailedFlag { get; set; }
	}
}