using System;
using System.Collections;
using System.Collections.Generic;
using BASIC.Tokenization;
using UnityEngine;

namespace BASIC {
    public class Parser {

        public void ParseLine(String line) {

            List<Token> tokenList = Tokenizer.Tokenize(line);
            tokenList.Reverse();
            Stack<Token> tokenStack = new Stack<Token>(tokenList);
            
            /*if (tokens.Peek().GetTokenType() == Tokenizer.NUMBER) {
                tokens.Pop();
            }*/

            ParseStatement(tokenStack);
        }

        public void ParseStatement(Stack<Token> tokens) {
            Debug.Log("test: " + tokens.Peek().GetValue());
            switch (tokens.Pop().GetValue()) {
                case "PRINT":
                    tokens.Pop();
                    Debug.Log(ParseExpression(tokens));
                    break;
                case "IF":
                    int operand1 = ParseExpression(tokens);
                    String relop = tokens.Pop().GetValue();
                    int operand2 = ParseExpression(tokens);

                    bool result = false;
                    switch (relop) {
                        case "<":
                            result = operand1 < operand2;
                            break;
                        case ">":
                            result = operand1 > operand2;
                            break;
                        case "=":
                            result = operand1 == operand2;
                            break;

                    }

                    if (result) {
                        ParseStatement(tokens);
                    }

                    break;
                
            }
        }

        public int ParseExpression(Stack<Token> tokens) {
            return int.Parse(tokens.Pop().GetValue());
        }

        public void ParseNumber() {

        }

    }
}