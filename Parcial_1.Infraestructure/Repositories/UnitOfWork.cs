using Parcial_1.Infraestructure.DataBase;
using Parcial_1.Infraestructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parcial_1.Infraestructure.Repositories;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly Parcial1Context _context;
    private IDocumentRepository _documentRepository;
    public UnitOfWork(Parcial1Context context)
    {
        _context = context;
    }
    public IDocumentRepository DocumentRepository
    {
        get
        {
            if (_documentRepository == null)
            {
                _documentRepository = new DocumentRepository(_context);
            }
            return _documentRepository;
        }
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
