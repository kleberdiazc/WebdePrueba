using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebdePrueba.Helper;

namespace WebdePrueba.Entities
{
    public class Autor
    {
        public int Id {get;set;}
        [Required]
        [PrimeraLetraMayuscula]
        public string Nombre { get; set; }
        public List<Libro> Libros { get; set; }

        public IEnumerable<ValidationResult> Validate (ValidationContext validationContext)
        {
            if ( string.IsNullOrEmpty(Nombre))
            {

            }
            var firstLetter = Nombre[0].ToString();
            if (firstLetter != firstLetter.ToUpper())
            {
                yield return new ValidationResult("La primera letra debe ser mayúscula", new String[] { nameof(Nombre) });


            }
        }
    }
}
