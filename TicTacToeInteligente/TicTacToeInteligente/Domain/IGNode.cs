namespace TicTacToeInteligente.Domain;

public interface IGNode<T>
{
    IGNode<T>? FirstChild();
    IGNode<T>? NextSibling();
    IGNode<T>? Parent { get; }
    T State { get; }
}
