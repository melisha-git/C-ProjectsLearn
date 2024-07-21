
struct Contact
{
    public string? name;
    public string? email;
    public string? phone;
    public string? address;
    public string? city;
}

class PhoneBook
{
    private Dictionary<string, Contact> _contacts = new Dictionary<string, Contact>();
    public void Add(in Contact contact)
    {
        if (string.IsNullOrEmpty(contact.name))
        {
            throw new Exception("Enter contact name again");
        }
        if (!_contacts.ContainsKey(contact.name))
        {
            _contacts.Add(contact.name, contact);
        }
    }

    public void Delete(in string name)
    {
        if (_contacts.ContainsKey(name))
        {
            _contacts.Remove(name);
        }
    }

    public Contact? GetContact(in string name)
    {
        if (!_contacts.ContainsKey(name))
        {
            return null;
        }
        return _contacts[name];
    }
}

public class Programm
{
    static private PhoneBook _phonebook = new PhoneBook();
    private delegate void HandleMessage(string message);

    static private Contact FillContact(HandleMessage messageHandler)
    {
        Contact contact = new Contact();
        contact.name = GetParamether("name", messageHandler);
        contact.email = GetParamether("email", messageHandler);
        contact.phone = GetParamether("phone", messageHandler);
        contact.address = GetParamether("address", messageHandler);
        contact.city = GetParamether("city", messageHandler);

        return contact;
    }

    static private string? GetParamether(in string paramether, HandleMessage messageHandler)
    {
        messageHandler($"Write contact {paramether}");
        string? name = Console.ReadLine();
        return name;
    }

    static private void HandleCommand(string command, HandleMessage messageHandler)
    {
        command = command.ToLower();
        switch (command)
        {
            case "add":
                Contact newContact = FillContact(messageHandler);
                _phonebook.Add(newContact);
                messageHandler($"Contact with name {newContact.name} was added");
                break;
            case "delete":
                string? name = GetParamether("name", messageHandler);
                if (string.IsNullOrEmpty(name))
                {
                    throw new Exception("Enter contact name again");
                }
                _phonebook.Delete(name);
                messageHandler($"Contact with name {name} was deleted");
                break;
            case "find":
                string? findName = GetParamether("name", messageHandler);
                if (string.IsNullOrEmpty(findName))
                {
                    throw new Exception("Enter contact name again");
                }
                Contact? contact = _phonebook.GetContact(findName);
                if (contact == null)
                {
                    Console.WriteLine($"Contact {findName} not found");
                    break;
                }
                messageHandler($"Contact info:");
                messageHandler($"Contact name: {contact?.name}");
                messageHandler($"Contact email: {contact?.email}");
                messageHandler($"Contact phone: {contact?.phone}");
                messageHandler($"Contact address: {contact?.address}");
                messageHandler($"Contact city: {contact?.city}");
                break;
        }
    }

    static public void Main()
    {
        string? line = null;

        while ((line = Console.ReadLine()) != null)
        {
            if (line.Length == 0 && line.ToLower() != "exit") continue;
            try
            {
                HandleCommand(line, (message) => Console.WriteLine(message));
            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}