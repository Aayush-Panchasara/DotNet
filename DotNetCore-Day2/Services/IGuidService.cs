namespace DotNetCore_Day2.Services
{
    public interface IGuidTransientService
    {
        public Guid Get();
    }
    public interface IGuidSingletonService
    {
        public Guid Get();
    }
    public interface IGuidScopedService
    {
        public Guid Get();
    }
}
