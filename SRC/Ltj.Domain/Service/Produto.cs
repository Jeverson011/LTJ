using Ltj.Domain.Interface.Repository;
using Ltj.Domain.Interface.Services;
using Ltj.Shared.Entities;
using Ltj.Shared.Helpers;
using Ltj.Shared.Models;

namespace Ltj.Domain.Service
{
    public class Produto : IProduto
    {
        private readonly IProdutoRepository _repoProd;
        public Produto(IProdutoRepository rep)
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

        public async Task<ProdutoEntity> Get(string id)
        {
            try
            {
                return await  _repoProd.Get(id);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ValidResult<List<ProdutoEntity>>> GetAll()
        {
            var result = new ValidResult<List<ProdutoEntity>>();
            try
            {
                result.Value = (List<ProdutoEntity>)await _repoProd.GetAll();
                result.Status = true;
                return result;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                return result;
            }
        }

        public async Task<ValidResult<bool>> InsertAsync(ProdutoEntity obj)
        {
            var result = new ValidResult<bool>();
            try
            {
                var produtos = await GetAll();

                if (produtos.Value == null || !produtos.Status)
                {
                    result.Message = "Error: when querying user existence. Please try again!";
                    return result;
                }

                if (!Validation.IsName(obj.Marca))
                {
                    result.Message = "Erro: Product manufacturer name not found. Please try again!";
                    return result;
                }
                await _repoProd.InsertAsync(obj);
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

        public async Task<ValidResult<bool>> UpdateAsync(ProdutoEntity obj)
        {
            var result = new ValidResult<bool>();
            try
            {
                //var produto = await _repoProd.Get(obj.Id.ToString());

                //produto.Nome = obj.Nome;
                //produto.Marca = obj.Marca;
                //produto.PrecoVenda = obj.PrecoVenda;
                //produto.PrecoCusto = obj.PrecoCusto;
                //produto.Quantidade = obj.Quantidade;
                //produto.Tipo = obj.Tipo;                
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
