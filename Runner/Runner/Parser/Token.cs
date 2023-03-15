using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Runner.Parser
{
    enum TokenType
    {
        AssigmentKeyword,
        ExecutionKeyword,
        Param,
        Delimiter,
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
}
