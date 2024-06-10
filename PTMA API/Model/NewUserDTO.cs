using System.ComponentModel.DataAnnotations;

namespace PTMA_API.Model
{
    public class NewUserDTO
    {
        [MaxLength(100)]
        [Required]
        public string Name { get; set; }
        [MaxLength(50)]
        [Required]

        public string Email { get; set; }
        [MaxLength(50)]
        [Required]

        public string Password { get; set; }
    }
}
