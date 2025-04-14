using System.Collections.Generic;
using System.Linq;
using BusinessLayer.Repository;
using DataLayer;
using DataLayer.Repositories;
using ModelLayer.DTO;

namespace BusinessLayer.Services
{
    public class UsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService()
        {
            _usuarioRepository = new UsuarioRepository();
        }

        public List<UsuarioViewModel> ObtenerUsuarios()
        {
            return _usuarioRepository.ObtenerUsuarios()
                .Select(u => new UsuarioViewModel
                {
                    Id = u.Id,
                    NombreUsuario = u.NombreUsuario,
                    Contraseña = u.Contrasena,
                    Rol = u.Rol,
                    FechaCreacion = (System.DateTime)u.FechaCreacion
                }).ToList();
        }

        public void AgregarUsuario(UsuarioViewModel model)
        {
            _usuarioRepository.AgregarUsuario(new Usuario
            {
                NombreUsuario = model.NombreUsuario,
                Contrasena = model.Contraseña, // Hashear aquí si es necesario
                Rol = model.Rol
            });
        }

        public void ActualizarUsuario(UsuarioViewModel model)
        {
            _usuarioRepository.ActualizarUsuario(new Usuario
            {
                Id = model.Id,
                NombreUsuario = model.NombreUsuario,
                Contrasena = model.Contraseña,
                Rol = model.Rol
            });
        }

        public void EliminarUsuario(int id)
        {
            _usuarioRepository.EliminarUsuario(id);
        }
    }
}