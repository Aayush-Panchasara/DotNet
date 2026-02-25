namespace DotNetCore_Day2.Services
{
    public class GuidTransientService : IGuidTransientService
    {
        private readonly Guid _guid;

        public GuidTransientService()
        {
            _guid = Guid.NewGuid();
        }

        public Guid Get()
        {
            return _guid;
        }
    }
    public class GuidSingletonService : IGuidSingletonService
    {
        private readonly Guid _guid;

        public GuidSingletonService()
        {
            _guid = Guid.NewGuid();
        }

        public Guid Get()
        {
            return _guid;
        }
    }
    public class GuidScopedService : IGuidScopedService
    {
        private readonly Guid _guid;

        public GuidScopedService()
        {
            _guid = Guid.NewGuid();
        }

        public Guid Get()
        {
            return _guid;
        }
    }
}
