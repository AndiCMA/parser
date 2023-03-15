using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
class Lexer
{
    private readonly Regex _regex;
    private readonly string _input;
    private int _pos;

    private static readonly Dictionary<Regex, TokenType> TokenDefinitions = new Dictionary<Regex, TokenType> {
        { new Regex(@"\b(int|float|double|string|bool)\b"), TokenType.Keyword },
        { new Regex(@"\btrue\b|\bfalse\b"), TokenType.Boolean },
        { new Regex(@"\b[A-Za-z_][A-Za-z0-9_]*\b"), TokenType.Identifier },
        { new Regex(@"\d+(\.\d+)?"), TokenType.Number },
        { new Regex(@""".*?"""), TokenType.String },
        { new Regex(@"[+\-*/=]"), TokenType.Operator },
        { new Regex(@"[;\[\],]"), TokenType.Delimiter },
        { new Regex(@"\s+"), TokenType.Whitespace }
    };

    public Lexer(string input)
    {
        _input = input;
        _regex = new Regex(string.Join("|", TokenDefinitions.Keys));
        _pos = 0;
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

            if (match.Value.Trim().Length == 0)
            {
                tokens.Add(new Token(TokenType.Whitespace, match.Value));
                continue; // skip whitespace
            }

            TokenType type = TokenDefinitions.First(pair => pair.Key.IsMatch(match.Value)).Value;
            tokens.Add(new Token(type, match.Value));
            _pos = match.Index + match.Length;
        }

        // Remove any whitespace tokens
        tokens.RemoveAll(t => t.Type == TokenType.Whitespace);

        return tokens;
    }
}
