using System;
using System.IO;

namespace AoC21_1_Sonar_Sweep
{
    class Program
    {
        static void Main(string[] args)
        {
            string userInput;
            string content;
            string[] lines;
            int? count = null;
            ConsoleKeyInfo consoleKeyInfo;

            if (args == null || args.Length == 0)
            {
                Console.WriteLine($"Please enter a path to a valid path to an input textfile.");
                userInput = Console.ReadLine();
            }
            else
            {
                userInput = args[0];
            }

            if (!string.IsNullOrEmpty(userInput))
            {
                if (File.Exists(userInput))
                {
                    Console.WriteLine($"Reading Input...");
                    content = ReadInput(userInput);
                    lines = content.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

                    if (lines != null && lines.Length > 0)
                    {
                        Console.WriteLine($"{lines.Length} lines read.");
                        Console.WriteLine($"decide which measurements to read. [+] to count increased measuremnts, [-] to count decreased ones.");

                        do
                        {
                            consoleKeyInfo = Console.ReadKey();

                            if (consoleKeyInfo.Key == ConsoleKey.Add)
                            {
                                count = CountMeasurements(lines, Direction.Increase);
                                Console.WriteLine($"Counted {count.Value} increased measurements.");
                            }
                            else if (consoleKeyInfo.Key == ConsoleKey.Subtract)
                            {
                                count = CountMeasurements(lines, Direction.Decrease);
                                Console.WriteLine($"Counted {count.Value} decreased measurements.");
                            }
                            else
                            {
                                Console.WriteLine($"{consoleKeyInfo.Key} is not a valid choice. Please try again.");
                            }
                        }
                        while (count == null);
                    }
                    else
                    {
                        Console.WriteLine($"no lines read.");
                    }
                }
                else
                {
                    Console.WriteLine($"\"{userInput}\" contains invalid characters or does not exist or you do not have permission to load the file.");
                }
            }
            else
            {
                Console.WriteLine($"the input path is null or empty.");
            }

            Console.WriteLine($"Press [{ConsoleKey.Escape}] to exit...");

            do
            {
                consoleKeyInfo = Console.ReadKey();
            }
            while (consoleKeyInfo.Key != ConsoleKey.Escape);
        }

        private static string ReadInput(string inputPath)
        {
            if (inputPath == null)
            {
                throw new ArgumentNullException(nameof(inputPath));
            }

            if (inputPath == string.Empty)
            {
                throw new ArgumentException("The path to the input most not be null or empty.");
            }

            string content;

            using (StreamReader reader = new StreamReader(inputPath))
            {
                content = reader.ReadToEnd();
            }

            return content;
        }

        private static int CountMeasurements(string[] lines, Direction direction)
        {
            if (lines == null)
            {
                throw new ArgumentNullException(nameof(lines));
            }

            int count = 0;
            int length = lines.Length;
            string line;
            int previousMeasurement;
            int measurement;

            if (length > 1)
            {
                for (int i = 1; i < length; i++)
                {
                    line = lines[i - 1];
                    previousMeasurement = int.Parse(line);

                    line = lines[i];
                    measurement = int.Parse(line);

                    switch (direction)
                    {
                        case Direction.Decrease:
                            if (previousMeasurement > measurement)
                            {
                                count++;
                            }
                            break;
                        case Direction.Increase:
                            if (previousMeasurement < measurement)
                            {
                                count++;
                            }
                            break;
                        default:
                            throw new NotImplementedException();
                    }
                }
            }

            return count;
        }
    }
}
