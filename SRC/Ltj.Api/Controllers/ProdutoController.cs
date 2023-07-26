using AutoMapper;
using Ltj.Domain.Interface.Services;
using Ltj.Shared.Entities;
using Microsoft.AspNetCore.Mvc;



namespace Ltj.api.Controllers
{
    [Route("api/produto")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly IProduto _produtoService;
        private readonly IMapper _mapper;

        public ProdutoController (IProduto produtoService, IMapper mapper)
        {
            _produtoService = produtoService;
            _mapper = mapper;
         
        }

        [HttpPost]
        [Route("insert")]
        public async Task<ActionResult> Insert([FromBody] Shared.Model.Produto produto)
        {
            var ent = _mapper.Map<ProdutoEntity>(produto);

            var result = await _produtoService.InsertAsync(ent);

                if (result.Status)
                    return Ok(result);

            return BadRequest(result);
        }
        [HttpGet]
        [Route("consultar")]
        public async Task<ActionResult> Consultar(string id)
        {
            var result = await _produtoService.Get(id);
            if (result != null)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPut]
        [Route("atualizar")]
        public async Task<ActionResult> Atualizar([FromBody] ProdutoEntity produto)
        {
            var result = await _produtoService.UpdateAsync(produto);
            if (result.Status)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpDelete]
        [Route("deletar")]
        public async Task<ActionResult> Deletar(string  id)
        {
            var result = await _produtoService.DeleteAsync(id);
            if (result.Status)
                return Ok(result);

            return BadRequest(result);
        }



    }
}
