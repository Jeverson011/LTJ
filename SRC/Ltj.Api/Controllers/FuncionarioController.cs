using AutoMapper;
using Ltj.Domain.Interface.Services;
using Ltj.Domain.Service;
using Ltj.Shared.Entities;
using Ltj.Shared.Model;
using Microsoft.AspNetCore.Mvc;

namespace Ltj.api.Controllers
{
    [Route("api/funcionario")]
    [ApiController]
    public class FuncionarioController : ControllerBase
    {
        private readonly IFuncionario _funcionarioService;
        private readonly IMapper _mapper;
        public FuncionarioController(IMapper mapper, 
            IFuncionario funcionarioService)
        {
            _funcionarioService = funcionarioService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("insert")]
        public async Task<ActionResult> Insert([FromBody] Shared.Model.Funcionario funcionario)
        {
            var ent = _mapper.Map<FuncionarioEntity>(funcionario);

            var result = await _funcionarioService.InsertAsync(ent); 

            if (result.Status)
                return Ok(result);

            return BadRequest(result);
        }
        
        [HttpGet]
        [Route("consultar")]
        public async Task<ActionResult> Consultar(string id)
        {
            var result = await _funcionarioService.Get(id);
            if (result != null)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPut]        
        [Route("atualizar")]
        public async Task<ActionResult> Atualizar([FromBody] FuncionarioEntity funcionario)
        {
            var result = await _funcionarioService.UpdateAsync(funcionario);
            if (result.Status)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpDelete]
        [Route("deletar")]
        public async Task<ActionResult> Deletar(string id)
        {
            var result = await _funcionarioService.DeleteAsync(id);
            if (result.Status)
                return Ok(result);

            return BadRequest(result);
        }



    }
}