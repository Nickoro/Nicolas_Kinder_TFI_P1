using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parcial_1.Infraestructure.Interfaces;

public interface IUnitOfWork
{
    IDocumentRepository DocumentRepository { get; }
    Task<int> SaveChangesAsync();
}
