using System.Text.Json.Nodes;

public class Programm
{
    static private string GetLine(string console)
    {
        string? line = null;
        do {
            Console.WriteLine(console);
            line = Console.ReadLine();
        }
        while (line == null || line.Length == 0);
        return line == null ? "" : line;
    }

    static private void ValueGettint(out string source, out string target, out int amount)
    {
        source = GetLine("Введите текущую валюту");
        target = GetLine("Введите валюту в которую необходимо перевести");

        amount = Convert.ToInt32(GetLine("Введите количество текущей валюты"));
    }

    static public void Main()
    {
        Console.WriteLine("Добро пожаловать в конвертер валют");
        HttpClient client = new HttpClient();
        while (true)
        {
            try
            {
                string source; string target; int amount;
                ValueGettint(out source, out target, out amount);
                client.BaseAddress = new Uri($"https://api.exchangerate-api.com/v4/latest/{source.ToUpper()}");

                HttpResponseMessage response = client.GetAsync("").Result;

                string responceBody = response.Content.ReadAsStringAsync().Result;
                JsonNode? obj = JsonObject.Parse(responceBody)?.AsObject();

                if (obj != null)
                {
                    var value = obj["rates"]?[target.ToUpper()]?.GetValue<double>();
                    Console.WriteLine(value * amount);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}