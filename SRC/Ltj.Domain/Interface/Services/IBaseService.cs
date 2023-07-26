using Ltj.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ltj.Domain.Interface.Services
{
    public interface IBaseService<T>
    {
        Task<ValidResult<bool>> UpdateAsync(T obj);
        Task<ValidResult<bool>> InsertAsync(T obj);
        Task<ValidResult<bool>> DeleteAsync(string id);
        Task<T> Get(string id);
        Task<ValidResult<List<T>>> GetAll();
    }
}
