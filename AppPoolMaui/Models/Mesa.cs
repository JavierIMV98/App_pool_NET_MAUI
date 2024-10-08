﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace AppPoolMaui.Models
{
    [Table("mesas")]
    public class Mesa
    {
        [PrimaryKey]
        public string Numero { get; set; }
        [MaxLength(250)]
        public double pminuto { get; set; }
        [MaxLength(250)]
        public DateTime HoraInicio { get; set; }
        public string Imagen { get; set; }
    }
}
