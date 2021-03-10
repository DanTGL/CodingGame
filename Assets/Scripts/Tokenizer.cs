using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class Tokenizer {

    public class TokenType {
        String pattern;
        
        public TokenType(String pattern) {
            this.pattern = pattern;
        }

        public Match Match(String input) {
            return Regex.Match(input, pattern);
        }
    }

    public static readonly String RELOP = "<(>|=|ε)|>(=|ε)|=";
    public static readonly String NUMBER = "[0-9]+";
    public static readonly String VAR = "[A-Z]";
    public static readonly String BINOP = "+|-|ε";

    public class Token {
        String pattern;


    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
