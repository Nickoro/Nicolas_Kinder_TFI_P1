using Microsoft.AspNetCore.Mvc;
using Parcial_1.Application.Interfaces;
using Parcial_1.Shared;

namespace Parcial_1.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PrintingController : Controller
{
    private readonly IPrintingService _printingService;
    public PrintingController(IPrintingService printingService)
    {
        _printingService = printingService;
    }

    [HttpPost("print")]
    public async Task<IActionResult> PrintDocument([FromBody] PrintRequestDto request)
    {
        var result = await _printingService.SendPrintRequestAsync(request);
        if (!result)
        {
            return BadRequest(new { success = false, message = "La prioridad debe estar seteada entre 1 y 10, y el nombre no puede ser repetido." });
        }

        return Ok(new { success = true, message = "Solicitud de impresión enviada correctamente" });
    }
    [HttpGet("status/{documentName}")]
    public async Task<ActionResult<PrintResponseDto>> GetPrintStatus(string documentName)
    {
        var result = await _printingService.CheckPrintStatusAsync(documentName);
        if (result == null)
            return NotFound(new { Message = "No se encontro el documento" });

        return Ok(result);
    }
    [HttpGet("statusId/{documentId}")]
    public async Task<ActionResult<PrintResponseDto>> GetIdPrintStatus(int documentId)
    {
        var result = await _printingService.CheckPrintStatusAsync(documentId);
        if (result == null)
            return NotFound(new { Message = "No se encontro el documento" });

        return Ok(result);
    }

}
