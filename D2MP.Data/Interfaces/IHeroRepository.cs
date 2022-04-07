using D2MP.Models;

namespace D2MP.Data.Interfaces
{
    public interface IHeroRepository
    {
        Task<List<Hero>> GetAll();
    }
}
