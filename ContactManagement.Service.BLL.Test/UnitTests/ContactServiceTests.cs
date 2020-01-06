using NUnit.Framework;
using Moq;
using ContactManagement.Service.DLL.Repository;
using ContactManagement.Service.BLL.Services.Impl;
using ContactManagement.Service.DLL.Models;
using ContactManagement.Service.BLL.Models;
using System.Collections.Generic;
using System.Linq;

namespace ContactManagement.Service.BLL.Test.UnitTests
{
    [TestFixture]
    [Category("TestSuite.Unit")]
    public class ContactServiceTests
    {
        #region Private Methods
        private ContactModel getMockContactModel()
        {
            ContactModel model = new ContactModel
            {
                ContactId = 1,
                FirstName = "John",
                LastName = "Doe",
                Address = "Pune",
                EmailId = "john.doe@mailinator.com",
                PhoneNumber = "3456789",
                Status = true
            };
            return model;
        }

        private ContactInfo getMockContactInfo()
        {
            ContactInfo info = new ContactInfo
            {
                ContactId = 1,
                FirstName = "John",
                LastName = "Doe",
                Address = "Pune",
                EmailId = "john.doe@mailinator.com",
                PhoneNumber = "3456789",
                Status = true
            };
            return info;
        }

        private List<ContactInfo> getMockContactInfoList(int count)
        {
            List<ContactInfo> contactInfoes = new List<ContactInfo>();
            for (int i = 1; i <= count; i++)
            {
                contactInfoes.Add(new ContactInfo
                {
                    ContactId = i,
                    FirstName = "John" + i,
                    LastName = "Doe" + i,
                    Address = "Pune" + i,
                    EmailId = "john.doe@mailinator.com",
                    PhoneNumber = "3456789" + i,
                    Status = true
                });
            }
            return contactInfoes;
        }

        private List<ContactModel> getMockContactModelList(int count)
        {
            List<ContactModel> contactInfoes = new List<ContactModel>();
            for (int i = 1; i <= count; i++)
            {
                contactInfoes.Add(new ContactModel
                {
                    ContactId = i,
                    FirstName = "John" + i,
                    LastName = "Doe" + i,
                    Address = "Pune" + i,
                    EmailId = "john.doe@mailinator.com",
                    PhoneNumber = "3456789" + i,
                    Status = true
                });
            }
            return contactInfoes;
        }
        #endregion

        Mock<IContactRepository> repo;
        [SetUp]
        public void SetUp()
        {
            repo = new Mock<IContactRepository>();
        }


        [Test(Description = "Valid CreateContact test")]
        public void Unit_CreateContact_Valid()
        {
            ContactInfo contactInfo = getMockContactInfo();

            repo.Setup(r => r.CreateContact(It.Is<ContactInfo>(c => c.FirstName == contactInfo.FirstName && c.EmailId == contactInfo.EmailId)))
                .Returns(true);
            ContactService service = new ContactService(repo.Object);

            ContactModel contactModel = getMockContactModel();
            bool response = service.CreateContact(contactModel);
            Assert.AreEqual(true, response);
        }

        [Test(Description = "Invalid CreateContact test")]
        public void Unit_CreateContact_Invalid()
        {
            ContactInfo contactInfo = getMockContactInfo();
            contactInfo.EmailId = "wrongemailid@mailinator.com";
            repo.Setup(r => r.CreateContact(It.Is<ContactInfo>(c => c.FirstName == contactInfo.FirstName && c.EmailId == contactInfo.EmailId)))
                .Returns(true);
            ContactService service = new ContactService(repo.Object);

            ContactModel contactModel = getMockContactModel();
            bool response = service.CreateContact(contactModel);
            Assert.AreEqual(false, response);
        }

        [Test(Description = "Valid DeleteContact test")]
        public void Unit_DeleteContact_Valid()
        {
            ContactInfo contactInfo = getMockContactInfo();

            repo.Setup(r => r.DeleteContact(It.Is<int>(c => c == contactInfo.ContactId)))
                .Returns(true);

            ContactService service = new ContactService(repo.Object);

            ContactModel contactModel = getMockContactModel();
            bool response = service.DeleteContact(contactModel.ContactId);

            Assert.AreEqual(true, response);
        }

        [Test(Description = "InValid DeleteContact test")]
        public void Unit_DeleteContact_Invalid()
        {
            ContactInfo contactInfo = getMockContactInfo();
            contactInfo.ContactId = 5;

            repo.Setup(r => r.DeleteContact(It.Is<int>(c => c == contactInfo.ContactId)))
                .Returns(true);

            ContactService service = new ContactService(repo.Object);

            ContactModel contactModel = getMockContactModel();
            bool response = service.DeleteContact(contactModel.ContactId);

            Assert.AreEqual(false, response);
        }

        [Test(Description = "Valid GetAllContacts test")]
        public void Unit_GetAllContacts_Valid()
        {
            List<ContactInfo> contactInfoList = getMockContactInfoList(10);

            repo.Setup(r => r.GetAllContacts())
                .Returns(contactInfoList);

            ContactService service = new ContactService(repo.Object);

            List<ContactModel> output = service.GetAllContacts();

            foreach (ContactModel model in output)
            {
                ContactInfo contact = contactInfoList.Where(c => c.ContactId == model.ContactId).FirstOrDefault();

                Assert.AreEqual(contact.ContactId, model.ContactId);
                Assert.AreEqual(contact.FirstName, model.FirstName);
                Assert.AreEqual(contact.LastName, model.LastName);
                Assert.AreEqual(contact.EmailId, model.EmailId);
                Assert.AreEqual(contact.PhoneNumber, model.PhoneNumber);
                Assert.AreEqual(contact.Status, model.Status);
            }
        }

        [Test(Description = "InValid GetAllContacts test")]
        public void Unit_GetAllContacts_Invalid()
        {
            List<ContactInfo> contactInfoList = getMockContactInfoList(0);

            repo.Setup(r => r.GetAllContacts())
                .Returns(contactInfoList);

            ContactService service = new ContactService(repo.Object);

            List<ContactModel> output = service.GetAllContacts();
            Assert.AreEqual(0, output.Count);
        }

        [Test(Description = "Valid GetContactById test")]
        public void Unit_GetContactById_Valid()
        {
            ContactInfo contact = getMockContactInfo();

            repo.Setup(r => r.GetContactById(It.Is<int>(g => g == contact.ContactId)))
                .Returns(contact);

            ContactService service = new ContactService(repo.Object);

            ContactModel output = service.GetContactById(contact.ContactId);

            Assert.AreEqual(contact.ContactId, output.ContactId);
            Assert.AreEqual(contact.FirstName, output.FirstName);
            Assert.AreEqual(contact.LastName, output.LastName);
            Assert.AreEqual(contact.EmailId, output.EmailId);
            Assert.AreEqual(contact.PhoneNumber, output.PhoneNumber);
            Assert.AreEqual(contact.Status, output.Status);
        }

        [Test(Description = "InValid GetContactById test")]
        public void Unit_GetContactById_Invalid()
        {
            repo.Setup(r => r.GetContactById(It.IsAny<int>()))
                .Returns(new ContactInfo());

            ContactService service = new ContactService(repo.Object);

            ContactModel output = service.GetContactById(0);
            Assert.AreEqual(null, output.EmailId);
        }

        [Test(Description = "Valid UpdateContact test")]
        public void Unit_UpdateContact_Valid()
        {
            ContactInfo contactInfo = getMockContactInfo();

            repo.Setup(r => r.UpdateContact(It.IsAny<int>(), It.Is<ContactInfo>(c => c.FirstName == contactInfo.FirstName && c.EmailId == contactInfo.EmailId)))
                .Returns(true);
            ContactService service = new ContactService(repo.Object);

            ContactModel contactModel = getMockContactModel();
            bool response = service.UpdateContact(contactModel.ContactId, contactModel);
            Assert.AreEqual(true, response);
        }

        [Test(Description = "Invalid UpdateContact test")]
        public void Unit_UpdateContact_Invalid()
        {
            ContactInfo contactInfo = getMockContactInfo();
            contactInfo.EmailId = "wrongemailid@mailinator.com";
            repo.Setup(r => r.UpdateContact(It.IsAny<int>(), It.Is<ContactInfo>(c => c.FirstName == contactInfo.FirstName && c.EmailId == contactInfo.EmailId)))
                .Returns(true);
            ContactService service = new ContactService(repo.Object);

            ContactModel contactModel = getMockContactModel();
            bool response = service.UpdateContact(contactModel.ContactId, contactModel);
            Assert.AreEqual(false, response);
        }

    }
}
