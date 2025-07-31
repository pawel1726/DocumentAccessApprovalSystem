namespace DocumentAccessApprovalSystemAPI.Services
{
    public interface IMailService
    {
        void Send(string mailTo, string subject, string message);
    }
}