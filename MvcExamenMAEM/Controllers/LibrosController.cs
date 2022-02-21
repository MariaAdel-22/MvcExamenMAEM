using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MvcExamenMAEM.Extensions;
using PracticaCore2MAEM.Filters;
using PracticaCore2MAEM.Models;
using PracticaCore2MAEM.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PracticaCore2MAEM.Controllers
{
    public class LibrosController : Controller
    {
        private RepositoryExamen repo;

        public LibrosController(RepositoryExamen repo) {

            this.repo = repo;
        }

        public IActionResult Index()
        {
            List<Libro> Libros = this.repo.GetLibros();

            return View(Libros);
        }

        public IActionResult CargarGeneroLibros(int idGenero) {

            List <Libro> libros= this.repo.FindGeneroLibros(idGenero);

            return View("Index",libros);
        }

        public IActionResult Detalles(int idLibro) {

            Libro lib = this.repo.FindLibro(idLibro);

            return View(lib);
        }

        public JsonResult InsertarCompra(string id) {

            Libro lib = this.repo.FindLibro(int.Parse(id));

            if (lib != null)
            {

                List<Libro> LibProduct;

                if (HttpContext.Session.GetString("Productos") != null)
                {
                   
                    LibProduct = HttpContext.Session.GetObject<List<Libro>>("Productos");

                    if (HttpContext.Session.GetString("CantidadLibros") != null)
                    {

                        int cantidadLibros = LibProduct.Count;

                        HttpContext.Session.SetString("CantidadLibros", cantidadLibros.ToString());
                    }
                    else
                    {
                        int cantidadLibros = LibProduct.Count;

                        HttpContext.Session.SetString("CantidadLibros", cantidadLibros.ToString());
                    }


                    if (LibProduct.Contains(lib) == false)
                    {
                        LibProduct.Add(lib);
                        HttpContext.Session.SetObject("Productos", LibProduct);

                    }
                }
                else
                {
                    LibProduct = new List<Libro>();

                    LibProduct.Add(lib);
                    HttpContext.Session.SetObject("Productos", LibProduct);

                    if (HttpContext.Session.GetString("CantidadLibros") != null)
                    {

                        int cantidadLibros = LibProduct.Count;

                        HttpContext.Session.SetString("CantidadLibros", cantidadLibros.ToString());
                    }
                    else
                    {
                        int cantidadLibros = LibProduct.Count;

                        HttpContext.Session.SetString("CantidadLibros", cantidadLibros.ToString());
                    }
                }
            }

           
            return Json("funciona");
        }

        public IActionResult Carrito() {

            return PartialView("_Carrito");
        }

        public IActionResult EliminarLibro(int IdLibro) {

            List<Libro> LibProduct= new List<Libro>();

            if (HttpContext.Session.GetObject<List<Libro>>("Productos") != null)
            {

                LibProduct = HttpContext.Session.GetObject<List<Libro>>("Productos");

                Libro li = this.repo.FindLibro(IdLibro);

                foreach (Libro lib in LibProduct) {

                    if (lib.IdLibro.Equals(li.IdLibro)) {

                        LibProduct.Remove(li);
                        HttpContext.Session.SetObject("Productos", LibProduct);   
                    }
                }

            }

            return RedirectToAction("Carrito");
        }

        [AuthorizeExamen]
        public IActionResult CompraFinalizada() {

            List<Libro> LibProduct = new List<Libro>();
             Usuario usu = this.repo.FindUsuario(HttpContext.User.Identity.Name, HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
             int cantidadTotal = 0;

             if (HttpContext.Session.GetObject<List<Libro>>("Productos").Count != 0)
             {

                 LibProduct = HttpContext.Session.GetObject<List<Libro>>("Productos");


                 foreach(Libro lib in LibProduct){

                     this.repo.InsertarPedido(lib.IdLibro,usu.IdUsuario,1);

                     cantidadTotal += lib.Precio;
                 }
             }

             List<VistaPedido> pedidos = this.repo.FindPedido(usu.IdUsuario);

             ViewBag.Cantidad = cantidadTotal;


             return View(pedidos);
        }

        [AuthorizeExamen]
        public IActionResult Perfil() {

            Usuario usu = this.repo.FindUsuario(HttpContext.User.Identity.Name, HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

            return View(usu);
        }
    }
}
