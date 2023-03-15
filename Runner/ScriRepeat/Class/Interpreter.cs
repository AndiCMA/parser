using System;
using System.Collections.Generic;
using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;

public class Interpreter : MyScriptLanguageBaseListener
{
    private Variables variables = new Variables();
    private Functions functions = new Functions();

    public void Execute(string script)
    {
        ICharStream stream = CharStreams.fromString(script);
        ITokenSource lexer = new MyScriptLanguageLexer(stream);
        ITokenStream tokens = new CommonTokenStream(lexer);
        MyScriptLanguageParser parser = new MyScriptLanguageParser(tokens);
        IParseTree tree = parser.program();
        ParseTreeWalker walker = new ParseTreeWalker();
        walker.Walk(this, tree);
    }

    public override void ExitVariableDeclaration(MyScriptLanguageParser.VariableDeclarationContext context)
    {
        string name = context.variableName().GetText();
        object value = Evaluate(context.expression());
        variables.Set(name, value);
    }

    public override void ExitFunctionCall(MyScriptLanguageParser.FunctionCallContext context)
    {
        string name = context.functionName().GetText();
        List<object> arguments = new List<object>();
        if (context.argumentList() != null)
        {
            foreach (var expressionContext in context.argumentList().expression())
            {
                arguments.Add(Evaluate(expressionContext));
            }
        }
        functions.Execute(name, arguments);
    }

    public override void ExitWhileLoop(MyScriptLanguageParser.WhileLoopContext context)
    {
        while ((bool)Evaluate(context.condition()))
        {
            Execute(context.program());
        }
    }

    public override void ExitDoWhileLoop(MyScriptLanguageParser.DoWhileLoopContext context)
    {
        do
        {
            Execute(context.program());
        } while ((bool)Evaluate(context.condition()));
    }

    public override void ExitForEachLoop(MyScriptLanguageParser.ForEachLoopContext context)
    {
        IEnumerable<object> collection = (IEnumerable<object>)Evaluate(context.collection());
        string variableName = context.variableName().GetText();
        foreach (object item in collection)
        {
            variables.Set(variableName, item);
            Execute(context.program());
        }
    }

    public override void ExitIfStatement(MyScriptLanguageParser.IfStatementContext context)
    {
        if ((bool)Evaluate(context.condition()))
        {
            Execute(context.program(0));
        }
        else if (context.program(1) != null)
        {
            Execute(context.program(1));
        }
    }

    private object Evaluate(MyScriptLanguageParser.ExpressionContext context)
    {
        if (context.INT() != null)
        {
            return int.Parse(context.INT().GetText());
        }
        if (context.STRING() != null)
        {
            return context.STRING().GetText().Trim('\"');
        }
    }
}

