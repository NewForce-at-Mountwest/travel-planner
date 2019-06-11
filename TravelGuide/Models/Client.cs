using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TravelGuide.Models
{
    public class Client
    {

        [Required]
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [NotMapped()]
        public string FullName
        {
            get
            {
                return FirstName + ' ' + LastName;
            }
        }

        [Required]
        public string phoneNumber { get; set; }

        public List<Trip> Trips { get; set; } = new List<Trip>();

        public string ApplicationUserId { get; set; }

    }
}
