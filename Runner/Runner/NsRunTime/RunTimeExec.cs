using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Runner.NsRunTime
{
    internal class RunTimeExec
    {
        delegate void MyFunctionDelegate(Dictionary<string, string> parameters);
        
        private static readonly Dictionary<string, MyFunctionDelegate> functionDictionary = InitializeDictionary();

        // Static function to initialize the dictionary with the functions
        private static Dictionary<string, MyFunctionDelegate> InitializeDictionary()
        {
            var dict = new Dictionary<string, MyFunctionDelegate>();
            dict.Add("VariableDeclaration", VariableDeclaration);
            dict.Add("VariableSet", VariableSet);
            return dict;
        }

        public static void Execute(string activity, Dictionary<String, String> parameters)
        {
            functionDictionary[activity](parameters);
        }



        private static void VariableDeclaration(Dictionary<String, String> Parameters)
        {
            AssigmentActivities.VariableDeclaration(Parameters);
        }
        private static void VariableSet(Dictionary<String, String> Parameters)
        {
            AssigmentActivities.VariableSet(Parameters);
        }
    }
}
