using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PTMA_API.Model
{
    public class BusModel
    {
        [Key]
        [Required]
        public int Id;
        [Required]
        public LatLong busLocation;
        [Required]
        public Station startStation;
        [Required]
        public Station endStation;
        [Required]
        public string busname;
        [Required]
        public string busnumber;
        [Required]
        public bool isActive;
        
    }
}
