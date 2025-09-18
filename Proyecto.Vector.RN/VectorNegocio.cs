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
        // Michael: BuscarSubvector
        public (bool encontrado, int posicion) BuscarSubvector(VectorDatos vectorA, VectorDatos vectorB)
        {
            if (vectorA.Elementos.Length == 0 || vectorB.Elementos.Length == 0)
                throw new InvalidOperationException("Los vectores no pueden estar vacíos.");

            if (vectorB.Elementos.Length > vectorA.Elementos.Length)
                return (false, -1); // CA4: B más grande que A, no puede ser subvector

            // CA2: Buscar primera aparición contigua de B en A
            for (int i = 0; i <= vectorA.Elementos.Length - vectorB.Elementos.Length; i++)
            {
                bool coincide = true;
                for (int j = 0; j < vectorB.Elementos.Length; j++)
                {
                    if (vectorA.Elementos[i + j] != vectorB.Elementos[j])
                    {
                        coincide = false;
                        break;
                    }
                }

                if (coincide)
                    return (true, i); // CA3: Encontrado, retorna true y posición
            }

            return (false, -1); // CA4: No encontrado
        }


        // Michael: CalcularProductoEscalar
        public int CalcularProductoEscalar(VectorDatos vector1, VectorDatos vector2)
        {
            // CA2 y CA5: Validar que tengan el mismo tamaño
            if (vector1.Elementos.Length != vector2.Elementos.Length)
                throw new ArgumentException("Error: Los vectores deben tener el mismo tamaño.");

            if (vector1.Elementos.Length == 0)
                throw new InvalidOperationException("Los vectores no pueden estar vacíos.");

            // CA3: Calcular producto escalar
            int producto = 0;
            for (int i = 0; i < vector1.Elementos.Length; i++)
            {
                producto += vector1.Elementos[i] * vector2.Elementos[i];
            }

            return producto; // CA4: Retorna resultado
        }


    }
}