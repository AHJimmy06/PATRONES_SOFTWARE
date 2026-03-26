using _8_Reinas.Domain;
using _8_Reinas.Interfaces;

namespace _8_Reinas.Services;

public class BacktrackingSolver : IQueenSolver
{
    public List<QueensSolution> Solve(int n)
    {
        var solutionList = new List<QueensSolution>();
        int [] board = new int[n];

        SolveRecursive(0, n, board, solutionList);

        return solutionList;

    }

    public void SolveRecursive(int row , int n , int[] board ,List<QueensSolution> solutionList) {

        if (row == n) { 
            solutionList.Add(new QueensSolution(n, (int[])board.Clone()));
            return;
        }

        for (int col = 0; col < n; col++) {
            if (IsSafe(row, col, board)) {
                board[row]=col;
                SolveRecursive (row + 1, n, board, solutionList);
            }
        }
    }

    private bool IsSafe(int row, int col, int[] board)
    {
        for (int i = 0; i < row; i++)
        {
            if (board[i] == col)
            {
                return false;
            }

            int rowDifference = Math.Abs(row - i);
            int colDifference = Math.Abs(col - board[i]);
            if (rowDifference == colDifference)
            {
                return false;
            }
        }
        return true;
    }
}
