using TicTacToeInteligente.Domain;
using TicTacToeInteligente.Infraestructure;

namespace TicTacToeInteligente.Aplication;

public class TicTacToeGame
{
    private TicTacToeState _currentState;
    private readonly ConsoleUI _ui;
    private readonly MinimaxEngine _ai;

    public TicTacToeGame()
    {
        _currentState = new TicTacToeState(new Player[9], Player.X); 
        _ui = new ConsoleUI();
        _ai = new MinimaxEngine();
    }

    public void Run()
    {
        while (!_currentState.IsTerminal())
        {
            _ui.DisplayBoard(_currentState);

            int move;
            if (_currentState.CurrentPlayer == Player.X)
            {
                move = _ui.GetHumanMove(_currentState);
            }
            else
            {
                Console.WriteLine("La computadora está pensando...");
                move = _ai.GetBestMove(_currentState);
            }

            _currentState = _currentState.MakeMove(move);
        }

        _ui.DisplayEndGame(_currentState);
    }
}
