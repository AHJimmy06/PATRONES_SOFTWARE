using System;
using System.Collections.Generic;
using System.Linq;
using ProblemaDelViajero.Application;
using ProblemaDelViajero.Domain;

namespace ProblemaDelViajero
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] cities = { "Quito", "Guayaquil", "Cuenca", "Manta" };
            double[,] distances = {
                { 0, 420, 450, 400 },
                { 420, 0, 190, 190 },
                { 450, 190, 0, 430 },
                { 400, 190, 430, 0 }
            };

            Console.WriteLine("=== Problema del Viajero (Branch and Bound) ===");

            // 1. Definir estado inicial (Ciudad 0)
            var startState = new TspState(0);
            
            // 2. Crear nodo raíz de búsqueda
            var rootNode = new SearchNode(startState, null, 0, 0, 0, distances, cities.Length);

            // 3. Configurar y ejecutar el Solver (Searcher)
            var solver = new BranchAndBoundSolver(distances, cities.Length);
            var resultNode = solver.Solve(rootNode);

            if (resultNode != null)
            {
                var finalState = ((SearchNode)resultNode).State;
                var pathNames = finalState.Path.Select(i => cities[i]).ToList();
                pathNames.Add(cities[0]); // Volver al origen

                double totalDistance = resultNode.Cost + distances[finalState.CurrentCity, 0];

                Console.WriteLine("Mejor Ruta Encontrada:");
                Console.WriteLine(string.Join(" -> ", pathNames));
                Console.WriteLine($"Distancia Total: {totalDistance} km");
            }
            else
            {
                Console.WriteLine("No se encontró solución.");
            }
        }
    }
}
