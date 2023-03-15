using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

enum TokenType
{
    Keyword,
    Identifier,
    Number,
    String,
    Operator,
    Delimiter,
    Array,
    Whitespace,
    Boolean
}

class Token
{
    public TokenType Type { get; set; }
    public string Value { get; }

    public Token(TokenType type, string value)
    {
        Type = type;
        Value = value;
    }

    public override string ToString()
    {
        return $"({Type}, {Value})";
    }
}