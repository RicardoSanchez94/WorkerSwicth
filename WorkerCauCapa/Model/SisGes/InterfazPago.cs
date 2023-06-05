using System;
using System.Collections.Generic;

#nullable disable

namespace WorkerCauCapa.Model.SisGes
{
    public partial class InterfazPago
    {
        public string NroRef { get; set; }
        public Guid? IdInterfaces { get; set; }
        public string CodigoEmpresa { get; set; }
        public DateTime FechaAutorizacion { get; set; }
        public string CodigoAgencia { get; set; }
        public int? NumeroTerminal { get; set; }
        public string CodigoUsuario { get; set; }
        public string Numerorecibo { get; set; }
        public string TipoArchivo { get; set; }
        public string CodigoCliente { get; set; }
        public string Numerocuenta { get; set; }
        public int? Monto { get; set; }
        public int? CodigoMedioPago { get; set; }
        public int? CódigoTransacción { get; set; }
        public int? NroTrxPos { get; set; }
        public int? NroCaja { get; set; }
        public string TipoDiferencia { get; set; }
        public string Estado { get; set; }
        public string NumeroAutorizacion { get; set; }

        public virtual Interface IdInterfacesNavigation { get; set; }
    }
}
