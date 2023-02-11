using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
//Investigar como hacerlo 
namespace AppPoolMaui.Models
{
    [Table("ordenes")]
    public class Orden
    {

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Indexed]
        public string NroMesa { get; set; }

        [Indexed]
        public string PrecioProducto { get; set; }

        [MaxLength(50)]
        public int Cantidad { get; set; }
    }
}
