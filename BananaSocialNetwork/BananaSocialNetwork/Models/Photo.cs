using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BananaSocialNetwork.Models
{
    public class Photo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhotoPath { get; set; }
        public string Adress { get; set; } // название места
        public double GeoLong { get; set; } // долгота - для карт google
        public double GeoLat { get; set; } // широта - для карт google
        public virtual Album Album { get; set; }
        public virtual IEnumerable<Comment> Coments { get; set; }
    }
}