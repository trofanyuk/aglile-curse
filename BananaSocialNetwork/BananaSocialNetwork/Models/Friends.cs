using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BananaSocialNetwork.Models
{
    public class Friends
    {
        public int Id { get; set; }
        public User user { get; set; }
        public User friend { get; set; }

        public Friends(User user, User friend)
        {
            this.user = user;
            this.friend = friend;
        }
        public Friends()
        {
            
        }
    }
}