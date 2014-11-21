using BananaSocialNetwork.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BananaSocialNetwork.IRepository
{
    public interface IRepositoryUser
    {
        User FirstOrDefault(string name);
    }
}
