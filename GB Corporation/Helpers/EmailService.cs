using GB_Corporation.DTOs;
using GB_Corporation.Interfaces.Repositories;
using GB_Corporation.Models;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;

namespace GB_Corporation.Helpers
{
    public class EmailService
    {
		private readonly IRepository<Employee> _employeeRepository;
        private readonly IConfiguration _configuration;

        public EmailService(IRepository<Employee> employeeRepository, IConfiguration configuration)
		{
            _employeeRepository = employeeRepository;
            _configuration = configuration;
        }

        public void SendEmail(EmailDTO model)
        {
            try
            {
                EmailModel mailSettings = _configuration.GetSection("mailSettings").Get<EmailModel>();
                
                // create message
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse(mailSettings.From));
                email.Subject = model.Subject;
                email.Body = new TextPart(TextFormat.Html) { Text = model.Text };

                // send email
                var clients = _employeeRepository.GetListResultSpec(x => x.Where(p => model.SendTo.Contains(p.Id))).Select(x => x.Email).ToList();

                foreach (var item in clients)
                {
                    email.To.Clear();
                    email.To.Add(MailboxAddress.Parse(item));

                    using (var client = new SmtpClient())
                    {
                        client.Connect(mailSettings.Host, mailSettings.Port, SecureSocketOptions.Auto);
                        client.Send(email);
                        client.Disconnect(true);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending with message: {ex.Message} Stack Trace: {ex.StackTrace} {ex}");
            }
        }
	}
}
