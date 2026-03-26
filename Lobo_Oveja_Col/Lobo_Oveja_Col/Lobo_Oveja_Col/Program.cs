using Lobo_Oveja_Col.Domain;
using Lobo_Oveja_Col.Strategies;

namespace Lobo_Oveja_Col;

internal class Program
{
    static void Main(string[] args)
    {
        // Estado inicial: Todos en la orilla izquierda (False)
        var initialState = new RiverState(false, false, false, false);

        // --- APLICACIÓN DEL PATRÓN STRATEGY ---
        // Aquí puedes cambiar fácilmente entre BreadthFirstSearch() y DepthFirstSearch()
        ISearchStrategy searchAlgorithm = new BreadthFirstSearch();

        Console.WriteLine($"\nResolviendo el problema usando: {searchAlgorithm.GetType().Name}...\n");
        var solutionPath = searchAlgorithm.FindPath(initialState);

        if (solutionPath.Any())
        {
            Console.WriteLine($"¡Solución encontrada en {solutionPath.Count - 1} movimientos!\n");
            Console.WriteLine("Leyenda: G=Granjero, L=Lobo, C=Cabra, V=Col\n");

            for (int i = 0; i < solutionPath.Count; i++)
            {
                Console.WriteLine($"Paso {i:D2}: {solutionPath[i]}");
            }
        }
        else
        {
            Console.WriteLine("No se encontró solución.");
        }
    }
}
