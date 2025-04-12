using System.Collections.Generic;
using DataLayer;

namespace DataLayer.Repositories
{
    public interface IProveedorRepository
    {
        IEnumerable<proveedore> ObtenerProveedores();
        proveedore ObtenerProveedorPorId(int id);
        void AgregarProveedor(proveedore proveedor);
        void ActualizarProveedor(proveedore proveedor);
        bool EliminarProveedor(int id);
        bool TieneTransportesAsociados(int id); // NUEVO: Validación de relación con transporte

        void EliminarProveedorCascade(int id); // Nuevo método para eliminación en cascada
    }
}
