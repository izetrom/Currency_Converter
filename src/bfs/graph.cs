using System;
using System.Collections.Generic;

namespace CurrencyConvertor
    {
    namespace Bfs
    {
        interface IGraph<T> where T : notnull
        {
            public Dictionary<T, HashSet<T>> AdjacencyList  { get; }

        }
        public class Graph<T> : IGraph<T> where T : notnull 
        {
            public Dictionary<T, HashSet<T>> AdjacencyList  { get; } = new Dictionary<T, HashSet<T>>();
            public Graph(IEnumerable<T> vertices, IEnumerable<Tuple< T, T, T>> edges)
            {
                foreach(T vertex in vertices)
                    AddVertex(vertex);
                foreach(Tuple<T, T, T> edge in edges)
                    AddEdge(edge);
            }
            private void AddVertex(T vertex)
            {
                AdjacencyList[vertex] = new HashSet<T>();
            }
            private void AddEdge(Tuple< T, T, T> edge)
            {
                if (AdjacencyList.ContainsKey(edge.Item1) && AdjacencyList.ContainsKey(edge.Item2)) {
                    AdjacencyList[edge.Item1].Add(edge.Item2);
                    AdjacencyList[edge.Item2].Add(edge.Item1);
                }
            }
        }   
    }
}