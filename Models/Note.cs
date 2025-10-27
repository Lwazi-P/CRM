using System;
namespace CRM_ManagementInterface.Models
{
    public class Note
    {
         public int NoteId { get; set; }
        public int ClientId { get; set; }
        public Clients Client { get; set; }

        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public string Content { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
