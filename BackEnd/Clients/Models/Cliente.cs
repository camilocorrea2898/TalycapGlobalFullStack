using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Clientes.Models
{
    public partial class Cliente
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("idTipoDocumento")]
        public int IdTipoDocumento { get; set; }
        [Column("numeroDocumento")]
        public long NumeroDocumento { get; set; }
        [Column("nombreCompleto")]
        [StringLength(200)]
        public string NombreCompleto { get; set; } = null!;
        [Column("fechaNacimiento", TypeName = "date")]
        public DateTime FechaNacimiento { get; set; }
        [Column("empresa")]
        [StringLength(150)]
        public string? Empresa { get; set; }
        [Required]
        [Column("activo")]
        public bool? Activo { get; set; }
        [Column("fechaCreacion", TypeName = "datetime")]
        public DateTime FechaCreacion { get; set; }

        [ForeignKey("IdTipoDocumento")]
        [InverseProperty("Clientes")]
        public virtual TipoDocumento IdTipoDocumentoNavigation { get; set; } = null!;
    }
}
