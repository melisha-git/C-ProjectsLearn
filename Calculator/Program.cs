
using System.Text;

class Calculator
{
    private delegate int Operation(int x, int y);
    public Calculator()
    {
        
    }

    static private void CheckExpression(in string expression)
    {
        if (expression.Count(c => char.IsDigit(c)) != expression.Length)
        {
            throw new Exception($"Expression {expression} is not numeric");
        }
    }

    static private Operation GetOpeartion(in string operation)
    {
        switch (operation)
        {
            case "*":
                return (x, y) => x * y;
            case "/":
                return (x, y) => x / y;
            case "+":
                return (x, y) => x + y;
            case "-":
                return (x, y) => x - y;
        }
        throw new Exception($"Operation {operation} is not found");
    }

    static private string MakeOperation(in string operation, in string leftExpression, in string rightExpression)
    {
        CheckExpression(leftExpression);
        CheckExpression(rightExpression);
        Operation oper = GetOpeartion(operation);
        return oper(Convert.ToInt32(leftExpression), Convert.ToInt32(rightExpression)).ToString();
    }

    static public int Calculate(string line)
    {
        int firstBracket = -1;
        int lastBracker = -1;
        while ((firstBracket = line.IndexOf('(')) != -1 | (lastBracker = line.LastIndexOf(')')) != -1)
        {
            if (firstBracket != -1 && lastBracker == -1)
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append(' ', 39 + firstBracket);
                string errorMsg = "The closing quotation mark is missing: " + line + "\n" + stringBuilder.ToString() + "|";
                throw new Exception(errorMsg);
            }
            else if (firstBracket == -1 && lastBracker != -1)
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append(' ', 39 + lastBracker);
                string errorMsg = "The opening quotation mark is missing: " + line + "\n" + stringBuilder.ToString() + "|";
                throw new Exception(errorMsg);
            }

            int newValue = Calculate(line.Substring(firstBracket + 1, lastBracker - firstBracket - 1));
            line = line.Replace(line.Substring(firstBracket, lastBracker - firstBracket + 1), newValue.ToString());
        }

        List<string> vars = line.Split(' ').ToList();

        int operationIndex = 0;
        while ((operationIndex = vars.IndexOf("*")) != -1 || (operationIndex = vars.IndexOf("/")) != -1 
            || (operationIndex = vars.IndexOf("+")) != -1 || (operationIndex = vars.IndexOf("-")) != -1)

        {
            if (operationIndex == 0 || operationIndex == vars.Count - 1)
            {
                throw new Exception("Errors in expression logic");
            }
            string newValue = MakeOperation(vars[operationIndex], vars[operationIndex - 1], vars[operationIndex + 1]);
            vars[operationIndex] = newValue;
            vars.RemoveAt(operationIndex + 1);
            vars.RemoveAt(operationIndex - 1);
        }

        return Convert.ToInt32(vars[0]);
    }
}

class Programm
{
    static public void Main()
    {
        
            string? line = null;
            while ((line = Console.ReadLine()) != null)
            {
                try
                {
                    if (line.Length != 0)
                    {
                        Console.WriteLine(Calculator.Calculate(line));
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        
    }
}
