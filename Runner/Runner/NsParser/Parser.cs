using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Runner.NsParser
{
    class Parser
    {
        private string? input;
        private Lexer lexer;
        private List<Block>? blocks;

        public Parser(string input)
        {
            this.input = input;
            this.lexer = new Lexer(this.input);
            this.blocks = this.lexer.Blockenize();
        }

        public void Run(){
            foreach (var block in blocks)
            {
                block.Run();
            }
        }

        public List<Block> GetBlocks()
        {
            return this.blocks;
        }

    }
}
