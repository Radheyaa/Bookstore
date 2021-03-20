using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Models
{
    public class BookModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Please Enter the Title")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Please Enter the Author")]
        public string Author { get; set; }
        [Required(ErrorMessage = "Please Enter the Description")]
        [StringLength(100, MinimumLength = 5)]
        public string Description { get; set; }
        [Required(ErrorMessage = "Please Enter the Category")]
        public string Category { get; set; }
        [Required(ErrorMessage = "Please Enter the Pages")]
        [Display(Name ="Total Pages")]
        public string Pages { get; set; }
        public string Language { get; set; }
    }
}
