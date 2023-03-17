using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Runner.Grammar;

namespace Runner.NsParser
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
            { new Regex(@"<Execution>(.*?)<\/Execution>"), BlockType.Activity }
        }; 
        private static readonly Dictionary<Regex, String> StatementDefinitions = new Dictionary<Regex, String>
        {
            { new Regex(@"<ConditionStatement>([\s\S]*?)<\/ConditionStatement>"), "Condition"},
            { new Regex(@"<ConditionTrue>([\s\S]*?)<\/ConditionTrue>"), "True"},
            { new Regex(@"<ConditionFalse>([\s\S]*?)<\/ConditionFalse>"), "False" },

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
                string value = GetGroupValue(match);

                BlockType type = BlockDefinition.First(pair => pair.Key.IsMatch(match.Value)).Value;


                Regex StatementRegex;
                Lexer StatementLexerTrue, StatementLexerFalse, StatementLexerLoop;
                List<Block> StatementBlocksTrue, StatementBlocksFalse, StatementBlocksLoop;
                string condition , trueValue , falseValue, loopValue;
                ConditionBlock conditionBlock;
                LoopBlock loopBlock;
                switch (type)
                {
                    case BlockType.Assigment:
                        blocks.Add(new AssigmentBlock(value));
                        break;
                    case BlockType.Activity:
                        blocks.Add(new ActivityBlock(value));
                        break;
                     case BlockType.Condition:
                        condition = "";
                        trueValue = "";
                        falseValue = "";
                        StatementRegex = new Regex(@"<ConditionStatement>([\s\S]*?)<\/ConditionStatement>");
                        condition = GetGroupValue(StatementRegex.Matches(value)[0]);
                        StatementRegex = new Regex(@"<ConditionTrue>([\s\S]*?)<\/ConditionTrue>");
                        trueValue = GetGroupValue(StatementRegex.Matches(value)[0]);
                        StatementRegex = new Regex(@"<ConditionFalse>([\s\S]*?)<\/ConditionFalse>");
                        falseValue = GetGroupValue(StatementRegex.Matches(value)[0]);
                        StatementLexerTrue = new Lexer(trueValue);
                        StatementLexerFalse = new Lexer(falseValue);

                        conditionBlock = new ConditionBlock(condition);
                        StatementBlocksTrue = StatementLexerTrue.Blockenize();
                        StatementBlocksFalse = StatementLexerFalse.Blockenize();
                        conditionBlock.SetBlocks(StatementBlocksTrue, StatementBlocksFalse);

                        blocks.Add(conditionBlock);
                        break;
                    case BlockType.Loop:
                        condition = "";
                        loopValue = "";
                        StatementRegex = new Regex(@"<ConditionStatement>([\s\S]*?)<\/ConditionStatement>");
                        condition = GetGroupValue(StatementRegex.Matches(value)[0]);
                        StatementRegex = new Regex(@"<LoopBody>([\s\S]*?)<\/LoopBody>");
                        loopValue = GetGroupValue(StatementRegex.Matches(value)[0]);
                        StatementLexerLoop = new Lexer(loopValue);

                        loopBlock = new LoopBlock(condition);
                        StatementBlocksLoop = StatementLexerLoop.Blockenize();
                        loopBlock.SetBlock(StatementBlocksLoop);


                        blocks.Add(loopBlock);
                         break;
                    default:
                        blocks.Add(new Block(type, value));
                        break;
                }
                
                _pos = match.Index + match.Length;
            }
            
            return blocks;
        }
        

        private string GetGroupValue(Match match)
        {
            string value = "";
            int i = 1;
            while (value == "" && i < match.Groups.Count)
            {
                value = match.Groups[i].Value;
                i++;
            }
            return value;
        }



      

    };

}
