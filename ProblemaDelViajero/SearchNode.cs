namespace ProblemaDelViajero.Infrastructure
{
    using Domain;

    /// <summary>
    /// Nodo de búsqueda que envuelve al Estado con metadatos de búsqueda (Fred Mellender).
    /// </summary>
    public class SearchNode
    {
        public TspState State { get; }
        public SearchNode Parent { get; }
        public double Cost { get; } // g(n)
        public double LowerBound { get; } // f(n) o h(n)
        public int Depth { get; }

        public SearchNode(TspState state, SearchNode parent, double cost, double lowerBound, int depth)
        {
            State = state;
            Parent = parent;
            Cost = cost;
            LowerBound = lowerBound;
            Depth = depth;
        }
    }
}
