using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace WinServiceClassLib.Utils
{
    public class DiskHelper
    {
        public long Free { get; set; }
        public long Used { get; set; }

        public DiskHelper()
        {
            DriveInfo[] allDrives = DriveInfo.GetDrives();

            long free = 0;
            long total = 0;
            long used = 0;

            foreach (DriveInfo d in allDrives)
            {
                if (d.IsReady == true)
                {
                    free += d.AvailableFreeSpace;
                    total += d.TotalSize;
                }
            }

            used = total - free;

            Free = free;
            Used = used;
        }
    }
}
