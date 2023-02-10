using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace AppPoolMaui.Models
{
    [Table("productos")]
    public class Producto
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [MaxLength(50), Unique]
        public string Nombre { get; set; }
        [MaxLength(50)]
        public double Precio { get; set; }
    }
}
