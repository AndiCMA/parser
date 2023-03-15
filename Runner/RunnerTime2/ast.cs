public enum ASTNodeType
{
    // Define different types of AST nodes here
    // For example:
    BinaryOperation,
    VariableDeclaration,
    Number,
    Variable,
    FunctionCall,
    ArrayAccess,
    String,
    Boolean
}

class ASTNode
{
    public ASTNodeType NodeType { get; }
    // Define additional properties for each node type here
    // For example:
    public ASTNode Left { get; set; }
    public ASTNode Right { get; set; }
    public Token Operator { get; set; }
    public double Value { get; set; }
    public string Identifier { get; set; }
    public List<ASTNode> Arguments { get; set; }

    public ASTNode(ASTNodeType nodeType)
    {
        NodeType = nodeType;
    }
}
class BinaryOperationNode : ASTNode
{
    public BinaryOperationNode(Token op, ASTNode left, ASTNode right) : base(ASTNodeType.BinaryOperation)
    {
        Operator = op;
        Left = left;
        Right = right;
    }
    public ASTNode Left { get; set; }
    public ASTNode Right { get; set; }
    public Token Operator { get; set; }
}

class FunctionCallNode : ASTNode
{
    public FunctionCallNode(string functionName, List<ASTNode> arguments) : base(ASTNodeType.FunctionCall)
    {
        FunctionName = functionName;
        Arguments = arguments;
    }
    public string FunctionName { get; set; }
    public List<ASTNode> Arguments { get; set; }
}

class NumberNode : ASTNode
{
    public NumberNode(Token token) : base(ASTNodeType.Number)
    {
        if (token.Type != TokenType.Number)
        {
            throw new ArgumentException("Token must be a number", nameof(token));
        }
        Value = double.Parse(token.Value);
    }
    public double Value { get; set; }
}
class VariableNode : ASTNode
{
    public VariableNode(Token token) : base(ASTNodeType.Variable)
    {
        if (token.Type != TokenType.Identifier)
        {
            throw new ArgumentException("Token must be an identifier", nameof(token));
        }
        Name = token.Value;
    }
    public string Name { get; set; }
}

class ArrayAccessNode : ASTNode
{
    public ArrayAccessNode(VariableNode variable, ASTNode index) : base(ASTNodeType.ArrayAccess)
    {
        Variable = variable;
        Index = index;
    }
    public VariableNode Variable { get; set; }
    public ASTNode Index { get; set; }
}

class BooleanNode : ASTNode
{
    public BooleanNode(Token token) : base(ASTNodeType.Boolean)
    {
        if (token.Type != TokenType.Boolean)
        {
            throw new ArgumentException("Token must be a boolean", nameof(token));
        }
        Value = bool.Parse(token.Value);
    }
    public bool Value { get; set; }
}

class StringNode : ASTNode
{
    public StringNode(Token token) : base(ASTNodeType.String)
    {
        if (token.Type != TokenType.String)
        {
            throw new ArgumentException("Token must be a string", nameof(token));
        }
        Value = token.Value;
    }
    public string Value { get; set; }
}

class VariableDeclarationNode : ASTNode
{
    public VariableDeclarationNode(string name, ASTNode expression) : base(ASTNodeType.VariableDeclaration)
    {
        Name = name;
        Expression = expression;
    }
    public string Name { get; set; }
    public ASTNode Expression { get; set; }
}
