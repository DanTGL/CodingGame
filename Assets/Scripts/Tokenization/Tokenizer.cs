using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace BASIC.Tokenization {

    public class Tokenizer {
        
        public static readonly TokenType RELOP = new TokenType("^(<(>|=|ε)|>(=|ε)|=|<|>)");
        public static readonly TokenType NUMBER = new TokenType("^([0-9]+)");
        public static readonly TokenType VAR = new TokenType("^[A-Z]");
        public static readonly TokenType BINOP = new TokenType(@"^(\+|-|\*|/|ε)");
        public static readonly TokenType L_PAREN = new TokenType(@"^\(");
        public static readonly TokenType R_PAREN = new TokenType(@"^\)");
        public static readonly TokenType FUNC = new TokenType("^[A-Z][A-Z]+");
        
        public static readonly TokenType[] tokenTypes = {
            L_PAREN,
            R_PAREN,
            FUNC,
            VAR,
            NUMBER,
            RELOP,
            BINOP
        };

        public static Match Match(String input, TokenType tokenType) {
            return tokenType.Match(input);
        }

        public static Queue<Token> Tokenize(String input) {
            Queue<Token> tokens = new Queue<Token>();
            while (input != "") {
                bool matched = false;
                input = input.TrimStart(' ');
                //Debug.Log("1: " + input);
                for (int i = 0; i < tokenTypes.Length; i++) {
                    Match match = Regex.Match(input, tokenTypes[i].GetPattern());

                    if (match.Success) {
                        //Debug.Log("2: " + match.Value);
                        tokens.Enqueue(new Token(tokenTypes[i], match.Value));
                        input = input.Substring(match.Length);
                        matched = true;
                        break;
                    }
                }

                if (!matched) {
                    Debug.LogError("Invalid syntax: " + input);
                    break;
                }
            }

            /*String result = "";

            foreach (Token token in tokens) {
                result += token.GetValue() + ", ";
            }

            Debug.Log(result);*/
            return tokens;
        }

    }

}
