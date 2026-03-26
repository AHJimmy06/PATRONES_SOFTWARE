using MissionaryAndCannibals.Application;
using MissionaryAndCannibals.Infrastructure;

namespace MissionaryAndCannibals;

internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Problema de los Misioneros y Caníbales (3 y 3)");
        Console.WriteLine("Patrones de búsqueda de Fred Mellender");
        Console.WriteLine("----------------------------------------------");

        var solver = new SolverService();
        var printer = new ConsolePrinter();

        var solution = solver.Solve();
        printer.PrintSolution(solution);
        
        Console.WriteLine("\nPresione cualquier tecla para salir...");
        Console.ReadKey();
    }
}
