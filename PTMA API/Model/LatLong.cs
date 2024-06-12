using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PTMA_API.Model
{
    public class LatLong

    {

        [Required]
        public double Latitude { get; set; }
        [Required]
        public double Longitude { get; set; }

        public LatLong() { }


        public LatLong(double latitude, double longitude)
        {
            Latitude= latitude;
            Longitude= longitude;
        }
    }
}
