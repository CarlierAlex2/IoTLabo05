using System;
using System.Collections.Generic;
using System.Text;

namespace IoT_Labo_05_Project.Models
{
    public class GarbageRegistration
    {
        public Guid GarbageRegistrationId { get; set; }

        public string Name { get; set; }

        public string EMail { get; set; }

        public string Description { get; set; }

        public Guid GarbageTypeId { get; set; }

        public Guid CityId { get; set; }

        public string Street { get; set; }

        public float Weight { get; set; }

        public float Lat { get; set; }

        public float Long { get; set; }

        public DateTime Timestamp { get; set; }
    }
}
