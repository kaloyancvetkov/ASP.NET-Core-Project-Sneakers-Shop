using System;

namespace FinalExam
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string command = Console.ReadLine();

            while (command != "Last note")
            {
                if (command.Contains('.') || command.Contains(',') || command.Contains(':') || !command.Contains("<<") || !command.Contains('='))
                {
                    Console.WriteLine("Nothing found!");
                }
                else
                {
                    if (command.IndexOf('=') >= command.IndexOf("<<") || command.IndexOf('=') == 0)
                    {
                        Console.WriteLine("Nothing found!");
                    }

                    var firstIndex = command.IndexOf('=');
                    var secondIndex = command.IndexOf("<<");
                    bool isValid = true;

                    var numberString = command.Substring(firstIndex + 1, secondIndex - firstIndex - 1);

                    for (int i = 0; i < numberString.Length; i++)
                    {
                        if (char.IsLetter(numberString[i]))
                        {
                            isValid = false;
                            break;
                        }
                    }
                    if (isValid)
                    {
                        var number = int.Parse(numberString);
                        var length = command.Length - (secondIndex + 2);
                        if (number == length)
                        {
                            var word = command.Substring(secondIndex + 2, length);
                            var coordinates = command.Substring(0, firstIndex);
                            var location = "";
                            for (int i = 0; i < coordinates.Length; i++)
                            {
                                if (coordinates[i] == '!' || coordinates[i] == '@' || coordinates[i] == '#' || coordinates[i] == '$' || coordinates[i] == '?')
                                {
                                    continue;
                                }
                                else
                                {
                                    location += coordinates[i];
                                }
                            }
                            Console.WriteLine($"Coordinates found! {location} -> {word}");
                        }
                        else
                        {
                            Console.WriteLine("Nothing found!");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Nothing found!");
                    }
                    
                }

                command = Console.ReadLine();

            }

        }
    }
}

