using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WebDemo.Entity.Attributes;

namespace WebDemo.Entity
{
    [Table("Cliente")]
    public class Cliente
    {
        [Key]
        [Column("CodigoCliente", Order = 0)]
        public int codigoCliente { get; set; }

        [Column("NombreCompleto", Order = 1)]
        public string nombreCompleto { get; set; }

        [Column("NombreCorto", Order = 2)]
        public string nombreCorto { get; set; }
        [Column("Abreviatura", Order = 3)]
        public string abreviatura { get; set; }
        [Column("Ruc", Order = 4)]
        public string ruc { get; set; }

        [Column("Estado", Order = 5)]
        public string estado { get; set; }

        [Column("GrupoFacturacion", Order = 6)]
        public string? grupoFacturacion { get; set; }

        [Column("InactivoDesde", Order = 7)]
        public DateTime? inactivoDesde { get; set; }

        [Column("CodigoSap", Order = 8)]
        public string? codigoSap { get; set; }
    }
}
