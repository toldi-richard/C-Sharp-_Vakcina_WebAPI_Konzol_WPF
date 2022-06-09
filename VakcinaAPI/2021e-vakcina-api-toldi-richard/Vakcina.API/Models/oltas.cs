using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Vakcina.API.Models
{
    [Index(nameof(orvos_id), Name = "orvos_id")]
    [Index(nameof(vakcina_id), Name = "vakcina_id")]
    [Table("oltas")]
    public partial class oltas
    {
        [Key]
        [Column(TypeName = "int(9) unsigned zerofill")]
        public uint taj_szam { get; set; }
        [Column(TypeName = "int(11)")]
        public int vakcina_id { get; set; }
        [Column(TypeName = "int(11)")]
        public int orvos_id { get; set; }
        [Column(TypeName = "date")]
        public DateTime datum_utolso { get; set; }
        [Column(TypeName = "int(1)")]
        public int oltas_szam { get; set; }

        [JsonIgnore]
        [ForeignKey(nameof(orvos_id))]
        [InverseProperty("oltas")]
        public virtual orvos orvos { get; set; }
        [JsonIgnore]
        [ForeignKey(nameof(vakcina_id))]
        [InverseProperty("oltas")]
        public virtual vakcina vakcina { get; set; }
    }
}
