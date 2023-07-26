using Ltj.Domain.Interface;
using Ltj.Shared.Entities;
using Ltj.Shared.Enum;

namespace Ltj.Domain.Service
{
    public class Ponto : IPonto
    {        

    public PontoEntity Consulta(string id, string idponto)
        {
            var ret = new PontoEntity {
                DtInclusao = DateTime.Now,
                EntradaSaida = TipoPonto.Entrada,
                Id = 1,
                DtAlteracao = DateTime.Now,
                Funcio = new FuncionarioEntity
                {
                    Sexo = Sexo.Hibrido,
                    Id=1
                }
            };
            return ret;
        }

     public   List<PontoEntity> ConsultaTodos(string id, DateTime datainicio, DateTime datafim)
        {
            throw new NotImplementedException();
        }


        public bool Cadastrar(PontoEntity ponto)
        {
            var ret = new Ponto();
            return false;
        }

        public bool Alteracao(PontoEntity ponto)
        {
            var ret = new Ponto();
            return false;
        }

        public bool Cadastro(PontoEntity ponto)
        {
            throw new NotImplementedException();
        }
    }
}
