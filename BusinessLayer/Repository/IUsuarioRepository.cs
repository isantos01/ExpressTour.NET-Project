using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using ModelLayer.DTO;

namespace BusinessLayer.Repository
{
    public interface IUsuarioRepository
    {
        IEnumerable<Usuario> ObtenerUsuarios();
        Usuario ObtenerUsuarioPorId(int id);
        void AgregarUsuario(Usuario usuario);
        void ActualizarUsuario(Usuario usuario);
        void EliminarUsuario(int id);
    }
}
