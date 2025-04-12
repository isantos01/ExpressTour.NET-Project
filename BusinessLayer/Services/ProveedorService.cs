using DataLayer.Repositories;
using DataLayer;
using ModelLayer.DTO;
using System.Collections.Generic;
using System.Linq;

public class ProveedorService
{
    private readonly IProveedorRepository _repository;

    public ProveedorService()
    {
        _repository = new ProveedorRepository();
    }

    public List<ProveedorViewModel> ObtenerProveedores()
    {
        var proveedores = _repository.ObtenerProveedores();
        return proveedores.Select(p => new ProveedorViewModel
        {
            Id = p.id_proveedor,
            NombreProveedor = p.nombre,
            NombreContacto = p.contacto,
            Telefono = p.telefono
        }).ToList();
    }

    public ProveedorViewModel ObtenerProveedorPorId(int id)
    {
        var entidad = _repository.ObtenerProveedorPorId(id);
        if (entidad == null) return null;

        return new ProveedorViewModel
        {
            Id = entidad.id_proveedor,
            NombreProveedor = entidad.nombre,
            NombreContacto = entidad.contacto,
            Telefono = entidad.telefono
        };
    }

    public void AgregarProveedor(ProveedorViewModel model)
    {
        var proveedor = new proveedore
        {
            nombre = model.NombreProveedor,
            contacto = model.NombreContacto,
            telefono = model.Telefono
        };

        _repository.AgregarProveedor(proveedor);
    }

    public void ActualizarProveedor(ProveedorViewModel model)
    {
        var proveedor = new proveedore
        {
            id_proveedor = model.Id,
            nombre = model.NombreProveedor,
            contacto = model.NombreContacto,
            telefono = model.Telefono
        };

        _repository.ActualizarProveedor(proveedor);
    }

    public void EliminarProveedor(int id)
    {
        _repository.EliminarProveedor(id);
    }

    public void EliminarProveedorCascade(int id)
    {
        _repository.EliminarProveedorCascade(id);
    }

    public bool TieneTransportesAsociados(int id)
    {
        return _repository.TieneTransportesAsociados(id); // Si ya tienes este método en el repositorio
    }
}
