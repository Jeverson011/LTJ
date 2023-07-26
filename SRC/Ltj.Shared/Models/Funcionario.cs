using Ltj.Shared.Enum;

namespace Ltj.Shared.Model
{
    public class Funcionario
    {

        public string CPF { get; set; } // CPF do funcionário (alterado para string)
        public string PIS { get; set; } // PIS do funcionário (alterado para string)
        public string Nome { get; set; } // Nome completo do funcionário
        public DateTime DtNascimento { get; set; }
        public Sexo Sexo { get; set; }
        public StatusFuncionario Status { get; set; }
        public string Motivo { get; set; }

       
    }
}
