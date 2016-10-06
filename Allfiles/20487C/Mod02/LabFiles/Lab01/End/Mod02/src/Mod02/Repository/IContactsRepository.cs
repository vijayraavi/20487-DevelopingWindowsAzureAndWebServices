using Mod02.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mod02.Repository
{
    public interface IContactsRepository
    {
        void Add(Contacts item);
        IEnumerable<Contacts> GetAll();
        Contacts Find(string key);
        void Remove(string Id);
        void Update(Contacts item);
    }
}
