using Microsoft.EntityFrameworkCore;
using SewControl.Domain.Entities;
using SewControl.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IUsuarioRepository : IGenericRepository<Usuario>
{
    Task<Usuario?> GetByEmailAsync(string email);
}

public class UsuarioRepository : GenericRepository<Usuario>, IUsuarioRepository
{
    public UsuarioRepository(DbContext ctx) : base(ctx) { }

    public async Task<Usuario?> GetByEmailAsync(string email) =>
        await _dbSet.FirstOrDefaultAsync(u => u.Email == email && !u.IsDeleted);
}