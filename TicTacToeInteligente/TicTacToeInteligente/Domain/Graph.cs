namespace TicTacToeInteligente.Domain;

public class Graph<T>
{
    public List<IGNode<T>> GetChildren(IGNode<T> node)
    {
        var children = new List<IGNode<T>>();
        var child = node.FirstChild();
        while (child != null)
        {
            children.Add(child);
            child = child.NextSibling();
        }
        return children;
    }
}
