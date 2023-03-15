using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Runner.Parser
{
    enum BlockType
    {
        Assigment,
        Execution,
        Condition,
        Loop
    }
    class Block
    {
        public BlockType Type { get; set; }
        public List<Token> tokens { get; set; }
        public string Value { get; }

        public Block(BlockType type, string value)
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
