using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using AppPoolMaui.Models;

namespace AppPoolMaui
{
    public class ProductoRepository
    {
        string _dbPath;
        public string StatusMessage { get; set; }

        private SQLiteAsyncConnection _connection;

        private SQLiteConnection conn;

        private void InitMainThread()
        {
            if (conn != null)
                return;

            conn = new SQLiteConnection(_dbPath);
            conn.CreateTable<Producto>();
        }

        private async Task Init()
        {
            if (_connection != null) return;

            _connection = new SQLiteAsyncConnection(_dbPath);
            await _connection.CreateTableAsync<Producto>();
        }

        public ProductoRepository(string dbPath)
        {
            _dbPath = dbPath;
        }

        public async Task AddNewProducto(string nombre, double precio)
        {
            int result = 0;
            try
            {
                await Init();

                if (string.IsNullOrEmpty(nombre))
                    throw new Exception("numero valido requerido");
                result = await _connection.InsertAsync(new Producto
                {
                    Nombre = nombre,
                    Precio= precio
                });
                StatusMessage = $"Producto {nombre} se ha creado";
            }
            catch (Exception)
            {

                StatusMessage = "Fallo en crear producto";
            }
        }
        public async Task<List<Producto>> GetAllProductos()
        {
            try
            {
                await Init();
                return await _connection.Table<Producto>().ToListAsync();
            }
            catch (Exception ex)
            {

                StatusMessage = string.Format("Fallo, ", ex.Message);
            }
            return new List<Producto>();
        }

        public List<Producto> SeleccionarProductos()
        {
            try
            {
                InitMainThread();
                return conn.Table<Producto>().ToList();
            }
            catch (Exception ex)
            {

                StatusMessage = string.Format("Fallo, ", ex.Message);
            }
            return new List<Producto>();
        }
    }
}