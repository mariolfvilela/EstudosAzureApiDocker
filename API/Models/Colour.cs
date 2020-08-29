using System;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class Colour
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(250)]
        public string ColourName { get; set; }

    }
}
