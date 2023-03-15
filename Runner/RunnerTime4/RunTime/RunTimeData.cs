using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

enum RunTimedataTypes
{
    Int,
    Float,
    String
}

static class RunTimeData
{
    public static Dictionary<string, object> variables = new Dictionary<string, object>();

    public static void DeclareVariable(string name, RunTimedataTypes type)
    {
        if (variables.ContainsKey(name))
        {
            throw new Exception("Variable already declared");
        }
        else
        {
            if (type == RunTimedataTypes.Int)
            {
                variables.Add(name, 0);
            }
            else if (type == RunTimedataTypes.Float)
            {
                variables.Add(name, 0.0f);
            }
            else if (type == RunTimedataTypes.String)
            {
                variables.Add(name, "");
            }
        }
    }
    public static void AddVariable(string name, object value, RunTimedataTypes type)
    {
        switch (type)
        {
            case RunTimedataTypes.Int:
                variables.Add(name, Convert.ToInt32(value));
                break;
            case RunTimedataTypes.Float:
                variables.Add(name, Convert.ToSingle(value));
                break;
            case RunTimedataTypes.String:
                variables.Add(name, Convert.ToString(value));
                break;
        }
    }
    public static void SetVariable(string name, object value)
    {
        variables[name] = value;
    }
    public static object GetVariable(string name)
    {
        return variables[name];
    }
    public static bool VariableExists(string name)
    {
        return variables.ContainsKey(name);
    }
    
}
