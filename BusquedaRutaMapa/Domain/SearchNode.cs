using System.Collections.Generic;
using BusquedaRutaMapa.Domain;

namespace BusquedaRutaMapa.Domain
{
    /// <summary>
    /// Nodo de búsqueda que implementa ISearchNode según el libro.
    /// Encapsula el estado (MapState) y sabe cómo encontrar sus sucesores.
    /// </summary>
    public class SearchNode : ISearchNode
    {
        public MapState State { get; }
        public ISearchNode Parent { get; set; }
        public double G { get; set; }
        public int Depth { get; }
        private readonly Dictionary<string, List<(string Target, double Distance)>> _graph;

        public SearchNode(MapState state, ISearchNode parent, double g, int depth, Dictionary<string, List<(string, double)>> graph)
        {
            State = state;
            Parent = parent;
            G = g;
            Depth = depth;
            _graph = graph;
        }

        public IEnumerable<ISearchNode> GetSuccessors()
        {
            if (_graph != null && _graph.ContainsKey(State.CityName))
            {
                foreach (var edge in _graph[State.CityName])
                {
                    yield return new SearchNode(new MapState(edge.Target), this, G + edge.Distance, Depth + 1, _graph);
                }
            }
        }

        public bool IsGoal(ISearchNode goalNode)
        {
            if (goalNode is SearchNode other)
            {
                return State.CityName == other.State.CityName;
            }
            return false;
        }

        public override string ToString() => State.ToString();
    }
}
