namespace MissionaryAndCannibals.Infrastructure;

using MissionaryAndCannibals.Domain;

public class ConsolePrinter
{
    public void PrintSolution(List<RiverState>? path)
    {
        if (path == null)
        {
            Console.WriteLine("No se encontró solución.");
            return;
        }

        Console.WriteLine("Solución encontrada:");
        Console.WriteLine("==================================================");
        Console.WriteLine("Paso | Orilla Izquierda (M,C) | Bote | Orilla Derecha (M,C)");
        Console.WriteLine("==================================================");

        for (int i = 0; i < path.Count; i++)
        {
            var state = path[i];
            string boatStr = state.BoatIsLeft ? "<<<" : ">>>";
            Console.WriteLine($"{i,4} | ({state.MissionariesLeft}M, {state.CannibalsLeft}C)        | {boatStr}  | ({state.MissionariesRight}M, {state.CannibalsRight}C)");
        }
        Console.WriteLine("==================================================");
        Console.WriteLine("¡Todos han cruzado con éxito!");
    }
}
