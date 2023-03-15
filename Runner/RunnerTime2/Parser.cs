class Parser
{
    private readonly List<Token> _tokens;
    private int _pos;

    public Parser(List<Token> tokens)
    {
        _tokens = tokens;
        _pos = 0;
    }

    public ASTNode Parse()
    {
        return ParseExpression();
    }

    private ASTNode ParseExpression()
    {
        ASTNode node = ParseTerm();
        while (Match(TokenType.Operator, "+") || Match(TokenType.Operator, "-"))
        {
            Token op = Consume();
            ASTNode right = ParseTerm();
            node = new BinaryOperationNode(op, node, right);
        }
        return node;
    }

    private ASTNode ParseTerm()
    {
        ASTNode node = ParseFactor();
        while (Match(TokenType.Operator, "*") || Match(TokenType.Operator, "/"))
        {
            Token op = Consume();
            ASTNode right = ParseFactor();
            node = new BinaryOperationNode(op, node, right);
        }
        return node;
    }

    private ASTNode ParseFactor()
    {
        if (Match(TokenType.Number))
        {
            return new NumberNode(Consume());
        }
        else if (Match(TokenType.Identifier))
        {
            return new VariableNode(Consume());
        }else if (Match(TokenType.Keyword ,"int"))
        {
            Consume(); // Consume the "int" keyword
            if (_tokens[_pos].Type == TokenType.Identifier)
            {
                string name = _tokens[_pos].Value;
                Consume(); // Consume the identifier
                if (_tokens[_pos].Type == TokenType.Operator && _tokens[_pos].Value == "=")
                {
                    Consume(); // Consume the "=" operator
                    ASTNode expression = ParseExpression();
                    return new VariableDeclarationNode(name, expression);
                }
                else
                {
                    return new VariableNode(_tokens[_pos]);
                }
            }
            else
            {
                throw new ParseException($"Unexpected token: {_tokens[_pos]}");
            }
        }
        else if (Match(TokenType.Keyword, "float"))
        {
            Consume(); // Consume the "int" keyword
            if (_tokens[_pos].Type == TokenType.Identifier)
            {
                string name = _tokens[_pos].Value;
                Consume(); // Consume the identifier
                if (_tokens[_pos].Type == TokenType.Operator && _tokens[_pos].Value == "=")
                {
                    Consume(); // Consume the "=" operator
                    ASTNode expression = ParseExpression();
                    return new VariableDeclarationNode(name, expression);
                }
                else
                {
                    return new VariableNode(_tokens[_pos]);
                }
            }
            else
            {
                throw new ParseException($"Unexpected token: {_tokens[_pos]}");
            }
        }
        else if (Match(TokenType.Keyword, "string"))
        {
            Consume(); // Consume the "int" keyword
            if (_tokens[_pos].Type == TokenType.Identifier)
            {
                string name = _tokens[_pos].Value;
                Consume(); // Consume the identifier
                if (_tokens[_pos].Type == TokenType.Operator && _tokens[_pos].Value == "=")
                {
                    Consume(); // Consume the "=" operator
                    ASTNode expression = ParseExpression();
                    return new VariableDeclarationNode(name, expression);
                }
                else
                {
                    return new VariableNode(_tokens[_pos]);
                }
            }
            else
            {
                throw new ParseException($"Unexpected token: {_tokens[_pos]}");
            }
        }
        else if (Match(TokenType.Delimiter, "["))
        {
            Consume(); // consume "["
            ASTNode index = ParseExpression();
            Match(TokenType.Delimiter, "]"); // ensure matching "]"
            Consume(); // consume "]"
            return new ArrayAccessNode(new VariableNode(Consume()), index);
        }
        else if (Match(TokenType.Boolean))
        {
            return new BooleanNode(Consume());
        }
        else if (Match(TokenType.String))
        {
            return new StringNode(Consume());
        }
        else if (Match(TokenType.Operator, "("))
        {
            Consume(); // consume "("
            ASTNode node = ParseExpression();
            Match(TokenType.Operator, ")"); // ensure matching ")"
            Consume(); // consume ")"
            return node;
        }
        else
        {
            throw new ParseException("Unexpected token: " + Peek());
        }
    }

    private bool Match(TokenType type, string value = null)
    {
        if (_pos >= _tokens.Count)
        {
            return false;
        }

        Token token = _tokens[_pos];
        if (token.Type == type && (value == null || token.Value == value))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private Token Consume()
    {
        if (_pos >= _tokens.Count)
        {
            throw new ParseException("Unexpected end of input.");
        }

        Token token = _tokens[_pos];
        _pos++;
        return token;
    }

    private Token Peek()
    {
        if (_pos >= _tokens.Count)
        {
            throw new ParseException("Unexpected end of input.");
        }

        return _tokens[_pos];
    }
}

class ParseException : Exception
{
    public ParseException(string message) : base(message) { }
}
