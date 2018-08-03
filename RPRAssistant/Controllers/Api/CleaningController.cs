using RPRAssistant.Core.Helpers;
using RPRAssistant.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;

namespace RPRAssistant.Controllers.Api
{
	[RoutePrefix("api")]
	public class CleaningController : ApiController
	{
		private ApplicationDbContext _context;

		public CleaningController()
		{
			_context = new ApplicationDbContext();
		}

		public IHttpActionResult GetCleaningSched()
		{
			var sched = _context.CleaningSchedules.ToList();

			if (sched != null)
			{
				return Ok(sched);
	}
			else
			{
				return NotFound();
			}
		}

		public IHttpActionResult GetCleaningSched(int id)
		{
			var sched = _context.CleaningSchedules.Where(cs => cs.Id == id).SingleOrDefault();

			if (sched != null)
			{
				return Ok(sched);
			}
			else
			{
				return NotFound();
			}
		}

		public IHttpActionResult GetCleaningSchedPerDate(DateTime date)
		{
			var sched = _context.CleaningSchedules
				.Include(p => p.Person)
				.Where(p => p.SchedDate <= date).FirstOrDefault();


			if (sched != null)
			{
				return Ok(sched);
			}
			else
			{
				return NotFound();
			}
		}

		//public IHttpActionResult GetCleaningSchedForSpecificDay(DateTime date)
		//{
		//	var file = @"
		//	{
		//		""PersonId"": ""Ale"", 
		//		""Date"": ""2018 -07-30T18:44:56.2727464-04:00""
		//	}";

		//	CleaningSchedDto sched = SerializationHelper.JsonParse<CleaningSchedDto>(file);

		//	return Ok(sched.PersonId);
		//}

		[HttpGet]
		[Route("cleaning/notify")]
		public IHttpActionResult SendScheduleNotification(DateTime date)
		{
			var sched = _context.CleaningSchedules
				.Include(p => p.Person)
				.Where(p => p.SchedDate <= date)
				.SingleOrDefault();

			if (sched != null)
			{
				var person = sched.Person.Name;

				string[] emailList = { @"alazo8807@gmail.com" };
				string body = string.Format("Hello RPR!, This is {0}'s cleaning week. Have a good one!" + "\n\r" + "\n\r", person);
				string footer = string.Format("Please do not reply to this email. This is an automatic notification service created by the amazing Alejo ;).");

				var result = EmailHelper.SendSMTPWithGmail(emailList, string.Format("{0}{1}", body, footer));

				return Ok(result);
			}
			else
			{
				return NotFound();
			}
		}

		//[HttpGet]
		//public IHttpActionResult SendScheduleNotification(DateTime date)
		//{
		//	//var file = @"
		//	//{
		//	//	""PersonId"": ""Ale"", 
		//	//	""Date"": ""2018 -07-30T18:44:56.2727464-04:00"",
		//	//	""EmailedFlag"": ""True""
		//	//}";

		//	//CleaningSchedDto sched = SerializationHelper.JsonParse<CleaningSchedDto>(file);

		//	var sched = _context.CleaningSchedules
		//		.Include(p => p.Person)
		//		.Where(p => p.SchedDate <= date)
		//		.SingleOrDefault();

		//	if (sched != null)
		//	{
		//		var person = sched.PersonId;

		//		string[] emailList = { @"alazo8807@gmail.com" };
		//		string body = string.Format("Hello RPR!, This week is {0}'s cleaning turn. Have a good week!" + "\r\n", person);
		//		string footer = string.Format("Please do not reply to this email. This is an automatic notification service.");

		//		var result = EmailHelper.SendSMTPWithGmail(emailList, string.Format("{0}{1}", body, footer));

		//		return Ok(result);
		//	}
		//	else
		//	{
		//		return NotFound();
		//	}
			
		//}
	}
}
