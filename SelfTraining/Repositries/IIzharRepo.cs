using SelfTraining.DTO;
using SelfTraining.Models;

namespace SelfTraining.Repositries
{
    public interface IIzharRepo
    {
        public Task<IEnumerable<Izhar>> GetAllFamily();
        public Task<Izhar> GetFamilyById(int id);
        public Task<Izhar> CreateFamily(CreateFamilyDTO createFamily);
        public Task DeleteFamily(int id);
        public Task UpdateFamily(int id,UpdateFamilyDTO updateFamily);
    }
}