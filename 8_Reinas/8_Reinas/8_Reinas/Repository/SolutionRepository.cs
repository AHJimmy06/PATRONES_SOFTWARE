using _8_Reinas.Domain;
using _8_Reinas.Interfaces;

namespace _8_Reinas.Repository;

public class SolutionRepository : ISolutionRepository
{
    private readonly Dictionary<int, List<QueensSolution>> _database = new();
    public List<QueensSolution> getSolutions(int boardSize)
    {
        return _database.ContainsKey(boardSize) ? _database[boardSize] : new List<QueensSolution>();
    }

    public bool hasSolutions(int boardSize)
    {
        return _database.ContainsKey(boardSize);
    }

    public void saveSolution(int boardSize, List<QueensSolution> solutions)
    {
        _database[boardSize] = solutions;
    }
}
