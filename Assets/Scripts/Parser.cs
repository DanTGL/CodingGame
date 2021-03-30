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

        private static readonly char[] delimiter = { ' ' };
        

        public (int, Token[]) ParseLine(String line) {
            //Stack<Token> tokenStack = new Stack<Token>(tokenList);
            
            /*if (tokens.Peek().GetTokenType() == Tokenizer.NUMBER) {
                tokens.Pop();
            }*/

            //ParseStatement(tokenStack);

            String[] splitLine = line.Split(delimiter, 2);
            int lineNum = int.Parse(splitLine[0]);
            Token[] tokens = Tokenizer.Tokenize(splitLine[1]).ToArray();

            return (lineNum, tokens);
        }

        public SortedList<int, Token[]> ParseCodeArray(String[] lines) {
            SortedList<int, Token[]> lineList = new SortedList<int, Token[]>();

            

            for (int i = 0; i < lines.Length; i++) {
                (int lineNum, Token[] tokens) = ParseLine(lines[i]);
                lineList.Add(lineNum, tokens);
            }

            return lineList;
        }

        public SortedList<int, Token[]> ParseCode(String code) {
            SortedList<int, Token[]> lineList = new SortedList<int, Token[]>();
            
            String line = null;
            StringReader reader = new StringReader(code);

            while ((line = reader.ReadLine()) != null) {
                Debug.Log("Line: " + line);
                (int lineNum, Token[] tokens) = ParseLine(line);
                lineList.Add(lineNum, tokens);
            }

            return lineList;
        }

        

        public static int ParseExpression(Queue<Token> tokens) {
            return int.Parse(tokens.Dequeue().GetValue());
        }

    }
}