using System;
using CurrencyConvertor.Tools;
using CurrencyConvertor.Bfs;
namespace CurrencyConvertor {
    class Program {
        static void Parse(HandleFile handleFile, string[] args)
        {
            string[] file = handleFile.openFile(args[0]);
            handleFile.Goal = handleFile.parseLine(file[0]);
            handleFile.initAllNodeAndEdges(file);
        }

        static float calcul(HandleFile handleFile)
        {
            List<string> path = new List<string>();
            Graph<string> graph = new Graph<string>(handleFile.AllNodes, handleFile.Edges);
            Algorithm algorithm = new Algorithm();
            Func<string, IEnumerable<string>> shortestPath = algorithm.shortestPathFunction(graph, handleFile.Goal[0]);
            path = shortestPath(handleFile.Goal[2]).ToList<string>();
            return algorithm.doConversion(handleFile, path);
        }
        static void Main(string[] args)
        {
            Tools.ErrorHandling errorHandling = new ErrorHandling();
            Tools.HandleFile handleFile = new HandleFile(errorHandling);
            try {
                errorHandling.checkArgs(args);
                Parse(handleFile, args);
                float result = calcul(handleFile);
                Console.WriteLine(result);
            } catch(Exception e)
            {
                Console.WriteLine(e.Message);
                Environment.Exit(84);
            }
        }
    }
}