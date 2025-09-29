namespace Company.Services
{
    public interface IScopedService
    {
        public Guid Guid { get; set; }
        string GetGuid();
    }
}
