using TicTacToeInteligente.Domain;

namespace TicTacToeInteligente.Infraestructure;

public class ConsoleUI
{
    public void DisplayBoard(TicTacToeState state)
    {
        Console.Clear();
        Console.WriteLine(" Tic-Tac-Toe Inteligente (Mellender Patterns)");
        Console.WriteLine(" --------------------------------------------");
        Console.WriteLine($" Turno: {state.CurrentPlayer}");
        Console.WriteLine();

        for (int i = 0; i < 9; i++)
        {
            string cell = state.Board[i] == Player.None ? (i + 1).ToString() : state.Board[i].ToString();
            Console.Write($" {cell} ");
            if ((i + 1) % 3 == 0)
            {
                Console.WriteLine();
                if (i < 6) Console.WriteLine("---+---+---");
            }
            else
            {
                Console.Write("|");
            }
        }
        Console.WriteLine();
    }

    public int GetHumanMove(TicTacToeState state)
    {
        while (true)
        {
            Console.Write("Tu movimiento (1-9): ");
            if (int.TryParse(Console.ReadLine(), out int move) && move >= 1 && move <= 9)
            {
                int index = move - 1;
                if (state.Board[index] == Player.None)
                {
                    return index;
                }
            }
            Console.WriteLine("Movimiento inválido. Intenta de nuevo.");
        }
    }

    public void DisplayEndGame(TicTacToeState state)
    {
        DisplayBoard(state);
        Player winner = state.GetWinner();
        if (winner == Player.None)
            Console.WriteLine("¡Es un empate!");
        else
            Console.WriteLine($"¡El ganador es {winner}!");
    }
}
