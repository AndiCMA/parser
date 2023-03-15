using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

enum RunTimeDataContainers
{
    Int,
    Float,
    String
}

static class RunTimeData
{
    public static Dictionary<string, object> variables = new Dictionary<string, object>();

    public static void setVariable(string name, object value)
    {
        if (variables.ContainsKey(name))
        {
            variables[name] = value;
        }
        else
        {
            variables.Add(name, value);
        }
    }
    public static void declareVariable(string name, RunTimeDataContainers type = RunTimeDataContainers.Int)
    {
        if (!variables.ContainsKey(name))
        {
            switch (type)
            {
                case RunTimeDataContainers.Int:
                    variables.Add(name, 0);
                    break;
                case RunTimeDataContainers.Float:
                    variables.Add(name, 0.0);
                    break;
                case RunTimeDataContainers.String:
                    variables.Add(name, "");
                    break;
            }
        }
    }

    public static object getVariable(string name)
    {
        if (variables.ContainsKey(name))
        {
            return variables[name];
        }
        else
        {
            return null;
        }
    }
}
