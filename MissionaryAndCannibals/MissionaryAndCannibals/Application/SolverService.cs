namespace MissionaryAndCannibals.Application;

using MissionaryAndCannibals.Domain;

public class SolverService
{
    public List<RiverState>? Solve()
    {
        var startState = new RiverState(3, 3, true);
        var visited = new HashSet<RiverState> { startState };
        var root = new MissionaryNode(startState, null, visited);
        var graph = new Graph<RiverState>(root);

        foreach (var node in graph.BreadthFirst())
        {
            if (node.Data.IsGoal())
            {
                return ReconstructPath(node);
            }
        }

        return null;
    }

    private List<RiverState> ReconstructPath(IGNode<RiverState> goalNode)
    {
        var path = new List<RiverState>();
        IGNode<RiverState>? current = goalNode;
        while (current != null)
        {
            path.Insert(0, current.Data);
            current = current.Parent;
        }
        return path;
    }
}
