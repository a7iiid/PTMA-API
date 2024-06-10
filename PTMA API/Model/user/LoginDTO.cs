using System.ComponentModel.DataAnnotations;

namespace PTMA_API.Model.user
{
    public class LoginDTO
    {
        [Required]
        public string Email { get; set; }

        [Required]

        public string Password { get; set; }
    }
}
