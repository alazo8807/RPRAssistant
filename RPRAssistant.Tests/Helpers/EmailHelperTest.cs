using NUnit.Framework;
using RPRAssistant.Core.Helpers;

namespace RPRAssistant.Tests.Helpers
{
	[TestFixture]
	public class EmailHelperTest
	{
		[Test]
		public void SendEmail()
		{
			string[] emailList = { @"alazo8807@gmail.com" };

			var result = EmailHelper.SendSMTPWithGmail(emailList, "Hello World");

			Assert.That(result, Is.EqualTo("Mail has been successfully sent!"));
		}
	}
}


