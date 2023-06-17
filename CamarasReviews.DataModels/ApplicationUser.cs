﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CamarasReviews.Models
{
    public class ApplicationUser : IdentityUser<Guid> // Guid es el tipo de dato de la llave primaria
    {
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener mas de {1} caracteres.")]
        [Display(Name = "Nombre")]
        public string Name { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener mas de {1} caracteres.")]
        [Display(Name = "Apellido")]
        public string LastName { get; set; }
        [Display(Name = "Nombre Completo")]
        public string FullName => $"{Name} {LastName}";
        [DataType(DataType.ImageUrl)]
        [Display(Name = "ProfilePicture")]
        public string ProfilePicture { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [Display(Name = "Direccion")]
        public string Address { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [Display(Name = "Ciudad")]
        public string City { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [Display(Name = "Pais")]
        public string Country { get; set; }
        public ICollection<ReviewModel> Reviews { get; set; }
        // la relacion de arriva es de uno a muchos, un usuario puede tener muchas reviews
    }
}