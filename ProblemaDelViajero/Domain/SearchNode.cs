using System.Collections.Generic;
using System.Linq;

namespace ProblemaDelViajero.Domain
{
    /// <summary>
    /// Nodo de búsqueda para el Problema del Viajero.
    /// Implementa ISearchNode y encapsula la lógica de sucesores y poda (LowerBound).
    /// </summary>
    public class SearchNode : ISearchNode
    {
        public TspState State { get; }
        public ISearchNode Parent { get; }
        public double Cost { get; }       // g(n) - costo real acumulado
        public double LowerBound { get; } // f(n) - cota inferior estimada
        public int Depth { get; }

        private readonly double[,] _distances;
        private readonly int _numCities;

        public SearchNode(TspState state, ISearchNode parent, double cost, double lowerBound, int depth, double[,] distances, int numCities)
        {
            State = state;
            Parent = parent;
            Cost = cost;
            LowerBound = lowerBound;
            Depth = depth;
            _distances = distances;
            _numCities = numCities;
        }

        public IEnumerable<ISearchNode> GetSuccessors()
        {
            if (IsGoal()) yield break;

            for (int i = 0; i < _numCities; i++)
            {
                if (!State.Path.Contains(i))
                {
                    var nextState = (TspState)State.Clone();
                    double travelCost = _distances[State.CurrentCity, i];
                    nextState.Path.Add(i);
                    nextState.CurrentDistance += travelCost;

                    double bound = CalculateLowerBound(nextState);
                    yield return new SearchNode(nextState, this, nextState.CurrentDistance, bound, Depth + 1, _distances, _numCities);
                }
            }
        }

        public bool IsGoal() => State.IsComplete(_numCities);

        private double CalculateLowerBound(TspState state)
        {
            double bound = state.CurrentDistance;
            bool[] visited = new bool[_numCities];
            foreach (var city in state.Path) visited[city] = true;

            // Para cada ciudad no visitada (o la actual para volver al inicio), 
            // sumar el costo de la arista mínima saliente.
            for (int i = 0; i < _numCities; i++)
            {
                if (!visited[i] || i == state.CurrentCity)
                {
                    double minEdge = double.MaxValue;
                    for (int j = 0; j < _numCities; j++)
                    {
                        if (i != j && _distances[i, j] < minEdge) 
                            minEdge = _distances[i, j];
                    }
                    if (minEdge != double.MaxValue) 
                        bound += minEdge;
                }
            }
            return bound;
        }
    }
}
