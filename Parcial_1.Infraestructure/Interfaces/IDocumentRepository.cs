using Parcial_1.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parcial_1.Infraestructure.Interfaces;

public interface IDocumentRepository
{
    Task<PrintedDocument> GetByNameAsync(string documentName);
    Task<PrintedDocument> GetByIdAsync(int documentId);
    Task AddAsync(PrintedDocument document);
}
