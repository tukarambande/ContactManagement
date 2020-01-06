using ContactManagement.Service.DLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManagement.Service.DLL.Repository
{
    public interface IContactRepository
    {
        List<ContactInfo> GetAllContacts();

        ContactInfo GetContactById(int id);

        bool CreateContact(ContactInfo contactInfo);

        bool UpdateContact(int id, ContactInfo contactInfo);

        bool DeleteContact(int id);

        List<ContactInfo> GetContactListWithPagination(int iDisplayLength, int iDisplayStart, int iSortCol_0, string sSortDir_0, string sSearch);
    }
}
