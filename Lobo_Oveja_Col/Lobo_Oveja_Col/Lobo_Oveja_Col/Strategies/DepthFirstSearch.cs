using Lobo_Oveja_Col.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lobo_Oveja_Col.Strategies;

public class DepthFirstSearch : ISearchStrategy
{
    public List<RiverState> FindPath(RiverState initialState)
    {
        var stack = new Stack<List<RiverState>>();
        var visited = new HashSet<RiverState>();

        stack.Push(new List<RiverState> { initialState });

        while (stack.Count > 0)
        {
            var currentPath = stack.Pop();
            var currentState = currentPath.Last();

            if (currentState.IsGoal())
            {
                return currentPath;
            }

            if (!visited.Contains(currentState))
            {
                visited.Add(currentState);

                foreach (var nextState in currentState.GetValidNextStates())
                {
                    if (!visited.Contains(nextState))
                    {
                        var newPath = new List<RiverState>(currentPath) { nextState };
                        stack.Push(newPath);
                    }
                }
            }
        }

        return new List<RiverState>();
    }
}
