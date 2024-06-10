using System.ComponentModel.DataAnnotations;

namespace PTMA_API.Model
{
    public class LatLong

    {
        [Required]
        public double Latitude { get; set; }
        [Required]
        public double Longitude { get; set; }

        public LatLong(double lat,double lon)
        {
            Latitude= lat;
            Longitude= lon;
        }
    }
}
