﻿using MassTransit;
using Parcial_1.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parcial_1.Application.Services;

public class PrintRequestService : IConsumer<PrintRequestDto>
{
    private readonly IBus _bus;

    public PrintRequestService(IBus bus)
    {
        _bus = bus;
    }

    public async Task Consume(ConsumeContext<PrintRequestDto> context)
    {
        var request = context.Message;
        var priority = context.Message.Priority;
        Console.WriteLine($"Processing print request for {request.DocumentName} with priority {priority}");
        if (SimulatePrinting())
        {
            var response = new PrintResponseDto
            {
                DocumentName = request.DocumentName,
                PrintDate = DateTime.UtcNow
            };
            await _bus.Publish(response);
        }
    }
    private bool SimulatePrinting()
    {
        // Simular éxito o fallo aleatorio
        //Simulamos la tardanza en enviar el el documento a imprimir
        Task.Delay(1000).Wait();
        return new Random().Next(2) == 0;
    }

}
