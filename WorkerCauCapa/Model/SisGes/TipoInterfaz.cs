using System;
using System.Collections.Generic;

#nullable disable

namespace WorkerCauCapa.Model.SisGes
{
    public partial class TipoInterfaz
    {
        public TipoInterfaz()
        {
            Interfaces = new HashSet<Interface>();
        }

        public int IdTipo { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<Interface> Interfaces { get; set; }
    }
}
