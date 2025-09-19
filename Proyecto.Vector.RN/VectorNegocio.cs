using Proyecto.Vector.Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;


namespace Proyecto.Vector.RN
{
    public class VectorNegocio
    {
        // Métodos de utilidad
        public void Llenar(VectorDatos vector, int[] elementos)
        {
            if (elementos.Length != vector.Elementos.Length)
                throw new ArgumentException("La cantidad de elementos no coincide con el tamaño del vector.");

            for (int i = 0; i < elementos.Length; i++)
                vector.Elementos[i] = elementos[i];
        }

        public string Mostrar(VectorDatos vector)
        {
            if (vector == null || vector.Elementos == null || vector.Elementos.Length == 0)
                return "VECTOR VACÍO";

            return string.Join(", ", vector.Elementos);
        }

        // --- Sprint 1: Fundamentos y Operaciones Básicas ---

        // Promedio y Desviación Estándar
        public double CalcularPromedio(VectorDatos vector)
        {
            if (vector.Elementos.Length == 0)
                throw new InvalidOperationException("El vector está vacío.");

            return vector.Elementos.Average();
        }

        public double CalcularDesviacion(VectorDatos vector)
        {
            if (vector.Elementos.Length < 2)
                throw new InvalidOperationException("Se necesitan al menos 2 elementos para calcular la desviación.");

            double media = CalcularPromedio(vector);
            double sumaCuadrados = vector.Elementos.Sum(x => Math.Pow(x - media, 2));
            return Math.Sqrt(sumaCuadrados / (vector.Elementos.Length - 1));
        }

        // Máximo y Mínimo con Posiciones
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

        // Verificar Palíndromo
        public bool EsPalindromo(VectorDatos vector)
        {
            int n = vector.Elementos.Length;
            for (int i = 0; i < n / 2; i++)
            {
                if (vector.Elementos[i] != vector.Elementos[n - 1 - i])
                    return false;
            }
            return true;
        }

        // Suma Acumulativa
        public VectorDatos SumaAcumulativa(VectorDatos vector)
        {
            if (vector.Elementos.Length == 0)
                return new VectorDatos(0);

            VectorDatos resultado = new VectorDatos(vector.Elementos.Length);
            resultado.Elementos[0] = vector.Elementos[0];
            for (int i = 1; i < vector.Elementos.Length; i++)
            {
                resultado.Elementos[i] = resultado.Elementos[i - 1] + vector.Elementos[i];
            }
            return resultado;
        }

        // Frecuencia de Elementos
        public Dictionary<int, int> FrecuenciaElementos(VectorDatos vector)
        {
            if (vector.Elementos.Length == 0)
                return new Dictionary<int, int>();

            var frecuencias = new Dictionary<int, int>();
            foreach (int elemento in vector.Elementos)
            {
                if (frecuencias.ContainsKey(elemento))
                {
                    frecuencias[elemento]++;
                }
                else
                {
                    frecuencias[elemento] = 1;
                }
            }
            return frecuencias;
        }

        // --- sprint 2 Operaciones con Dos Vectores ---

        // Búsqueda de Subvector
        public (bool encontrado, int posicion) BuscarSubvector(VectorDatos vectorA, VectorDatos vectorB)
        {
            // CORRECCIÓN: Validar que los vectores no estén vacíos
            if (vectorA.Elementos.Length == 0 || vectorB.Elementos.Length == 0)
            {
                throw new InvalidOperationException("Los vectores no pueden estar vacíos.");
            }

            if (vectorB.Elementos.Length > vectorA.Elementos.Length)
            {
                return (false, -1); // CA4: B más grande que A, no puede ser subvector
            }

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

        // Producto Escalar
        public int CalcularProductoEscalar(VectorDatos vector1, VectorDatos vector2)
        {
            if (vector1.Elementos.Length != vector2.Elementos.Length)
                throw new ArgumentException("Error: Los vectores deben tener el mismo tamaño.");
            if (vector1.Elementos.Length == 0)
                throw new InvalidOperationException("Los vectores no pueden estar vacíos.");

            int producto = 0;
            for (int i = 0; i < vector1.Elementos.Length; i++)
            {
                producto += vector1.Elementos[i] * vector2.Elementos[i];
            }
            return producto;
        }
        // Intercalar Vectores
        public VectorDatos IntercalarVectores(VectorDatos vec1, VectorDatos vec2)
        {
            int n1 = vec1.Elementos.Length;
            int n2 = vec2.Elementos.Length;
            int n = n1 + n2;

            int[] nuevoVector = new int[n];
            int i = 0, j = 0, k = 0;

            while (i < n1 || j < n2)
            {
                if (i < n1)
                {
                    nuevoVector[k++] = vec1.Elementos[i++];
                }
                if (j < n2)
                {
                    nuevoVector[k++] = vec2.Elementos[j++];
                }
            }
            return new VectorDatos(nuevoVector);
        }

        // Fusión Ordenada
        public VectorDatos FusionOrdenada(VectorDatos vec1, VectorDatos vec2)
        {
            if (vec1.Elementos.Length == 0) return vec2;
            if (vec2.Elementos.Length == 0) return vec1;

            var listaCombinada = new List<int>(vec1.Elementos.Length + vec2.Elementos.Length);
            listaCombinada.AddRange(vec1.Elementos);
            listaCombinada.AddRange(vec2.Elementos);

            var vectorOrdenado = listaCombinada.OrderBy(e => e).ToArray();
            return new VectorDatos(vectorOrdenado);
        }

        // --- Sprint 3: Transformación y Manipulación de Vectores ---

        // Rotación Circular
        public VectorDatos RotacionCircular(VectorDatos vector, int k)
        {
            if (vector.Elementos.Length == 0) return new VectorDatos(0);

            int n = vector.Elementos.Length;
            int[] temp = new int[n];

            // Si k es negativo, se convierte a positivo para la rotación
            k = (k % n + n) % n;

            for (int i = 0; i < n; i++)
            {
                int nuevaPosicion = (i + k) % n;
                temp[nuevaPosicion] = vector.Elementos[i];
            }
            return new VectorDatos(temp);
        }

        // Eliminar Duplicados
        public VectorDatos EliminarDuplicados(VectorDatos vector)
        {
            if (vector.Elementos.Length == 0) return new VectorDatos(0);

            var elementosUnicos = new HashSet<int>(vector.Elementos);
            return new VectorDatos(elementosUnicos.ToArray());
        }

        // Ordenar Pares e Impares
        public VectorDatos OrdenarParesImpares(VectorDatos vector)
        {
            if (vector.Elementos.Length == 0) return new VectorDatos(0);

            var pares = vector.Elementos.Where(e => e % 2 == 0).OrderBy(e => e).ToList();
            var impares = vector.Elementos.Where(e => e % 2 != 0).OrderBy(e => e).ToList();

            pares.AddRange(impares);
            return new VectorDatos(pares.ToArray());
        }

        // Inversión Parcial
        public VectorDatos InversionParcial(VectorDatos vector, int inicio, int fin)
        {
            int n = vector.Elementos.Length;
            if (inicio < 0 || fin >= n || inicio >= fin)
                throw new ArgumentException("Los índices de inicio y fin son inválidos.");

            int[] nuevoVector = (int[])vector.Elementos.Clone();
            while (inicio < fin)
            {
                int temp = nuevoVector[inicio];
                nuevoVector[inicio] = nuevoVector[fin];
                nuevoVector[fin] = temp;
                inicio++;
                fin--;
            }
            return new VectorDatos(nuevoVector);
        }


        public bool EsPrimo(int numero)
        {
            if (numero <= 1) return false;
            if (numero == 2) return true;
            if (numero % 2 == 0) return false;

            for (int i = 3; i <= Math.Sqrt(numero); i += 2)
            {
                if (numero % i == 0) return false;
            }
            return true;
        }

        // Compactación por Condición
        public VectorDatos CompactacionPorCondicion(VectorDatos vector, Func<int, bool> condicion)
        {
            if (vector.Elementos.Length == 0) return new VectorDatos(0);

            var elementosCumplen = vector.Elementos.Where(condicion).ToArray();
            return new VectorDatos(elementosCumplen);
        }

        // --- Sprint 4: Analisis avnazado ---

        // Subvector creciente más largo
        public int SubvectorCrecienteMasLargo(VectorDatos vector)
        {
            if (vector.Elementos.Length == 0) return 0; // CA4: Vector vacío

            if (vector.Elementos.Length == 1) return 1; // Un solo elemento

            int longitudMaxima = 1;
            int longitudActual = 1;

            // CA1: Iterar a través del vector
            for (int i = 1; i < vector.Elementos.Length; i++)
            {
                // Verificar si el elemento actual es mayor que el anterior
                if (vector.Elementos[i] > vector.Elementos[i - 1])
                {
                    longitudActual++;
                }
                else
                {
                    // CA2: Actualizar la longitud máxima si es necesario
                    if (longitudActual > longitudMaxima)
                    {
                        longitudMaxima = longitudActual;
                    }
                    longitudActual = 1; // Reiniciar contador
                }
            }

            // Verificar una última vez por si la secuencia más larga está al final
            if (longitudActual > longitudMaxima)
            {
                longitudMaxima = longitudActual;
            }

            // CA3: Retornar al menos 1 (cada elemento es una secuencia de longitud 1)
            return longitudMaxima;
        }

        // Segundo valor mayor
        public int SegundoValorMayor(VectorDatos vector)
        {
            if (vector.Elementos.Length < 2)
                throw new InvalidOperationException("El vector debe tener al menos dos elementos.");

            int max = int.MinValue;
            int segundoMax = int.MinValue;

            // CA1: Identificar máximo y segundo máximo en una sola pasada
            foreach (int elemento in vector.Elementos)
            {
                if (elemento > max)
                {
                    segundoMax = max;
                    max = elemento;
                }
                else if (elemento > segundoMax && elemento != max)
                {
                    segundoMax = elemento;
                }
            }

            // CA4: Validar si hay suficientes elementos únicos
            if (segundoMax == int.MinValue)
                throw new InvalidOperationException("No hay un segundo valor mayor (todos los elementos son iguales).");

            return segundoMax;
        }

        // Conteo de secuencias iguales
        public int ConteoSecuenciasIguales(VectorDatos vector)
        {
            if (vector.Elementos.Length < 2) return 0; // Necesita al menos 2 elementos para una secuencia

            int conteo = 0;
            bool enSecuencia = false;

            // CA1: Iterar sobre el vector
            for (int i = 1; i < vector.Elementos.Length; i++)
            {
                // CA2: Identificar secuencias de elementos consecutivos idénticos
                if (vector.Elementos[i] == vector.Elementos[i - 1])
                {
                    if (!enSecuencia)
                    {
                        // CA3: Contar cada secuencia (solo una vez por secuencia)
                        conteo++;
                        enSecuencia = true;
                    }
                }
                else
                {
                    enSecuencia = false;
                }
            }

            return conteo;
        }

        // Reemplazo por frecuencias
        public VectorDatos ReemplazoPorFrecuencias(VectorDatos vector)
        {
            if (vector.Elementos.Length == 0) return new VectorDatos(0);

            // CA1: Contar la frecuencia de aparición de cada elemento
            Dictionary<int, int> frecuencias = FrecuenciaElementos(vector);

            // CA2: Crear un nuevo vector del mismo tamaño
            VectorDatos resultado = new VectorDatos(vector.Elementos.Length);

            // CA3: Para cada posición, insertar la frecuencia del elemento correspondiente
            for (int i = 0; i < vector.Elementos.Length; i++)
            {
                resultado.Elementos[i] = frecuencias[vector.Elementos[i]];
            }

            return resultado;
        }

        // Balance positivo/negativo
        public string BalancePositivoNegativo(VectorDatos vector)
        {
            if (vector.Elementos.Length == 0) return "Vector vacío";

            int positivos = 0;
            int negativos = 0;
            int ceros = 0;

            // CA1: Iterar y contar elementos positivos, negativos y ceros
            foreach (int elemento in vector.Elementos)
            {
                if (elemento > 0) positivos++;
                else if (elemento < 0) negativos++;
                else ceros++;
            }

            // CA2: Mostrar conteos
            string resultado = $"Positivos: {positivos}, Negativos: {negativos}, Ceros: {ceros}. ";

            // CA3: Determinar si están balanceados
            if (positivos == negativos)
                resultado += "✅ Los positivos y negativos están balanceados.";
            else
                resultado += "❌ Los positivos y negativos NO están balanceados.";

            return resultado;
        }

        // Mayor diferencia
        public int MayorDiferencia(VectorDatos vector)
        {
            if (vector.Elementos.Length < 2)
                throw new InvalidOperationException("El vector debe tener al menos dos elementos.");

            int minHastaAhora = vector.Elementos[0];
            int diferenciaMaxima = 0;

            // CA1: Encontrar el valor mínimo hasta el momento
            // CA2: Calcular diferencia entre elemento actual y mínimo
            for (int i = 1; i < vector.Elementos.Length; i++)
            {
                if (vector.Elementos[i] < minHastaAhora)
                {
                    minHastaAhora = vector.Elementos[i];
                }
                else
                {
                    int diferencia = vector.Elementos[i] - minHastaAhora;
                    // CA3: Mantener registro de la diferencia máxima
                    if (diferencia > diferenciaMaxima)
                    {
                        diferenciaMaxima = diferencia;
                    }
                }
            }

            return diferenciaMaxima;
        }

    }
}