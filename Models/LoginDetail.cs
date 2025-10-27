namespace CRM_ManagementInterface.Models
{
    public class LoginDetail
    {
        public int LoginId { get; set; }
        public int ClientId { get; set; }
        public required Clients Client { get; set; }

        public required string Username { get; set; }
        public required string PasswordHash { get; set; }
    }
}
