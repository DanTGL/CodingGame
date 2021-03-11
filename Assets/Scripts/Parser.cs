using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using BASIC.Tokenization;
using UnityEngine;

namespace BASIC {
    public class Parser {

        public Parser() {
            
        }

        

        public Stack<Token> ParseLine(String line) {
            List<Token> tokenList = Tokenizer.Tokenize(line);
            tokenList.Reverse();
            Stack<Token> tokenStack = new Stack<Token>(tokenList);
            
            /*if (tokens.Peek().GetTokenType() == Tokenizer.NUMBER) {
                tokens.Pop();
            }*/

            //ParseStatement(tokenStack);

            return tokenStack;
        }

        

        public static int ParseExpression(Stack<Token> tokens) {
            return int.Parse(tokens.Pop().GetValue());
        }

        public void ParseNumber() {

        }

    }
}