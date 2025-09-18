using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        // NUEVO: Constructor que acepta array directamente
        public VectorDatos(int[] elementos)
        {
            if (elementos == null)
            {
                throw new ArgumentNullException(nameof(elementos), "El Vector de elementos no puede ser nulo.");
            }
        }
    }
}