using System.Collections.Generic;

namespace BusquedaRutaMapa.Domain
{
    public interface ISearchNode
    {
        IEnumerable<ISearchNode> GetSuccessors();
        bool IsGoal(ISearchNode goalNode);
        double G { get; set; }
        ISearchNode Parent { get; set; }
    }
}
