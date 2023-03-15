using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

class Token
{
    public string Type { get; }
    public string Value { get; }

    public Token(string type, string value)
    {
        Type = type;
        Value = value;
    }

    public override string ToString()
    {
        return $"({Type}, {Value})";
    }
}

class Lexer
{
    static void Main()
    {
        string input = "int x = 42; string s = \"hello world\"; int[] a = {1, 2, 3};";
        Regex regex = new Regex(@"\b(int|float|double|string|bool)\b|\b[A-Za-z_][A-Za-z0-9_]*\b|\d+(\.\d+)?|"".*?""|[+\-*/=;,\[\]]|\s+");

        List<Token> tokens = new List<Token>();

        MatchCollection matches = regex.Matches(input);
        foreach (Match match in matches)
        {
            if (match.Value.Trim().Length == 0)
            {
                continue; // skip whitespace
            }

            string type;
            switch (match.Value)
            {
                case "+":
                case "-":
                case "*":
                case "/":
                case "=":
                case ";":
                case ",":
                case "[":
                case "]":
                    type = "Delimiter";
                    break;
                case "int":
                case "float":
                case "double":
                case "string":
                case "bool":
                    type = "Keyword";
                    break;
                case "true":
                case "false":
                    type = "Boolean";
                    break;
                default:
                    if (match.Value.StartsWith("\"") && match.Value.EndsWith("\""))
                    {
                        type = "String";
                    }
                    else if (match.Value.StartsWith("[") && match.Value.EndsWith("]"))
                    {
                        type = "Array";
                    }
                    else if (Regex.IsMatch(match.Value, @"\d+(\.\d+)?"))
                    {
                        type = "Number";
                    }
                    else
                    {
                        type = "Identifier";
                    }
                    break;
            }

            tokens.Add(new Token(type, match.Value));
        }

        foreach (Token token in tokens)
        {
            Console.WriteLine(token);
        }
    }
}
