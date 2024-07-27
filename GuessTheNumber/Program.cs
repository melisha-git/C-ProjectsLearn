public class Program
{
    public static void Main()
    {
        Console.WriteLine("Угадай число!");
        Random rnd = new Random();
        int sz = rnd.Next(3, 5);
        int minNumber = (int)(Math.Pow(10, sz)), maxNumber = (int)(minNumber * 9.9999);
        int number = rnd.Next(minNumber, maxNumber);
        string? line = null;
        while ((line = Console.ReadLine()) != null)
        {
            try
            {
                if (line.Length == 0)
                {
                    continue;
                }
                int lineNumber = int.Parse(line);
                if (lineNumber == number) {
                    Console.WriteLine("Вы угадали число");
                    break;
                } 
                else if (lineNumber < number) {
                    Console.WriteLine("Введите число побольше");
                }
                else if (lineNumber > number) {
                    Console.WriteLine("Введите число поменьше");
                }
            } 
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}