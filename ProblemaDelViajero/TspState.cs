using System;
using System.Collections.Generic;
using System.Linq;

namespace ProblemaDelViajero.Domain
{
    /// <summary>
    /// Representa el ESTADO del problema según Fred Mellender.
    /// Encapsula la configuración actual del recorrido.
    /// </summary>
    public class TspState : ICloneable
    {
        public List<int> Path { get; private set; }
        public double CurrentDistance { get; set; }
        public int CurrentCity => Path.Last();

        public TspState(int startCity)
        {
            Path = new List<int> { startCity };
            CurrentDistance = 0;
        }

        private TspState(List<int> path, double distance)
        {
            Path = path;
            CurrentDistance = distance;
        }

        public object Clone()
        {
            return new TspState(new List<int>(this.Path), this.CurrentDistance);
        }

        public bool IsComplete(int totalCities) => Path.Count == totalCities;

        public override string ToString() => string.Join(" -> ", Path);
    }
}
