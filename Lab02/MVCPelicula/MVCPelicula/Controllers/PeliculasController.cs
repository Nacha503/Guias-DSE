using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

using MVCPelicula.Models;

namespace MVCPelicula.Controllers
{
    public class PeliculasController : Controller
    {
        private PeliculaDBContext db = new PeliculaDBContext(); // esto se llama instanciación de un contexto de base de datos en este caso para Peliculas

        // GET: Peliculas
        //public ActionResult Index(string buscarString)
        //{
        //    // cuando queremos pasar por URL el ID a ser buscado -->  /Peliculas/Index/John --> {controller}/{action}/{id}
        //    //string buscarString = id; 

        //    var peliculas = from p in db.Peliculas
        //                    select p;
        //    if (!String.IsNullOrEmpty(buscarString))
        //        peliculas = peliculas.Where(s => s.Titulo.Contains(buscarString)); //busca parcial o palabra complete, no distingue minusculas de mayusculas

        //    return View(peliculas);
        //}


        public ActionResult Index(string buscarString, string generoPelicula)
        {
            var generoLst = new List<string>();
            var generoQry = from d in db.Peliculas
                            orderby d.Genero
                            select d.Genero;
            generoLst.AddRange(generoQry.Distinct()); // Add vs AddRange. Add se usa para ir agregando uno por uno los elementos dentro de la lista en cambio AddRange agrega en una sola operacion
            ViewBag.generoPelicula = new SelectList(generoLst);

            var peliculas = from p in db.Peliculas
                            select p;


            // estas validaciones individuales se pueden manejar en una sola sentencia
            if (!String.IsNullOrEmpty(buscarString))
            {
                peliculas = peliculas.Where(s => s.Titulo.Contains(buscarString));
            }

            if (!String.IsNullOrEmpty(generoPelicula))
            {
                peliculas = peliculas.Where(x => x.Genero == generoPelicula);
            }

            return View(peliculas); // esta filtrando por los dos criterios aaaaaaa, es que primero filtra por cadena y sobre ese mismo resultado se vuelve a filtrar por genero!!!!
        }


        // GET: Peliculas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pelicula pelicula = db.Peliculas.Find(id);
            if (pelicula == null)
            {
                return HttpNotFound();
            }
            return View(pelicula);
        }







        // GET: Peliculas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Peliculas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Titulo,FechaLanzamiento,Genero,Precio")] Pelicula pelicula)
        {
            if (ModelState.IsValid)
            {
                db.Peliculas.Add(pelicula);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pelicula);
        }

        // GET: Peliculas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pelicula pelicula = db.Peliculas.Find(id);
            if (pelicula == null)
            {
                return HttpNotFound();
            }
            return View(pelicula);
        }

        // POST: Peliculas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Titulo,FechaLanzamiento,Genero,Precio")] Pelicula pelicula)
        {

            // el modelo binding va y verifica que lo que se ha enviado desde el formulario de EDICION vengan los parametros ( <input type = "text" name = "ID"> <input type = "text" name = "Titulo"> 
            // <input type = "text" name = "FechaLanzamiento">  <input type = "text" name = "Genero"> <input type = "text" name = "Precio"> )
            if (ModelState.IsValid)
            {
                db.Entry(pelicula).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pelicula);
        }

        // GET: Peliculas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pelicula pelicula = db.Peliculas.Find(id);
            if (pelicula == null)
            {
                return HttpNotFound();
            }
            return View(pelicula);
        }

        // POST: Peliculas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pelicula pelicula = db.Peliculas.Find(id);
            db.Peliculas.Remove(pelicula);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
