using System.Globalization;

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
        if (string.IsNullOrWhiteSpace(expression))
            throw new ArgumentException("Expression cannot be empty.");

        var rpn = ToRpn(expression);
        var func = BuildEvaluator(rpn);
        var result = func();

        if (double.IsNaN(result))
            throw new ArithmeticException("Expression resulted in an undefined value.");
        if (double.IsInfinity(result))
            throw new ArithmeticException("Expression resulted in infinity, possibly due to division by zero.");

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
            if (double.TryParse(token, NumberStyles.Float, CultureInfo.InvariantCulture, out _))
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
                    output.Add(stack.Pop());

                if (stack.Count == 0)
                    throw new ArgumentException("Mismatched parentheses: unexpected ')'.");

                stack.Pop();
                if (stack.Count > 0 && functions.Contains(stack.Peek()))
                    output.Add(stack.Pop());
            }
        }
        while (stack.Count > 0)
        {
            var top = stack.Pop();
            if (top == "(")
                throw new ArgumentException("Mismatched parentheses: unclosed '('.");
            output.Add(top);
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

            bool matchedOp = false;
            foreach (var op in operators)
            {
                if (expr.Substring(i).StartsWith(op))
                {
                    if (op == "-" && (tokens.Count == 0 || tokens[^1] == "(" || operators.Contains(tokens[^1])))
                        tokens.Add("0");
                    tokens.Add(op);
                    i += op.Length;
                    matchedOp = true;
                    break;
                }
            }
            if (!matchedOp)
                throw new ArgumentException($"Unrecognized character '{expr[i]}' at position {i}.");
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
            if (double.TryParse(token, NumberStyles.Float, CultureInfo.InvariantCulture, out double num))
            {
                stack.Push(() => num);
            }
            else if (operators.Contains(token))
            {
                if (stack.Count < 2)
                    throw new ArgumentException($"Insufficient operands for operator '{token}'.");
                var right = stack.Pop();
                var left = stack.Pop();
                var op = operationsMap[token];
                stack.Push(() => op(left(), right()));
            }
            else if (functions.Contains(token))
            {
                if (stack.Count < 1)
                    throw new ArgumentException($"Insufficient operands for function '{token}'.");
                var arg = stack.Pop();
                var func = functionsMap[token];
                stack.Push(() => func(arg()));
            }
        }
        if (stack.Count != 1)
            throw new ArgumentException("Expression is invalid.");
        return stack.Pop();
    }

    public ExpressionCalculatorInstructions Instructions()
    { 
        return new ExpressionCalculatorInstructions(new ExpressionCalculatorInfoTitle("Instructions"))
            .AddInstruction("This calculator supports basic arithmetic operations, exponentiation, and common mathematical functions.")
            .AddInstruction("You can use parentheses to group expressions and control the order of operations.")
            .AddInstruction("Supported operators: " + string.Join(", ", operators), ExpressionCalculatorStyleOptions.CreateDefaultHighlight)
            .AddInstruction("Supported functions: " + string.Join(", ", functions.Select(fn => $"{fn}()")), ExpressionCalculatorStyleOptions.CreateDefaultHighlight)
            .AddInstruction("Example inputs:", ExpressionCalculatorStyleOptions.CreateDefaultTitle)
            .AddInstruction("-> 2 + 3 * 4")
            .AddInstruction("-> (1 + 2) * (3 + 4)")
            .AddInstruction("-> 5 ^ 2")
            .AddInstruction("-> sin(0.5) + cos(1.0)")
            .AddInstruction("-> log(10) * sqrt(16)");

    }

    public ExpressionCalculatorHistory GetFullHistory()
    {
        return new ExpressionCalculatorHistory(new ExpressionCalculatorInfoTitle("History"))
            .AddHistoryEntries(CalculationHistory);
    }
}

internal class ExpressionCalculatorInstructions(ExpressionCalculatorInfoTitle infoTitle)
{
    public ExpressionCalculatorInfoTitle InfoTitle { get; private set; } = infoTitle;
    public List<ExpressionCalculatorInfoEntry> InstructionEntries { get; private set; } = new();

    public ExpressionCalculatorInstructions AddInstruction(string description, ExpressionCalculatorStyleOptions? style = null)
    {
        InstructionEntries.Add(new ExpressionCalculatorInfoEntry(description, style ?? ExpressionCalculatorStyleOptions.CreateDefaultText));

        return this;
    }
}

internal class ExpressionCalculatorHistory(ExpressionCalculatorInfoTitle infoTitle)
{
    public ExpressionCalculatorInfoTitle InfoTitle { get; private set; } = infoTitle;
    public List<ExpressionCalculatorInfoEntry> HistoryEntries { get; private set; } = new();
    public ExpressionCalculatorHistory AddHistoryEntry(string description, ExpressionCalculatorStyleOptions? style = null)
    {
        HistoryEntries.Add(new ExpressionCalculatorInfoEntry(description, style ?? ExpressionCalculatorStyleOptions.CreateDefaultText));

        return this;
    }

    public ExpressionCalculatorHistory AddHistoryEntries(IEnumerable<Memory> history, ExpressionCalculatorStyleOptions? style = null)
    {
        foreach (var entry in history)
        {
            HistoryEntries.Add(new ExpressionCalculatorInfoEntry($"{entry.Expression} = {entry.Result}", style ?? ExpressionCalculatorStyleOptions.CreateDefaultText));
        }

        return this;
    }
}

internal class ExpressionCalculatorInfoTitle(string title, ExpressionCalculatorStyleOptions? style = null)
{
    public string Title { get; } = title;
    public ExpressionCalculatorStyleOptions Style { get; } = style ?? ExpressionCalculatorStyleOptions.CreateDefaultTitle;
}

internal class ExpressionCalculatorStyleOptions(int width, ConsoleColor color)
{
    public ConsoleColor Color { get; } = color;
    public int Width { get; } = width;

    public static ExpressionCalculatorStyleOptions CreateDefaultHighlight
        => new(
            32,
            ConsoleColor.Yellow);

    public static ExpressionCalculatorStyleOptions CreateDefaultTitle
        => new(
            32,
            ConsoleColor.Cyan);

    public static ExpressionCalculatorStyleOptions CreateDefaultText
        => new(
            32,
            ConsoleColor.White);
}


internal class ExpressionCalculatorInfoEntry(string description, ExpressionCalculatorStyleOptions? style = null)
{
    public string Description { get; } = description;
    public ExpressionCalculatorStyleOptions Style { get; } = style ?? ExpressionCalculatorStyleOptions.CreateDefaultTitle;
}



