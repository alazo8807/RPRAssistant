using System;
using System.Net;
using System.Net.Mail;

namespace RPRAssistant.Core.Helpers
{
	public class EmailHelper
	{
		public static string SendSMTPWithGmail(string [] ReciverMail, string body)
		{
			MailMessage msg = new MailMessage();

			msg.From = new MailAddress("alazo8807@gmail.com");

			foreach (var email in ReciverMail)
			{
				msg.To.Add(email);
			}

			msg.Subject = "Hello world! " + DateTime.Now.ToString();
			msg.Body = body;

			SmtpClient client = new SmtpClient();

			client.UseDefaultCredentials = false;
			client.Host = "smtp.gmail.com";
			client.Port = 587;
			client.EnableSsl = true;
			client.DeliveryMethod = SmtpDeliveryMethod.Network;
			client.Credentials = new NetworkCredential("42rprmembers@gmail.com", "42RpR1234");
			client.Timeout = 20000;
			try
			{
				client.Send(msg);
				return "Mail has been successfully sent!";
			}
			catch (Exception ex)
			{
				return "Fail Has error" + ex.Message;
			}
			finally
			{
				msg.Dispose();
			}
		}
	}
}