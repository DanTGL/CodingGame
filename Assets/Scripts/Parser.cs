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

        

        public Queue<Token> ParseLine(String line) {
            Queue<Token> tokenQueue = Tokenizer.Tokenize(line);
            //Stack<Token> tokenStack = new Stack<Token>(tokenList);
            
            /*if (tokens.Peek().GetTokenType() == Tokenizer.NUMBER) {
                tokens.Pop();
            }*/

            //ParseStatement(tokenStack);

            return tokenQueue;
        }

        

        public static int ParseExpression(Queue<Token> tokens) {
            return int.Parse(tokens.Dequeue().GetValue());
        }

    }
}