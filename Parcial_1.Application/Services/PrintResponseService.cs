using MassTransit;
using Microsoft.Extensions.Hosting;
using Parcial_1.Domain;
using Parcial_1.Infraestructure.Interfaces;
using Parcial_1.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parcial_1.Application.Services;

public class PrintResponseService : IConsumer<PrintResponseDto>
{
    private readonly IUnitOfWork _unitOfWork;
    public PrintResponseService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Consume(ConsumeContext<PrintResponseDto> context)
    {
        var response = context.Message;
        var document = new PrintedDocument
        {
            DocumentName = response.DocumentName,
            PrintDate = response.PrintDate,
            InsertionDate = DateTime.UtcNow
        };

        await _unitOfWork.DocumentRepository.AddAsync(document);
        await _unitOfWork.SaveChangesAsync();
    }
    
}
