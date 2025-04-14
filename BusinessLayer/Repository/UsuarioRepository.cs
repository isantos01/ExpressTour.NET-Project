using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using DataLayer;

namespace BusinessLayer.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ExpressTourDataContext _db;

        public UsuarioRepository()
        {
            _db = new ExpressTourDataContext();
        }

        public IEnumerable<Usuario> ObtenerUsuarios()
        {
            return _db.Usuarios.ToList();
        }
        public Usuario ObtenerUsuarioPorId(int id)
        {
            return _db.Usuarios.FirstOrDefault(u => u.Id == id);
        }

        public void AgregarUsuario(Usuario usuario)
        {
            // Fecha automática al crear
            usuario.FechaCreacion = DateTime.Now;
            _db.Usuarios.InsertOnSubmit(usuario);
            _db.SubmitChanges();
        }

        public void ActualizarUsuario(Usuario usuario)
        {
            var existente = _db.Usuarios.FirstOrDefault(u => u.Id == usuario.Id);
            if (existente != null)
            {
                // Actualizar campos editables
                existente.NombreUsuario = usuario.NombreUsuario;
                existente.Contrasena = usuario.Contrasena;
                existente.Rol = usuario.Rol;

                // Actualizar fecha al editar (según tu requerimiento)
                existente.FechaCreacion = DateTime.Now;

                _db.SubmitChanges();
            }
        } 
        
        public void EliminarUsuario(int id)
        {
            var usuario = _db.Usuarios.FirstOrDefault(u => u.Id == id);
            if (usuario != null)
            {
                _db.Usuarios.DeleteOnSubmit(usuario);
                _db.SubmitChanges();
            }
        }
    }
}