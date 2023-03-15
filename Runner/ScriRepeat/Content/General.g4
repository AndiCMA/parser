grammar General;

program: statement*;

statement: 
    assignment | 
    loop | 
    condition |
    function_call ';';

assignment: 
    ID '=' expression ';';

expression: 
    ID | 
    INT | 
    STRING;

loop: 
    'while' '(' expression ')' '{' statement* '}';

condition: 
    'if' '(' expression ')' '{' statement* '}' ('else' '{' statement* '}')?;

function_call: 
    'startApp' '(' STRING ')' | 
    'startBrowser' '(' STRING ')' | 
    'readRegistry' '(' STRING ')';

ID: 
    [a-zA-Z][a-zA-Z0-9_]*;

INT: 
    [0-9]+;

STRING: 
    '"' .*? '"';

WS: 
    [ \t\n\r]+ -> skip;