namespace DocumentAccessApprovalSystemAPI.Services
{
    public class LocalMailService : IMailService
    {
        
        private string _mailFrom = "noreply@company.com";
        public void Send(string mailTo, string subject, string message)
        {
            // Code to send email
            System.Console.WriteLine($"Mail sent to {mailTo} from {_mailFrom} with subject {subject} and message {message}");
        }
    }
}
