

public class Program
{
    public static bool isEven(string number)
    {
        if (number.Count(c => char.IsDigit(c)) != number.Length)
        {
            throw new Exception($"Argument{number} is not number\n");
        }
        return (number[^1] & 1) != 1;
    }

    public static void Main(string[] args)
    {
        if (args.Length != 0)
        {
            Console.WriteLine($"Number {args[0]} is " + (isEven(args[0]) ? "even" : "odd"));
            return;
        }

        string? line = null;
        while ((line = Console.ReadLine()) != null)
        {
            if (line.Length != 0)
            {
                Console.WriteLine($"Number {line} is " + (isEven(line) ? "even" : "odd"));
            }
        }
    }
}
