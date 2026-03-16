using System;
using System.Collections.Generic;
using ProblemaDelViajero.Application;

namespace ProblemaDelViajero
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] cities = { "Quito", "Guayaquil", "Cuenca", "Ambato", "Manta", "Loja" };
            
            double[,] distances = {
                { 0, 420, 440, 130, 380, 650 },   // Quito
                { 420, 0, 190, 280, 190, 400 },   // Guayaquil
                { 440, 190, 0, 310, 350, 210 },   // Cuenca
                { 130, 280, 310, 0, 300, 520 },   // Ambato
                { 380, 190, 350, 300, 0, 550 },   // Manta
                { 650, 400, 210, 520, 550, 0 }    // Loja
            };

            Console.WriteLine("=== Problema del Viajero (Branch and Bound) ===");
            Console.WriteLine($"Ciudades a visitar: {string.Join(", ", cities)}");

            var solver = new BranchAndBoundSolver(cities, distances);
            var result = solver.Solve();

            Console.WriteLine("\nRESULTADO ENCONTRADO:");
            Console.WriteLine($"Ruta óptima: {string.Join(" -> ", result.path)}");
            Console.WriteLine($"Distancia total: {result.distance} km");
        }
    }
}
