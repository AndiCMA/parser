using System;
using System.Collections.Generic;

public class Variables
{
    private Dictionary<string, object> values = new Dictionary<string, object>();

    public void Set(string name, object value)
    {
        values[name] = value;
    }

    public object Get(string name)
    {
        if (!values.ContainsKey(name))
        {
            throw new Exception($"Variable '{name}' not defined");
        }
        return values[name];
    }
}