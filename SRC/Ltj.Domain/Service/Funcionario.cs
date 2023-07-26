using Ltj.Domain.Interface.Repository;
using Ltj.Domain.Interface.Services;
using Ltj.Shared.Entities;
using Ltj.Shared.Helpers;
using Ltj.Shared.Models;

namespace Ltj.Domain.Service
{
    public class Funcionario : IFuncionario
    {
        private readonly IFuncionarioRepository _repoProd;
        public Funcionario(IFuncionarioRepository rep)
        {
            _repoProd = rep;        
        }
             

    public async Task<ValidResult<bool>> DeleteAsync(string id)
    {
        var result = new ValidResult<bool>();
        try
        {
            await _repoProd.DeleteAsync(id);
            result.Status = true;
            result.Value = true;

            return result;
        }
        catch (Exception ex)
        {
            result.Message = ex.Message;
            return result;
        }
    }

    public async Task<FuncionarioEntity> Get(string id)
    {
        try
        {
            return await _repoProd.Get(id);

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<ValidResult<List<FuncionarioEntity>>> GetAll()
    {
        var result = new ValidResult<List<FuncionarioEntity>>();
        try
        {
            result.Value = (List<FuncionarioEntity>)await _repoProd.GetAll();
            result.Status = true;
            return result;
        }
        catch (Exception ex)
        {
            result.Message = ex.Message;
            return result;
        }
    }

    public async Task<ValidResult<bool>> InsertAsync(FuncionarioEntity funcionario)
    {
        var result = new ValidResult<bool>();
        try
        {              
                /*
                 * Comparar se novo funcionario ja existe na lista
                 * de funcioanrio já cadastrados.
                 * 
                 * Como?
                 * Comparando se o CPF ja existe
                 */
                if (!Validation.ValidaCPF(funcionario.CPF))
                {
                    return new ValidResult<bool> { Message = "CPF invalido", Value = false, Status = false };
                }

                if (Validation.ValidaPIS(funcionario.PIS))
                {
                    return new ValidResult<bool> { Message = "PIS invalido", Value = false, Status = false };
                }                


                var listaFuncionarios =  GetAll().Result.Value;
               
                if (listaFuncionarios.Any(l => l.CPF == funcionario.CPF))
                {
                    return new ValidResult<bool> { Message = "CPF ja possui cadastro", Value = false, Status = false };
                }
                                                

                await _repoProd.InsertAsync(funcionario);
                result.Status = true;
                result.Value = true;
                return result;

            }
        catch (Exception ex)
        {
            result.Message = ex.Message;
            return result;
        }
    }

  
        public async Task<ValidResult<bool>> UpdateAsync(FuncionarioEntity obj)
    {
        var result = new ValidResult<bool>();
        try
        {                         
            await _repoProd.UpdateAsync(obj);
            result.Status = true;
            result.Value = true;
            return result;
        }
        catch (Exception ex)
        {
            result.Message = ex.Message;
            return result;
        }
    }
}    
}
