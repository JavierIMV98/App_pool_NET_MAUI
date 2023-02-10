using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using AppPoolMaui.Models;

namespace AppPoolMaui
{
    public class MesaRepository
    {
        string _dbPath;
        public string StatusMessage { get; set; }

        private SQLiteAsyncConnection _connection;

        private async Task Init()
        {
            if (_connection != null) return;

            _connection = new SQLiteAsyncConnection(_dbPath);
            await _connection.CreateTableAsync<Mesa>();
        }

        public MesaRepository(string dbPath)
        {
            _dbPath= dbPath;
        }

        public async Task AddNewMesa(string numero)
        {
            int result = 0;
            try
            {
                await Init();

                if (string.IsNullOrEmpty(numero))
                    throw new Exception("numero valido requerido");
                result = await _connection.InsertAsync(new Mesa {Numero = numero, HoraInicio = 
                    DateTime.Now.ToString("HH:mm -- dd/MM")});
                StatusMessage = $"Mesa {numero} se ha creado";
            }
            catch (Exception)
            {

                StatusMessage = "Fallo en crear mesa";
            }
        }
        public async Task <List<Mesa>> GetAllMesas()
        {
            try
            {
                await Init();
                return await _connection.Table<Mesa>().ToListAsync();
            }
            catch (Exception ex)
            {

                StatusMessage = string.Format("Fallo, ", ex.Message);
            }
            return new List<Mesa>();
        }
    }
}
