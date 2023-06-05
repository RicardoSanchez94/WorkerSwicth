using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using WorkerCauCapa.Model.Clases;

namespace WorkerCauCapa.Helper 
{
    public static class Helper
    {
        public static string ConexionStringModel()
        {

            var builder = new ConfigurationBuilder()
              .SetBasePath(AppContext.BaseDirectory)
              .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfiguration configuration = builder.Build();
            var conexion = configuration.GetSection("ConnectionModelString:StringConexionSisges");
            return conexion.Value;
        }

        public static sFTPCredentials GetCrendicales()
        {
            var model = new sFTPCredentials();
            var builder = new ConfigurationBuilder()
               //.SetBasePath(Directory.GetCurrentDirectory())
               .SetBasePath(AppContext.BaseDirectory)
              .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfiguration configuration = builder.Build();
            model = configuration.GetSection("CredencialesSFTP").Get<sFTPCredentials>();
            return model;
        }

        public static SwCredenciales GetSw()
        {
            var model = new SwCredenciales();
            var builder = new ConfigurationBuilder()
               //.SetBasePath(Directory.GetCurrentDirectory())
               .SetBasePath(AppContext.BaseDirectory)
              .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfiguration configuration = builder.Build();
            model = configuration.GetSection("CredencialesSw").Get<SwCredenciales>();
            return model;
        }



        public static char GetCharFieldWhitPosition(string field)
        {
            char value = '+';
            value = Convert.ToChar(field);
            return value;
        }

        public static int GetIntFieldWhitPosition(string field, int startIndex, int length)
        {
            var aaaa = "";
            try
            {
                string value = "";
                int valueFin = 0;
                char[] MyChar = { '+', '0' };

                value = field.Substring(startIndex, length).Trim().TrimStart(MyChar);
                aaaa = value;
                if (value == "NA" || value == "")
                {
                    value = "0";
                }
                valueFin = Convert.ToInt32(value.Length == 10 ? "0" : value);
                return valueFin;
            }
            catch (Exception ex)
            {
                var ddd = aaaa;
                throw;
            }
        }

        public static string GetStringFieldWhitPosition(string field, int startIndex, int length)
        {
            string value = "";
            char[] MyChar = { '+', '0' };
            value = field.Substring(startIndex, length).Replace(" ", "").Replace("+", "").Trim().TrimStart(MyChar);
            return value;
        }


        //SE USA PARA DATOS QUE VENGAN EN CADENA CON FORMATO DECIMAL EJEM: 23456.00
        // CB02
        public static decimal GetDecimalFieldWhitPosition(string field, int startIndex, int length)
        {
            char[] MyChar = { '0' };
            string decimalValueString = field.Substring(startIndex, length).TrimStart(MyChar).Replace("+", "").Replace(" ", "");
            decimal d = decimalValueString != "" ? Convert.ToDecimal(decimalValueString, CultureInfo.InvariantCulture) : 0;
            return d;
        }

        // SE USA PARA DATOS QUE VENGAN EN CADENA CON FORMATO ENTERO PERO LOS DOS ULTIMO DIGIOS SON DECIMALES EJEM: 2345600(2 ULTIMOS SON DECIMALES)
        // USADO EN INTERFAZ Z012, FPP1, FPM1
        public static decimal GetDecimalFieldWhitPositionAuxiliar(string field, int startIndex, int length, int decimales)
        {

            var aaa = "";
            try
            {
                string value = "";
                char[] MyChar = { '+', '0' };
                value = field.Substring(startIndex, length).Trim().TrimStart(MyChar);
                aaa = value;
                if (value == "NA" || value == "")
                {
                    value = "0";
                }
                var vfinal = 0.0m;
                if (value.ToString() != "0")
                {
                    // poner coma a los dos ultimos digitos
                    value = value.Insert((value.Length - decimales), ",");
                    NumberFormatInfo formato = new CultureInfo("es-AR").NumberFormat;
                    formato.CurrencyGroupSeparator = ".";
                    formato.NumberDecimalSeparator = ",";
                    var valor = String.Format("{0:N2}", value);
                    vfinal = Convert.ToDecimal(valor);
                }
                return vfinal;
            }
            catch (Exception ex)
            {
                var ddd = aaa;
                throw;
            }
        }

        public static decimal GetDecimalFieldWhitcsvAuxiliar(string field, int decimales)
        {

            var aaa = "";
            try
            {
                string value = "";
                char[] MyCharIni = { '+', '0' };
                value = field.Trim().TrimStart(MyCharIni);
                aaa = value;
                if (value == "NA" || value == "")
                {
                    value = "0";
                }
                var vfinal = 0.0m;
                if (value.ToString() != "0")
                {
                    //var valueDecimal = value.Replace('.', ',');
                    //var partsValue = valueDecimal.Split(',');
                    //var CantDecimalFinal = partsValue[1].Length;
                    //value = CantDecimalFinal == 1 ? valueDecimal + "0" : valueDecimal;

                    // poner coma a los dos ultimos digitos
                    //value = value.Insert((value.Length - decimales), ",");
                    value = value.Replace('.', ',');
                    NumberFormatInfo formato = new CultureInfo("es-AR").NumberFormat;
                    formato.CurrencyGroupSeparator = ".";
                    formato.NumberDecimalSeparator = ",";
                    var valor = String.Format("{0:N2}", value);
                    vfinal = Convert.ToDecimal(valor);
                }
                return vfinal;
            }
            catch (Exception ex)
            {
                var ddd = aaa;
                throw;
            }
        }


        public static string GetStringFieldRutCSV(string field)
        {
            char[] MyChar = { '0' };
            var value = field.TrimStart(MyChar);
            return value.Trim();
        }
        public static string GetStringFieldRut(string field, int startIndex, int length)
        {
            char[] MyChar = { '0' };
            var value = field.Substring(startIndex, length).TrimStart(MyChar);
            return value.Trim();
        }
        public static string GetStringField(string field, int startIndex, int length)
        {
            var aaaa = "";
            try
            {
                var value = field.Substring(startIndex, length).Trim();
                aaaa = value;
                return value;
            }
            catch (Exception)
            {
                var ddd = aaaa;
                throw;
            }
        }
        public static int GetIntField(string field)
        {
            var aaaa = "";
            try
            {
                var value = "";

                char[] MyChar = { '0' };
                value = field.Trim().TrimStart(MyChar);

                return Convert.ToInt32(value != "" ? value : "0");

            }
            catch (Exception)
            {
                var ddd = aaaa;
                throw;
            }
        }
        public static Int64 GetIntField64(string field)
        {
            var aaaa = "";
            try
            {
                var value = "";

                char[] MyChar = { '0' };
                value = field.Trim().TrimStart(MyChar);

                value = (value != "" ? value : "0");

                Int64 valFinal = Convert.ToInt64(value);

                return valFinal;
            }
            catch (Exception)
            {
                var ddd = aaaa;
                throw;
            }
        }
        public static long GetLongField(string field)
        {
            char[] MyChar = { '0' };
            var value = field.Trim().TrimStart(MyChar);
            return Convert.ToInt64(value != "" ? value : "0");
        }
        public static decimal GetDecimalField(string field)
        {
            char[] MyChar = { '0' };
            string decimalValueString = field.TrimStart(MyChar);
            decimal d = decimalValueString != "" ? Convert.ToDecimal(decimalValueString, CultureInfo.InvariantCulture) : 0;
            return d;
        }
        public static DateTime GetDateTimeFormatDate(string date)
        {
            try
            {
                DateTime dateResult = new DateTime(int.Parse(date.Substring(4, 4)), int.Parse(date.Substring(2, 2)), int.Parse(date.Substring(0, 2)));
                //string formatOutPut = "yyyy/MM/dd";
                //DateTime dt1;

                //DateTime.TryParseExact(date, formatOutPut, CultureInfo.InvariantCulture, DateTimeStyles.None, out dt1);
                //var dateResult = Convert.ToDateTime(dt1.ToString(formatOutPut, CultureInfo.InvariantCulture));
                return dateResult;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public static string GetDateTimeFormatString(string date, string formatInput)
        {
            var dateResult = new DateTime();
            string fechaAux = "00/00/0000";
            string fechaFinal = "";
            if (date != "00000000")
            {
                string formatOutPut = "yyyy/MM/dd";
                DateTime dt1;
                DateTime.TryParseExact(date, formatInput, CultureInfo.InvariantCulture, DateTimeStyles.None, out dt1);
                dateResult = Convert.ToDateTime(dt1.ToString(formatOutPut, CultureInfo.InvariantCulture));
                fechaFinal = dateResult.ToString("dd/MM/yyyy");
            }
            else
            {
                fechaFinal = fechaAux;
            }
            return fechaFinal;
        }



        public static DateTime GetDateTimeFormatWhitTime(string date)
        {
            string formatOutPut = "yyyy/MM/dd HH:mm:ss";
            DateTime dt1;
            DateTime.TryParseExact(date, "ddMMyyyy HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out dt1);

            var dateResult = Convert.ToDateTime(dt1.ToString(formatOutPut, CultureInfo.InvariantCulture));
            return dateResult;
        }

        public static int GetCodigoTransaccion(string Tipo)
        {
            int number = 0;
            var M = Tipo.ToUpper();
            try
            {
                switch (M)
                {
                    case "PAGO":
                        number = 62;
                        break;
                    case "PAGO PROMOCION COBRANZA":
                        number = 63;
                        break;
                    case "PAGO PROMOCION CASTIGO":
                        number = 64;
                        break;
                    case "PREPAGO DEUDA TOTAL":
                        number = 65;
                        break;
                    case "PAGO PIE RENEGOCIACION":
                        number = 66;
                        break;
                    case "PAGO PIE REFINANCIAMIENTO":
                        number = 67;
                        break;
                    default:
                        number = 0;
                        break;
                }
                return number;
            }
            catch (Exception)
            {

                throw;
            }
          
        }
    }
}
