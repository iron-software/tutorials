using System.ComponentModel.DataAnnotations;

namespace TicketsApps.Models
{
    public class ClientModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Email { get; set; }

    }
}
