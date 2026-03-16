using System.Collections.Generic;

namespace ProblemaDelViajero.Domain
{
    public interface ISearchNode
    {
        IEnumerable<ISearchNode> GetSuccessors();
        bool IsGoal();
        double Cost { get; }
        double LowerBound { get; }
        ISearchNode Parent { get; }
    }
}
