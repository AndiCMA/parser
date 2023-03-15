using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

public class Lexer
{
    private string input;
    private readonly List<TokenDefinition> tokenDefinitions;
    private readonly List<Token> tokens;

    public Lexer(string input)
    {
        this.input = input;
        this.tokenDefinitions = new List<TokenDefinition>();
        this.tokens = new List<Token>();
        AddTokenDefinitions();
    }

    public List<Token> Tokenize()
    {
        List<Token> tokens = new List<Token>();

        var inputPosition = 0;
        while (inputPosition < input.Length)
        {
            var match = false;
            foreach (var rule in tokenDefinitions)
            {
                var regex = rule.Pattern;
                var matchResult = regex.Match(input, inputPosition);
                if (matchResult.Success && matchResult.Index == inputPosition)
                {
                    var value = matchResult.Value;
                    tokens.Add(new Token(rule.Type, value));
                    inputPosition += value.Length;
                    match = true;
                    break;
                }
            }
            if (!match)
            {
                throw new Exception($"Unable to match input at position {inputPosition}");
            }
        }

        // Remove any whitespace tokens
        //tokens.RemoveAll(tok => tok.Type == TokenType.Whitespace);

        return tokens;
    }

    private void AddTokenDefinitions()
    {
        tokenDefinitions.Add(new TokenDefinition(TokenType.Keyword, new Regex(@"\b(if|else|while|int|float|string)\b")));
        tokenDefinitions.Add(new TokenDefinition(TokenType.Number, new Regex(@"\d+(\.\d+)?")));
        tokenDefinitions.Add(new TokenDefinition(TokenType.String, new Regex("\"[^\"]*\"")));
        tokenDefinitions.Add(new TokenDefinition(TokenType.String, new Regex("\'[^\']*\'")));
        tokenDefinitions.Add(new TokenDefinition(TokenType.Operator, new Regex(@"\+|\-|\*|\=|\>|\<|\!")));
        tokenDefinitions.Add(new TokenDefinition(TokenType.Identifier, new Regex(@"[a-zA-Z_]\w*(?!\d)")));
        tokenDefinitions.Add(new TokenDefinition(TokenType.Delimiter, new Regex(@"\,|\;|\{|\}|\(|\)")));
        tokenDefinitions.Add(new TokenDefinition(TokenType.Array, new Regex(@"[a-zA-Z_]\w*\[\d+\]")));
        tokenDefinitions.Add(new TokenDefinition(TokenType.Whitespace, new Regex(@"\s+")));
    }
}

public class TokenDefinition
{
    public TokenType Type { get; }
    public Regex Pattern { get; }

    public TokenDefinition(TokenType type, Regex pattern)
    {
        this.Type = type;
        this.Pattern = pattern;
    }
}

public enum TokenType
{
    Keyword,
    Identifier,
    Number,
    String,
    Operator,
    Delimiter,
    Array,
    Whitespace
}

public class Token
{
    public TokenType Type { get; }
    public string Value { get; }

    public Token(TokenType type, string value)
    {
        this.Type = type;
        this.Value = value;
    }

    public override string ToString()
    {
        return $"{Type}: {Value}";
    }
}

public class Parser
{
    private readonly IEnumerable<Token> tokens;
    private IEnumerator<Token> tokenEnumerator;

    public Parser(IEnumerable<Token> tokens)
    {
        this.tokens = tokens;
        this.tokenEnumerator = tokens.GetEnumerator();
    }

    public void Parse()
    {
        while (tokenEnumerator.MoveNext())
        {
            var currentToken = tokenEnumerator.Current;
            if (currentToken.Type == TokenType.Keyword && currentToken.Value == "if")
            {
                ParseIfStatement();
            }
            else if (currentToken.Type == TokenType.Keyword && currentToken.Value == "while")
            {
                ParseWhileStatement();
            }
            else if (currentToken.Type == TokenType.Identifier)
            {
                ParseAssignmentStatement();
            }
            else
            {
                throw new Exception($"Unexpected token: {currentToken}");
            }
        }
    }
    private void ParseIfStatement()
    {
        // match if keyword
        var ifToken = Expect(TokenType.Keyword, "if");

        // match opening parenthesis
        var openingParenthesisToken = Expect(TokenType.Delimiter, "(");

        // parse condition
        ParseExpression();

        // match closing parenthesis
        var closingParenthesisToken = Expect(TokenType.Delimiter, ")");

        // match opening brace
        var openingBraceToken = Expect(TokenType.Delimiter, "{");

        // parse body
        Parse();

        // match closing brace
        var closingBraceToken = Expect(TokenType.Delimiter, "}");
    }

    private void ParseWhileStatement()
    {
        // match while keyword
        var whileToken = Expect(TokenType.Keyword, "while");

        // match opening parenthesis
        var openingParenthesisToken = Expect(TokenType.Delimiter, "(");

        // parse condition
        ParseExpression();

        // match closing parenthesis
        var closingParenthesisToken = Expect(TokenType.Delimiter, ")");

        // match opening brace
        var openingBraceToken = Expect(TokenType.Delimiter, "{");

        // parse body
        Parse();

        // match closing brace
        var closingBraceToken = Expect(TokenType.Delimiter, "}");
    }

    private void ParseAssignmentStatement()
    {
        // match identifier
        var identifierToken = Expect(TokenType.Identifier);

        // match assignment operator
        var assignmentOperatorToken = Expect(TokenType.Operator, "=");

        // parse expression
        ParseExpression();

        // match semicolon
        var semicolonToken = Expect(TokenType.Delimiter, ";");
    }

    private void ParseExpression()
    {
        var currentToken = tokenEnumerator.Current;
        if (currentToken.Type == TokenType.Identifier)
        {
            ParseVariable();
        }
        else if (currentToken.Type == TokenType.Number || currentToken.Type == TokenType.String)
        {
            ParseLiteral();
        }
        else
        {
            throw new Exception($"Unexpected token: {currentToken}");
        }
    }

    private void ParseVariable()
    {
        var identifierToken = Expect(TokenType.Identifier);

        if (tokenEnumerator.Current.Type == TokenType.Delimiter && tokenEnumerator.Current.Value == "[")
        {
            var openingBracketToken = Expect(TokenType.Delimiter, "[");

            // parse expression
            ParseExpression();

            var closingBracketToken = Expect(TokenType.Delimiter, "]");
        }
    }

    private void ParseLiteral()
    {
        if (tokenEnumerator.Current.Type == TokenType.Number)
        {
            Expect(TokenType.Number);
        }
        else if (tokenEnumerator.Current.Type == TokenType.String)
        {
            Expect(TokenType.String);
        }
    }
    private Token Expect(TokenType expectedType, string expectedValue = null)
    {
        var currentToken = tokenEnumerator.Current;
        if (currentToken.Type != expectedType)
        {
            throw new Exception($"Expected token of type {expectedType}, but found {currentToken.Type}");
        }
        if (expectedValue != null && currentToken.Value != expectedValue)
        {
            throw new Exception($"Expected token with value '{expectedValue}', but found '{currentToken.Value}'");
        }
        tokenEnumerator.MoveNext();
        return currentToken;
    }
}