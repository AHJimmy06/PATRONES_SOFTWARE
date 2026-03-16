namespace BusquedaRutaMapa.Domain
{
    /// <summary>
    /// Representa el ESTADO en el mapa (Fred Mellender).
    /// </summary>
    public class MapState
    {
        public string CityName { get; }

        public MapState(string cityName)
        {
            CityName = cityName;
        }

        public override bool Equals(object obj) => obj is MapState other && CityName == other.CityName;
        public override int GetHashCode() => CityName.GetHashCode();
        public override string ToString() => CityName;
    }
}
