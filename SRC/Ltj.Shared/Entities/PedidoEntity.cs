namespace Ltj.Shared.Entities
{
    public class PedidoEntity : BaseEntity
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public string Tipo { get; set; }
        public float PrecoCusto { get; set; }
        public float PrecoVenda { get; set; }
        public int Quantidade { get; set; }
    }   
}
