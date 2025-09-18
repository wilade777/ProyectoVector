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

        // Alexander: Calcular promedio
        public double CalcularPromedio(VectorDatos vector)
        {
            if (vector.Elementos.Length == 0)
                throw new InvalidOperationException("El vector está vacío.");

            return vector.Elementos.Average();
        }

        // Alexander: Calcular desviación estándar muestral
        public double CalcularDesviacion(VectorDatos vector)
        {
            if (vector.Elementos.Length < 2)
                throw new InvalidOperationException("Se necesitan al menos 2 elementos para calcular la desviación.");

            double media = CalcularPromedio(vector);
            double sumaCuadrados = vector.Elementos.Sum(x => Math.Pow(x - media, 2));
            return Math.Sqrt(sumaCuadrados / (vector.Elementos.Length - 1));
        }

        // Jepsent: Valor máximo con posiciones
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

        // Jepsent: Valor mínimo con posiciones
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


        // Jepsent: Verificar si un vector es palíndromo
        public bool EsPalindromo(VectorDatos vector)
        {
            int n = vector.Elementos.Length;

            for (int i = 0; i < n / 2; i++)
            {
                if (vector.Elementos[i] != vector.Elementos[n - 1 - i])
                    return false; // CA2: no es palíndromo
            }

            return true; // CA1 y CA3: es palíndromo
        }



    }
}