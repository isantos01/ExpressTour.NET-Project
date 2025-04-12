using System.Collections.Generic;
using System.Linq;
using DataLayer;
using DataLayer.Repositories;
using ModelLayer.DTO;

namespace BusinessLayer.Services
{
    public class PaqueteService
    {
        private readonly IPaqueteRepository _paqueteRepository;

        public PaqueteService()
        {
            _paqueteRepository = new PaqueteRepository();
        }

        public List<PaqueteViewModel> ObtenerPaquetes()
        {
            var paquetes = _paqueteRepository.ObtenerPaquetes();
            return paquetes.Select(p => new PaqueteViewModel
            {
                Id = p.id_paquete,
                Nombre = p.nombre,
                Descripcion = p.descripcion,
                Precio = p.precio,
                DuracionDias = p.duracion_dias,
                IdTransporte = (int)p.id_transporte
            }).ToList();
        }

        public PaqueteViewModel ObtenerPaquetePorId(int id)
        {
            var p = _paqueteRepository.ObtenerPaquetePorId(id);
            if (p == null) return null;

            return new PaqueteViewModel
            {
                Id = p.id_paquete,
                Nombre = p.nombre,
                Descripcion = p.descripcion,
                Precio = p.precio,
                DuracionDias = p.duracion_dias,
                IdTransporte = (int)p.id_transporte
            };
        }

        public void AgregarPaquete(PaqueteViewModel model)
        {
            var paquete = new paquete
            {
                nombre = model.Nombre,
                descripcion = model.Descripcion,
                precio = model.Precio,
                duracion_dias = model.DuracionDias,
                id_transporte = model.IdTransporte
            };

            _paqueteRepository.AgregarPaquete(paquete);
        }

        public void ActualizarPaquete(PaqueteViewModel model)
        {
            var paquete = new paquete
            {
                id_paquete = model.Id,
                nombre = model.Nombre,
                descripcion = model.Descripcion,
                precio = model.Precio,
                duracion_dias = model.DuracionDias,
                id_transporte = model.IdTransporte
            };

            _paqueteRepository.ActualizarPaquete(paquete);
        }

        public void EliminarPaquete(int id)
        {
            _paqueteRepository.EliminarPaquete(id);
        }

        public void EliminarPaqueteCascade(int id)
        {
            _paqueteRepository.EliminarPaqueteCascade(id);
        }
    }
}
