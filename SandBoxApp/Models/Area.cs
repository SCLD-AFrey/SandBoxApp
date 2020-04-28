using System;
using System.Collections.Generic;

namespace SandBoxApp.Models
{
    public class Area
    {
        public string id { get; set; }
        public string displayName { get; set; }
        public List<Area> areas { get; set; }
        public int totalConfirmed { get; set; }
        public int? totalDeaths { get; set; }
        public int? totalRecovered { get; set; }
        public int? totalRecoveredDelta { get; set; }
        public int? totalDeathsDelta { get; set; }
        public int? totalConfirmedDelta { get; set; }
        public DateTime lastUpdated { get; set; }
        public double lat { get; set; }
        public double @long { get; set; }
        public string parentId { get; set; }
        public int areaCount
        {
            get { return areas.Count; }
        }

        public int? totalActive
        {
            get
            {
                return (totalConfirmed - totalDeaths - totalRecovered);
            }
        }
    }
}
