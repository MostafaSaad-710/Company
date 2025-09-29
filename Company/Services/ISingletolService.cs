namespace Company.Services
{
    public interface ISingletolService
    {
        public Guid Guid { get; set; }
        string GetGuid();
    }
}
