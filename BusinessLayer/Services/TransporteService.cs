using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Repository;
using DataLayer;

namespace BusinessLayer.Services
{
   public class TransporteService
    {
        private readonly ITransporteRepository _transporteRepository;

        public TransporteService()
        {
            _transporteRepository = new TransporteRepository();
        }

        public List<transporte> ObtenerTransporte()
        {
            return _transporteRepository.GetAllTransporte();
        }

        public transporte ObtenerTransportePorId(int id)
        {
            return _transporteRepository.GetTransporteById(id);
        }

        public void AgregarTransporte(transporte transporte)
        {
            _transporteRepository.AddTransporte(transporte);
        }

        public void ActualizarTransporte(transporte transporte)
        {
            _transporteRepository.UpdateTransporte(transporte);
        }
        public bool DeleteTransporte(int id)
        {
            return _transporteRepository.DeleteTransporte(id);
        }

        public void EliminarTransporteCascade(int id)
        {
            _transporteRepository.DeleteTransporteCascade(id);
        }
        public bool ProveedorExiste(int idProveedor)
        {
            return _transporteRepository.ProveedorExiste(idProveedor);
        }


    }
}
