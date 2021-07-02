using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMPedidos.App
{
   public class ClsCommon
    {
        public static int flag = 0;
        public static bool payCommited = false;
        public static string FileName_Ticket;

        public static readonly string version = "1.0.1";
        public static readonly string app = "SMPedidos";

        //parametros para la conexion a la base de datos
        public static string Server = "localhost";
        public static string Database = "smpedidos";
        public static string User = "root";
        public static string Password = "";
        public static string connectionString = $"Server ={Server};Database={Database}; User Id={User};";

        //propiedades para manejar los mensajes de tipo genéricos
        
        //crud messages

        public static readonly string NoRowsAfected = "Ningún registro eliminado";
        public static readonly string NoRowsAdded = "No se guardó el registro, intente nuevamente..";
        public static readonly string NoRowsUpdated = "No se actualizó el registro";
        public static readonly string NoRowsDeleted = "No se eliminó el registro";


        public static readonly string RowsCreated = "Se ha agregado satisfactriamente el registro a la tabla";
        public static readonly string RowsDeleted = "Se ha eliminado satisfactoriamente el registro";
        public static readonly string RowsUpdated = "Se actualizó satisfactoriamente el registro";
        


    }
}
