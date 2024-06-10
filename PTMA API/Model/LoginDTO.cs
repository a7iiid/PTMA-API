using System.ComponentModel.DataAnnotations;

namespace PTMA_API.Model
{
    public class LoginDTO
    {
        [Required]
        public String Email { get; set; }

        [Required]

        public String Password { get; set; }
    }
}
