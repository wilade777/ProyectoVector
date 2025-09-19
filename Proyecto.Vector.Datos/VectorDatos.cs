using System;

namespace Proyecto.Vector.Datos
{
    public class VectorDatos
    {
        public int[] Elementos { get; set; }

        // Constructor que acepta tamaño
        public VectorDatos(int tamaño)
        {
            if (tamaño < 0)
            {
                throw new ArgumentException("El tamaño del vector no puede ser negativo.", nameof(tamaño));
            }
            Elementos = new int[tamaño];
        }

        // Constructor que acepta array directamente
        public VectorDatos(int[] elementos)
        {
            if (elementos == null)
            {
                throw new ArgumentNullException(nameof(elementos), "El array de elementos no puede ser nulo.");
            }
            Elementos = elementos;
        }
    }
}