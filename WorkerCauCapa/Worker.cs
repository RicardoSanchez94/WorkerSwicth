using ICSharpCode.SharpZipLib.Lzw;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WorkerCauCapa.Model.Clases;
using WorkerCauCapa.Model.SisGes;

namespace WorkerCauCapa
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private SisgesDBContext db = new SisgesDBContext();
        private EnviodeCorreo Correo = new EnviodeCorreo();

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                // await StarProceso();
                try
                {
                    await IniciodeProceso();
                    _logger.LogInformation("Se finalizo Correctamente");
                }
                catch (Exception ex)
                {

                    _logger.LogInformation("Error en el ExecuteASync", ex);
                }
                finally
                {
                    _logger.LogInformation("Fin del ExecuteAsync ");
                    await Task.Delay(30000000, stoppingToken);
                
                }
             
                
            }
        }

        public async Task<ResponseModel> IniciodeProceso()
        {
            ResponseModel response = new ResponseModel();
            //DateTime Fecha = DateTime.Today.AddDays(-1);
            DateTime Fecha = new DateTime(2023, 04, 22);
            int Mes = Fecha.Month;
            int Yr = Fecha.Year;
            int dia = Fecha.Day;
            _logger.LogInformation("Inicio de proceso con la fecha : " + Fecha.ToString("dd-MM-yyyy"));

            var Sw = Helper.Helper.GetSw();

            
            string cadenaConexion = "Server=" + Sw.servidor + "; Database=" + Sw.bd + ";User Id=" + Sw.usuario + "; Password=" + Sw.password + ";";


            //Instancia para conexión a MySQL, recibe la cadena de conexión
            MySqlConnection conexionBD = new MySqlConnection(cadenaConexion);
            MySqlDataReader reader = null; //Variable para leer el resultado de la consulta
          

            //Agregamos try-catch para capturar posibles errores de conexión o sintaxis.
            try
            {
              
                var consulta = string.Format(Sw.query ,Yr, Mes, dia);


                MySqlCommand comando = new MySqlCommand(consulta); //Declaración SQL para ejecutar contra una base de datos MySQL
                comando.CommandTimeout = 0;
                comando.Connection = conexionBD; //Establece la MySqlConnection utilizada por esta instancia de MySqlCommand
                conexionBD.Open(); //Abre la conexión
              
                reader = comando.ExecuteReader(); //Ejecuta la consulta y crea un MySqlDataReader
                _logger.LogInformation("Apertura Exitosa al Mysql y Ejecucion");
                var dt = new DataTable();
                dt.Columns.Add("fecha_hora", typeof(DateTime));
                dt.Columns.Add("local", typeof(int));
                dt.Columns.Add("caja", typeof(int));
                dt.Columns.Add("nro_trx", typeof(int));
                dt.Columns.Add("secuencia", typeof(int));
                dt.Columns.Add("rut_cliente", typeof(int));
                dt.Columns.Add("pan", typeof(Int64));
                dt.Columns.Add("monto", typeof(Int64));
                dt.Columns.Add("credit_type", typeof(string));
                dt.Columns.Add("iso_tipo_credito", typeof(string));
                dt.Columns.Add("bellbox1", typeof(string));
                dt.Columns.Add("iso2", typeof(string));
                dt.Columns.Add("iso3", typeof(string));
                dt.Columns.Add("bellbox4", typeof(string));
                dt.Columns.Add("req_type", typeof(string));
                dt.Columns.Add("iso_reverse1", typeof(string));
                dt.Columns.Add("iso_reverse2", typeof(string));
                dt.Columns.Add("estado", typeof(string));
                dt.Columns.Add("time_stamp", typeof(DateTime));
                dt.Columns.Add("cuenta", typeof(int));
                dt.Columns.Add("numero_cuotas", typeof(int));
                dt.Columns.Add("valor_cuota", typeof(int));
                dt.Columns.Add("monto_credito", typeof(int));
                dt.Columns.Add("tasa_interes", typeof(decimal));
                dt.Columns.Add("terminal_id", typeof(string));
                dt.Columns.Add("pie_pago_minimo", typeof(int));
                dt.Columns.Add("autorizacion_fp", typeof(string));
                dt.Columns.Add("impuesto", typeof(int));
                dt.Columns.Add("comision", typeof(int));
                dt.Columns.Add("fecha_primer_venc", typeof(DateTime));
                dt.Columns.Add("pago_min_eecc", typeof(int));
                dt.Columns.Add("monto_redondeado", typeof(int));
                dt.Columns.Add("monto_recaudado", typeof(int));
                dt.Columns.Add("ajuste_ley", typeof(int));
                dt.Columns.Add("cod_autorizacion", typeof(string));
                dt.Columns.Add("pago_cod_medio_pago", typeof(string));
                dt.Columns.Add("rut_cajero", typeof(string));
                dt.Columns.Add("payment_id", typeof(int));

                dt.Load(reader);
                //var x = JsonConvert.SerializeObject(dt);

                //List<RegistroTrx> trxs = JsonConvert.DeserializeObject<List<RegistroTrx>>(x);

                //var repetidos = trxs.Where(x => x.Local == 83 && x.Caja == 57 && x.NroTrx == 8618).SingleOrDefault();
                //var repetidosfecha = trxs.Where(x => x.FechaHora == "28-03-2023")
                //              .SingleOrDefault();

                //dt = JsonConvert.DeserializeObject<DataTable>(x);

                //DateTime fechaBuscada = new DateTime(2023, 3, 28); // fecha buscada
                //foreach (DataRow row in dt.Rows)
                //{
                //    DateTime fechaHora = Convert.ToDateTime(row["fecha_hora"]);
                //    if (fechaHora.Date == fechaBuscada.Date) // comparar solo la fecha sin la hora
                //    {
                //        // hacer algo con la fila que coincide con la fecha buscada
                //        Console.WriteLine($"Fecha y hora encontrada: {fechaHora}, monto: {row["monto"]}");
                //    }
                //}

                conexionBD.Close();
                var con = Helper.Helper.ConexionStringModel();
                using (SqlConnection sql = new SqlConnection(con))
                {
                    sql.Open();
                    _logger.LogInformation("Apertura de Conexion BD SisGes");

                    using (SqlCommand cmd = new SqlCommand("spInserfP_registro_trxs", sql))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.CommandTimeout = 0;

                        cmd.Parameters.AddWithValue("@TAU", dt);
                        //cmd.Parameters.AddWithValue("@TAU", null);
                        cmd.Parameters.AddWithValue("@Fecha", Fecha);

                        //parametros.SqlDbType = SqlDbType.Structured;
                        var reader1 = cmd.ExecuteNonQuery();
                        _logger.LogInformation("Se inserto correctamente");
                    }
                    sql.Close();
                }

                response.error = false;
                Correo.ErrorApiTransbank(response);
                response.respuesta = ("Se insertaron correctamentos los datos a la tabla fp_registro_trxs con Fecha :" + Fecha);
                //Correo.ErrorApiTransbank(response);
                return response;
            }
            catch (Exception ex)
            {
                response.error = true;
                response.respuesta = "Error al insertar los datos a la tabla fp_registro_trxs" + ex.Message;
                Correo.ErrorApiTransbank(response);
                return response;
            }
            finally
            {

                conexionBD.Close(); //Cierra la conexión a MySQL
            }

            _logger.LogInformation("--Inicio de Carga CAAU y CAPA--");


            return response;
        }

      
    }
}
