namespace Calculator.Calculator.Engine;

internal interface ICalculator<TExpression, TResult>
{
    public TResult Evaluate(TExpression expression);
    public TResult LastAnswer();
    public ExpressionCalculatorInstructions Instructions();
    public ExpressionCalculatorHistory GetFullHistory();
    public Stack<Memory> CalculationHistory { get; }
}

internal struct Memory(string expression, string result)
{
    public string Expression { get; } = expression;
    public string Result { get; } = result;
}

