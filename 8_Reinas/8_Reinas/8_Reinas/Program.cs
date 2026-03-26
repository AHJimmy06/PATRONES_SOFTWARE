using _8_Reinas.Domain;
using _8_Reinas.Interfaces;
using _8_Reinas.Repository;
using _8_Reinas.Services;

namespace _8_Reinas;

internal class Program
{
    static void Main(string[] args)
    {
        // 1. Configuración de Inyección de Dependencias (Manual)
        IQueenSolver solver = new BacktrackingSolver();
        ISolutionRepository repository = new SolutionRepository();

        Console.WriteLine("=====================================");
        Console.WriteLine("    EL PROBLEMA DE LAS N REINAS      ");
        Console.WriteLine("=====================================\n");

        int n = GetBoardSizeFromUser();
        
        List<QueensSolution> solutions;

        // 2. Lógica usando el Repositorio (Caché en memoria)
        if (repository.hasSolutions(n))
        {
            Console.WriteLine("\n[INFO] Recuperando soluciones desde el Repositorio...");
            solutions = repository.getSolutions(n);
        }
        else
        {
            Console.WriteLine("\n[INFO] Calculando soluciones con el Algoritmo...");
            solutions = solver.Solve(n);

            // Guardamos en el repositorio para futuras consultas
            repository.saveSolution(n, solutions);
        }

        Console.WriteLine($"\n¡Se encontraron {solutions.Count} soluciones posibles para un tablero de {n}x{n}!");

        if (solutions.Count > 0)
        {
            PrintSolutions(solutions);
        }
    }

    private static int GetBoardSizeFromUser()
    {
        int n;
        while (true)
        {
            Console.Write("Ingrese el tamaño del tablero (N) no mayor a 14: ");
            string input = Console.ReadLine();

            if (int.TryParse(input, out n) && n > 0 && n < 15) break;
            Console.WriteLine("Por favor, ingrese un número entero positivo válido.\n");
        }
        return n;
    }

    private static void PrintSolutions(List<QueensSolution> solutions)
    {
        int count = 1;
        foreach (var solution in solutions)
        {
            Console.WriteLine($"\n--- Solución {count} ---");
            int size = solution._size;
            int[] boardPositions = solution._position;

            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    Console.Write(boardPositions[row] == col ? " Q " : " . ");
                }
                Console.WriteLine();
            }
            count++;
        }
    }
}

