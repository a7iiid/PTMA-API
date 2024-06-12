using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace PTMA_API.Model
{
    public class Station
    {
        [Required]
        [Key]
        public int Id { get;  }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        
        [Required]
        public LatLong Location { get; set; }
        public Station() { }


        public Station(string name,LatLong location)
        {
            Name = name;
            Location = location;
        }
       
    }
}
