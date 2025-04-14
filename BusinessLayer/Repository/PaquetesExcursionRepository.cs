using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BusinessLayer.Repository;
using DataLayer;

namespace DataLayer.Repositories
{
    public class PaquetesExcursionesRepository : IPaquetesExcursionRepository
    {
        private readonly ExpressTourDataContext _context;

        public PaquetesExcursionesRepository()
        {
            _context = new ExpressTourDataContext();
        }

        // Retorna todos los registros de la tabla intermedia
        public IEnumerable<paquetes_excursione> ObtenerTodos()
        {
            return _context.paquetes_excursiones.ToList();
        }

        // Retorna un registro específico dado su ID
        public paquetes_excursione ObtenerPorId(int id)
        {
            return _context.paquetes_excursiones
                           .FirstOrDefault(pe => pe.id_paquete_excursion == id);
        }

        // Agrega un nuevo registro a la tabla intermedia
        public void Agregar(paquetes_excursione entidad)
        {
            _context.paquetes_excursiones.InsertOnSubmit(entidad);
            _context.SubmitChanges();
        }

        // Actualiza un registro existente de la tabla intermedia
        public void Actualizar(paquetes_excursione entidad)
        {
            var existente = _context.paquetes_excursiones
                                     .FirstOrDefault(pe => pe.id_paquete_excursion == entidad.id_paquete_excursion);
            if (existente != null)
            {
                existente.id_paquete = entidad.id_paquete;
                existente.id_excursion = entidad.id_excursion;
                _context.SubmitChanges();
            }
        }

        // Elimina un registro de la tabla intermedia por su ID
        public void Eliminar(int id)
        {
            var entidad = _context.paquetes_excursiones
                                  .FirstOrDefault(pe => pe.id_paquete_excursion == id);
            if (entidad != null)
            {
                _context.paquetes_excursiones.DeleteOnSubmit(entidad);
                _context.SubmitChanges();
            }
        }

        // Elimina en cascada todos los registros asociados a un paquete específico
        public void EliminarPorPaquete(int idPaquete)
        {
            var entidades = _context.paquetes_excursiones
                                    .Where(pe => pe.id_paquete == idPaquete)
                                    .ToList();
            if (entidades.Any())
            {
                _context.paquetes_excursiones.DeleteAllOnSubmit(entidades);
                _context.SubmitChanges();
            }
        }

        public IEnumerable<SelectListItem> ObtenerDropdownPaquetes()
        {
            return _context.paquetes.Select(p => new SelectListItem { Value = p.id_paquete.ToString(), Text = p.nombre }).ToList();
        }

        public IEnumerable<SelectListItem> ObtenerDropdownExcursiones()
        {
            return _context.excursiones.Select(e => new SelectListItem { Value = e.id_excursion.ToString(), Text = e.nombre }).ToList();
        }
    }
}
