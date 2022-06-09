using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Vakcina.WPF.Models
{
    [Index(nameof(felhasznalo_nev), Name = "felhasznalo_nev", IsUnique = true)]
    [Table("orvos")]
    public partial class orvos
    {
        public orvos()
        {
            oltas = new HashSet<oltas>();
        }

        [Key]
        [Column(TypeName = "int(11)")]
        public int id { get; set; }
        [Required]
        [StringLength(50)]
        public string felhasznalo_nev { get; set; }
        [Required]
        [StringLength(128)]
        public string jelszo { get; set; }
        [Required]
        [StringLength(100)]
        public string megjelenitendo_nev { get; set; }
        public bool admin { get; set; }

        [InverseProperty("orvos")]
        public virtual ICollection<oltas> oltas { get; set; }
    }
}
