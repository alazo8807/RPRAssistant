using System;

namespace RPRAssistant.Core.Dtos
{
	public class CleaningSchedDto
	{
		public string PersonId { get; set; }
		public DateTime Date { get; set; }
		public bool EmailedFlag { get; set; }
	}
}