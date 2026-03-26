namespace _8_Reinas.Interfaces;
using _8_Reinas.Domain;

public interface ISolutionRepository
{
    void saveSolution(int boardSize ,List<QueensSolution> solutions);
    List<QueensSolution> getSolutions(int boardSize);
    bool hasSolutions(int boardSize);
}
