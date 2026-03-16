using System.Collections.Generic;
using System.Linq;
using BusquedaRutaMapa.Domain;
using BusquedaRutaMapa.Infrastructure;

namespace BusquedaRutaMapa.Application
{
    /// <summary>
    /// El MOTOR de búsqueda que implementa Dijkstra.
    /// </summary>
    public class DijkstraSearcher
    {
        private readonly Dictionary<string, List<(string Target, double Distance)>> _graph;

        public DijkstraSearcher(Dictionary<string, List<(string, double)>> graph)
        {
            _graph = graph;
        }

        public List<string> FindShortestPath(string startCity, string goalCity, out double totalDistance)
        {
            var openList = new List<SearchNode>();
            var closedList = new HashSet<string>();

            var startNode = new SearchNode(new MapState(startCity), null, 0, 0);
            openList.Add(startNode);

            while (openList.Count > 0)
            {
                var current = openList.OrderBy(n => n.G).First();
                openList.Remove(current);

                if (current.State.CityName == goalCity)
                {
                    totalDistance = current.G;
                    return ReconstructPath(current);
                }

                closedList.Add(current.State.CityName);

                if (_graph.ContainsKey(current.State.CityName))
                {
                    foreach (var edge in _graph[current.State.CityName])
                    {
                        if (closedList.Contains(edge.Target)) continue;

                        double newG = current.G + edge.Distance;
                        var existingNode = openList.FirstOrDefault(n => n.State.CityName == edge.Target);

                        if (existingNode == null)
                        {
                            openList.Add(new SearchNode(new MapState(edge.Target), current, newG, current.Depth + 1));
                        }
                        else if (newG < existingNode.G)
                        {
                            existingNode.G = newG;
                        }
                    }
                }
            }

            totalDistance = -1;
            return null;
        }

        private List<string> ReconstructPath(SearchNode node)
        {
            var path = new List<string>();
            while (node != null)
            {
                path.Add(node.State.CityName);
                node = node.Parent;
            }
            path.Reverse();
            return path;
        }
    }
}
