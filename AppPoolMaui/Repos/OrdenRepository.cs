using AppPoolMaui.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPoolMaui.Repos
{
    public class OrdenRepository
    {

        string _dbPath;
        public string StatusMessage { get; set; }
        private SQLiteAsyncConnection _connection;
        private SQLiteConnection conn;

        private void InitMainThread()
        {
            if (conn != null) return;
            conn = new SQLiteConnection(_dbPath);
            conn.CreateTable<Orden>();
        }

        private async Task Init()
        {
            if (_connection != null) return;

            _connection = new SQLiteAsyncConnection(_dbPath);
            await _connection.CreateTableAsync<Orden>();
        }
        public OrdenRepository(string dbPath)
        {
            _dbPath = dbPath;
        }
        public async Task AddNewOrden(string numeromesa, string precioproducto, int cantidad)
        {
            int result = 0;
            try
            {
                await Init();
                if (string.IsNullOrEmpty(numeromesa))
                    throw new Exception("Numero requerido");
                result = await _connection.InsertAsync(new Orden{ NroMesa = numeromesa,
                    PrecioProducto = precioproducto, Cantidad = cantidad});
                StatusMessage = $"Orden de la mesa {numeromesa} creada";
            }
            catch (Exception)
            {
                StatusMessage = "Fallo en crear orden.";
            }
        }

        public async Task DeletOrden(string numero)
        {
            int result = 0;
            try
            {
                await Init();
                var lista = IdByNumero(numero);

                if (string.IsNullOrEmpty(numero))
                    throw new Exception("numero valido requerido");
                foreach(var item in lista)
                {
                    result = await _connection.DeleteAsync(new Orden { Id = item, });
                }
            }
            catch (Exception)
            {

                StatusMessage = "Fallo en crear mesa";
            }
        }
        public List<int> IdByNumero(string numero)
        {
            InitMainThread();
            var listaOrdenes =  conn.Table<Orden>().ToList();
            List<int> mesasMatch= new List<int>();
            foreach(var orden in listaOrdenes)
            {
                if(orden.NroMesa == numero)
                {
                    mesasMatch.Add(orden.Id);
                }

            }
            return mesasMatch;
        }

        public double valorTotalOrden(string numeroMesa)
        {
            InitMainThread();
            double totalidad = 0;
            var idbynumero = IdByNumero(numeroMesa);
            List<Orden> listaOrdenes = new List<Orden>();
            foreach(var ordenid in idbynumero)
            {
                listaOrdenes.Add(conn.Get<Orden>(ordenid));
            }
            foreach(var item in listaOrdenes)
            {
                totalidad = totalidad + (item.Cantidad * double.Parse(item.PrecioProducto));
            }
            return (totalidad);

            
        }
        public async Task <List<Orden>> GetAllOrdenes()
        {
            try
            {
                await Init();
                return await _connection.Table<Orden>().ToListAsync();
            }
            catch(Exception ex)
            {
                StatusMessage = string.Format("Fallo, ", ex.Message);
            }
            return new List<Orden>();
        }
    }
}
