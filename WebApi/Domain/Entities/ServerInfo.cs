namespace Domain.Entities
{
    public class ServerInfo : BaseEntity
    {
        public string Name { get; set; }
        public string Ip { get; set; }
        public bool Antivirus { get; set; }
        public bool Firewall { get; set; }
        public string WindowsVersion { get; set; }
        public string DotNetFrameworkVersion { get; set; }
        public long AvailableDiskStorage { get; set; }
        public long UsedDiskStorage { get; set; }
    }
}
