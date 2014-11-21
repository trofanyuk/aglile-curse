using BananaSocialNetwork.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BananaSocialNetwork.IRepository
{

    public class ApplicationDbContextIRepository
    {
        public UserIRepository User { get; set; }
        public AlbumsIRepository Albums { get; set; }
        public PhotoIRepository Photos { get; set; }
        public CommentIRepository Comments { get; set; }
    }

    public class UserIRepository : IRepositoryUser
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public User FirstOrDefault (string name)
        {
           return db.Users.Where(m => m.Email == name).FirstOrDefault();
        }
    }


    public class AlbumsIRepository
    {
        private ApplicationDbContext db = new ApplicationDbContext();
    }


    public class PhotoIRepository
    {
        private ApplicationDbContext db = new ApplicationDbContext();
    }


    public class CommentIRepository
    {
        private ApplicationDbContext db = new ApplicationDbContext();
    }
}
