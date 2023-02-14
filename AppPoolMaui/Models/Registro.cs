using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace AppPoolMaui.Models
{
    [Table("registros")]
    public class Registro
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [MaxLength(2)]
        public string NumeroMesa { get; set; }
        [MaxLength(50)]
        public double Total { get; set; }
        [MaxLength(20)]
        public DateTime FechaFinalizada { get; set; }
    }
}
