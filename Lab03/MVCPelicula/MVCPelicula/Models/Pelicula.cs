using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

using System.ComponentModel.DataAnnotations; // esto me servirá para darle formato a la fecha


namespace MVCPelicula.Models
{
    public class Pelicula
    {
        public int ID { get; set; }
        public string Titulo { get; set; }


        [Display(Name = "FechaLanzamiento de lanzamiento")] // . El atributo Display especifica qué mostrar para el nombre de un campo(en este caso, "Fecha de lanzamiento" en lugar de"FechaLanzamiento")
        [DataType(DataType.Date)]  // El atributo DataType especifica el tipo de datos, en este caso es una fecha, por lo que no se muestra la información de tiempo almacenada en el campo
        [DisplayFormat(DataFormatString = "{0 : yyyy-MM-dd}", ApplyFormatInEditMode = true)] // El atributo DisplayFormat es necesario para un error en el navegador Chrome que representa los formatos de fecha incorrectamente.
        public DateTime FechaLanzamiento { get; set; }
        public string Genero { get; set; }
        public decimal Precio { get; set; }
    }

    public class PeliculaDBContext : DbContext
    {
        public DbSet<Pelicula> Peliculas { get; set; } // como funciona el DbSet?
    }

}