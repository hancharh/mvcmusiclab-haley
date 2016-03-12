using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Music.Models
{
    public class Genre
    {
        public int GenreID { get; set; }
        //[Required(ErrorMessage = "Name of genre is required")]
        //[MaxLength(20, ErrorMessage = "Name must be shorter than 20 characters")]
        public string Name { get; set; }
        public List<Album> Albums { get; set; }
    }
}