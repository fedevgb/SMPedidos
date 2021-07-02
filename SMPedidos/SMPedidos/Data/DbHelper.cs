using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient; //para realizar la conexion con la base de datos mysql
using System.Collections.Generic; //lo usamos para trabajar con listas
using System.Data.Common; //para trabajar con la conexion generica a nuestra DB
using System.Data;

namespace SMPedidos.Data
{
    public class DbHelper
    {
        //propiedades
        //cadena de conexion
        private string _Connectionstring = "";
        //clase abstracta que representa una conección con una base de datos
        private DbConnection _Connection;
        //Clase abstracta que representa una sentencia SQL o Procedimiento Almacenado
        private DbCommand _Command;
        //clase abstracta que provee un conjunto de metodos para crear instancias de conexion a diferentes motoros de Base de Datos
        private DbProviderFactory _factory = null;
        //lista de proveedores de base de datos tipo enum
        private DbProviders _providers;
        //objeto de tipo enum que indica como se va a interpretar la propiedad commandtext del comando (1-query, 4-procedimientos, 512-tabledirect )//vamos a trabajar con la opcion 4 ->procedimientos almacenados
        private CommandType _Commandtype;



        //getters and setters
        public string Connectionstring { get => _Connectionstring; set => _Connectionstring = value; }
        public DbConnection Connection { get => _Connection; set => _Connection = value; }
        public DbCommand Command { get => _Command; set => _Command = value; }
        public DbProviderFactory Factory { get => _factory; set => _factory = value; }
     


        //constructor

        public DbHelper(string ConnectString, CommandType CommandType, DbProviders ProviderName = DbHelper.DbProviders.MySQL)
        {
            _providers = ProviderName; //le indicamos que nuestro motor va a ser MySQL 
            _Commandtype = CommandType; //vamos a trabajar con procedimientos almacenados
            _factory = MySqlClientFactory.Instance;

            Connection = _factory.CreateConnection(); //aqui creamos la conexion
            Connection.ConnectionString = ConnectString;

            Command = _factory.CreateCommand(); //creamos el comando 

            Command.Connection = Connection; //es el responsable de realizar todas las operaciones contra nuestra base de datos

        }


        //metodo que indica si esta abierta o cerrada la conexion a la base de datos
        private void BeginTransaction()
        {
            //primero debemos validar que el estatus de conexion a la base de datos esté abierto
            if(Connection.State == ConnectionState.Closed) //si el estado a la conexion de la base de datos esta cerrado, entonces la abrimos
            {
                Connection.Open(); // abrimos la conexion a la base de datos
            }
            Command.Transaction = Connection.BeginTransaction();
        }

        //Metodo de confirmar la transaccion con la base de datos
        private void CommitTransaction()
        {
            if(Connection.State == ConnectionState.Open)
            {
                Command.Transaction.Commit();
                Connection.Close();
            }
        }

        //Metodo para deshacer las operaciones en la db que hayan quedado incompleta
        private void RollBackTransaction()
        {
            if (Connection.State == ConnectionState.Open)
            {
                Command.Transaction.Rollback();
                Connection.Close();
            } 
        }


        // metodo CRUD (CReate Update Deleted) en la base de datos
        public int CRUD(string query)
        {
            Command.CommandText = query;
            Command.CommandType = _Commandtype;
            int i = -1;

            try
            {
                if(Connection.State == ConnectionState.Closed)
                {
                    Connection.Open();
                }

                BeginTransaction();

                i = Command.ExecuteNonQuery(); //nos retoma las filas afectadas y la guardamos en la variable I

                CommitTransaction();
            }
            catch (Exception ex)
            {
                RollBackTransaction();
                //logs
            }
            finally
            {
                Command.Parameters.Clear();

                if(Connection.State == ConnectionState.Open)
                {
                    //cerramos la conección
                    Connection.Close();
                    Connection.Dispose();
                    Command.Dispose();
                }
            }

            return i;
        }


        //metodo datatable
        public DataTable GetDataTable(string query)
        {
            DbDataAdapter adapter = _factory.CreateDataAdapter();
            Command.CommandText = query;
            Command.CommandType = _Commandtype;
            adapter.SelectCommand = Command;
            DataSet ds = new DataSet();

            try
            {
                adapter.Fill(ds); //traemos el resultado de nuestra consulta y lo llenamos a nuestro dataset
            }
            catch (Exception ex)
            {
                //logs
                //throw;
            }
            finally
            {
                Command.Parameters.Clear();

                if(Connection.State == ConnectionState.Open)
                {
                    //cerramos la conección
                    Connection.Close();
                    Connection.Dispose();
                    Command.Dispose();
                }
            }

            return ds.Tables[0];
        }


        //metodo parameters que nos ayudara a manejar parametros en las operaciones con la base de datos
        public int AddParameters(string name, object value)
        {
            DbParameter parm = _factory.CreateParameter();
            parm.ParameterName = name;
            parm.Value = value;

            return Command.Parameters.Add(parm);
        }


        //Lista de Proveedores de motores de DB
        public enum DbProviders
        {
            MySQL, SqlServer, Oracle, OleDB, SQLite
        }



    }
}
