namespace NSE.Core.DomainObjects
{
    public abstract class Entity
    {
        protected Entity()
        {
            DataCadastro = DateTime.Now;
        }
        public Guid Id { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}
