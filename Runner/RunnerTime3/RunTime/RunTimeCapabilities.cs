using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


static class RunTimeCapabilities
{
    public static void declareVariable(string name, string type)
    {
        if (!RunTimeData.variables.ContainsKey(name))
        {
            switch (type)
            {
                case "int":
                    RunTimeData.variables.Add(name, RunTimeDataContainers.Int);
                    break;
                case "float":
                    RunTimeData.variables.Add(name, RunTimeDataContainers.Float);
                    break;
                case "string":
                    RunTimeData.variables.Add(name, RunTimeDataContainers.String);
                    break;
            }
        }
        RunTimeData.declareVariable(name);
    }
    public static void setVariable(string name, object value)
    {
        RunTimeData.setVariable(name, value);
    }
    public static object getVariable(string name)
    {
        return RunTimeData.getVariable(name);
    }
}

