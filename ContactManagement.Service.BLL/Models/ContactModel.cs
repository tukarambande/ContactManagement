namespace ContactManagement.Service.BLL.Models
{
    public class ContactModel
    {
        public int ContactId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
        public string PhoneNumber { get; set; }
        public bool Status { get; set; }
        public string Address { get; set; }
    }
}
