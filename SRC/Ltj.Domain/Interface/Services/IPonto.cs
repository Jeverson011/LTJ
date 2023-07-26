using Ltj.Domain.Service;
using Ltj.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ltj.Domain.Interface
{
    public interface IPonto
    {
        PontoEntity Consulta(string id, string idponto);

        List<PontoEntity> ConsultaTodos(string id, DateTime datainicio, DateTime datafim);

        bool Cadastro(PontoEntity ponto); // Usei o void pq ele cadastrar um novo ponto

        bool Alteracao(PontoEntity ponto); // Usei o void pq ele nao vai retornar nenhum valor (testar um boleano)


    }
}
