using Runner.NsRunTime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Runner.NsParser
{
    enum BlockType
    {
        Assigment,
        Activity,
        Condition,
        Loop
    }
    class Block
    {
        public int Id;
        public static int IdCounter = 0;
        public BlockType Type { get; set; }
        public string Value { get; set; }

        public Block(BlockType type, string value)
        {
            Id = ++IdCounter;
            Type = type;
            Value = value;
        }

        public virtual void Run()
        {
            
        }
        

        public override string ToString()
        {
            return $"({Type}, {Value})";
        }
    }

    class AssigmentBlock : Block
    {
        public string Activity { get; set; }
        public Dictionary<String, String> Parameters;
        public AssigmentBlock(string value) : base(BlockType.Assigment, value)
        {
            Activity = "";
            Parameters = new Dictionary<string, string>();
            ParseValue();
        }

        public override void Run()
        {
            RunTimeExec.Execute(Activity, Parameters);
            
            //Console.WriteLine(Id +":AssigmentBlock - " + Activity);
        }

        private void ParseValue()
        {
            Activity = Value.Substring(0, Value.IndexOf(" "));
            Value = Value.Remove(0, Activity.Length + 1);
            string[] words = Value.Split(';');
            foreach(string word in words)
            {
                string[] breaked = word.Split(':');
                Parameters.Add(breaked[0].Trim(), breaked[1].Trim());
            }
        }
        
    }
    
    class ActivityBlock : Block
    {
        public string Activity { get; set; }
        public Dictionary<String, String> Parameters;
        public ActivityBlock(string value) : base(BlockType.Activity, value)
        {
            Activity = "";
            Parameters = new Dictionary<string, string>();
            ParseValue();
        }
        public override void Run()
        {
            try
            { 
            Console.WriteLine(Id + ":ActivityBlock - " + Activity);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message} at line {this.Id}" );
    }
}

        private void ParseValue()
        {
            Activity = Value.Substring(0, Value.IndexOf(" "));
            Value = Value.Remove(0, Activity.Length + 1);
            string[] words = Value.Split(';');
            foreach (string word in words)
            {
                string[] breaked = word.Split(':');
                Parameters.Add(breaked[0].Trim(), breaked[1].Trim());
            }
        }
    }

    class ConditionBlock : Block
    {
        public string Activity { get; set; }
        public Dictionary<String, String> Parameters;
        public List<Block>? TrueBlock;
        public List<Block>? FalseBlock;
        public ConditionBlock(string value) : base(BlockType.Condition, value)
        {
            Activity = "";
            Parameters = new Dictionary<string, string>();
            ParseValue();
            TrueBlock = null;
            FalseBlock = null;
        }
        public ConditionBlock(string value, List<Block> TrueBlock, List<Block> FalseBlock) : base(BlockType.Condition, value)
        {
            Activity = "";
            Parameters = new Dictionary<string, string>();
            ParseValue();
            this.TrueBlock = TrueBlock;
            this.FalseBlock = FalseBlock;
        }
        public void SetBlocks(List<Block> TrueBlock, List<Block> FalseBlock)
        {
            this.TrueBlock = TrueBlock;
            this.FalseBlock = FalseBlock;
        }
        public override void Run()
        {
            Console.WriteLine("ConditionBlock - " + Activity);
            //if true
            Console.WriteLine("ConditionBlock True : ");
            foreach (var block in TrueBlock)
            {
                block.Run();
            }
            //else
            Console.WriteLine("ConditionBlock False : ");
            foreach (var block in FalseBlock)
            {
                block.Run();
            }
            Console.WriteLine("ConditionBlock Ended");
        }

        private void ParseValue()
        {
            Activity = Value.Substring(0, Value.IndexOf(" "));
            Value = Value.Remove(0, Activity.Length + 1);
            string[] words = Value.Split(';');
            foreach (string word in words)
            {
                string[] breaked = word.Split(':');
                Parameters.Add(breaked[0].Trim(), breaked[1].Trim());
            }
        }     
    }

    class LoopBlock : Block
    {
        public string Activity { get; set; }
        public Dictionary<String, String> Parameters;
        public List<Block>? block;

        public LoopBlock(string value) : base(BlockType.Loop, value)
        {
            Activity = "";
            Parameters = new Dictionary<string, string>();
            this.block = null;
            ParseValue();
        }
        public LoopBlock(string value, List<Block> block) : base(BlockType.Loop, value)
        {
            Activity = "";
            Parameters = new Dictionary<string, string>();
            this.block = block;
            ParseValue();
        }
        public void SetBlock(List<Block> block)
        {
            this.block = block;
        }

        public override void Run()
        {
            //while
            Console.WriteLine("LoopBlock While : " + Activity);
            Console.WriteLine("LoopBlock Start : ");
            Random rnd = new Random();
            int i = 1;
            while (rnd.Next(100) < 75)
            {
                Console.WriteLine("Loop No : " + i);
                i++;
                foreach (Block block in block)
                {
                    block.Run();
                }

            }
            
            Console.WriteLine("LoopBlock ended ");
        }

        private void ParseValue()
        {
            Activity = Value.Substring(0, Value.IndexOf(" "));
            Value = Value.Remove(0, Activity.Length + 1);
            string[] words = Value.Split(';');
            foreach (string word in words)
            {
                string[] breaked = word.Split(':');
                Parameters.Add(breaked[0].Trim(), breaked[1].Trim());
            }
        }

    }

}

