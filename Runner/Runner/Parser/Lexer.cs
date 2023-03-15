using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Runner.Parser
{
    class Lexer
    {
        private readonly Regex _regex;
        private readonly string _input;
        private int _pos;
        private static readonly Dictionary<Regex, BlockType> BlockDefinition = new Dictionary<Regex, BlockType>
        {
            { new Regex(@"<Loop>([\s\S]*?)<\/Loop>"), BlockType.Loop },
            { new Regex(@"<Condition>([\s\S]*?)<\/Condition>"), BlockType.Condition },
            { new Regex(@"<Assigment>(.*?)<\/Assigment>"), BlockType.Assigment },
            { new Regex(@"<Execution>(.*?)<\/Execution>"), BlockType.Execution }
        }; 
        private static readonly Dictionary<Regex, TokenType> TokenDefinitions = new Dictionary<Regex, TokenType>
        {
            { new Regex(@"\b(VariableDeclaration|VariableSet)\b"), TokenType.AssigmentKeyword },
            { new Regex(@"\b(sqrt|pow)\b"), TokenType.ExecutionKeyword },

        };

        public Lexer(string input)
        {
            _input = input;
            _regex = new Regex(string.Join("|", BlockDefinition.Keys));
            _pos = 0;
        }
        public List<Block> Blockenize()
        {
            List<Block> blocks = new List<Block>();

            MatchCollection matches = _regex.Matches(_input);
            foreach (Match match in matches)
            {
                if (match.Index < _pos)
                {
                    continue; // skip already processed matches
                }
                
                BlockType type = BlockDefinition.First(pair => pair.Key.IsMatch(match.Value)).Value;
                blocks.Add(new Block(type, match.Value));
                _pos = match.Index + match.Length;
            }
            
            return blocks;
        }
        public List<Token> Tokenize()
        {
            List<Token> tokens = new List<Token>();

            MatchCollection matches = _regex.Matches(_input);
            foreach (Match match in matches)
            {
                if (match.Index < _pos)
                {
                    continue; // skip already processed matches
                }

                TokenType type = TokenDefinitions.First(pair => pair.Key.IsMatch(match.Value)).Value;
                tokens.Add(new Token(type, match.Value));
                _pos = match.Index + match.Length;
            }

            return tokens;
        }

    };

}
