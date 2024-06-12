using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PTMA.DB;
using PTMA_API.Model;

namespace PTMA_API.Reposetory
{
    public class SationRepoImplemant : IStationRepo
    {
        private PtmaDBContext db { set;get; }
        public SationRepoImplemant(PtmaDBContext db)
        {
            this.db = db;
            
        }
        public async Task<Station> AddStation(Station station)
        {
            if (station == null) throw new ArgumentNullException();
           await db.Stations.AddAsync(station);
            return station;
            
        }

        public async Task<bool> DeleteStation(int id)
        {
            Station staion=db.Stations.Where(s=>s.Id==id).FirstOrDefault();
            if (staion!=null)
            {
                db.Stations.Remove(staion);
                await SaveChange();
                return true;
            }
            return false;
        }

        public async Task<int> EditStation(int id, [FromBody] JsonPatchDocument<Station> patchDoc)
        {
            if (patchDoc == null)
                return 0;

            var existingAuthor = await GetStation(id);
            if (existingAuthor == null)
                return -1;

            patchDoc.ApplyTo(existingAuthor);

            

            try
            {
                await SaveChange();
                return 1;
            }
            catch (DbUpdateConcurrencyException)
            {
                return 0;
            }

        }

    

        public async Task<IEnumerable<Station>> GetStation()
        {
            IEnumerable<Station> stations = db.Stations;
            return stations;
                
        }
        public async Task<Station> GetStation(int id)
        {
            Station stations = db.Stations.Where(s => s.Id == id).FirstOrDefault();
            return stations;

        }

        public async Task SaveChange()
        {
            await db.SaveChangesAsync();
        }
    }
}
