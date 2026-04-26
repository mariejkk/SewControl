using AutoMapper;
using SewControl.Application.Dtos.Encargos;
using SewControl.Application.Exceptions;
using SewControl.Application.Responses;
using SewControl.Application.Validators;
using SewControl.Domain.Entities.Encargos;
using SewControl.Infrastructure.Repositories;

namespace SewControl.Application.Services;

public class EncargoService
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public EncargoService(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    public async Task<ApiResponse<List<EncargoDto>>> GetAllAsync()
    {
        var encargos = await _uow.Encargos.GetAllWithDetailsAsync();
        return ApiResponse<List<EncargoDto>>.Ok(_mapper.Map<List<EncargoDto>>(encargos));
    }

    public async Task<ApiResponse<EncargoDto>> GetByIdAsync(int id)
    {
        var encargo = await _uow.Encargos.GetWithDetailsByIdAsync(id)
            ?? throw new NotFoundException("Encargo", id);

        return ApiResponse<EncargoDto>.Ok(_mapper.Map<EncargoDto>(encargo));
    }

    public async Task<ApiResponse<List<EncargoDto>>> GetByClienteAsync(int clienteId)
    {
        var encargos = await _uow.Encargos.GetByClienteAsync(clienteId);
        return ApiResponse<List<EncargoDto>>.Ok(_mapper.Map<List<EncargoDto>>(encargos));
    }

    public async Task<ApiResponse<List<EncargoDto>>> GetByCostureraAsync(int costureraId)
    {
        var encargos = await _uow.Encargos.GetByCostureraAsync(costureraId);
        return ApiResponse<List<EncargoDto>>.Ok(_mapper.Map<List<EncargoDto>>(encargos));
    }

    public async Task<ApiResponse<List<EncargoDto>>> GetByEstadoAsync(EstadoEncargo estado)
    {
        var encargos = await _uow.Encargos.GetByEstadoAsync(estado);
        return ApiResponse<List<EncargoDto>>.Ok(_mapper.Map<List<EncargoDto>>(encargos));
    }

    public async Task<ApiResponse<EncargoDto>> UpdateEncargoAsync(int id, CreateEncargoDto dto)
    {
        EncargoValidator.Validate(dto);

        var encargo = await _uow.Encargos.GetByIdAsync(id)
            ?? throw new NotFoundException("Encargo", id);

        encargo.Tipo = dto.Tipo;
        encargo.FechaEntregaEstimada = dto.FechaEntregaEstimada;
        encargo.PrecioTotal = dto.PrecioTotal;
        encargo.Anticipo = dto.Anticipo;
        encargo.Observaciones = dto.Observaciones;
        if (dto.CostureraId > 0) encargo.CostureraId = dto.CostureraId;
        if (dto.ClienteId > 0) encargo.ClienteId = dto.ClienteId;

        _uow.Encargos.Update(encargo);
        await _uow.SaveChangesAsync();

        var updated = await _uow.Encargos.GetWithDetailsByIdAsync(id);
        return ApiResponse<EncargoDto>.Ok(_mapper.Map<EncargoDto>(updated!), "Encargo actualizado.");
    }

    public async Task<ApiResponse<EncargoDto>> CreateAsync(CreateEncargoDto dto)
    {
        EncargoValidator.Validate(dto);

        var encargo = _mapper.Map<Encargo>(dto);
        encargo.FechaRecepcion = DateTime.UtcNow;
        encargo.Estado = EstadoEncargo.Pendiente;

        await _uow.Encargos.AddAsync(encargo);
        await _uow.SaveChangesAsync();

        var created = await _uow.Encargos.GetWithDetailsByIdAsync(encargo.Id);
        return ApiResponse<EncargoDto>.Ok(_mapper.Map<EncargoDto>(created!), "Encargo creado exitosamente.");
    }

    public async Task<ApiResponse<EncargoDto>> UpdateEstadoAsync(int id, UpdateEncargoDto dto)
    {
        EncargoValidator.ValidateUpdate(dto);

        var encargo = await _uow.Encargos.GetByIdAsync(id)
            ?? throw new NotFoundException("Encargo", id);

        encargo.Estado = dto.Estado;

        if (dto.FechaEntregaEstimada.HasValue)
            encargo.FechaEntregaEstimada = dto.FechaEntregaEstimada.Value;
        if (dto.FechaEntregaReal.HasValue)
            encargo.FechaEntregaReal = dto.FechaEntregaReal.Value;
        if (dto.PrecioTotal.HasValue && dto.PrecioTotal > 0)
            encargo.PrecioTotal = dto.PrecioTotal.Value;
        if (dto.Observaciones != null)
            encargo.Observaciones = dto.Observaciones;
        if (dto.CostureraId.HasValue && dto.CostureraId > 0)
            encargo.CostureraId = dto.CostureraId.Value;

        if (dto.Estado == EstadoEncargo.Entregado && encargo.FechaEntregaReal == null)
            encargo.FechaEntregaReal = DateTime.UtcNow;

        _uow.Encargos.Update(encargo);
        await _uow.SaveChangesAsync();

        var updated = await _uow.Encargos.GetWithDetailsByIdAsync(id);
        return ApiResponse<EncargoDto>.Ok(_mapper.Map<EncargoDto>(updated!), "Encargo actualizado.");
    }

    public async Task<ApiResponse<bool>> DeleteAsync(int id)
    {
        var encargo = await _uow.Encargos.GetByIdAsync(id)
            ?? throw new NotFoundException("Encargo", id);

        encargo.IsDeleted = true;
        _uow.Encargos.Update(encargo);
        await _uow.SaveChangesAsync();

        return ApiResponse<bool>.Ok(true, "Encargo eliminado.");
    }

    public async Task<ApiResponse<PrendaDto>> AddPrendaAsync(CreatePrendaDto dto)
    {
        var encargo = await _uow.Encargos.GetByIdAsync(dto.EncargoId)
            ?? throw new NotFoundException("Encargo", dto.EncargoId);

        var prenda = _mapper.Map<Prenda>(dto);
        await _uow.Prendas.AddAsync(prenda);
        await _uow.SaveChangesAsync();

        return ApiResponse<PrendaDto>.Ok(_mapper.Map<PrendaDto>(prenda), "Prenda agregada.");
    }

    public async Task<ApiResponse<bool>> DeletePrendaAsync(int prendaId)
    {
        var prenda = await _uow.Prendas.GetByIdAsync(prendaId)
            ?? throw new NotFoundException("Prenda", prendaId);

        prenda.IsDeleted = true;
        _uow.Prendas.Update(prenda);
        await _uow.SaveChangesAsync();

        return ApiResponse<bool>.Ok(true, "Prenda eliminada.");
    }

    public async Task<ApiResponse<ArregloDto>> AddArregloAsync(CreateArregloDto dto)
    {
        var encargo = await _uow.Encargos.GetByIdAsync(dto.EncargoId)
            ?? throw new NotFoundException("Encargo", dto.EncargoId);

        var arreglo = _mapper.Map<Arreglo>(dto);
        await _uow.Arreglos.AddAsync(arreglo);
        await _uow.SaveChangesAsync();

        return ApiResponse<ArregloDto>.Ok(_mapper.Map<ArregloDto>(arreglo), "Arreglo registrado.");
    }

    public async Task<ApiResponse<bool>> DeleteArregloAsync(int arregloId)
    {
        var arreglo = await _uow.Arreglos.GetByIdAsync(arregloId)
            ?? throw new NotFoundException("Arreglo", arregloId);

        arreglo.IsDeleted = true;
        _uow.Arreglos.Update(arreglo);
        await _uow.SaveChangesAsync();

        return ApiResponse<bool>.Ok(true, "Arreglo eliminado.");
    }
}