using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mod02.Models;

namespace Mod02.Repository
{
    public class ContactsRepository : IContactsRepository
    {
        static List<Contacts> ContactsList = new List<Contacts>();

        static ContactsRepository()
        {
            ContactsList.Add(new Contacts() { FirstName = "First1", LastName = "Last1", Email = "l1f1@contoso.com", MobilePhone = "111" });
            ContactsList.Add(new Contacts() { FirstName = "First2", LastName = "Last2", Email = "l2f2@contoso.com", MobilePhone = "222", Company="Contoso" });
            ContactsList.Add(new Contacts() { FirstName = "First3", LastName = "Last3", Email = "l3f3@contoso.com", MobilePhone = "333" });
            ContactsList.Add(new Contacts() { FirstName = "First4", LastName = "Last4", Email = "l4f4@contoso.com", MobilePhone = "444", Company="Contoso" });
            ContactsList.Add(new Contacts() { FirstName = "First5", LastName = "Last5", Email = "l5f5@contoso.com", MobilePhone = "555" });

        }

        public void Add(Contacts item)
        {
            ContactsList.Add(item);
        }

        public Contacts Find(string key)
        {
            return ContactsList
                .Where(e => e.MobilePhone.Equals(key))
                .SingleOrDefault();
        }

        public IEnumerable<Contacts> GetAll()
        {
            return ContactsList;
        }

        public void Remove(string Id)
        {
            var itemToRemove = ContactsList.SingleOrDefault(r => r.MobilePhone == Id);
            if (itemToRemove != null)
                ContactsList.Remove(itemToRemove);
        }

        public void Update(Contacts item)
        {
            var itemToUpdate = ContactsList.SingleOrDefault(r => r.MobilePhone == item.MobilePhone);
            if (itemToUpdate != null)
            {
                itemToUpdate.FirstName = item.FirstName;
                itemToUpdate.LastName = item.LastName;
                itemToUpdate.Company = item.Company;
                itemToUpdate.Email = item.Email;
                itemToUpdate.MobilePhone = item.MobilePhone;
            }
        }
    }
}
