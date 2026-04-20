using Microsoft.EntityFrameworkCore;
using SewControl.Domain.Entities.Encargos;
using SewControl.Domain.Entities.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SewControl.Infrastructure.Repositories;

public interface IEncargoRepository : IGenericRepository<Encargo>
{
    Task<IEnumerable<Encargo>> GetAllWithDetailsAsync();
    Task<Encargo?> GetWithDetailsByIdAsync(int id);
    Task<IEnumerable<Encargo>> GetByClienteAsync(int clienteId);
    Task<IEnumerable<Encargo>> GetByCostureraAsync(int costureraId);
    Task<IEnumerable<Encargo>> GetByEstadoAsync(EstadoEncargo estado);
}

public interface IClienteRepository : IGenericRepository<Cliente>
{
    Task<IEnumerable<Cliente>> GetAllWithEncargosAsync();
    Task<Cliente?> GetWithEncargosByIdAsync(int id);
}

public interface ICostureraRepository : IGenericRepository<Costurera>
{
    Task<IEnumerable<Costurera>> GetAllWithEncargosAsync();
    Task<Costurera?> GetWithEncargosByIdAsync(int id);
}

public interface IUnitOfWork : IDisposable
{
    IEncargoRepository Encargos { get; }
    IClienteRepository Clientes { get; }
    ICostureraRepository Costureras { get; }
    IGenericRepository<Prenda> Prendas { get; }
    IGenericRepository<Arreglo> Arreglos { get; }
    Task<int> SaveChangesAsync();
}
public class EncargoRepository : GenericRepository<Encargo>, IEncargoRepository
{
    public EncargoRepository(DbContext ctx) : base(ctx) { }

    public async Task<IEnumerable<Encargo>> GetAllWithDetailsAsync() =>
        await _dbSet
            .Include(e => e.Cliente)
            .Include(e => e.Costurera)
            .Include(e => e.Prendas.Where(p => !p.IsDeleted))
            .Include(e => e.Arreglos.Where(a => !a.IsDeleted))
            .Where(e => !e.IsDeleted)
            .OrderByDescending(e => e.FechaRecepcion)
            .ToListAsync();

    public async Task<Encargo?> GetWithDetailsByIdAsync(int id) =>
        await _dbSet
            .Include(e => e.Cliente)
            .Include(e => e.Costurera)
            .Include(e => e.Prendas.Where(p => !p.IsDeleted))
            .Include(e => e.Arreglos.Where(a => !a.IsDeleted))
            .FirstOrDefaultAsync(e => e.Id == id && !e.IsDeleted);

    public async Task<IEnumerable<Encargo>> GetByClienteAsync(int clienteId) =>
        await _dbSet
            .Include(e => e.Costurera)
            .Where(e => e.ClienteId == clienteId && !e.IsDeleted)
            .OrderByDescending(e => e.FechaRecepcion)
            .ToListAsync();

    public async Task<IEnumerable<Encargo>> GetByCostureraAsync(int costureraId) =>
        await _dbSet
            .Include(e => e.Cliente)
            .Where(e => e.CostureraId == costureraId && !e.IsDeleted)
            .OrderByDescending(e => e.FechaRecepcion)
            .ToListAsync();

    public async Task<IEnumerable<Encargo>> GetByEstadoAsync(EstadoEncargo estado) =>
        await _dbSet
            .Include(e => e.Cliente)
            .Include(e => e.Costurera)
            .Where(e => e.Estado == estado && !e.IsDeleted)
            .OrderBy(e => e.FechaEntregaEstimada)
            .ToListAsync();
}

public class ClienteRepository : GenericRepository<Cliente>, IClienteRepository
{
    public ClienteRepository(DbContext ctx) : base(ctx) { }

    public async Task<IEnumerable<Cliente>> GetAllWithEncargosAsync() =>
        await _dbSet
            .Include(c => c.Encargos)
            .Where(c => !c.IsDeleted)
            .OrderBy(c => c.Apellido)
            .ToListAsync();

    public async Task<Cliente?> GetWithEncargosByIdAsync(int id) =>
        await _dbSet
            .Include(c => c.Encargos)
            .FirstOrDefaultAsync(c => c.Id == id && !c.IsDeleted);
}

public class CostureraRepository : GenericRepository<Costurera>, ICostureraRepository
{
    public CostureraRepository(DbContext ctx) : base(ctx) { }

    public async Task<IEnumerable<Costurera>> GetAllWithEncargosAsync() =>
        await _dbSet
            .Include(c => c.Encargos)
            .Where(c => !c.IsDeleted)
            .OrderBy(c => c.Nombre)
            .ToListAsync();

    public async Task<Costurera?> GetWithEncargosByIdAsync(int id) =>
        await _dbSet
            .Include(c => c.Encargos)
            .FirstOrDefaultAsync(c => c.Id == id && !c.IsDeleted);
}

public class UnitOfWork : IUnitOfWork
{
    private readonly DbContext _context;

    public IEncargoRepository Encargos { get; }
    public IClienteRepository Clientes { get; }
    public ICostureraRepository Costureras { get; }
    public IGenericRepository<Prenda> Prendas { get; }
    public IGenericRepository<Arreglo> Arreglos { get; }

    public UnitOfWork(DbContext context)
    {
        _context = context;
        Encargos = new EncargoRepository(context);
        Clientes = new ClienteRepository(context);
        Costureras = new CostureraRepository(context);
        Prendas = new GenericRepository<Prenda>(context);
        Arreglos = new GenericRepository<Arreglo>(context);
    }

    public async Task<int> SaveChangesAsync() =>
        await _context.SaveChangesAsync();

    public void Dispose() =>
        _context.Dispose();
}
