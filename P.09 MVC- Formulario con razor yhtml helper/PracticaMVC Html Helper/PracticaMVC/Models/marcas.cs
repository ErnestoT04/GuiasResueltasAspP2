﻿using System.ComponentModel.DataAnnotations;

namespace PracticaMVC.Models
{
    public class marcas
    {
        [Key]
        [Required]
        [Display(Name = "ID")]
        public int id_marcas { get; set; }
        [Required]

        [Display(Name = "Nombre de la Marca")]
        public string? nombre_marca { get; set; }
        [Required]

        [Display(Name = "Estado")]
        public string? estados { get; set; }
    }
}
