using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using BASIC;
using BASIC.Tokenization;

public class Interpreter : MonoBehaviour {

    public SortedList<int, String> lines;
    public int curLine = 0;

    [SerializeField]
    private Display display;

    Parser parser;

    public Dictionary<String, int> variables;

    public static int GetPrecedence(String op) {
        switch (op) {
            case "/":
            case "*":
                return 1;

            case "+":
            case "-":
                return 0;
        }

        return -1;
    }

    public int EvaluateFunction(Token func, Queue<Token> tokens) {
        int result = 0;
        switch (func.GetValue()) {
            case "RND(":
                int arg = EvaluateExpression(tokens);
                result = UnityEngine.Random.Range(0, arg);
                break;
        }

        tokens.Dequeue();
        return result;
    }
    
    public int EvaluatePrimary(Queue<Token> tokens) {
        Token token = tokens.Dequeue();
        if (token.GetTokenType().Equals(Tokenizer.FUNC)) {
            return EvaluateFunction(token, tokens);
        } else if (token.GetTokenType().Equals(Tokenizer.L_PAREN)) {
            int result = EvaluateExpression(tokens);
            tokens.Dequeue();
            return result;
        } else if (token.GetTokenType().Equals(Tokenizer.NUMBER)) {
            return int.Parse(token.GetValue());
        } else if (token.GetTokenType().Equals(Tokenizer.VAR)) {
            return variables[token.GetValue()];
        }
        
        return 0;
    }

    public int EvaluateExpression(Queue<Token> tokens) {
        return EvaluateExpression1(tokens, EvaluatePrimary(tokens), 0);
    }

    public int EvaluateExpression1(Queue<Token> tokens, int lhs, int min_precedence) {
        if (tokens.Count == 0) return lhs;
        Token lookahead = tokens.Peek();
        while (tokens.Count != 0 && GetPrecedence(lookahead.GetValue()) >= min_precedence) {
            String op = lookahead.GetValue();
            if (tokens.Count == 0) return lhs;
            tokens.Dequeue();
            int rhs = EvaluatePrimary(tokens);

            if (tokens.Count > 0) {
                lookahead = tokens.Peek();

                while (GetPrecedence(lookahead.GetValue()) > GetPrecedence(op)) {
                    rhs = EvaluateExpression1(tokens, rhs, min_precedence + 1);

                    if (tokens.Count == 0) break;
                    lookahead = tokens.Peek();
                }
            }

            switch (op) {
                case "+":
                    lhs = lhs + rhs;
                    break;

                case "-":
                    lhs = lhs - rhs;
                    break;

                case "*":
                    lhs = lhs * rhs;
                    break;

                case "/":
                    lhs = lhs / rhs;
                    break;
            }
        }

        return lhs;
    }

    void ExtractLabels(String code) {
        String line = null;
        StringReader reader = new StringReader(code);
        while ((line = reader.ReadLine()) != null) {
            Debug.Log("Line: " + line);
            String[] splitLine = line.Split(new char[] {' '}, 2);
            lines.Add(int.Parse(splitLine[0]), splitLine[1]);
        }
    }

    public void ExecuteStatement(Queue<Token> tokens) {
        //Debug.Log("test: " + tokens.Peek().GetValue());
        switch (tokens.Dequeue().GetValue()) {
            case "PRINT":
                Debug.Log(EvaluateExpression(tokens));
                break;
            case "IF":
                int operand1 = EvaluateExpression(tokens);
                String relop = tokens.Dequeue().GetValue();
                int operand2 = EvaluateExpression(tokens);

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
                    ExecuteStatement(tokens);
                }

                break;
            case "GOTO":
                curLine = lines.IndexOfKey(Parser.ParseExpression(tokens)) - 1;
                break;

            case "LET":
                String variable = tokens.Dequeue().GetValue();
                Debug.Assert(tokens.Dequeue().GetValue().Equals("="));
                variables[variable] = EvaluateExpression(tokens);
                break;

            case "LINE":
                int x1 = EvaluateExpression(tokens);
                int y1 = EvaluateExpression(tokens);
                int x2 = EvaluateExpression(tokens);
                int y2 = EvaluateExpression(tokens);

                int c = EvaluateExpression(tokens);

                display.DrawLine(x1, y1, x2, y2, c);
                break;
            
            case "PLOT":
                int x = EvaluateExpression(tokens);
                int y = EvaluateExpression(tokens);

                int col = EvaluateExpression(tokens);

                display.PlotPixel(x, y, col);
                break;
        }
    }

    // Start is called before the first frame update
    void Start() {
        lines = new SortedList<int, string>();
        variables = new Dictionary<string, int>();
        parser = new Parser();
        //ExtractLabels("10 LET X = 1\n20 PRINT X\n30 LET X = X * 2\n40 IF X < 10 GOTO 20");
        String code = Resources.Load<TextAsset>("test").ToString();
        ExtractLabels(code);
    }

    void FixedUpdate() {
        if (curLine < lines.Count) {
            int key = lines.Keys[curLine];
            String statement = lines.Values[curLine];
            ExecuteStatement(parser.ParseLine(statement));

            curLine++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
