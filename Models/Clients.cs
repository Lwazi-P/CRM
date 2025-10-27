using System.ComponentModel.DataAnnotations;

namespace CRM_ManagementInterface.Models
{
    public class Clients
    {
        [Key]
        public int ClientId { get; set; }

        public int TitleId { get; set; }
        public Title? Title { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string ContactNumber { get; set; }
        public string Address { get; set; }

        public int ClientTypeId { get; set; }
        public ClientType? ClientType {  get; set; }
        public ClientType? ClientTypeName { get; set; }

        
    }
}

