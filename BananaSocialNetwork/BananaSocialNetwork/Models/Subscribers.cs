using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BananaSocialNetwork.Models
{
    public class Subscribers
    {
        public int Id { get; set; }
        public User user { get; set; }
        public User subscriber { get; set; }

        public Subscribers(User user, User subscriber)
        {
            this.user = user;
            this.subscriber = subscriber;
        }
        public Subscribers()
        {
            
        }
    }
}