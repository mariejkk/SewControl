using AutoMapper;
using SewControl.Application.Dtos.Usuarios;
using SewControl.Application.Exceptions;
using SewControl.Application.Responses;
using SewControl.Domain.Entities.Usuarios;
using SewControl.Infrastructure.Repositories;

namespace SewControl.Application.Services;

public class UsuariosService
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public UsuariosService(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    public async Task<ApiResponse<List<ClienteDto>>> GetAllClientesAsync()
    {
        var clientes = await _uow.Clientes.GetAllWithEncargosAsync();
        return ApiResponse<List<ClienteDto>>.Ok(_mapper.Map<List<ClienteDto>>(clientes));
    }

    public async Task<ApiResponse<ClienteDto>> GetClienteByIdAsync(int id)
    {
        var cliente = await _uow.Clientes.GetWithEncargosByIdAsync(id)
            ?? throw new NotFoundException("Cliente", id);

        return ApiResponse<ClienteDto>.Ok(_mapper.Map<ClienteDto>(cliente));
    }

    public async Task<ApiResponse<ClienteDto>> CreateClienteAsync(CreateClienteDto dto)
    {
        ClienteValidator.Validate(dto);

        var cliente = _mapper.Map<Cliente>(dto);
        await _uow.Clientes.AddAsync(cliente);
        await _uow.SaveChangesAsync();

        return ApiResponse<ClienteDto>.Ok(_mapper.Map<ClienteDto>(cliente), "Cliente registrado.");
    }

    public async Task<ApiResponse<ClienteDto>> UpdateClienteAsync(int id, CreateClienteDto dto)
    {
        ClienteValidator.Validate(dto);

        var cliente = await _uow.Clientes.GetByIdAsync(id)
            ?? throw new NotFoundException("Cliente", id);

        cliente.Nombre = dto.Nombre;
        cliente.Apellido = dto.Apellido;
        cliente.Telefono = dto.Telefono;
        cliente.Email = dto.Email;
        cliente.Direccion = dto.Direccion;

        _uow.Clientes.Update(cliente);
        await _uow.SaveChangesAsync();

        return ApiResponse<ClienteDto>.Ok(_mapper.Map<ClienteDto>(cliente), "Cliente actualizado.");
    }

    public async Task<ApiResponse<bool>> DeleteClienteAsync(int id)
    {
        var cliente = await _uow.Clientes.GetByIdAsync(id)
            ?? throw new NotFoundException("Cliente", id);

        cliente.IsDeleted = true;
        _uow.Clientes.Update(cliente);
        await _uow.SaveChangesAsync();

        return ApiResponse<bool>.Ok(true, "Cliente eliminado.");
    }
    public async Task<ApiResponse<List<CostureraDto>>> GetAllCosturerasAsync()
    {
        var costureras = await _uow.Costureras.GetAllWithEncargosAsync();
        return ApiResponse<List<CostureraDto>>.Ok(_mapper.Map<List<CostureraDto>>(costureras));
    }

    public async Task<ApiResponse<CostureraDto>> GetCostureraByIdAsync(int id)
    {
        var costurera = await _uow.Costureras.GetWithEncargosByIdAsync(id)
            ?? throw new NotFoundException("Costurera", id);

        return ApiResponse<CostureraDto>.Ok(_mapper.Map<CostureraDto>(costurera));
    }

    public async Task<ApiResponse<CostureraDto>> CreateCostureraAsync(CreateCostureraDto dto)
    {
        CostureraValidator.Validate(dto);

        var costurera = _mapper.Map<Costurera>(dto);
        await _uow.Costureras.AddAsync(costurera);
        await _uow.SaveChangesAsync();

        return ApiResponse<CostureraDto>.Ok(_mapper.Map<CostureraDto>(costurera), "Costurera registrada.");
    }

    public async Task<ApiResponse<bool>> ToggleActivaAsync(int id)
    {
        var costurera = await _uow.Costureras.GetByIdAsync(id)
            ?? throw new NotFoundException("Costurera", id);

        costurera.Activa = !costurera.Activa;
        _uow.Costureras.Update(costurera);
        await _uow.SaveChangesAsync();

        var estado = costurera.Activa ? "activada" : "desactivada";
        return ApiResponse<bool>.Ok(costurera.Activa, $"Costurera {estado}.");
    }

    public async Task<ApiResponse<bool>> DeleteCostureraAsync(int id)
    {
        var costurera = await _uow.Costureras.GetByIdAsync(id)
            ?? throw new NotFoundException("Costurera", id);

        costurera.IsDeleted = true;
        _uow.Costureras.Update(costurera);
        await _uow.SaveChangesAsync();

        return ApiResponse<bool>.Ok(true, "Costurera eliminada.");
    }
}
