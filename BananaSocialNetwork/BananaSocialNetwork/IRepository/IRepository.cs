using BananaSocialNetwork.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BananaSocialNetwork.IRepository
{
    public interface IRepositoryUser : IDisposable
    {
        List<User> ToList();
        User Find(int? id);
        void Create(User item);
        void Update(User item);
        void Delete(int id);
        void Save();
    }

    public interface IRepositoryAlbum : IDisposable
    {
        List<Album> ToList();
        Album Find(int? id);
        void Create(Album item);
        void Update(Album item);
        void Delete(int id);
        void Save();
    }

    public interface IRepositoryPhoto : IDisposable
    {
        List<Photo> ToList();
        Photo Find(int? id);
        void Create(Photo item);
        void Update(Photo item);
        void Delete(int id);
        void Save();
    }

    public interface IRepositoryComment : IDisposable
    {
        List<Comment> ToList();
        Comment Find(int? id);
        void Create(Comment item);
        void Update(Comment item);
        void Delete(int id);
        void Save();
    }

    public interface IRepositoryFriend : IDisposable
    {
        List<Friends> ToList();
        Friends Find(int? id);
        void Create(Friends item);
        void Update(Friends item);
        void Delete(int id);
        void Save();
    }

    public interface IRepositorySubscribers : IDisposable
    {
        List<Subscribers> ToList();
        Subscribers Find(int? id);
        void Create(Subscribers item);
        void Update(Subscribers item);
        void Delete(int id);
        void Save();
    }

    public interface IRepositoryNews : IDisposable
    {
        List<News> ToList();
        News Find(int? id);
        void Create(News item);
        void Update(News item);
        void Delete(int id);
        void Save();
    }
}
