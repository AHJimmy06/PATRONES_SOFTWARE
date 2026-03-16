using System.Collections.Generic;
using System.Linq;
using BusquedaRutaMapa.Domain;

namespace BusquedaRutaMapa.Application
{
    /// <summary>
    /// Implementación de Dijkstra como una estrategia de búsqueda (Strategy Pattern).
    /// </summary>
    public class DijkstraSearcher : ISearcher
    {
        public ISearchNode Search(ISearchNode startNode, ISearchNode goalNode)
        {
            var openList = new List<ISearchNode> { startNode };
            var closedList = new HashSet<string>();

            while (openList.Count > 0)
            {
                // Seleccionar el nodo con menor G (costo acumulado)
                var current = openList.OrderBy(n => n.G).First();
                openList.Remove(current);

                if (current.IsGoal(goalNode))
                {
                    return current;
                }

                closedList.Add(current.ToString());

                foreach (var successor in current.GetSuccessors())
                {
                    // Usar ToString o una propiedad única para identificar el estado
                    if (closedList.Contains(successor.ToString())) 
                        continue;

                    var existingNode = openList.FirstOrDefault(n => n.ToString() == successor.ToString());

                    if (existingNode == null)
                    {
                        openList.Add(successor);
                    }
                    else if (successor.G < existingNode.G)
                    {
                        // Actualizar si encontramos un camino más corto
                        existingNode.G = successor.G;
                        existingNode.Parent = current;
                    }
                }
            }

            return null;
        }
    }
}
