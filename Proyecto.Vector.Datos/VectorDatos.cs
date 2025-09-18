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
            Elementos = new int[tamaño];
        }

        // NUEVO: Constructor que acepta array directamente
        public VectorDatos(int[] elementos)
        {
            Elementos = elementos;
        }
    }
}