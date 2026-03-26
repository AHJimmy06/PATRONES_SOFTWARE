namespace MissionaryAndCannibals.Application;

using MissionaryAndCannibals.Domain;

public class Graph<T>
{
    private readonly IGNode<T> _root;

    public Graph(IGNode<T> root)
    {
        _root = root;
    }

    public IEnumerable<IGNode<T>> BreadthFirst()
    {
        Queue<IGNode<T>> queue = new Queue<IGNode<T>>();
        queue.Enqueue(_root);

        while (queue.Count > 0)
        {
            IGNode<T> current = queue.Dequeue();
            yield return current;

            IGNode<T>? child = current.FirstChild();
            while (child != null)
            {
                queue.Enqueue(child);
                child = child.NextSibling();
            }
        }
    }
}
