using System;
using System.Collections.Generic;

namespace FoodTrucks
{
    public class FoodTruckFilter
    {
        public List<string> Status { get; set; } = new List<string>();
        public List<string> FacilityType { get; set; } = new List<string>();
        public bool? IsNotExpired { get; set; }
        public bool? IsExpired { get; set; } 
        public List<string> FoodItems { get; set; } = new List<string>();
        public List<string> Applicant { get; set; } = new List<string>();
    }
}
