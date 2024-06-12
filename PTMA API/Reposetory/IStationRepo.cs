using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using PTMA_API.Model;

namespace PTMA_API.Reposetory
{
    public interface IStationRepo
    {
        public Task<IEnumerable<Station>> GetStation();
        public Task<bool> DeleteStation();
        public Task<Station> AddStation(Station station);
        public Task EditStation(int id,
           [FromBody] JsonPatchDocument<Station> patchDoc);
        public Task SaveChange();
        
    }
}
