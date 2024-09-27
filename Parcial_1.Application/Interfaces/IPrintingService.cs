using Parcial_1.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parcial_1.Application.Interfaces;

public interface IPrintingService
{
    Task<bool> SendPrintRequestAsync(PrintRequestDto request);
    Task<PrintResponseDto> CheckPrintStatusAsync(string documentName);
    Task<PrintResponseDto> CheckPrintStatusAsync(int documentId);
}
