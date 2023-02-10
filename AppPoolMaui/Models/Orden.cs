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
        int Id { get; set; }

        [Indexed]
        int NroMesa { get; set; }

        [Indexed]
        int IdProducto { get; set; }

        [MaxLength(50)]
        int Cantidad { get; set; }
    }
}
