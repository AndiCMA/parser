using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Runner.Grammar
{
    static class Activities
    {
        public static List<String> AssigmentFunctions = new List<string>{
            "VariableDeclaration" ,
            "VariableSet" ,
            "VariableGet"
        };
        public static List<String> ActivitiesFunctions = new List<string>{
            "isEqual",
            "sqrt" ,
            "pow"
        };

    }
}
