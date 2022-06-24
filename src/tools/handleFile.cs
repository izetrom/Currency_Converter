using System;
using System.Text.RegularExpressions;

namespace CurrencyConvertor
{
    namespace Tools
    {
        interface IHandleFile
        {
            public string[] openFile(string filePath);
            public string[] parseLine(string line);
            public void initAllNodeAndEdges(string[] file);
            public List<Tuple<string, string, string>> Edges {get;}
            public List<string> AllNodes { get;}
        }
        class HandleFile: IHandleFile
        {
            private ErrorHandling errorHandling;
            private List<string> allNodes = new List<string>();
            public List<string> AllNodes { get { return this.allNodes; }}

            private List<Tuple<string, string, string>> edges = new List<Tuple<string, string, string>>();
            public List<Tuple<string, string, string>> Edges { get { return this.edges; }}

            private Regex formatLine = new Regex(@"([a-zA-Z\s]{3});([0-9]*);([a-zA-Z\s]{3})");
            private Regex numberLine = new Regex(@"([0-9]*)");
            private Regex convertLine = new Regex(@"([a-zA-Z\s]{3});([a-zA-Z\s]{3});[0-9]*(?:\.[0-9]+)");
            private string[] goal = new string[] {};
            public string[] Goal { get { return this.goal; } set { this.goal = value; }
}

            public HandleFile() {
                errorHandling = new ErrorHandling();
            }
            public HandleFile(ErrorHandling errorHandler) {
                errorHandling = errorHandler;
            }
            public string[] openFile(string filePath)
            {
                string[] lines = new String[]{};
                try {
                    lines = System.IO.File.ReadAllLines(filePath);
                    errorHandling.checkFormatFile(lines, formatLine, numberLine, convertLine);
                } catch(Exception e) {
                    System.Console.WriteLine(e);
                }
                return lines;
            }
            public string[] parseLine(string line)
            {
                return Regex.Split(line, ";");
            }
            public void initAllNodeAndEdges(string[] file)
            {
                for (int i = 2; i < file.Length; i++)
                {
                    string[] tmp = parseLine(file[i]);
                    Tuple<string, string, string> tmpTupple = new Tuple<string, string, string>(tmp[0], tmp[1], tmp[2]);
                    if (!edges.Contains(tmpTupple))
                        edges.Add(tmpTupple);
                    if (!allNodes.Contains(tmp[0]))
                        allNodes.Add(tmp[0]);
                    if (!allNodes.Contains(tmp[1]))
                        allNodes.Add(tmp[1]);
                }
            }
        }
    }
}