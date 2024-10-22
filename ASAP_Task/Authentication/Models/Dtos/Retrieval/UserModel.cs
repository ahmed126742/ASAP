namespace ASAP_Task.Authentication.Models.Dtos.Retrieval
{
    public class UserModel
    {
        public Guid Id { get; set; }

        public string? Role { get; set; }

        public string? FirstName { get; set; }

        public string? SecondName { get; set; }

        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }

        public int? AttachmentHeaderId { get; set; }
    }
}
