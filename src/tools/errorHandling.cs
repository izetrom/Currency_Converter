using System;
using System.Text.RegularExpressions;

namespace CurrencyConvertor
{
    namespace Tools
    {
        interface IErrorHandling
        {
            public void checkArgs(string[] args);
            public void checkFormatFile(string[] file, Regex firstLineRegex, Regex numberRegex, Regex convertRegex);
        }
        class ErrorHandling: IErrorHandling
        {
            public void checkArgs(string[] args)
            {
                if (args.Length != 1)
                    throw new Exception("Bad number of arguments");
            }
            public void checkFormatFile(string[] file, Regex firstLineRegex, Regex numberRegex, Regex convertRegex)
            {
                try {
                int i = 2;
                if (file.Length < 3)
                    throw new Exception("File have bad size");
                checkFormatLine(file[0], firstLineRegex);
                checkFormatLine(file[1], numberRegex);
                for (; i < file.Length; i++)
                    checkFormatLine(file[i], convertRegex);
                if ((i - 2).ToString() != file[1])
                    throw new Exception("Bad number of convertions");
                } catch(Exception e) {
                    Console.WriteLine(e.Message);
                    Environment.Exit(84);
                }
            }
            static void checkFormatLine(string line, Regex regex)
            {
                if (!regex.IsMatch(line))
                    throw new Exception("Bad format on this line: " + line);                
            }
        }
    }
}