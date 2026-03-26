using TicTacToeInteligente.Domain;

namespace TicTacToeInteligente.Aplication;

public class MinimaxEngine
{
    private readonly Graph<TicTacToeState> _graph;

    public MinimaxEngine()
    {
        _graph = new Graph<TicTacToeState>();
    }

    public int GetBestMove(TicTacToeState state)
    {
        var root = new TicTacToeNode(state);
        var children = _graph.GetChildren(root);

        int bestValue = int.MinValue;
        int bestMove = -1;

        foreach (var child in children)
        {
            int value = Minimax((TicTacToeNode)child, 0, false, int.MinValue, int.MaxValue);
            if (value > bestValue)
            {
                bestValue = value;
                bestMove = ((TicTacToeNode)child).AppliedMove ?? -1;
            }
        }

        return bestMove;
    }

    private int Minimax(TicTacToeNode node, int depth, bool isMaximizing, int alpha, int beta)
    {
        var state = node.State;
        
        if (state.IsTerminal())
        {
            Player winner = state.GetWinner();
            if (winner == Player.O) return 10 - depth; 
            if (winner == Player.X) return depth - 10; 
            return 0; 
        }

        var children = _graph.GetChildren(node);

        if (isMaximizing)
        {
            int maxEval = int.MinValue;
            foreach (var child in children)
            {
                int eval = Minimax((TicTacToeNode)child, depth + 1, false, alpha, beta);
                maxEval = Math.Max(maxEval, eval);
                alpha = Math.Max(alpha, eval);
                if (beta <= alpha) break;
            }
            return maxEval;
        }
        else
        {
            int minEval = int.MaxValue;
            foreach (var child in children)
            {
                int eval = Minimax((TicTacToeNode)child, depth + 1, true, alpha, beta);
                minEval = Math.Min(minEval, eval);
                beta = Math.Min(beta, eval);
                if (beta <= alpha) break;
            }
            return minEval;
        }
    }
}
