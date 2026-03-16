using System.Collections.Generic;
using System.Linq;
using ProblemaDelViajero.Domain;

namespace ProblemaDelViajero.Application
{
    /// <summary>
    /// Buscador que implementa Branch and Bound como una estrategia de búsqueda.
    /// Utiliza el patrón Searcher del libro.
    /// </summary>
    public class BranchAndBoundSolver
    {
        private readonly double[,] _distances;
        private readonly int _numCities;

        public BranchAndBoundSolver(double[,] distances, int numCities)
        {
            _distances = distances;
            _numCities = numCities;
        }

        public ISearchNode Solve(ISearchNode rootNode)
        {
            var priorityQueue = new List<ISearchNode> { rootNode };
            double bestDistance = double.PositiveInfinity;
            ISearchNode bestLeaf = null;

            while (priorityQueue.Count > 0)
            {
                // Seleccionar el nodo más prometedor (menor LowerBound)
                var current = priorityQueue.OrderBy(n => n.LowerBound).First();
                priorityQueue.Remove(current);

                // Poda: Si el límite inferior es peor que lo mejor encontrado, descartar rama
                if (current.LowerBound >= bestDistance) 
                    continue;

                if (current.IsGoal())
                {
                    // Completar el ciclo volviendo a la ciudad de origen (0)
                    var state = ((SearchNode)current).State;
                    double totalDist = current.Cost + _distances[state.CurrentCity, 0];
                    
                    if (totalDist < bestDistance)
                    {
                        bestDistance = totalDist;
                        bestLeaf = current;
                    }
                    continue;
                }

                // Expandir sucesores
                foreach (var successor in current.GetSuccessors())
                {
                    if (successor.LowerBound < bestDistance)
                    {
                        priorityQueue.Add(successor);
                    }
                }
            }

            return bestLeaf;
        }
    }
}
