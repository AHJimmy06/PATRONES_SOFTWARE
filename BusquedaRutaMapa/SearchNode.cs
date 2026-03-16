namespace BusquedaRutaMapa.Infrastructure
{
    using Domain;

    /// <summary>
    /// Nodo de búsqueda para Dijkstra según Fred Mellender.
    /// </summary>
    public class SearchNode
    {
        public MapState State { get; }
        public SearchNode Parent { get; }
        public double G { get; set; } // Costo acumulado real
        public int Depth { get; }

        public SearchNode(MapState state, SearchNode parent, double g, int depth)
        {
            State = state;
            Parent = parent;
            G = g;
            Depth = depth;
        }
    }
}
