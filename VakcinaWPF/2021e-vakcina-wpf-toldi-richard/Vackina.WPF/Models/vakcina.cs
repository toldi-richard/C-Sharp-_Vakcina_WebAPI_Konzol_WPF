﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Vakcina.WPF.Models
{
    [Index(nameof(megnevezes), Name = "megnevezes", IsUnique = true)]
    [Table("vakcina")]
    public partial class vakcina
    {
        public vakcina()
        {
            oltas = new HashSet<oltas>();
        }

        [Key]
        [Column(TypeName = "int(11)")]
        public int id { get; set; }
        [Required]
        [StringLength(50)]
        public string megnevezes { get; set; }
        [Required]
        [StringLength(100)]
        public string szarmazasi_hely { get; set; }
        [Column(TypeName = "int(11)")]
        public int mennyiseg { get; set; }

        [InverseProperty("vakcina")]
        public virtual ICollection<oltas> oltas { get; set; }
    }
}
