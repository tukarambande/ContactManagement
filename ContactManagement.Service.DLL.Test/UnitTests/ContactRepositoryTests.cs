using NUnit.Framework;
using System.Data.Entity;
using Moq;
using ContactManagement.Service.DLL.Models;
using ContactManagement.Service.DLL.Repository;

namespace ContactManagement.Service.DLL.Test.UnitTests
{
    [TestFixture]
    [Category("TestSuite.Unit")]
    public class ContactRepositoryTests : DbSet
    {
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

        Mock<ContactsManagementDbContext> dbContext;
        [SetUp]
        public void SetUp()
        {
            dbContext = new Mock<ContactsManagementDbContext>();
        }

        [Test(Description = "Valid CreateContact test")]
        public void Unit_CreateContact_Valid()
        {
            ContactInfo contactInfo = getMockContactInfo();

            ContactRepository repo = new ContactRepository(dbContext.Object);

            bool response = repo.CreateContact(contactInfo);
            Assert.AreEqual(false, response);
        }
    }
}
