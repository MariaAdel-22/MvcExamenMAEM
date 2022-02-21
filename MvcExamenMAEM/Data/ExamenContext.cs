using Microsoft.EntityFrameworkCore;
using PracticaCore2MAEM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

#region PROCEDIMIENTOS
/*
 * CREATE PROCEDURE INSERTAR_PEDIDO(@IDLIBRO INT, @IDUSUARIO INT,@CANTIDAD INT)
AS
INSERT INTO PEDIDOS VALUES((SELECT MAX(IDPEDIDO)+1 FROM PEDIDOS),1,GETDATE(),@IDLIBRO,@IDUSUARIO,@CANTIDAD)
GO
 */
#endregion
namespace PracticaCore2MAEM.Data
{
    public class ExamenContext:DbContext
    {
        public ExamenContext(DbContextOptions<ExamenContext> options) : base(options)
        {

        }

        public DbSet<Genero> Generos { get; set; }
        public DbSet<Libro> Libros { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<VistaPedido> VistaPedidos { get; set; }
    }
}
