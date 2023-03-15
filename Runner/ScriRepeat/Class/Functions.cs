using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Win32;

public class Functions
{
    public object Execute(string name, List<object> arguments)
    {
        switch (name)
        {
            case "start":
                string appPath = (string)arguments[0];
                Process.Start(appPath);
                break;
            case "cmd":
                string command = (string)arguments[0];
                Process.Start("cmd", $"/c {command}");
                break;
            case "open":
                string url = (string)arguments[0];
                Process.Start(url);
                break;
            case "read":
                string key = (string)arguments[0];
                string valueName = (string)arguments[1];
                RegistryKey regKey = Registry.CurrentUser.OpenSubKey(key);
                object value = regKey.GetValue(valueName);
                return value.ToString();
            default:
                throw new Exception($"Function '{name}' not defined");
        }
        return 1;
    }
}
