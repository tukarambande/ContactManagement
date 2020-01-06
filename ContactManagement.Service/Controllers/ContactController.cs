using ContactManagement.Service.BLL.Models;
using ContactManagement.Service.BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ContactManagement.Service.Controllers
{
    public class ContactController : ApiController
    {
        private IContactService _contactService;
        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }
        
        // GET api/<controller>/5
        [HttpGet]
        [ActionName("GetAllContacts")]
        public HttpResponseMessage GetAllContacts()
        {
            List<ContactModel> contacts = _contactService.GetAllContacts();
            if(contacts == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, 0);
            }
            return Request.CreateResponse(HttpStatusCode.OK, contacts);
        }

        [HttpGet]
        [ActionName("GetContactById")]
        public HttpResponseMessage GetContactById(int id)
        {
            ContactModel contact = _contactService.GetContactById(id);
            if (contact == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, id);
            }
            return Request.CreateResponse(HttpStatusCode.OK, contact);
        }

        [HttpPost]
        [ActionName("CreateContact")]
        public HttpResponseMessage CreateContact([FromBody] ContactModel contact)
        {
            bool result = _contactService.CreateContact(contact);
            if (result)
            {
                return Request.CreateResponse(HttpStatusCode.NoContent);
            }
            return Request.CreateResponse(HttpStatusCode.InternalServerError, contact);
        }

        [HttpPut]
        [ActionName("UpdateContact")]
        public HttpResponseMessage UpdateContact(int id, [FromBody] ContactModel contact)
        {
            bool result = _contactService.UpdateContact(id, contact);
            if (result)
            {
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            return Request.CreateResponse(HttpStatusCode.InternalServerError, contact);
        }

        [HttpDelete]
        [ActionName("DeleteContact")]
        public HttpResponseMessage DeleteContact(int id)
        {
            bool result = _contactService.DeleteContact(id);
            if (result)
            {
                return Request.CreateResponse(HttpStatusCode.NoContent);
            }
            return Request.CreateResponse(HttpStatusCode.InternalServerError);
        }

        [HttpGet]
        [ActionName("GetContactListWithPagination")]
        public HttpResponseMessage GetContactListWithPagination(int iDisplayLength, int iDisplayStart, int iSortCol_0, string sSortDir_0, string sSearch)
        {
            List<ContactModel> contacts = _contactService.GetContactListWithPagination(iDisplayLength, iDisplayStart, iSortCol_0, sSortDir_0, sSearch);
            if (contacts == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, 0);
            }
            return Request.CreateResponse(HttpStatusCode.OK, contacts);
        }
    }
}