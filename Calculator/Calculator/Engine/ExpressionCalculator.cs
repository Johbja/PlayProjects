namespace Calculator.Calculator.Engine;

internal class ExpressionCalculator<TResult>
    : ICalculator<string, TResult>
    where TResult : struct
{
    public Stack<Memory> CalculationHistory => _calculationHistory;

    private TResult? _answer;
    private string[] operators = new string[] { "+", "-", "*", "/", "^" };
    private string[] functions = new string[] { "sin", "cos", "tan", "log", "sqrt" };
    private Stack<Memory> _calculationHistory = new();

    private Dictionary<string, Func<double, double, double>> operationsMap = new()
    {
        {"+", (a, b) => a + b},
        {"-", (a, b) => a - b},
        {"*", (a, b) => a * b},
        {"/", (a, b) => a / b},
        {"^", Math.Pow }
    };

    private Dictionary<string, Func<double, double>> functionsMap = new()
    {
        {"sin", Math.Sin},
        {"cos", Math.Cos},
        {"tan", Math.Tan},
        {"log", Math.Log},
        {"sqrt", Math.Sqrt}
    };

    public TResult Evaluate(string expression)
    {
        var rpn = ToRpn(expression);
        var func = BuildEvaluator(rpn);
        var result = func();
        TResult typedResult = (TResult)Convert.ChangeType(result, typeof(TResult));

        _calculationHistory.Push(new Memory(expression, typedResult.ToString()));
        _answer = typedResult;

        return typedResult;
    }

    public TResult LastAnswer()
    {
        return _answer.HasValue ? _answer.Value : default;
    }

    private List<string> ToRpn(string expression)
    {
        var output = new List<string>();
        var stack = new Stack<string>();
        var tokens = Tokenize(expression);

        foreach (var token in tokens)
        {
            if (double.TryParse(token, out _))
            {
                output.Add(token);
            }
            else if (functions.Contains(token))
            {
                stack.Push(token);
            }
            else if (operators.Contains(token))
            {
                while (stack.Count > 0 && operators.Contains(stack.Peek()) &&
                       (Precedence(stack.Peek()) > Precedence(token) ||
                        Precedence(stack.Peek()) == Precedence(token) && token != "^"))
                {
                    output.Add(stack.Pop());
                }
                stack.Push(token);
            }
            else if (token == "(")
            {
                stack.Push(token);
            }
            else if (token == ")")
            {
                while (stack.Count > 0 && stack.Peek() != "(")
                {
                    output.Add(stack.Pop());
                }
                stack.Pop();
                if (stack.Count > 0 && functions.Contains(stack.Peek()))
                {
                    output.Add(stack.Pop());
                }
            }
        }
        while (stack.Count > 0)
        {
            output.Add(stack.Pop());
        }
        return output;
    }

    private List<string> Tokenize(string expr)
    {
        var tokens = new List<string>();
        int i = 0;
        while (i < expr.Length)
        {
            if (char.IsWhiteSpace(expr[i]))
            {
                i++;
                continue;
            }
            if (char.IsDigit(expr[i]) || expr[i] == '.')
            {
                int start = i;
                while (i < expr.Length && (char.IsDigit(expr[i]) || expr[i] == '.')) i++;
                tokens.Add(expr.Substring(start, i - start));
                continue;
            }
            if (expr[i] == '(' || expr[i] == ')')
            {
                tokens.Add(expr[i].ToString());
                i++;
                continue;
            }

            bool matchedFunc = false;
            foreach (var func in functions)
            {
                if (expr.Substring(i).StartsWith(func))
                {
                    tokens.Add(func);
                    i += func.Length;
                    matchedFunc = true;
                    break;
                }
            }
            if (matchedFunc) continue;

            foreach (var op in operators)
            {
                if (expr.Substring(i).StartsWith(op))
                {
                    tokens.Add(op);
                    i += op.Length;
                    break;
                }
            }
        }
        return tokens;
    }

    private int Precedence(string op)
    {
        return op switch
        {
            "+" or "-" => 1,
            "*" or "/" => 2,
            "^" => 3,
            _ => 0
        };
    }

    private Func<double> BuildEvaluator(List<string> rpn)
    {
        var stack = new Stack<Func<double>>();
        foreach (var token in rpn)
        {
            if (double.TryParse(token, out double num))
            {
                stack.Push(() => num);
            }
            else if (operators.Contains(token))
            {
                var right = stack.Pop();
                var left = stack.Pop();
                var op = operationsMap[token];
                stack.Push(() => op(left(), right()));
            }
            else if (functions.Contains(token))
            {
                var arg = stack.Pop();
                var func = functionsMap[token];
                stack.Push(() => func(arg()));
            }
        }
        return stack.Pop();
    }

    public string[] Instructions()
    {
        return
        [
            "---Instructions---",
            "",
            "Supported actions: " + string.Join(", ", operators),
            "Supported functions: " + string.Join(", ", functions.Select(fn => $"{fn}()")),
            "Parentheses for grouping: ( )",
            "Example inputs:",
            "-> 2 + 3 * 4",
            "-> (1 + 2) * (3 + 4)",
            "-> 5 ^ 2",
            "-> sin(0.5) + cos(1.0)",
            "-> log(10) * sqrt(16)"
        ];
    }

    public string[] GetFullHistory()
    {
        string[] header = [
            "---History---",
            "",
        ];

        return header.Concat(CalculationHistory.Select(entry => $"{entry.Expression} = {entry.Result}")).ToArray();
    }
}


