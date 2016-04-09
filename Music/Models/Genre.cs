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
        public string Name { get; set; }
        public List<Album> Albums { get; set; }

        //List<Album> distinct = Album.Distinct().ToList();
        
    }
}