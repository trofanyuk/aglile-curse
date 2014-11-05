using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BananaSocialNetwork.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime DateCreate { get; set; }
        public virtual User User { get; set; }
        public virtual Photo Photo { get; set; }
    }
}