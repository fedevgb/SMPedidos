using SMPedidos.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMPedidos.Models
{
    public class Category
    {
        //instancia de conexion a la base de datos
        private readonly DbHelper DB = new DbHelper(App.ClsCommon.connectionString, CommandType.StoredProcedure);

        //entidad del modelo
        private readonly string Entity = "Categorias";

        //propiedades del modelo categoria
        private int id;
        private string name;
        private string image;

        //getters and setters
        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Image { get => image; set => image = value; }

        //metodo para traer la data de la tabla de la base de datos
        public DataTable Data()
        {
            return DB.GetDataTable("sp_category_data");
        }

        //metodo list
        public DataTable List()
        {
            return DB.GetDataTable("sp_category_list");
        }

        //metodo create
        public string Create()
        {
            
            DB.AddParameters("name_", this.Name);
            DB.AddParameters("image_", this.Image);
            int res = DB.CRUD("sp_category_create");

            return (res == 1 ? $"{App.ClsCommon.RowsCreated} {Entity}" : App.ClsCommon.NoRowsAdded);

        }

        //metodo update
        public string Update()
        {
            DB.AddParameters("id_", this.Id);
            DB.AddParameters("name_", this.Name);
            DB.AddParameters("image_", this.Image);
            int res = DB.CRUD("sp_category_update");

            return (res == 1 ? $"{App.ClsCommon.RowsUpdated} {Entity}" : App.ClsCommon.NoRowsUpdated);

        }

        //metodo delete
        public string Destroy()
        {
            DB.AddParameters("id_", this.Id);
            int res = DB.CRUD("sp_category_destroy");

            return (res == 1 ? $"{App.ClsCommon.RowsDeleted} {Entity}" : App.ClsCommon.NoRowsDeleted);
        }

        //metodo search
        public DataTable search(string searchText)
        {
            DB.AddParameters("txt",searchText);
            return DB.GetDataTable("sp_category_search");
        }
        
        //metodo para validar si la categoria tiene productos relacionados asi no eliminarla del sistema
        public bool HasProduct(int category_id)
        {
            DB.AddParameters("id_", category_id);
            DataTable info = DB.GetDataTable("sp_category_hasproduct");

            return (info != null && info.Rows.Count > 0 ? true : false);
        }

        //metodo para verificar si existe la categoria y no duplicarla
        public bool CategoryExists(string name)
        {
            DB.AddParameters("name_", name);
            DataTable info = DB.GetDataTable("sp_category_exists");

            return (info != null && info.Rows.Count > 0 ? true : false);
        }

    }
}
