using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BananaSocialNetwork.Models
{
    public enum NewsType { AddAlbum, AddPhoto, AddComent};

    public class News
    {
        public int Id { get; set; }
        public DateTime CreationTime { get; set; }
        public NewsType NewsType { get; set; }
        public virtual User User { get; set; }
        public int IdContent { get; set; }
    }
}