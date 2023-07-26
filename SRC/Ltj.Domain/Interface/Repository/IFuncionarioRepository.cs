using Ltj.Shared.Entities;

namespace Ltj.Domain.Interface.Repository
{
    public interface IFuncionarioRepository : IBaseRepository<FuncionarioEntity>
    {
        Task<bool> DeleteAsync(string id);
    }
}
