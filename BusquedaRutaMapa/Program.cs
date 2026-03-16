using System;
using System.Collections.Generic;
using BusquedaRutaMapa.Application;
using BusquedaRutaMapa.Domain;

namespace BusquedaRutaMapa
{
    class Program
    {
        static void Main(string[] args)
        {
            // Definición del grafo de distancias
            var graph = new Dictionary<string, List<(string, double)>>
            {
                ["Quito"] = new List<(string, double)> { ("Ambato", 120), ("Santo Domingo", 100) },
                ["Ambato"] = new List<(string, double)> { ("Quito", 120), ("Riobamba", 60), ("Baños", 40) },
                ["Riobamba"] = new List<(string, double)> { ("Ambato", 60), ("Cuenca", 200) },
                ["Cuenca"] = new List<(string, double)> { ("Riobamba", 200), ("Guayaquil", 190) },
                ["Guayaquil"] = new List<(string, double)> { ("Cuenca", 190), ("Manta", 190) },
                ["Santo Domingo"] = new List<(string, double)> { ("Quito", 100), ("Manta", 220) },
                ["Baños"] = new List<(string, double)> { ("Ambato", 40) },
                ["Manta"] = new List<(string, double)> { ("Guayaquil", 190) }
            };

            Console.WriteLine("=== Búsqueda de Ruta más Corta (Dijkstra) ===");
            
            string origin = "Quito";
            string destination = "Cuenca";

            // Crear el contexto de búsqueda (Strategy Pattern)
            ISearcher searcher = new DijkstraSearcher();

            // Configurar los nodos de inicio y fin
            var startNode = new SearchNode(new MapState(origin), null, 0, 0, graph);
            var goalNode = new SearchNode(new MapState(destination), null, 0, 0, null);

            // Ejecutar la búsqueda
            var resultNode = searcher.Search(startNode, goalNode);

            if (resultNode != null)
            {
                Console.WriteLine($"Origen: {origin} -> Destino: {destination}");
                Console.WriteLine("RUTA: " + string.Join(" -> ", ReconstructPath(resultNode)));
                Console.WriteLine($"Distancia: {resultNode.G} km");
            }
            else
            {
                Console.WriteLine("No se encontró una ruta.");
            }
        }

        private static List<string> ReconstructPath(ISearchNode node)
        {
            var path = new List<string>();
            while (node != null)
            {
                path.Add(node.ToString());
                node = node.Parent;
            }
            path.Reverse();
            return path;
        }
    }
}
