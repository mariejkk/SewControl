using AutoMapper;
using SewControl.Application.Dtos.Encargos;
using SewControl.Application.Responses;
using SewControl.Domain.Entities.Encargos;
using SewControl.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


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
        var dtos = _mapper.Map<List<EncargoDto>>(encargos);
        return ApiResponse<List<EncargoDto>>.Ok(dtos);
    }

    public async Task<ApiResponse<EncargoDto>> GetByIdAsync(int id)
    {
        var encargo = await _uow.Encargos.GetWithDetailsByIdAsync(id);
        if (encargo == null || encargo.IsDeleted)
            return ApiResponse<EncargoDto>.Fail("Encargo no encontrado.");

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

    public async Task<ApiResponse<EncargoDto>> CreateAsync(CreateEncargoDto dto)
    {
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
        var encargo = await _uow.Encargos.GetByIdAsync(id);
        if (encargo == null || encargo.IsDeleted)
            return ApiResponse<EncargoDto>.Fail("Encargo no encontrado.");

        encargo.Estado = dto.Estado;
        if (dto.FechaEntregaEstimada.HasValue)
            encargo.FechaEntregaEstimada = dto.FechaEntregaEstimada.Value;
        if (dto.FechaEntregaReal.HasValue)
            encargo.FechaEntregaReal = dto.FechaEntregaReal.Value;
        if (dto.PrecioTotal.HasValue)
            encargo.PrecioTotal = dto.PrecioTotal.Value;
        if (dto.Observaciones != null)
            encargo.Observaciones = dto.Observaciones;
        if (dto.CostureraId.HasValue)
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
        var encargo = await _uow.Encargos.GetByIdAsync(id);
        if (encargo == null || encargo.IsDeleted)
            return ApiResponse<bool>.Fail("Encargo no encontrado.");

        encargo.IsDeleted = true;
        _uow.Encargos.Update(encargo);
        await _uow.SaveChangesAsync();

        return ApiResponse<bool>.Ok(true, "Encargo eliminado.");
    }

    public async Task<ApiResponse<PrendaDto>> AddPrendaAsync(CreatePrendaDto dto)
    {
        var encargo = await _uow.Encargos.GetByIdAsync(dto.EncargoId);
        if (encargo == null || encargo.IsDeleted)
            return ApiResponse<PrendaDto>.Fail("Encargo no encontrado.");

        var prenda = _mapper.Map<Prenda>(dto);
        await _uow.Prendas.AddAsync(prenda);
        await _uow.SaveChangesAsync();

        return ApiResponse<PrendaDto>.Ok(_mapper.Map<PrendaDto>(prenda), "Prenda agregada.");
    }

    public async Task<ApiResponse<bool>> DeletePrendaAsync(int prendaId)
    {
        var prenda = await _uow.Prendas.GetByIdAsync(prendaId);
        if (prenda == null || prenda.IsDeleted)
            return ApiResponse<bool>.Fail("Prenda no encontrada.");

        prenda.IsDeleted = true;
        _uow.Prendas.Update(prenda);
        await _uow.SaveChangesAsync();

        return ApiResponse<bool>.Ok(true, "Prenda eliminada.");
    }
    public async Task<ApiResponse<ArregloDto>> AddArregloAsync(CreateArregloDto dto)
    {
        var encargo = await _uow.Encargos.GetByIdAsync(dto.EncargoId);
        if (encargo == null || encargo.IsDeleted)
            return ApiResponse<ArregloDto>.Fail("Encargo no encontrado.");

        var arreglo = _mapper.Map<Arreglo>(dto);
        await _uow.Arreglos.AddAsync(arreglo);
        await _uow.SaveChangesAsync();

        return ApiResponse<ArregloDto>.Ok(_mapper.Map<ArregloDto>(arreglo), "Arreglo registrado.");
    }

    public async Task<ApiResponse<bool>> DeleteArregloAsync(int arregloId)
    {
        var arreglo = await _uow.Arreglos.GetByIdAsync(arregloId);
        if (arreglo == null || arreglo.IsDeleted)
            return ApiResponse<bool>.Fail("Arreglo no encontrado.");

        arreglo.IsDeleted = true;
        _uow.Arreglos.Update(arreglo);
        await _uow.SaveChangesAsync();

        return ApiResponse<bool>.Ok(true, "Arreglo eliminado.");
    }
}