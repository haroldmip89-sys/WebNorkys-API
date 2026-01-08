using NorkysAPI.Models;

namespace NorkysAPI.Interfaces
{
    public interface IItemDAO
    {
        //funciones para Item
        Task<List<Item>> GetAll();
        Task<Item?> GetById(int id);
        Task<Item> Add(Item item);
        Task<Item?> Update(Item item);
        Task<bool> Delete(int id);
        Task<List<Item>> GetByCategory(int idCategoria);
    }
}
