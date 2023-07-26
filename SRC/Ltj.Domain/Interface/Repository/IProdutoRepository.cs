using Ltj.Shared.Entities;

namespace Ltj.Domain.Interface.Repository
{
    public interface IProdutoRepository : IBaseRepository<ProdutoEntity>
    {
        Task<bool> DeleteAsync(string id);
    }
}
