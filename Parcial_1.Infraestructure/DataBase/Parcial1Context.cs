using Microsoft.EntityFrameworkCore;
using Parcial_1.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parcial_1.Infraestructure.DataBase;

public class Parcial1Context : DbContext
{
    public Parcial1Context(DbContextOptions<Parcial1Context> options) : base(options){ }

    public DbSet<PrintedDocument> PrintedDocuments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<PrintedDocument>()
            .HasKey(p => p.Id);

        modelBuilder.Entity<PrintedDocument>()
            .HasIndex(p => p.DocumentName)
            .IsUnique();
    }
}
