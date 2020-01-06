using ContactManagement.Service.DLL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManagement.Service.DLL.Repository
{
    public class ContactRepository : IContactRepository
    {
        private ContactsManagementDbContext dbContext;

        public ContactRepository(DbContext context)
        {
            dbContext = (ContactsManagementDbContext)context;
        }

        public List<ContactInfo> GetAllContacts()
        {
            try
            {
                List<ContactInfo> contacts = dbContext.ContactInfoes.ToList();
                return contacts;
            }
            catch (Exception)
            {
                return new List<ContactInfo>();
            }
        }
        
        public ContactInfo GetContactById(int id)
        {
            try
            {
                return dbContext.ContactInfoes.Where(c => c.ContactId == id).FirstOrDefault();   
            }
            catch (Exception)
            {
                return new ContactInfo();
            }
        }

        public bool CreateContact(ContactInfo contactInfo)
        {
            try
            {
                dbContext.ContactInfoes.Add(contactInfo);
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
   
        public bool UpdateContact(int id, ContactInfo contact)
        {
            try
            {
                var contactToUpdate = dbContext.ContactInfoes.Single(c => c.ContactId == id);
                if(contactToUpdate != null)
                {
                    contactToUpdate.FirstName = contact.FirstName;
                    contactToUpdate.LastName = contact.LastName;
                    contactToUpdate.EmailId = contact.EmailId;
                    contactToUpdate.PhoneNumber = contact.PhoneNumber;
                    contactToUpdate.Status = contact.Status;
                    contactToUpdate.Address = contact.Address;
                    dbContext.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteContact(int id)
        {
            try
            {
                var contactToDelete = dbContext.ContactInfoes.Single(c => c.ContactId == id);
                dbContext.ContactInfoes.Remove(contactToDelete);
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<ContactInfo> GetContactListWithPagination(int iDisplayLength, int iDisplayStart, int iSortCol_0, string sSortDir_0, string sSearch)
        {
            try
            {
                return dbContext.ContactInfoes.OrderBy(c => c.ContactId).Skip(iDisplayStart).Take(iDisplayLength).ToList();
            }
            catch (Exception ex)
            {
                return new List<ContactInfo>();
            }
        }
    }
}
