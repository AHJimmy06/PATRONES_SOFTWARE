namespace MissionaryAndCannibals.Application;

using MissionaryAndCannibals.Domain;

public class MissionaryNode : IGNode<RiverState>
{
    public RiverState Data { get; }
    public IGNode<RiverState>? Parent { get; }
    private readonly HashSet<RiverState> _visited;
    private List<IGNode<RiverState>>? _children;
    private MissionaryNode? _nextSibling;

    public MissionaryNode(RiverState state, IGNode<RiverState>? parent, HashSet<RiverState> visited)
    {
        Data = state;
        Parent = parent;
        _visited = visited;
    }

    public IGNode<RiverState>? FirstChild()
    {
        if (_children == null) GenerateChildren();
        return _children!.Count > 0 ? _children[0] : null;
    }

    public IGNode<RiverState>? NextSibling() => _nextSibling;

    internal void SetNextSibling(MissionaryNode next) => _nextSibling = next;

    private void GenerateChildren()
    {
        _children = new List<IGNode<RiverState>>();
        
        // Potential moves: (m, c)
        int[][] moves = { 
            new[] { 1, 0 }, 
            new[] { 2, 0 }, 
            new[] { 0, 1 }, 
            new[] { 0, 2 }, 
            new[] { 1, 1 } 
        };

        foreach (var move in moves)
        {
            int mMove = move[0];
            int cMove = move[1];

            int nextM = Data.BoatIsLeft ? Data.MissionariesLeft - mMove : Data.MissionariesLeft + mMove;
            int nextC = Data.BoatIsLeft ? Data.CannibalsLeft - cMove : Data.CannibalsLeft + cMove;

            RiverState nextState = new RiverState(nextM, nextC, !Data.BoatIsLeft);

            if (nextState.IsValid() && !_visited.Contains(nextState))
            {
                _visited.Add(nextState);
                var child = new MissionaryNode(nextState, this, _visited);
                _children.Add(child);
            }
        }

        // Link siblings
        for (int i = 0; i < _children.Count - 1; i++)
        {
            ((MissionaryNode)_children[i]).SetNextSibling((MissionaryNode)_children[i + 1]);
        }
    }
}
