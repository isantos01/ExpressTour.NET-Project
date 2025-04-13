using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;

namespace BusinessLayer.Repository
{
    public class ExcursionesRepository: IExcursionesRepository
    {
        private readonly ExpressTourDataContext _context;

        public ExcursionesRepository()
        {
            _context = new ExpressTourDataContext();
        }

        public IEnumerable<excursione> ObtenerExcursiones()
        {
            return _context.excursiones.ToList();
        }

        public excursione ObtenerExcursionPorId(int id)
        {
            return _context.excursiones.FirstOrDefault(e => e.id_excursion == id);
        }

        public void AgregarExcursion(excursione excursion)
        {
            _context.excursiones.InsertOnSubmit(excursion);
            _context.SubmitChanges();
        }

        public void ActualizarExcursion(excursione excursion)
        {
            var existente = _context.excursiones.FirstOrDefault(e => e.id_excursion == excursion.id_excursion);
            if (existente != null)
            {
                existente.nombre = excursion.nombre;
                existente.descripcion = excursion.descripcion;
                existente.precio = excursion.precio;
                existente.capacidad = excursion.capacidad;
                _context.SubmitChanges();
            }
        }

        public void EliminarExcursion(int id)
        {
            var excursion = _context.excursiones.FirstOrDefault(e => e.id_excursion == id);
            if (excursion != null)
            {
                _context.excursiones.DeleteOnSubmit(excursion);
                _context.SubmitChanges();
            }
        }
    }

}