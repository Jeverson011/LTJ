using Ltj.Shared.Enum;

namespace Ltj.Shared.Entities
{
    public class PontoEntity : BaseEntity
    {
        public FuncionarioEntity? Funcio { get; set; }
        public TipoPonto EntradaSaida { get; set; }
    }

    
}
