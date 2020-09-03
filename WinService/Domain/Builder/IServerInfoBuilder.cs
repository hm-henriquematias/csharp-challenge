namespace Domain.Builder
{
    public interface IServerInfoBuilder
    {
        void BuildEnvironmentInfo();
        void BuildSecurityInfo();
        void BuildDiskInfo();
    }
}
