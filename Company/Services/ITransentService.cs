namespace Company.Services
{
    public interface ITransentService 
    {
        public Guid Guid { get; set; }
        string GetGuid();
    }
}
