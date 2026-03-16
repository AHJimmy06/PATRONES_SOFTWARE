using System;
using System.Collections.Generic;
using System.Linq;
using ProblemaDelViajero.Domain;
using ProblemaDelViajero.Infrastructure;

namespace ProblemaDelViajero.Application
{
    /// <summary>
    /// El MOTOR de búsqueda (Searcher).
    /// Implementa Branch and Bound, aplicando poda basada en el límite inferior.
    /// </summary>
    public class BranchAndBoundSolver
    {
        private readonly double[,] _distances;
        private readonly int _numCities;
        private readonly string[] _cityNames;

        public BranchAndBoundSolver(string[] names, double[,] distances)
        {
            _cityNames = names;
            _distances = distances;
            _numCities = names.Length;
        }

        public (List<string> path, double distance) Solve()
        {
            var startState = new TspState(0);
            var root = new SearchNode(startState, null, 0, CalculateLowerBound(startState), 0);

            var pq = new List<SearchNode> { root };
            double bestDistance = double.PositiveInfinity;
            List<int> bestPath = null;

            while (pq.Count > 0)
            {
                pq = pq.OrderBy(n => n.LowerBound).ToList();
                var current = pq[0];
                pq.RemoveAt(0);

                if (current.LowerBound >= bestDistance) continue;

                if (current.State.IsComplete(_numCities))
                {
                    double totalDist = current.Cost + _distances[current.State.CurrentCity, 0];
                    if (totalDist < bestDistance)
                    {
                        bestDistance = totalDist;
                        bestPath = new List<int>(current.State.Path) { 0 };
                    }
                    continue;
                }

                for (int i = 0; i < _numCities; i++)
                {
                    if (!current.State.Path.Contains(i))
                    {
                        var nextState = (TspState)current.State.Clone();
                        double travelCost = _distances[current.State.CurrentCity, i];
                        nextState.Path.Add(i);
                        nextState.CurrentDistance += travelCost;

                        double bound = CalculateLowerBound(nextState);
                        
                        if (bound < bestDistance)
                        {
                            pq.Add(new SearchNode(nextState, current, nextState.CurrentDistance, bound, current.Depth + 1));
                        }
                    }
                }
            }

            return (bestPath.Select(i => _cityNames[i]).ToList(), bestDistance);
        }

        private double CalculateLowerBound(TspState state)
        {
            double bound = state.CurrentDistance;
            bool[] visited = new bool[_numCities];
            foreach (var city in state.Path) visited[city] = true;

            for (int i = 0; i < _numCities; i++)
            {
                if (!visited[i] || i == state.CurrentCity)
                {
                    double minEdge = double.MaxValue;
                    for (int j = 0; j < _numCities; j++)
                    {
                        if (i != j && _distances[i, j] < minEdge) minEdge = _distances[i, j];
                    }
                    bound += minEdge;
                }
            }
            return bound;
        }
    }
}
