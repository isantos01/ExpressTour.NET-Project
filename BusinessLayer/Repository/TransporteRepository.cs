using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;

namespace BusinessLayer.Repository
{
    public class TransporteRepository : ITransporteRepository
    {
        private readonly ExpressTourDataContext _db;

        public TransporteRepository()
        {
            _db = new ExpressTourDataContext();
        }

        public List<transporte> GetAllTransporte()
        {
            return _db.transportes.ToList();
        }

        public transporte GetTransporteById(int id)
        {
            return _db.transportes.FirstOrDefault(c => c.id_transporte == id);
        }

        public void AddTransporte(transporte transporte)
        {
            _db.transportes.InsertOnSubmit(transporte);
            _db.SubmitChanges();
        }

        public void UpdateTransporte(transporte transporte)
        {
            var existing = GetTransporteById(transporte.id_transporte);
            if (existing != null)
            {
                existing.tipo = transporte.tipo;
                existing.capacidad = transporte.capacidad;
                existing.id_proveedor = transporte.id_proveedor;
                _db.SubmitChanges();
            }
        }

        public bool DeleteTransporte(int id)
        {
            var transporteToDelete = GetTransporteById(id);
            if (transporteToDelete != null)
            {
                // Verifica si existen paquetes asociados al transporte
                bool existepaquete = _db.paquetes.Any(r => r.id_transporte == id);
                // Verifica si existen paquetes asociados (a través de Paquetes_excursiones)
                bool existeexcursion  = _db.paquetes_excursiones.Any(f => _db.paquetes.Any(r => r.id_transporte == id && r.id_paquete == f.id_paquete));

                if (existepaquete || existeexcursion)
                {
                    // Si tiene relaciones, no se elimina de forma parcial
                    return false;
                }

                // Si no tiene relaciones, eliminar normalmente
                _db.transportes.DeleteOnSubmit(transporteToDelete);
                _db.SubmitChanges();
                return true;
            }
            return false;
        }
        public void DeleteTransporteCascade(int id)
        {
            var transporteToDelete = GetTransporteById(id);
            if (transporteToDelete == null) return;

            // Buscar los paquetes asociados al transporte
            var paquetes = _db.paquetes.Where(p => p.id_transporte == id).ToList();

            foreach (var paquete in paquetes)
            {
                // 1. Eliminar opiniones relacionadas al paquete
                var opiniones = _db.opiniones_clientes.Where(o => o.id_paquete == paquete.id_paquete).ToList();
                if (opiniones.Any())
                {
                    _db.opiniones_clientes.DeleteAllOnSubmit(opiniones);
                }

                // 2. Eliminar reservas del paquete
                var reservas = _db.reservas.Where(r => r.id_paquete == paquete.id_paquete).ToList();
                foreach (var reserva in reservas)
                {
                    // 2.1. Eliminar facturas asociadas a la reserva
                    var facturas = _db.facturas.Where(f => f.id_reserva == reserva.id_reserva).ToList();
                    if (facturas.Any())
                    {
                        _db.facturas.DeleteAllOnSubmit(facturas);
                    }
                }

                if (reservas.Any())
                {
                    _db.reservas.DeleteAllOnSubmit(reservas);
                }

                // 3. Eliminar relaciones con excursiones (paquetes_excursiones)
                var excursiones = _db.paquetes_excursiones.Where(pe => pe.id_paquete == paquete.id_paquete).ToList();
                if (excursiones.Any())
                {
                    _db.paquetes_excursiones.DeleteAllOnSubmit(excursiones);
                }

                // 4. Finalmente, eliminar el paquete
                _db.paquetes.DeleteOnSubmit(paquete);
            }

            // 5. Finalmente, eliminar el transporte
            _db.transportes.DeleteOnSubmit(transporteToDelete);

            // 6. Guardar cambios
            _db.SubmitChanges();
        }

        public bool ProveedorExiste(int idProveedor)
        {
            // tabla proveesores _db.proveedores
            return _db.proveedores.Any(p => p.id_proveedor == idProveedor);
        }


    }
}
