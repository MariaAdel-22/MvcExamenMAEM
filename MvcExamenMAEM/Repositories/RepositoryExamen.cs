using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PracticaCore2MAEM.Data;
using PracticaCore2MAEM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracticaCore2MAEM.Repositories
{
    public class RepositoryExamen
    {
        private ExamenContext context;

        public RepositoryExamen(ExamenContext context) {

            this.context = context; 
        }

        public List<Libro> GetLibros() {

            var consulta = from datos in this.context.Libros select datos;
            return consulta.ToList();
        }

        public List<Genero> GetGeneros() {

            var consulta = from datos in this.context.Generos select datos;
            return consulta.ToList();
        }

        public Libro FindLibro(int idLibro) {

            var consulta = from datos in this.context.Libros where datos.IdLibro == idLibro select datos;

            return consulta.FirstOrDefault();

        }

        public List<Libro> FindGeneroLibros(int IdGenero) {

            var consulta = from datos in this.context.Libros where datos.IdGenero == IdGenero select datos;

            return consulta.ToList();
        }

        public Usuario FindUsuario(string email,string psswd) {

            var consulta = from datos in this.context.Usuarios where datos.Email == email && datos.Password == psswd select datos;

            return consulta.FirstOrDefault();
        }

        public void InsertarPedido(int IdLibro, int IdUsuario, int cantidad) {

            string sql = "INSERTAR_PEDIDO @IDLIBRO, @IDUSUARIO,@CANTIDAD";

            SqlParameter paramIdL = new SqlParameter("@IDLIBRO",IdLibro);
            SqlParameter paramIdU = new SqlParameter("@IDUSUARIO", IdUsuario);
            SqlParameter paramCan = new SqlParameter("@CANTIDAD", 1);

            this.context.Database.ExecuteSqlRaw(sql, paramIdL, paramIdU, paramCan);
        }

        public List<VistaPedido> FindPedido(int IdUsuario) {

            var consulta = from datos in this.context.VistaPedidos where datos.IdUsuario == IdUsuario select datos;
            return consulta.ToList();
        }
    }
}
