using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelGuide.Models.ViewModels
{
    public class TripViewModel
    {
        public Trip trip { get; set; }
        public SelectList Clients { get; set; }
    }
}
