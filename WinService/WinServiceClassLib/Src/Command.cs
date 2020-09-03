﻿using System.Collections.Generic;
using System.Management.Automation;

namespace WinServiceClassLib.Src
{
    public class Command
    {
        public List<string> Execute(string commandString)
        {
            using (var ps = PowerShell.Create())
            {
                List<string> resultList = new List<string>();

                var results = ps.AddScript(commandString).Invoke();

                foreach (var result in results)
                {
                    resultList.Add(result.ToString());
                }

                return resultList;
            }
        }
    }
}
