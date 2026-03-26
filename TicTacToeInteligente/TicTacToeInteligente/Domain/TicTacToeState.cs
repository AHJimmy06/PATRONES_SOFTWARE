namespace TicTacToeInteligente.Domain;

public enum Player { None, X, O }

public class TicTacToeState
{
    public Player[] Board { get; }
    public Player CurrentPlayer { get; }

    public TicTacToeState(Player[] board, Player currentPlayer)
    {
        Board = (Player[])board.Clone();
        CurrentPlayer = currentPlayer;
    }

    public bool IsTerminal()
    {
        return GetWinner() != Player.None || IsFull();
    }

    public Player GetWinner()
    {
        int[,] winPatterns = {
            {0, 1, 2}, {3, 4, 5}, {6, 7, 8},
            {0, 3, 6}, {1, 4, 7}, {2, 5, 8}, 
            {0, 4, 8}, {2, 4, 6}             
        };

        for (int i = 0; i < winPatterns.GetLength(0); i++)
        {
            if (Board[winPatterns[i, 0]] != Player.None &&
                Board[winPatterns[i, 0]] == Board[winPatterns[i, 1]] &&
                Board[winPatterns[i, 1]] == Board[winPatterns[i, 2]])
            {
                return Board[winPatterns[i, 0]];
            }
        }
        return Player.None;
    }

    public bool IsFull()
    {
        foreach (var cell in Board)
            if (cell == Player.None) return false;
        return true;
    }

    public List<int> GetAvailableMoves()
    {
        var moves = new List<int>();
        for (int i = 0; i < Board.Length; i++)
            if (Board[i] == Player.None) moves.Add(i);
        return moves;
    }

    public TicTacToeState MakeMove(int index)
    {
        var nextBoard = (Player[])Board.Clone();
        nextBoard[index] = CurrentPlayer;
        return new TicTacToeState(nextBoard, CurrentPlayer == Player.X ? Player.O : Player.X);
    }
}
