using BusinessLayer.Repository;
using DataLayer;
using System.Collections.Generic;

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

    public int AgregarTransporte(transporte trans)
    {
        // Llamamos al método del repositorio que debe encargarse de insertar el transporte
        _transporteRepository.AddTransporte(trans);
        return trans.id_transporte;
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
