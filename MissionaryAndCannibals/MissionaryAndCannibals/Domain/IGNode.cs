namespace MissionaryAndCannibals.Domain;

public interface IGNode<T>
{
    T Data { get; }
    IGNode<T>? FirstChild();
    IGNode<T>? NextSibling();
    IGNode<T>? Parent { get; }
}
