using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parcial_1.Domain;

public class PrintedDocument
{
    public int Id { get; set; }
    public string DocumentName { get; set; }
    public DateTime PrintDate { get; set; }
    public DateTime InsertionDate { get; set; }
}
