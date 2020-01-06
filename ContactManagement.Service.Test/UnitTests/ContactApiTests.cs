using System;
using NUnit.Framework;
using Moq;
using ContactManagement.Service.DLL.Repository;
using ContactManagement.Service.BLL.Services.Impl;
using ContactManagement.Service.DLL.Models;
using ContactManagement.Service.BLL.Models;
using System.Collections.Generic;
using System.Linq;
using ContactManagement.Service.BLL.Services;
using ContactManagement.Service.Controllers;
using System.Threading.Tasks;
using System.Net.Http;
using System.Web.Http;
using System.Net;

namespace ContactManagement.Service.Test.UnitTests
{
    [TestFixture]
    [Category("TestSuite.Unit")]
    public class ContactApiTests
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
        #endregion

        Mock<IContactService> service;
        [SetUp]
        public void SetUp()
        {
            service = new Mock<IContactService>();
        }

        [Test(Description = "Valid CreateContact test")]
        public void Unit_CreateContactApi_Valid()
        {
            ContactModel input = getMockContactModel();

            this.service.Setup(r => r.CreateContact(It.Is<ContactModel>(c => c.FirstName == input.FirstName && c.EmailId == input.EmailId)))
                .Returns(true);

            var controller = new ContactController(service.Object);
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            
            var response = controller.CreateContact(input);
            
            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Test(Description = "Invalid CreateContact test")]
        public void Unit_CreateContactApi_Invalid()
        {
            ContactModel input = getMockContactModel();
            input.EmailId = "wrongemailid@mailinator.com";
            this.service.Setup(r => r.CreateContact(It.Is<ContactModel>(c => c.FirstName == input.FirstName && c.EmailId == input.EmailId)))
                .Returns(true);

            var controller = new ContactController(service.Object);
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            var response = controller.CreateContact(input);

            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Test(Description = "Valid UpdateContact api test")]
        public void Unit_UpdateContactApi_Valid()
        {
            ContactModel input = getMockContactModel();

            this.service.Setup(r => r.UpdateContact(It.IsAny<int>(), It.Is<ContactModel>(c => c.FirstName == input.FirstName && c.EmailId == input.EmailId)))
                .Returns(true);

            var controller = new ContactController(service.Object);
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            
            var response = controller.UpdateContact(input.ContactId, input);
            
            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test(Description = "Invalid UpdateContact api test")]
        public void Unit_UpdateContactApi_Invalid()
        {
            ContactModel input = getMockContactModel();
            input.EmailId = "wrongemailid@mailinator.com";
            this.service.Setup(r => r.UpdateContact(It.IsAny<int>(), It.Is<ContactModel>(c => c.FirstName == input.FirstName && c.EmailId == input.EmailId)))
                .Returns(true);

            var controller = new ContactController(service.Object);
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            var response = controller.UpdateContact(input.ContactId, input);

            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test(Description = "Valid DeleteContact api test")]
        public void Unit_DeleteContactApi_Valid()
        {
            ContactModel input = getMockContactModel();

            this.service.Setup(r => r.DeleteContact(It.Is<int>(c => c == input.ContactId)))
                .Returns(true);

            var controller = new ContactController(service.Object);
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            
            var response = controller.DeleteContact(input.ContactId);
            
            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Test(Description = "Invalid DeleteContact api test")]
        public void Unit_DeleteContactApi_Invalid()
        {
            ContactModel input = getMockContactModel();
            input.EmailId = "wrongemailid@mailinator.com";
            this.service.Setup(r => r.CreateContact(It.Is<ContactModel>(c => c.FirstName == input.FirstName && c.EmailId == input.EmailId)))
                .Returns(true);

            var controller = new ContactController(service.Object);
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            var response = controller.DeleteContact(input.ContactId);

            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.InternalServerError, response.StatusCode);
        }

        [Test(Description = "Valid GetContactById api test")]
        public void Unit_GetContactByIdApi_Valid()
        {
            ContactModel input = getMockContactModel();

            this.service.Setup(r => r.GetContactById(It.Is<int>(c => c == input.ContactId)))
                .Returns(input);

            var controller = new ContactController(service.Object);
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            
            var response = controller.GetContactById(input.ContactId);
            
            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test(Description = "Invalid GetContactById api test")]
        public void Unit_GetContactByIdApi_Invalid()
        {
            ContactModel input = getMockContactModel();
            
            this.service.Setup(r => r.GetContactById(It.Is<int>(c => c == input.ContactId)))
                .Returns(input);

            input.EmailId = "wrongemailid@mailinator.com";

            var controller = new ContactController(service.Object);
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            var response = controller.GetContactById(input.ContactId);

            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

    }
}
