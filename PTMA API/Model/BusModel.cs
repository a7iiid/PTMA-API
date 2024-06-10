using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PTMA_API.Model
{
    public class BusModel
    {
        [Required]
        public LatLong busLocation;
        [Required]
        public LatLong startStation;
        [Required]
        public LatLong endStation;
        [Required]
        public string busname;
        [Required]
        public string busnumber;
        [Required]
        public bool isActive;
        [Required]
        public string Id;
    }
}
