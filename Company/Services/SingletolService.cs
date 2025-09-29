namespace Company.Services
{
    public class SingletolService : ISingletolService
    {
        public SingletolService()
        {
            Guid = Guid.NewGuid();
        }
        public Guid Guid { get; set; }

        public string GetGuid()
        {
            return Guid.ToString();
        }
    }
}
