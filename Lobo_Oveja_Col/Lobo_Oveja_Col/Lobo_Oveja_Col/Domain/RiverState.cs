namespace Lobo_Oveja_Col.Domain;

public record RiverState(bool Granjero, bool lobo, bool Cabra, bool Col)
{
    // Verifica si el estado actual es seguro
    public bool IsValid()
    {
        // El lobo se come a la cabra si el granjero no está
        if (lobo == Cabra && Granjero != lobo) return false;

        // La cabra se come la col si el granjero no está
        if (Cabra == Col && Granjero != Cabra) return false;

        return true;
    }

    // Verifica si hemos alcanzado la meta (todos cruzaron)
    public bool IsGoal() => Granjero && lobo && Cabra && Col;

    // Genera los siguientes movimientos válidos
    public IEnumerable<RiverState> GetValidNextStates()
    {
        var nextStates = new List<RiverState>();

        // 1. El granjero cruza solo
        nextStates.Add(this with { Granjero = !Granjero });

        // 2. El granjero cruza con el Lobo
        if (Granjero == lobo)
            nextStates.Add(this with { Granjero = !Granjero, lobo = !lobo });

        // 3. El granjero cruza con la Cabra
        if (Granjero == Cabra)
            nextStates.Add(this with { Granjero = !Granjero, Cabra = !Cabra });

        // 4. El granjero cruza con la Col
        if (Granjero == Col)
            nextStates.Add(this with { Granjero = !Granjero, Col = !Col });

        // Retornamos solo los estados que no violan las reglas
        return nextStates.Where(state => state.IsValid());
    }

    // Formato visual para la consola
    public override string ToString()
    {
        string left = $"{(Granjero ? " " : "G")} {(lobo ? " " : "L")} {(Cabra ? " " : "C")} {(Col ? " " : "V")}";
        string right = $"{(Granjero ? "G" : " ")} {(lobo ? "L" : " ")} {(Cabra ? "C" : " ")} {(Col ? "V" : " ")}";
        return $"[{left}] ~~~Río~~~ [{right}]";
    }
}
