using BananaSocialNetwork.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BananaSocialNetwork.IRepository
{

    public class IRepositoryClasses
    {
        public UserIRepository User { get; set; }
        public AlbumsIRepository Albums { get; set; }
        public PhotoIRepository Photos { get; set; }
        public CommentIRepository Comments { get; set; }
    }

    public class UserIRepository : IRepositoryUser
    {
        private UserContext db;
        public UserIRepository()
        {
            this.db = new UserContext();
        }
        public List<User> ToList()
        {
            return db.User.ToList();
        }
        public User Find(int? id)
        {
            return db.User.Find(id);
        }

        public void Create(User item)
        {
            db.User.Add(item);
        }

        public void Update(User item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            User user = db.User.Find(id);
            if (user != null)
            {
                db.User.Remove(user);
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }


    public class AlbumsIRepository : IRepositoryAlbum
    {
        private AlbumContext db;
        public AlbumsIRepository()
        {
            this.db = new AlbumContext();
        }
        public List<Album> ToList()
        {
            return db.Album.ToList();
        }
        public Album Find(int? id)
        {
            return db.Album.Find(id);
        }

        public void Create(Album item)
        {
            db.Album.Add(item);
        }

        public void Update(Album item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Album user = db.Album.Find(id);
            if (user != null)
            {
                db.Album.Remove(user);
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }


    public class PhotoIRepository : IRepositoryPhoto
    {
        private PhotoContext db;
        public PhotoIRepository()
        {
            this.db = new PhotoContext();
        }
        public List<Photo> ToList()
        {
            return db.Photo.ToList();
        }
        public Photo Find(int? id)
        {
            return db.Photo.Find(id);
        }

        public void Create(Photo item)
        {
            db.Photo.Add(item);
        }

        public void Update(Photo item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Photo user = db.Photo.Find(id);
            if (user != null)
            {
                db.Photo.Remove(user);
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }


    public class CommentIRepository : IRepositoryComment
    {
        private CommentContext db;
        public CommentIRepository()
        {
            this.db = new CommentContext();
        }
        public List<Comment> ToList()
        {
            return db.Comment.ToList();
        }
        public Comment Find(int? id)
        {
            return db.Comment.Find(id);
        }
        public void Create(Comment item)
        {
            db.Comment.Add(item);
        }
        public void Update(Comment item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Comment user = db.Comment.Find(id);
            if (user != null)
            {
                db.Comment.Remove(user);
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
