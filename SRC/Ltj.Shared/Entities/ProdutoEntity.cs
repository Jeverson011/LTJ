namespace Ltj.Shared.Entities
{
    public class ProdutoEntity : BaseEntity
    {
        public string Marca { get; set; }
        public string Nome { get; set; }
        public string Tipo { get; set; }
        public float PrecoCusto { get; set; }
        public float PrecoVenda { get; set; }
        public int Quantidade { get; set; }
    }   
}
