using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppPoolMaui.Models;

namespace AppPoolMaui.Repos
{
    public class RegistroRepository
    {
        string _dbPath;
        public string StatusMessage { get; set; }
        private SQLiteAsyncConnection _connection;
        private SQLiteConnection conn;

        private void InitMainThread()
        {
            if (conn != null) return;
            conn = new SQLiteConnection(_dbPath);
            conn.CreateTable<Registro>();
        }

        private async Task Init()
        {
            if (_connection != null) return;

            _connection = new SQLiteAsyncConnection(_dbPath);
            await _connection.CreateTableAsync<Orden>();
        }
        public RegistroRepository(string dbPath)
        {
            _dbPath = dbPath;
        }
        public async Task AddNewRegistro(string numeromesa, double total, DateTime horafinal)
        {
            int result = 0;
            try
            {
                await Init();
                if (string.IsNullOrEmpty(numeromesa))
                    throw new Exception("Numero requerido");
                result = await _connection.InsertAsync(new Registro
                {
                    NumeroMesa = numeromesa,
                    Total = total,
                    FechaFinalizada = horafinal
                });
                StatusMessage = $"Creado el registro";
            }
            catch (Exception)
            {
                StatusMessage = "Fallo en crear orden.";
            }
        }
        public async Task<List<Registro>> GetAllRegistros()
        {
            try
            {
                await Init();
                return await _connection.Table<Registro>().ToListAsync();
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Fallo, ", ex.Message);
            }
            return new List<Registro>();
        }
        public List<Registro> SeleccionarMesas()
        {
            try
            {
                InitMainThread();
                return conn.Table<Registro>().ToList();
            }
            catch (Exception ex)
            {

                StatusMessage = string.Format("Fallo, ", ex.Message);
            }
            return new List<Registro>();
        }
        public async Task DeleteRegistros()
        {
            int result = 0;
            try
            {
                await Init();
                result = await _connection.DeleteAllAsync<Registro>();
            }
            catch (Exception)
            {

                StatusMessage = "Fallo en crear mesa";
            }
        }
        public async Task DeleteRegistro(double id)
        {
            int result = 0;
            try
            {
                await Init();
                //result = await _connection.DeleteAsync<Registro>(id);
                result = await _connection.DeleteAsync<Registro>(id);
            }
            catch (Exception)
            {

                StatusMessage = "Fallo en crear mesa";
            }
        }
    }
}
