using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

enum RunTimeType
{
    Assigment,
    
}
class RunTime
{
    List<Token> tokens;
    RunTimeType type;

    public RunTime(List<Token> tokens)
    {
        this.tokens = tokens;
    }
    
    private void run()
    {
        switch (type)
        {
            case RunTimeType.Assigment:
                AssigmentRunTime();
                break;
        }
    }

    private RunTimeType discoverRunTimetype()
    {
        //Some rules
        return RunTimeType.Assigment;
    }

    private void  AssigmentRunTime()
    {
        if (tokens[0].Type == TokenType.Keyword)
        {
            if (tokens.Count() > 2)
            {

            }
        }
    }
    
}

