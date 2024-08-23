using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing
{
    class LineEditor
    {
        private string filePath;
        private List<string> lines;

        public LineEditor(string filePath)
        {
            this.filePath = filePath;

            if (File.Exists(filePath))
            {
                lines = new List<string>(File.ReadAllLines(filePath));
            }
            else
            {
                Console.WriteLine($"File {filePath} does not exist");
                Environment.Exit(1);
            }
        }

        public void Run()
        {
            Console.WriteLine(">> ");
            string input = Console.ReadLine();
            string[] commandParts = input.Split(' ', 2);
            string command = commandParts[0].ToLower();

            switch (command)
            {
                case "list":
                    ListLines();
                    Run();
                    break;

                case "del":
                    if (commandParts.Length > 1 && int.TryParse(commandParts[1], out int delLineNum))
                    {
                        DeleteLine(delLineNum);
                    }
                    else
                    {
                        Console.WriteLine("Usage: del <line_number>");
                    }
                    Run();
                    break;
                case "ins":
                    if (commandParts.Length > 1)
                    {
                        string[] insParts = commandParts[1].Split(' ', 2);
                        if (int.TryParse(insParts[0], out int insLineNum) && insParts.Length > 1)
                        {
                            InsertLine(insLineNum, insParts[1]);
                        }
                        else
                        {
                            Console.WriteLine("Usage: ins <line_number> <text>");
                        }
                    }
                    Run();
                    break;
                case "save":
                    SaveFile();
                    Run();
                    break;
                case "quit":
                    return;
                default:
                    Console.WriteLine("Unknown command.");
                    break;

            }
        }

        private void ListLines()
        {
            for (int i = 0; i < lines.Count; i++)
            {
                Console.WriteLine($"{i + 1}: {lines[i]}");
            }
        }

        public void DeleteLine(int number)
        {
            if (number > 0 && number < lines.Count)
            {
                lines.RemoveAt(number - 1);
                Console.WriteLine($"Line {number} deleted.");
            }
            else
            {
                Console.WriteLine("Invalid line number.");
            }
        }

        private void InsertLine(int lineNumber, string text)
        {
            if (lineNumber > 0 && lineNumber <= lines.Count + 1)
            {
                lines.Insert(lineNumber - 1, text);
                Console.WriteLine($"Line inserted at {lineNumber}.");
            }
            else
            {
                Console.WriteLine("Invalid line number.");
            }
        }

        public void SaveFile()
        {
            File.WriteAllLines(filePath, lines);
            Console.WriteLine("File saved successfully");
        }
    }
}
