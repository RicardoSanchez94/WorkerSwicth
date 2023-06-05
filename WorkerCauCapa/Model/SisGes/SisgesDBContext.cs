using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace WorkerCauCapa.Model.SisGes
{
    public partial class SisgesDBContext : DbContext
    {
        public SisgesDBContext()
        {
        }

        public SisgesDBContext(DbContextOptions<SisgesDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Interface> Interfaces { get; set; }
        public virtual DbSet<InterfazAutorizacion> InterfazAutorizacions { get; set; }
        public virtual DbSet<InterfazPago> InterfazPagos { get; set; }
        public virtual DbSet<TipoInterfaz> TipoInterfazs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("data source=172.18.18.18;initial catalog=sisges2;user id=ricardo.sanchez;password=fsp.2022;MultipleActiveResultSets=True;App=EntityFramework;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");

            modelBuilder.Entity<Interface>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Idtipo).HasColumnName("idtipo");

                entity.HasOne(d => d.IdtipoNavigation)
                    .WithMany(p => p.Interfaces)
                    .HasForeignKey(d => d.Idtipo)
                    .HasConstraintName("FK_Interfaces_TipoInterfaz");
            });

            modelBuilder.Entity<InterfazAutorizacion>(entity =>
            {
                entity.HasKey(e => new { e.FechaAutorizacion, e.CodigoAgencia, e.NumeroAutorizacion });

                entity.ToTable("InterfazAutorizacion");

                entity.Property(e => e.FechaAutorizacion).HasColumnType("datetime");

                entity.Property(e => e.CodigoAgencia)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NumeroAutorizacion)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CodigoEmpresa)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CodigoUsuario)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Confirmada)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.Estado)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.Extrafinanciamiento)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FechaAutoriza).HasColumnType("date");

                entity.Property(e => e.FechaPrimerVencimiento).HasColumnType("datetime");

                entity.Property(e => e.FechaUltimoVencimiento).HasColumnType("datetime");

                entity.Property(e => e.IdInterfaces).HasColumnName("idInterfaces");

                entity.Property(e => e.ModoProceso)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.NroRef)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NumeroBoleta)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NumeroTarjeta)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RutVendedor)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TipoArchivo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TipoDiferencia)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.TipoTransaccion)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdInterfacesNavigation)
                    .WithMany(p => p.InterfazAutorizacions)
                    .HasForeignKey(d => d.IdInterfaces)
                    .HasConstraintName("FK_InterfazAutorizacion_Interfaces");
            });

            modelBuilder.Entity<InterfazPago>(entity =>
            {
                entity.HasKey(e => new { e.FechaAutorizacion, e.CodigoAgencia, e.NumeroAutorizacion });

                entity.Property(e => e.FechaAutorizacion).HasColumnType("datetime");

                entity.Property(e => e.CodigoAgencia)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NumeroAutorizacion)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CodigoCliente)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CodigoEmpresa)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CodigoUsuario)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Estado)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.IdInterfaces).HasColumnName("idInterfaces");

                entity.Property(e => e.NroRef)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Numerocuenta)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Numerorecibo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TipoArchivo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TipoDiferencia)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.HasOne(d => d.IdInterfacesNavigation)
                    .WithMany(p => p.InterfazPagos)
                    .HasForeignKey(d => d.IdInterfaces)
                    .HasConstraintName("FK_InterfazPagos_Interfaces");
            });

            modelBuilder.Entity<TipoInterfaz>(entity =>
            {
                entity.HasKey(e => e.IdTipo);

                entity.ToTable("TipoInterfaz");

                entity.Property(e => e.IdTipo)
                    .ValueGeneratedNever()
                    .HasColumnName("idTipo");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
