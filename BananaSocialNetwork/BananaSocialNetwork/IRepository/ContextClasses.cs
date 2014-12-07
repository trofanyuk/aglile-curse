using BananaSocialNetwork.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BananaSocialNetwork.IRepository
{
    public class UserContext : DbContext
    {
        public DbSet<User> User { get; set; }
    }

    public class AlbumContext : DbContext
    {
        public DbSet<Album> Album { get; set; }
    }

    public class PhotoContext : DbContext
    {
        public DbSet<Photo> Photo { get; set; }
    }

    public class CommentContext : DbContext
    {
        public DbSet<Comment> Comment { get; set; }
    }

    public class FriendsContext : DbContext
    {
        public DbSet<Friends> Friends { get; set; }
    }

    public class SubscribersContext : DbContext
    {
        public DbSet<Subscribers> Subscribers { get; set; }
    }

    public class NewsContext : DbContext
    {
        public DbSet<News> News { get; set; }
    }
}