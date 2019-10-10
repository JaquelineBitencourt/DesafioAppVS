using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CEF.Modelos
{
    public partial class XimasAPPContext : DbContext
    {
        public XimasAPPContext()
        {
        }

        public XimasAPPContext(DbContextOptions<XimasAPPContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Usuarios> Usuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=carreteiro\\equipeinovacao;Database=XimasAPP;User Id=appchimarrao;Password=safeweb123;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Usuarios>(entity =>
            {
                entity.HasKey(e => e.IdUsuario);

                entity.Property(e => e.NomeDoUsuario)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.OrdemDt)
                    .HasColumnName("OrdemDT")
                    .HasColumnType("datetime");
            });
        }
    }
}
