namespace BAHelper.Common.DTOs.User
{
    public class UpdateUserDTO
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? OldPassword { get; set; }
        public string? Password { get; set; }
        public bool IsAgreedToNotification { get; set; }
        public bool ChangePassword { get; set; } = false;
    }
}
