using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BananaSocialNetwork.Models;

namespace BananaSocialNetwork.Models
{
    public class Album
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Adress { get; set; } // название места
        public double GeoLong { get; set; } // долгота - для карт google
        public double GeoLat { get; set; } // широта - для карт google
        public DateTime DateCreate { get; set; }
        public virtual User User { get; set; }
        public int CountPhotos { get; set; }
        public virtual IEnumerable<Photo> Photos { get; set; }

        public Album()
        {
            Photos = new List<Photo>();
        }
    }
}