using System;

namespace Testing
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Usage: LineEditor <file_path>");
                return;
            }

            Console.WriteLine("Available Commands: list | del | ins | save | quit");

            LineEditor editor = new LineEditor(args[0]);
            editor.Run();
        }
    }
}
