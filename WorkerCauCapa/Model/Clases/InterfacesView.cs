using System;
using System.Collections.Generic;
using System.Text;
using WorkerCauCapa.Model.SisGes;

namespace WorkerCauCapa.Model.Clases
{
    public class InterfacesView
    {
        public Interface OInterfaz { get; set; }

        public List<InterfazAutorizacion> ListaInterfazAuto { get; set; }

        public List<InterfazPago> ListaInterfazPago { get; set; }



        public InterfacesView()
        {

            ListaInterfazAuto = new List<InterfazAutorizacion>();
            ListaInterfazPago = new List<InterfazPago>();
            OInterfaz = new Interface();
        }
    }
}
