using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parcial_1.Shared;

public class PrintRequestDto
{
    public string DocumentName { get; set; }
    public byte[] Document { get; set; }
    public int Priority { get; set; }
}
