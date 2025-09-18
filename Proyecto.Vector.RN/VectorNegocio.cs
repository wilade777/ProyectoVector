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

        // Retorna el vector como string
        public string Mostrar(VectorDatos vector)
        {
            return string.Join(", ", vector.Elementos);
        }
    }
}