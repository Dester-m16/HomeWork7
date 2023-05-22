using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PhoneBookProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, string> phoneBook = new Dictionary<string, string>();

            string[] lines = File.ReadAllLines("phones.txt");
            foreach (string line in lines)
            {
                string[] parts = line.Split(':');
                if (parts.Length == 2)
                {
                    string name = parts[0].Trim();
                    string phoneNumber = parts[1].Trim();
                    phoneBook[name] = phoneNumber;
                }
            }

            using (StreamWriter writer = new StreamWriter("Phones.txt"))
            {
                foreach (string phoneNumber in phoneBook.Values)
                {
                    writer.WriteLine(phoneNumber);
                }
            }

            Console.Write("Enter a name to find the corresponding phone number: ");
            string inputName = Console.ReadLine();

            if (phoneBook.TryGetValue(inputName, out string foundPhoneNumber))
            {
                Console.WriteLine($"Phone number for {inputName}: {foundPhoneNumber}");
            }
            else
            {
                Console.WriteLine($"No phone number found for {inputName}.");
            }

            using (StreamWriter writer = new StreamWriter("New.txt"))
            {
                foreach (KeyValuePair<string, string> entry in phoneBook)
                {
                    string name = entry.Key;
                    string phoneNumber = entry.Value;

                    if (phoneNumber.StartsWith("80") && phoneNumber.Length == 11)
                    {
                        phoneNumber = "+380" + phoneNumber.Substring(2);
                    }

                    writer.WriteLine($"{name}: {phoneNumber}");
                }
            }

            Console.WriteLine("Phone numbers in the new format have been written to \"New.txt\".");
        }
    }
}