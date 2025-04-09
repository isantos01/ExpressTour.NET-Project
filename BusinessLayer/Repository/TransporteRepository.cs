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
            if (transporteToDelete != null)
            {
                // Eliminar las paquetes asociadas
                var paquetesToDelete = _db.paquetes.Where(r => r.id_transporte == id).ToList();
                foreach (var paquete in paquetesToDelete)
                {
                    // Eliminar las paquete_excursiones asociadas a cada paquete
                    var excursionesToDelete = _db.paquetes_excursiones.Where(f => f.id_paquete == paquete.id_paquete).ToList();
                    if (excursionesToDelete.Any())
                    {
                        _db.paquetes_excursiones.DeleteAllOnSubmit(excursionesToDelete);
                    }
                }
                if (paquetesToDelete.Any())
                {
                    _db.paquetes.DeleteAllOnSubmit(paquetesToDelete);
                }
                _db.transportes.DeleteOnSubmit(transporteToDelete);
                _db.SubmitChanges();
            }
        }
        public bool ProveedorExiste(int idProveedor)
        {
            // tabla proveesores _db.proveedores
            return _db.proveedores.Any(p => p.id_proveedor == idProveedor);
        }


    }
}
