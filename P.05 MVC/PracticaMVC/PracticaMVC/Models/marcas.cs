using System.ComponentModel.DataAnnotations;

namespace PracticaMVC.Models
{
    public class marcas
    {
        [Key]
        [Display(Name ="ID")]
        public int id_marcas { get; set; }

        [Display(Name ="Nombre de la Marca")]
        [Required(ErrorMessage ="El nombre de la marca NO es opcional!")]
        public string? nombre_marca { get; set; }

        [Display(Name ="Estado")]
        [StringLength(1,ErrorMessage ="La cantida maxima de caraccteres valida es {1}")]
        public string?  estados { get; set; }

        //[Range(2020,2099, ErrorMessage ="Los valores aceptados son entre 2020 y 2099")]
        //public int? anio_compra { get; set; }
    }
}
