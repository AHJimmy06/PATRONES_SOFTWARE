using Lobo_Oveja_Col.Domain;

namespace Lobo_Oveja_Col.Strategies;

public class BreadthFirstSearch : ISearchStrategy
{
    public List<RiverState> FindPath(RiverState initialState)
    {
        var queue = new Queue<List<RiverState>>();
        var visited = new HashSet<RiverState>();

        queue.Enqueue(new List<RiverState> { initialState });
        visited.Add(initialState);

        while (queue.Count > 0)
        {
            var currentPath = queue.Dequeue();
            var currentState = currentPath.Last();

            if (currentState.IsGoal())
            {
                return currentPath;
            }

            foreach (var nextState in currentState.GetValidNextStates())
            {
                if (!visited.Contains(nextState))
                {
                    visited.Add(nextState);
                    var newPath = new List<RiverState>(currentPath) { nextState };
                    queue.Enqueue(newPath);
                }
            }
        }

        return new List<RiverState>();
    }
}

