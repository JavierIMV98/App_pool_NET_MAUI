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

        private SQLiteConnection conn;

        private void InitMainThread()
        {
            if (conn != null)
                return;

            conn = new SQLiteConnection(_dbPath);
            conn.CreateTable<Mesa>();
        }
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
        public async Task AddNewMesaCustom(string numero, string hora, double precio)
        {
            int result = 0;
            try
            {
                await Init();

                if (string.IsNullOrEmpty(numero))
                    throw new Exception("numero valido requerido");
                result = await _connection.InsertAsync(new Mesa
                {
                    Numero = numero,
                    HoraInicio = Convert.ToDateTime(hora),
                    pminuto = precio,

                }) ;
                StatusMessage = $"Mesa {numero} se ha creado";
            }
            catch (Exception)
            {

                StatusMessage = "Fallo en crear mesa";
            }
        }
        public async Task DeleteMesa(string numero)
        {
            int result = 0;
            try
            {
                await Init();

                if (string.IsNullOrEmpty(numero))
                    throw new Exception("numero valido requerido");
                result = await _connection.DeleteAsync(new Mesa { Numero = numero, });
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

        public double valorTotalTiempo(string numeroMesa)
        {
            InitMainThread();
            var asd = conn.Get<Mesa>(numeroMesa);
            var horainicial = asd.HoraInicio;
            DateTime horafinal = DateTime.Now;
            var tiempo = horafinal - horainicial;
            var str = tiempo.ToString();
            var listatiempo = str.Split(":");
            double horas = double.Parse(listatiempo[0]);
            double minutos = double.Parse(listatiempo[1]);
            double precioporminuto = asd.pminuto;       
            double valortotaltiempo;
            double minutostotales = (horas * 60) + minutos + 1;
            valortotaltiempo = minutostotales * precioporminuto;
            return valortotaltiempo;
        }
        public Mesa valoresTotalesMesa(string numeroMesa)
        {
            InitMainThread();
            var asd = conn.Get<Mesa>(numeroMesa);
            return asd;
        }


            //Metodo en el main thread para ver como agregar la lista de mesas en el picker de ordenes
            public List<Mesa> SeleccionarMesas()
        {
            try
            {
                InitMainThread();
                return conn.Table<Mesa>().ToList();
            }
            catch (Exception ex)
            {

                StatusMessage = string.Format("Fallo, ", ex.Message);
            }
            return new List<Mesa>();
        }
    }
}
