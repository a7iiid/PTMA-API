using PTMA.DB;
using PTMA_API.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace PTMA.Models
{
    public static class DbInitializer
    {
        public static async void Initialize(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<PtmaDBContext>();

                // Ensure the database is created
                context.Database.EnsureCreated();

                // Check if there are any Stations already present
                if (!context.Stations.Any())
                {
                    // Create some sample stations
                    var station1 = new Station("Station1", new LatLong { Latitude = 40.7128, Longitude = -74.0060 });
                    var station2 = new Station("Station2", new LatLong { Latitude = 34.0522, Longitude = -118.2437 });

                    context.Stations.AddRange(station1, station2);
                    
                    context.SaveChanges();

                }

                // Check if there are any BusModels already present
                if (!context.BusModels.Any())
                {
                    // Retrieve the station IDs
                    var startStationId = context.Stations.First().Id;
                    var endStationId = context.Stations.Skip(1).First().Id;

                    // Create some sample buses
                    var bus1 = new BusModel("Bus1", 1, endStationId, new LatLong { Latitude = 40.7128, Longitude = -74.0060 }, startStationId);
                    var bus2 = new BusModel("Bus2", 2, endStationId, new LatLong { Latitude = 34.0522, Longitude = -118.2437 }, startStationId);

                    context.BusModels.AddRange(bus1, bus2);
                     context.SaveChanges();

                }

                // Check if there are any History records already present
                if (!context.History.Any())
                {
                    // Create some sample history records
                    var history1 = new History(1, "Bus1");
                    var history2 = new History(2, "Bus2");

                    context.History.AddRange(history1, history2);
                }

                // Save changes to the database
                context.SaveChanges();
            }
        }
    }
}
