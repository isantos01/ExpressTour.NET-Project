using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BusinessLayer.Repository;
using DataLayer; // Acceso a la entidad "paquetes_excursione"
using DataLayer.Repositories;
using ModelLayer.DTO; // Asegúrate de tener definido PaquetesExcursionesViewModel

namespace BusinessLayer.Services
{
    public class PaquetesExcursionesService
    {
        private readonly IPaquetesExcursionRepository _repository;

        public PaquetesExcursionesService()
        {
            _repository = new PaquetesExcursionesRepository();
        }

        // Devuelve una lista de DTOs de paquetes_excursiones
        public List<PaquetesExcursionesViewModel> ObtenerPaquetesExcursiones()
        {
            var entidades = _repository.ObtenerTodos();
            return entidades.Select(pe => new PaquetesExcursionesViewModel
            {
                Id = pe.id_paquete_excursion,
                IdPaquete = pe.id_paquete,
                IdExcursion = pe.id_excursion
            }).ToList();
        }

        // Obtiene un registro específico por ID y lo mapea a un ViewModel
        public PaquetesExcursionesViewModel ObtenerPaqueteExcursionePorId(int id)
        {
            var entidad = _repository.ObtenerPorId(id);
            if (entidad == null) return null;
            return new PaquetesExcursionesViewModel
            {
                Id = entidad.id_paquete_excursion,
                IdPaquete = entidad.id_paquete,
                IdExcursion = entidad.id_excursion
            };
        }

        // Agrega un registro nuevo (convierte el DTO a entidad)
        public void AgregarPaqueteExcursione(PaquetesExcursionesViewModel model)
        {
            var entidad = new paquetes_excursione
            {
                id_paquete = model.IdPaquete,
                id_excursion = model.IdExcursion
            };
            _repository.Agregar(entidad);
        }

        // Actualiza un registro existente
        public void ActualizarPaqueteExcursione(PaquetesExcursionesViewModel model)
        {
            var entidad = new paquetes_excursione
            {
                id_paquete_excursion = model.Id,
                id_paquete = model.IdPaquete,
                id_excursion = model.IdExcursion
            };
            _repository.Actualizar(entidad);
        }

        // Elimina un registro dado su ID
        public void EliminarPaqueteExcursione(int id)
        {
            _repository.Eliminar(id);
        }

        // Para eliminación en cascada: elimina todos los registros asociados a un paquete
        public void EliminarPaquetesExcursionesPorPaquete(int idPaquete)
        {
            _repository.EliminarPorPaquete(idPaquete);
        }

        public IEnumerable<SelectListItem> ObtenerDropdownPaquetes()
        {
            return _repository.ObtenerDropdownPaquetes();
        }

        public IEnumerable<SelectListItem> ObtenerDropdownExcursiones() 
        { 
            return _repository.ObtenerDropdownExcursiones(); 
        }
    }
}

