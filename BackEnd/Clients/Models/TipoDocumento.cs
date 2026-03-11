using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Clientes.Models
{
    [Table("TipoDocumento")]
    public partial class TipoDocumento
    {
        public TipoDocumento()
        {
            Clientes = new HashSet<Cliente>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("tipoDocumento")]
        [StringLength(100)]
        public string TipoDocumento1 { get; set; } = null!;

        [InverseProperty("IdTipoDocumentoNavigation")]
        public virtual ICollection<Cliente> Clientes { get; set; }
    }
}
