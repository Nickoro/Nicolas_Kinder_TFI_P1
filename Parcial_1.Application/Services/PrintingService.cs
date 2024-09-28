using MassTransit;
using Parcial_1.Application.Interfaces;
using Parcial_1.Domain;
using Parcial_1.Infraestructure.Interfaces;
using Parcial_1.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parcial_1.Application.Services;

public class PrintingService : IPrintingService
{
    private readonly IBus _bus;
    private readonly ISendEndpointProvider _sendEndpointProvider;
    private readonly IUnitOfWork _unitOfWork;

    public PrintingService(IBus bus, IUnitOfWork unitOfWork, ISendEndpointProvider sendEndpointProvider)
    {
        _bus = bus;
        _unitOfWork = unitOfWork;
        _sendEndpointProvider = sendEndpointProvider;
    }

    public async Task<PrintResponseDto> CheckPrintStatusAsync(string documentName)
    {
        var document = await _unitOfWork.DocumentRepository.GetByNameAsync(documentName);
        if (document == null)
            return null;
        var response = MapperPrintedADto(document);
        return response;
        
    }

    public async Task<PrintResponseDto> CheckPrintStatusAsync(int documentId)
    {
        var document = await _unitOfWork.DocumentRepository.GetByIdAsync(documentId);
        if (document == null)
            return null;
        var response = MapperPrintedADto(document);
        return response;
    }

    public async Task<bool> SendPrintRequestAsync(PrintRequestDto request)
    {
        var document = await _unitOfWork.DocumentRepository.GetByNameAsync(request.DocumentName);
        if (document != null)
            return false;

        if (request.Priority < 1 || request.Priority > 10)
            return false;

        await _bus.Publish<PrintRequestDto>(request, ctx =>
        {
            ctx.SetPriority((byte)request.Priority);
        });              

        return true;
    }
    private PrintResponseDto MapperPrintedADto(PrintedDocument document)
    {
        return new PrintResponseDto
        {
            DocumentName = document.DocumentName,
            PrintDate = document.PrintDate,
            InsertDate = document.InsertionDate
        };
    }
}
