using BusquedaRutaMapa.Domain;

namespace BusquedaRutaMapa.Application
{
    public interface ISearcher
    {
        ISearchNode Search(ISearchNode startNode, ISearchNode goalNode);
    }
}
