using Lobo_Oveja_Col.Domain;

namespace Lobo_Oveja_Col.Strategies;

public interface ISearchStrategy
{
    List<RiverState> FindPath(RiverState initialState);
}
