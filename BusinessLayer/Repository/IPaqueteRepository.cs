using System.Collections.Generic;
using DataLayer;

namespace DataLayer.Repositories
{
    public interface IPaqueteRepository
    {
        IEnumerable<paquete> ObtenerPaquetes();
        paquete ObtenerPaquetePorId(int id);
        void AgregarPaquete(paquete paquete);
        void ActualizarPaquete(paquete paquete);
        void EliminarPaquete(int id);
        void EliminarPaqueteCascade(int id);


    }
}
