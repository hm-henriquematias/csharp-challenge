namespace Domain.Builder
{
    public class ServerInfoDirector
    {
        private IServerInfoBuilder _builder;

        public IServerInfoBuilder Builder
        {
            set
            {
                _builder = value;
            }
        }

        public void BuildServerInfo()
        {
            _builder.BuildEnvironmentInfo();
            _builder.BuildSecurityInfo();
            _builder.BuildDiskInfo();
        }
    }
}
