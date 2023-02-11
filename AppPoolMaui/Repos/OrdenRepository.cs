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
