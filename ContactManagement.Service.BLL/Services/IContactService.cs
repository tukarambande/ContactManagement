using ContactManagement.Service.BLL.Models;
using System.Collections.Generic;

namespace ContactManagement.Service.BLL.Services
{
    public interface IContactService
    {
        List<ContactModel> GetAllContacts();

         ContactModel GetContactById(int id);
        
        bool CreateContact(ContactModel contactInfo);

        bool UpdateContact(int id, ContactModel contactInfo);

        bool DeleteContact(int id);

        List<ContactModel> GetContactListWithPagination(int iDisplayLength, int iDisplayStart, int iSortCol_0, string sSortDir_0, string sSearch);
    }
}