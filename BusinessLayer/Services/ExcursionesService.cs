using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Repository;
using DataLayer;
using ModelLayer.DTO;

namespace BusinessLayer.Services
{
   public class ExcursionesService
    {
        private readonly IExcursionesRepository _excursionesRepository;

        public ExcursionesService()
        {
            _excursionesRepository = new ExcursionesRepository();
        }

        public List<ExcursionesViewModel> ObtenerExcursiones()
        {
            var lista = _excursionesRepository.ObtenerExcursiones();
            return lista.Select(e => new ExcursionesViewModel
            {
                Id = e.id_excursion,
                Nombre = e.nombre,
                Descripcion = e.descripcion,
                Precio = e.precio,
                Capacidad = e.capacidad
            }).ToList();
        }

        public ExcursionesViewModel ObtenerExcursionPorId(int id)
        {
            var e = _excursionesRepository.ObtenerExcursionPorId(id);
            if (e == null) return null;

            return new ExcursionesViewModel
            {
                Id = e.id_excursion,
                Nombre = e.nombre,
                Descripcion = e.descripcion,
                Precio = e.precio,
                Capacidad = e.capacidad
            };
        }

        public void AgregarExcursion(ExcursionesViewModel model)
        {
            var entity = new excursione
            {
                nombre = model.Nombre,
                descripcion = model.Descripcion,
                precio = model.Precio,
                capacidad = model.Capacidad
            };

            _excursionesRepository.AgregarExcursion(entity);
        }

        public void ActualizarExcursion(ExcursionesViewModel model)
        {
            var entity = new excursione
            {
                id_excursion = model.Id,
                nombre = model.Nombre,
                descripcion = model.Descripcion,
                precio = model.Precio,
                capacidad = model.Capacidad
            };

            _excursionesRepository.ActualizarExcursion(entity);
        }

        public void EliminarExcursion(int id)
        {
            _excursionesRepository.EliminarExcursion(id);
        }
    }

}
