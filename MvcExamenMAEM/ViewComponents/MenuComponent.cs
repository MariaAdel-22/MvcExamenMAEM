using Microsoft.AspNetCore.Mvc;
using PracticaCore2MAEM.Repositories;
using PracticaCore2MAEM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace PracticaCore2MAEM.ViewComponents
{
    public class MenuComponent: ViewComponent
    {
        private RepositoryExamen repo;


        public MenuComponent(RepositoryExamen repo)
        {

            this.repo = repo;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
             List<Genero> Generos = this.repo.GetGeneros();
            
            return View(Generos);
        }
    }
}
