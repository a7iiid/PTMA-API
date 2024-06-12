using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PTMA_API.Model
{
    public class BusModel
    {
        [Required]
        public int Id { get; }
        [Required]
        public LatLong busLocation { get; set; }
        [Required]
        public int StartStationId { get; set; }
        public Station StartStation { get; set; }
        [Required]
        public int EndStationId { get; set; }
        public Station EndStation { get; set; }
        [Required]
        public string Busname { get; set; }
        [Required]
        public int Busnumber { get; set; }
        [Required]
        public bool IsActive { get; set; }

        public BusModel(string name, int num, int endStationId, LatLong location, int startStationId)
        {
            Busname = name;
            Busnumber = num;
            IsActive = false;
            EndStationId = endStationId;
            StartStationId = startStationId;
            busLocation = location; 
        }

        public BusModel()
        {
            
        }

    }
}
