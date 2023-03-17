using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Runner.NsRunTime
{
    enum RunTimeVariablesTypes
    {
        Int,
        Float,
        String
    };

    static class RunTimeVariables
    {
        public static Dictionary<String, object> Variables = new Dictionary<string, object>();

        public static void VariableDeclaration(string name, string type, string value = null)
        {
            if (Variables.ContainsKey(name))
            {
                throw new Exception("Variable already exists");
            }
            else
            {
                try
                {
                    switch (type.ToLower())
                    {
                        case "int":
                            Variables.Add(name, Convert.ToInt32(value));
                            break;
                        case "float":
                            Variables.Add(name, Convert.ToSingle(value));
                            break;
                        case "string":
                            Variables.Add(name, value);
                            break;
                        default:
                            throw new Exception("Variable type not supported");
                    }
                }
                catch (System.FormatException e)
                {
                    object a = VariableGet(value);
                    Variables.Add(name,a);

                }
            }
        }

        public static void VariableSet(string name, string value)
        {
            if (VariableExists(name))
            {
                Variables[name] = value;
            }
            else
            {
                if (value.Contains("\""))
                {
                    VariableDeclaration(name, "String", value);
                }
                else
                {
                    VariableDeclaration(name, "Int" , value);
                }
            }
        }
        public static void VariableSet(string name, object value)
        {
            if (VariableExists(name))
            {
                Variables[name] = value;
            }
            else
            {
                if (value.GetType() == typeof(string))
                    VariableDeclaration(name, "String", (string)value);
                else
                    VariableDeclaration(name, "Int", value.ToString());
            }
        }

        public static bool VariableExists(string name)
        {
            return Variables.ContainsKey(name);
        }

            public static object VariableGet(string name)
        {
            if (Variables.ContainsKey(name))
            {
                return Variables[name];
            }
            else
            {
                throw new Exception("Variable not found");
            }
        }

    }
}
