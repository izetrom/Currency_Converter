using System.Collections.Generic;
using CurrencyConvertor.Tools;

namespace CurrencyConvertor
{
    namespace Bfs
    {
        interface IAlgorithm
        {
            public int doConversion(HandleFile handleFile, List<string> order);
            public Func<T, IEnumerable<T>> shortestPathFunction<T>(Graph<T> graph, T start) where T : notnull;
        }
        class Algorithm : IAlgorithm
        {
            static float calcul(bool reverse, float valueToConvert, string multiplicater)
            {
                float tmp;
                float.TryParse(multiplicater, out tmp);
                if (reverse == true) {
                    tmp = 1 / tmp;
                   tmp = (float)Math.Round((float)tmp, 4);
                }
                valueToConvert = valueToConvert * tmp;
                valueToConvert = (float)Math.Round((float)valueToConvert, 4);
                return valueToConvert;
            }
            public int doConversion(HandleFile handleFile, List<string> order)
            {
                float valueToConvert;
                float.TryParse(handleFile.Goal[1], out valueToConvert);
                for (int i = 0; i + 1 < order.Count; i++) {
                    foreach(Tuple<string, string, string> line in handleFile.Edges)
                    {
                        if (line.Item1 == order[i] && line.Item2 == order[i + 1])
                            valueToConvert = calcul(false, valueToConvert, line.Item3);
                        else if (line.Item2 == order[i] && line.Item1 == order[i + 1])
                            valueToConvert = calcul(true, valueToConvert, line.Item3);
                    }
                }
                return (int)Math.Round(valueToConvert);
            }

            public Func<T, IEnumerable<T>> shortestPathFunction<T>(Graph<T> graph, T start) where T : notnull
            {
                Dictionary<T, T> previous = new Dictionary<T, T>();
                Queue<T> queue = new Queue<T>();
    
                queue.Enqueue(start);
                while (queue.Count > 0)
                {
                    T tmp = queue.Dequeue();
                    foreach(T neighbor in graph.AdjacencyList[tmp])
                    {
                        if (previous.ContainsKey(neighbor))
                            continue;
                        previous[neighbor] = tmp;
                        queue.Enqueue(neighbor);
                    }
                }
                Func<T, IEnumerable<T>> shortestPath = v => {
                    List<T> path = new List<T>{};
                    T current = v;
                    while (!current.Equals(start)) {
                        path.Add(current);
                        current = previous[current];
                    };
                    path.Add(start);
                    path.Reverse();
                    return path;
                };
                return shortestPath;
            }
        }
    }
}