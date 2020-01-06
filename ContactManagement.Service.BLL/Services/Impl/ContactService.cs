using System.Collections.Generic;
using ContactManagement.Service.BLL.Models;
using ContactManagement.Service.DLL.Repository;
using ContactManagement.Service.DLL.Models;
using System;
using AutoMapper;

namespace ContactManagement.Service.BLL.Services.Impl
{
    public class ContactService : IContactService
    {
        private IContactRepository _iContactRepository;

        public ContactService(IContactRepository iContactRepository)
        {
            _iContactRepository = iContactRepository;
        }

        public bool CreateContact(ContactModel newContact)
        {
            try
            {
                var config = new MapperConfiguration(cfg => {
                    cfg.CreateMap<ContactModel, ContactInfo>();
                });
                IMapper iMapper = config.CreateMapper();
                ContactInfo contact = iMapper.Map<ContactModel, ContactInfo>(newContact);
                return _iContactRepository.CreateContact(contact);
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
                return _iContactRepository.DeleteContact(id);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<ContactModel> GetAllContacts()
        {
            try
            {
                List<ContactInfo> contacts = _iContactRepository.GetAllContacts();
                var config = new MapperConfiguration(cfg => {
                    cfg.CreateMap<ContactInfo, ContactModel>();
                });
                IMapper iMapper = config.CreateMapper();
                List<ContactModel> contactModels = iMapper.Map<List<ContactInfo>, List<ContactModel>>(contacts);
                return contactModels;
            }
            catch (Exception)
            {
                return new List<ContactModel>();
            }
        }
        
        public ContactModel GetContactById(int id)
        {
            try
            {
                ContactInfo contactInfo = _iContactRepository.GetContactById(id);
                var config = new MapperConfiguration(cfg => {
                    cfg.CreateMap<ContactInfo, ContactModel>();
                });
                IMapper iMapper = config.CreateMapper();
                ContactModel contactModel = iMapper.Map<ContactInfo, ContactModel>(contactInfo);
                return contactModel;
            }
            catch (Exception)
            {
                return new ContactModel();
            }
        }
        
        public bool UpdateContact(int id, ContactModel contact)
        {
            try
            {
                var config = new MapperConfiguration(cfg => {
                    cfg.CreateMap<ContactModel, ContactInfo>();
                });
                IMapper iMapper = config.CreateMapper();
                ContactInfo updateContact = iMapper.Map<ContactModel, ContactInfo>(contact);
                return _iContactRepository.UpdateContact(id, updateContact);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<ContactModel> GetContactListWithPagination(int iDisplayLength, int iDisplayStart, int iSortCol_0, string sSortDir_0, string sSearch)
        {
            try
            {
                List<ContactInfo> contacts = _iContactRepository.GetContactListWithPagination(iDisplayLength, iDisplayLength, iSortCol_0, sSortDir_0, sSearch);
                var config = new MapperConfiguration(cfg => {
                    cfg.CreateMap<ContactInfo, ContactModel>();
                });
                IMapper iMapper = config.CreateMapper();
                List<ContactModel> contactModels = iMapper.Map<List<ContactInfo>, List<ContactModel>>(contacts);
                return contactModels;
            }
            catch (Exception)
            {
                return new List<ContactModel>();
            }
        }
    }
}