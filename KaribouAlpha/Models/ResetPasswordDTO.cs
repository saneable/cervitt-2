using System.ComponentModel.DataAnnotations;

namespace KaribouAlpha.Models
{
    public class ResetPasswordDTO
    {
        [Required]
        public long UserID { get; set; }

        [Required]
        public string Token { get; set; }

        [Required]
        public string NewPassword { get; set; }

        [Required]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string NewPasswordConfirmation { get; set; }
    }

    public class ResetPasswordTokenDTO
    {
        [Required]
        public long Id { get; set; }

        [Required]
        public string Token { get; set; }
    }
}