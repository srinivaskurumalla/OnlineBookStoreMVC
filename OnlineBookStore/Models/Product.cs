using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace OnlineBookStore.Models
{
    public class Product
    {
        /* "Online Book Store(Update, Index)
         Product Class
  Id, BookName, ISBN, AuthorName, Publisher, BookType, Description,
  Price, PublishedDate
 BookType should be displayed in Dropdownlist"*/

        public int Id { get; set; }

        [Required(ErrorMessage = "Book name is mandatory")]
        [Display(Name = "Book Name")]
        [Column(TypeName = "varchar")]
        [StringLength(50)]
        [Index(IsUnique = true)]
        public string BookName { get; set; }

        [Required(ErrorMessage = "ISBN is mandatory")]
        [Display(Name = "ISBN")]
        [Column(TypeName = "varchar")]
        [StringLength(50)]
        [Index(IsUnique = true)]
        public string ISBN { get; set; }

        [Required(ErrorMessage = "Author name is mandatory")]
        [Display(Name = "Author Name")]
        [Column(TypeName = "varchar")]
        [StringLength(50)]
        public string AuthorName { get; set; }

        [Required(ErrorMessage = "Publisher name is mandatory")]
        [Display(Name = "Publisher")]
        [Column(TypeName = "varchar")]
        [StringLength(50)]
        public string Publisher { get; set; }

       /* [Required(ErrorMessage = "Book Type is mandatory")]
        [Display(Name = "BookType")]
        [Column(TypeName = "varchar")]
        [StringLength(50)]
        public string BookType { get; set; }*/

        [Display(Name = "Description")]
        [Column(TypeName = "varchar")]
        [StringLength(50)]
        public string Description { get; set; }

        [Required(ErrorMessage = "Price is mandatory")]
        [Display(Name = "Price")]
        [Column(TypeName = "money")]
       
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Published Date is mandatory")]
        [Display(Name = "Published Date")]
        [Column(TypeName = "Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]

        public DateTime PublishedDate { get; set; }

        //Navigation
        public BookType BookType { get; set; }

      
        public int BookTypeId { get; set; }

        




    }
}