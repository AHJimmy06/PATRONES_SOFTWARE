namespace TicTacToeInteligente.Domain;

public class TicTacToeNode : IGNode<TicTacToeState>
{
    private readonly TicTacToeState _state;
    private readonly IGNode<TicTacToeState>? _parent;
    private readonly int _moveIndex; 
    private readonly List<int> _availableMoves;

    public TicTacToeNode(TicTacToeState state, IGNode<TicTacToeState>? parent = null, int moveIndex = -1)
    {
        _state = state;
        _parent = parent;
        _moveIndex = moveIndex;
        _availableMoves = _state.GetAvailableMoves();
    }

    public IGNode<TicTacToeState>? FirstChild()
    {
        if (_state.IsTerminal()) return null;
        if (_availableMoves.Count == 0) return null;

        int move = _availableMoves[0];
        TicTacToeState nextState = _state.MakeMove(move);
        return new TicTacToeNode(nextState, this, 0);
    }

    public IGNode<TicTacToeState>? NextSibling()
    {
        if (_parent == null) return null;
        
        int nextMoveIndex = _moveIndex + 1;
        var parentState = _parent.State;
        var parentMoves = parentState.GetAvailableMoves();

        if (nextMoveIndex < parentMoves.Count)
        {
            int nextMove = parentMoves[nextMoveIndex];
            TicTacToeState nextState = parentState.MakeMove(nextMove);
            return new TicTacToeNode(nextState, _parent, nextMoveIndex);
        }

        return null;
    }

    public IGNode<TicTacToeState>? Parent => _parent;

    public TicTacToeState State => _state;

    // Helper to get the move that led to this node
    public int? AppliedMove {
        get {
            if (_parent == null || _moveIndex == -1) return null;
            return _parent.State.GetAvailableMoves()[_moveIndex];
        }
    }
}
