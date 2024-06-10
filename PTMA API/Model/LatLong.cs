namespace PTMA_API.Model
{
    public class LatLong
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public LatLong(double lat,double lon)
        {
            Latitude= lat;
            Longitude= lon;
        }
    }
}
