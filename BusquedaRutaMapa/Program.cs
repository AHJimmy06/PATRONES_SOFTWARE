using System;
using System.Collections.Generic;
using BusquedaRutaMapa.Application;

namespace BusquedaRutaMapa
{
    class Program
    {
        static void Main(string[] args)
        {
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

            var searcher = new DijkstraSearcher(graph);
            var path = searcher.FindShortestPath(origin, destination, out double distance);

            if (path != null)
            {
                Console.WriteLine($"Origen: {origin} -> Destino: {destination}");
                Console.WriteLine("RUTA: " + string.Join(" -> ", path));
                Console.WriteLine($"Distancia: {distance} km");
            }
        }
    }
}
