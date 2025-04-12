using System.Collections.Generic;
using System.Linq;

namespace DataLayer.Repositories
{
    public class PaqueteRepository : IPaqueteRepository
    {
        private readonly ExpressTourDataContext _context;

        public PaqueteRepository()
        {
            _context = new ExpressTourDataContext();
        }

        public IEnumerable<paquete> ObtenerPaquetes()
        {
            return _context.paquetes.ToList();
        }

        public paquete ObtenerPaquetePorId(int id)
        {
            return _context.paquetes.FirstOrDefault(p => p.id_paquete == id);
        }

        public void AgregarPaquete(paquete paquete)
        {
            _context.paquetes.InsertOnSubmit(paquete);
            _context.SubmitChanges();
        }

        public void ActualizarPaquete(paquete paquete)
        {
            var existente = _context.paquetes.FirstOrDefault(p => p.id_paquete == paquete.id_paquete);
            if (existente != null)
            {
                existente.nombre = paquete.nombre;
                existente.descripcion = paquete.descripcion;
                existente.precio = paquete.precio;
                existente.duracion_dias = paquete.duracion_dias;
                existente.id_transporte = paquete.id_transporte;

                _context.SubmitChanges();
            }
        }

        public void EliminarPaquete(int id)
        {
            var paquete = _context.paquetes.FirstOrDefault(p => p.id_paquete == id);
            if (paquete != null)
            {
                _context.paquetes.DeleteOnSubmit(paquete);
                _context.SubmitChanges();
            }
        }

        public void EliminarPaqueteCascade(int id)
        {
            var paquete = _context.paquetes.FirstOrDefault(p => p.id_paquete == id);
            if (paquete != null)
            {
                // Eliminar las opiniones asociadas al paquete
                var opiniones = _context.opiniones_clientes.Where(o => o.id_paquete == id).ToList();
                _context.opiniones_clientes.DeleteAllOnSubmit(opiniones);

                // Eliminar las excursiones asociadas al paquete
                var paquetesExcursiones = _context.paquetes_excursiones.Where(pe => pe.id_paquete == id).ToList();
                _context.paquetes_excursiones.DeleteAllOnSubmit(paquetesExcursiones);

                // Eliminar el paquete
                _context.paquetes.DeleteOnSubmit(paquete);

                // Guardar cambios
                _context.SubmitChanges();
            }
        }
    }
}
