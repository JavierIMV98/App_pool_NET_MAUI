using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite; 
namespace AppPoolMaui.Models
{
    [Table("ordenes")]
    public class Orden
    {
        //TODO: Evaluar como poder o crear mas de una orden por mesa (quizas cambiando PK a algo que sea común para los mismos numeros...
        //Quizas se pueda hacer algo con al conn.Get(PrimaryKey) de las repos.
        [PrimaryKey]
        public string NroMesa { get; set; }

        [Indexed]
        public string PrecioProducto { get; set; }

        [MaxLength(50)]
        public int Cantidad { get; set; }
        public string Imagen { get; set; }
    }
}
