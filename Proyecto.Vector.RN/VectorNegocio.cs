using Proyecto.Vector.Datos;
using System;
using System.Linq;


namespace Proyecto.Vector.RN
{
    public class VectorNegocio
    {
        // Llenar vector con los elementos dados
        public void Llenar(VectorDatos vector, int[] elementos)
        {
            if (elementos.Length != vector.Elementos.Length)
                throw new ArgumentException("La cantidad de elementos no coincide con el tamaño del vector.");

            for (int i = 0; i < elementos.Length; i++)
                vector.Elementos[i] = elementos[i];
        }
        // Llenar vector con los elementos dados
        public string Mostrar(VectorDatos vector)
        {
            return string.Join(", ", vector.Elementos);
        }

        // CA1: Calcular promedio
        public double CalcularPromedio(VectorDatos vector)
        {
            if (vector.Elementos.Length == 0)
                throw new InvalidOperationException("El vector está vacío.");

            return vector.Elementos.Average();
        }

        // CA2: Calcular desviación estándar muestral
        public double CalcularDesviacion(VectorDatos vector)
        {
            if (vector.Elementos.Length < 2)
                throw new InvalidOperationException("Se necesitan al menos 2 elementos para calcular la desviación.");

            double media = CalcularPromedio(vector);
            double sumaCuadrados = vector.Elementos.Sum(x => Math.Pow(x - media, 2));
            return Math.Sqrt(sumaCuadrados / (vector.Elementos.Length - 1));
        }

        // CA3: Valor máximo con posiciones
        public (int valorMax, int[] posiciones) CalcularMaximo(VectorDatos vector)
        {
            if (vector.Elementos.Length == 0)
                throw new InvalidOperationException("El vector está vacío.");

            int max = vector.Elementos.Max();
            int[] posiciones = vector.Elementos
                                     .Select((valor, indice) => new { valor, indice })
                                     .Where(x => x.valor == max)
                                     .Select(x => x.indice)
                                     .ToArray();
            return (max, posiciones);
        }

        // CA4: Valor mínimo con posiciones
        public (int valorMin, int[] posiciones) CalcularMinimo(VectorDatos vector)
        {
            if (vector.Elementos.Length == 0)
                throw new InvalidOperationException("El vector está vacío.");

            int min = vector.Elementos.Min();
            int[] posiciones = vector.Elementos
                                     .Select((valor, indice) => new { valor, indice })
                                     .Where(x => x.valor == min)
                                     .Select(x => x.indice)
                                     .ToArray();
            return (min, posiciones);
        }
    }
}