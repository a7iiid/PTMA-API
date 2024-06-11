namespace PTMA_API.Model.RoutModel
{
    public class Route
    {

        public int distanceMeters { get; set; }
        public string duration { get; set; }
        public string routeToken { get; set; }
        public string[]? routeLabels { get; set; }
    }
}
