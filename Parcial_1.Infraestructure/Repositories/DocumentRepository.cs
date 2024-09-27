using Microsoft.EntityFrameworkCore;
using Parcial_1.Domain;
using Parcial_1.Infraestructure.DataBase;
using Parcial_1.Infraestructure.Interfaces;


namespace Parcial_1.Infraestructure.Repositories;

public class DocumentRepository : IDocumentRepository
{
    private readonly Parcial1Context _context;

    public DocumentRepository(Parcial1Context context)
    {
        _context = context;
    }
    public async Task AddAsync(PrintedDocument document)
    {
        await _context.PrintedDocuments.AddAsync(document);        
    }

    public async Task<PrintedDocument> GetByIdAsync(int documentId)
    {
        return await _context.PrintedDocuments.FirstOrDefaultAsync(x => x.Id == documentId);
    }

    public async Task<PrintedDocument> GetByNameAsync(string documentName)
    {
        return await _context.PrintedDocuments.FirstOrDefaultAsync(x => x.DocumentName == documentName);
    }
}
