using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TravelGuide.Models
{
    public class Trip
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public int ClientId { get; set; }

        public Client client { get; set; }
    }
}
