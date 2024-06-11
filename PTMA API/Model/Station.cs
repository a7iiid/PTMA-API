using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace PTMA_API.Model
{
    public class Station
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        public int Id { get; set; }
        [Required]
        public LatLong Location { get; set; }
        
        public Station(string name,LatLong location)
        {
            Name = name;
            Location = location;
        }
       
    }
}
