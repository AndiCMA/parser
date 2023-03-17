using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Runner.NsRunTime
{
    static class AssigmentActivities
    {
        public static void VariableDeclaration(Dictionary<String, String> Parameters)
        {
            string name, value, type;
            Parameters.TryGetValue("name", out name);
            Parameters.TryGetValue("value", out value);
            Parameters.TryGetValue("type", out type);

            if(value != null)
            {
                Regex regex = new Regex(@"[*\/%+=]");
                bool isMath = regex.IsMatch(value);

            }

            RunTimeVariables.VariableDeclaration(name, type, value);
        }
        public static void VariableSet(Dictionary<String, String> Parameters)
        {
            string name, value;
            Parameters.TryGetValue("name", out name);
            Parameters.TryGetValue("value", out value);
            object Value = "";


            if (value != null)
            {
                bool isMath = Regex.IsMatch(value, @"[-*\/+=]");

                if (isMath)
                {
                    List<String> Elements = new List<string>();
                    MatchCollection matches = Regex.Matches(value, @"([-+*/%])|(\w+)");
                    foreach(Match match in matches)
                    {
                        Elements.Add(match.ToString());
                    }

                    if (RunTimeVariables.VariableExists(Elements[0]))
                    {
                        Value = RunTimeVariables.VariableGet(Elements[0]);
                    }
                    else
                    {
                        Value = getStringValue(Elements[0]);
                    }
                    if(Value.GetType() == typeof(int))
                    {
                        Value = (int)Value - 1;
                    }

                }
                else
                {
                    if (RunTimeVariables.VariableExists(value))
                    {
                        Value = RunTimeVariables.VariableGet(value);

                    }
                    else
                    {
                        Value = value;
                    }
                }


            }

            RunTimeVariables.VariableSet(name, Value);
        }
        public static void VariableGet(Dictionary<String, String> Parameters)
        {
            string name;
            Parameters.TryGetValue("name", out name);
            RunTimeVariables.VariableGet(name);
        }

        private static object getStringValue(string value)
        {

            if (value.Contains("\""))
            {
                return value.Substring(1,value.Length-2);
            }else if (value.Contains("."))
            {
                return Convert.ToSingle(value);
            }
            else
            {
                return Convert.ToInt32(value);
            }
            return "";
        }
    }

}

