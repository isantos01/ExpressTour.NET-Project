using System.Collections.Generic;
using System.Linq;

namespace DataLayer.Repositories
{
    public class ProveedorRepository : IProveedorRepository
    {
        private readonly ExpressTourDataContext _context;

        public ProveedorRepository()
        {
            _context = new ExpressTourDataContext();
        }

        public IEnumerable<proveedore> ObtenerProveedores()
        {
            return _context.proveedores.ToList();
        }

        public proveedore ObtenerProveedorPorId(int id)
        {
            return _context.proveedores.FirstOrDefault(p => p.id_proveedor == id);
        }

        public void AgregarProveedor(proveedore proveedor)
        {
            _context.proveedores.InsertOnSubmit(proveedor);
            _context.SubmitChanges();
        }

        public void ActualizarProveedor(proveedore proveedor)
        {
            var existente = _context.proveedores.FirstOrDefault(p => p.id_proveedor == proveedor.id_proveedor);
            if (existente != null)
            {
                existente.nombre = proveedor.nombre;
                existente.contacto = proveedor.contacto;
                existente.telefono = proveedor.telefono;

                _context.SubmitChanges();
            }
        }

        public bool EliminarProveedor(int id)
        {
            var proveedor = _context.proveedores.FirstOrDefault(p => p.id_proveedor == id);
            if (proveedor != null)
            {
                _context.proveedores.DeleteOnSubmit(proveedor);
                _context.SubmitChanges();
                return true;
            }
            return false;
        }

        public bool TieneTransportesAsociados(int id)
        {
            return _context.transportes.Any(t => t.id_proveedor == id);
        }

        public void EliminarProveedorCascade(int id)
        {
            var proveedor = _context.proveedores.FirstOrDefault(p => p.id_proveedor == id);
            if (proveedor == null) return;

            var transportes = _context.transportes.Where(t => t.id_proveedor == id).ToList();

            foreach (var transporte in transportes)
            {
                var paquetes = _context.paquetes.Where(p => p.id_transporte == transporte.id_transporte).ToList();

                foreach (var paquete in paquetes)
                {
                    // Eliminar facturas asociadas a reservas del paquete
                    var reservas = _context.reservas.Where(r => r.id_paquete == paquete.id_paquete).ToList();
                    foreach (var reserva in reservas)
                    {
                        var facturas = _context.facturas.Where(f => f.id_reserva == reserva.id_reserva).ToList();
                        _context.facturas.DeleteAllOnSubmit(facturas);
                    }

                    // Eliminar reservas
                    _context.reservas.DeleteAllOnSubmit(reservas);

                    // Eliminar opiniones del paquete
                    var opiniones = _context.opiniones_clientes.Where(o => o.id_paquete == paquete.id_paquete).ToList();
                    _context.opiniones_clientes.DeleteAllOnSubmit(opiniones);

                    // Eliminar relaciones del paquete con excursiones
                    var relacionesExcursiones = _context.paquetes_excursiones
                        .Where(pe => pe.id_paquete == paquete.id_paquete)
                        .ToList();
                    _context.paquetes_excursiones.DeleteAllOnSubmit(relacionesExcursiones);

                    // Finalmente, eliminar el paquete
                    _context.paquetes.DeleteOnSubmit(paquete);
                }

                // Eliminar el transporte
                _context.transportes.DeleteOnSubmit(transporte);
            }

            // Eliminar proveedor
            _context.proveedores.DeleteOnSubmit(proveedor);

            // Guardar cambios en cascada
            _context.SubmitChanges();
        }

    }
}
