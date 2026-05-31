using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Proyecto.Models;

namespace Proyecto.Data;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Autor> Autors { get; set; }

    public virtual DbSet<Bibliotecario> Bibliotecarios { get; set; }

    public virtual DbSet<CopiaLibro> CopiaLibros { get; set; }

    public virtual DbSet<Credencial> Credencials { get; set; }

    public virtual DbSet<Editorial> Editorials { get; set; }

    public virtual DbSet<Libro> Libros { get; set; }

    public virtual DbSet<Pasillo> Pasillos { get; set; }

    public virtual DbSet<Prestamo> Prestamos { get; set; }

    public virtual DbSet<Sancion> Sancions { get; set; }

    public virtual DbSet<Socio> Socios { get; set; }

    public virtual DbSet<Telefono> Telefonos { get; set; }

    public virtual DbSet<Tema> Temas { get; set; }

    public virtual DbSet<Ubicacion> Ubicacions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Autor>(entity =>
        {
            entity.HasKey(e => e.IdAutor).HasName("PRIMARY");

            entity.ToTable("autor");

            entity.Property(e => e.IdAutor).HasColumnName("idAutor");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Bibliotecario>(entity =>
        {
            entity.HasKey(e => e.IdBibliotecario).HasName("PRIMARY");

            entity.ToTable("bibliotecario");

            entity.Property(e => e.IdBibliotecario).HasColumnName("idBibliotecario");
            entity.Property(e => e.CorreoElectronico)
                .HasMaxLength(100)
                .HasColumnName("correoElectronico");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<CopiaLibro>(entity =>
        {
            entity.HasKey(e => e.IdCopia).HasName("PRIMARY");

            entity.ToTable("copia_libro");

            entity.HasIndex(e => e.FolioLibro, "folioLibro");

            entity.Property(e => e.IdCopia).HasColumnName("idCopia");
            entity.Property(e => e.EstadoCopia)
                .HasMaxLength(50)
                .HasColumnName("estadoCopia");
            entity.Property(e => e.FolioLibro).HasColumnName("folioLibro");

            entity.HasOne(d => d.FolioLibroNavigation).WithMany(p => p.CopiaLibros)
                .HasForeignKey(d => d.FolioLibro)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("copia_libro_ibfk_1");
        });

        modelBuilder.Entity<Credencial>(entity =>
        {
            entity.HasKey(e => e.MatriculaCredencial).HasName("PRIMARY");

            entity.ToTable("credencial");

            entity.Property(e => e.MatriculaCredencial).HasColumnName("matriculaCredencial");
            entity.Property(e => e.Numero).HasColumnName("numero");
        });

        modelBuilder.Entity<Editorial>(entity =>
        {
            entity.HasKey(e => e.IdEditorial).HasName("PRIMARY");

            entity.ToTable("editorial");

            entity.Property(e => e.IdEditorial).HasColumnName("idEditorial");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Libro>(entity =>
        {
            entity.HasKey(e => e.FolioLibro).HasName("PRIMARY");

            entity.ToTable("libro");

            entity.HasIndex(e => e.IdEditorial, "idEditorial");

            entity.HasIndex(e => e.IdUbicacion, "idUbicacion");

            entity.Property(e => e.FolioLibro).HasColumnName("folioLibro");
            entity.Property(e => e.CondicionLibro)
                .HasMaxLength(50)
                .HasColumnName("condicionLibro");
            entity.Property(e => e.IdEditorial).HasColumnName("idEditorial");
            entity.Property(e => e.IdUbicacion).HasColumnName("idUbicacion");
            entity.Property(e => e.Isbn).HasColumnName("isbn");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
            entity.Property(e => e.NumeroCopias).HasColumnName("numeroCopias");
            entity.Property(e => e.NumeroPaginas).HasColumnName("numeroPaginas");
            entity.Property(e => e.PaisPublicacion)
                .HasMaxLength(50)
                .HasColumnName("paisPublicacion");

            entity.HasOne(d => d.IdEditorialNavigation).WithMany(p => p.Libros)
                .HasForeignKey(d => d.IdEditorial)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("libro_ibfk_1");

            entity.HasOne(d => d.IdUbicacionNavigation).WithMany(p => p.Libros)
                .HasForeignKey(d => d.IdUbicacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("libro_ibfk_2");

            entity.HasMany(d => d.IdAutors).WithMany(p => p.FolioLibros)
                .UsingEntity<Dictionary<string, object>>(
                    "LibroAutor",
                    r => r.HasOne<Autor>().WithMany()
                        .HasForeignKey("IdAutor")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("libro_autor_ibfk_2"),
                    l => l.HasOne<Libro>().WithMany()
                        .HasForeignKey("FolioLibro")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("libro_autor_ibfk_1"),
                    j =>
                    {
                        j.HasKey("FolioLibro", "IdAutor").HasName("PRIMARY");
                        j.ToTable("libro_autor");
                        j.HasIndex(new[] { "IdAutor" }, "idAutor");
                        j.IndexerProperty<int>("FolioLibro").HasColumnName("folioLibro");
                        j.IndexerProperty<int>("IdAutor").HasColumnName("idAutor");
                    });
        });

        modelBuilder.Entity<Pasillo>(entity =>
        {
            entity.HasKey(e => e.IdPasillo).HasName("PRIMARY");

            entity.ToTable("pasillo");

            entity.Property(e => e.IdPasillo).HasColumnName("idPasillo");
            entity.Property(e => e.NomPasillo)
                .HasMaxLength(100)
                .HasColumnName("nomPasillo");
        });

        modelBuilder.Entity<Prestamo>(entity =>
        {
            entity.HasKey(e => e.NumPrestamo).HasName("PRIMARY");

            entity.ToTable("prestamos");

            entity.HasIndex(e => e.IdBibliotecario, "fk_prestamo_bibliotecario");

            entity.HasIndex(e => e.FolioLibro, "folioLibro");

            entity.HasIndex(e => e.NumSocio, "numSocio");

            entity.Property(e => e.NumPrestamo).HasColumnName("numPrestamo");
            entity.Property(e => e.EstatusPrestamo)
                .HasMaxLength(50)
                .HasColumnName("estatusPrestamo");
            entity.Property(e => e.FechaEntrega)
                .HasColumnType("date")
                .HasColumnName("fechaEntrega");
            entity.Property(e => e.FechaInicio)
                .HasColumnType("date")
                .HasColumnName("fechaInicio");
            entity.Property(e => e.FolioLibro).HasColumnName("folioLibro");
            entity.Property(e => e.IdBibliotecario).HasColumnName("idBibliotecario");
            entity.Property(e => e.NumSocio).HasColumnName("numSocio");

            entity.HasOne(d => d.FolioLibroNavigation).WithMany(p => p.Prestamos)
                .HasForeignKey(d => d.FolioLibro)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("prestamos_ibfk_1");

            entity.HasOne(d => d.IdBibliotecarioNavigation).WithMany(p => p.Prestamos)
                .HasForeignKey(d => d.IdBibliotecario)
                .HasConstraintName("fk_prestamo_bibliotecario");

            entity.HasOne(d => d.NumSocioNavigation).WithMany(p => p.Prestamos)
                .HasForeignKey(d => d.NumSocio)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("prestamos_ibfk_2");
        });

        modelBuilder.Entity<Sancion>(entity =>
        {
            entity.HasKey(e => e.Folio).HasName("PRIMARY");

            entity.ToTable("sancion");

            entity.HasIndex(e => e.NumPrestamo, "numPrestamo");

            entity.Property(e => e.Folio).HasColumnName("folio");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(200)
                .HasColumnName("descripcion");
            entity.Property(e => e.IdBibliotecario).HasColumnName("idBibliotecario");
            entity.Property(e => e.LimitePago)
                .HasColumnType("date")
                .HasColumnName("limitePago");
            entity.Property(e => e.MontoSancion).HasColumnName("montoSancion");
            entity.Property(e => e.NumPrestamo).HasColumnName("numPrestamo");

            entity.HasOne(d => d.NumPrestamoNavigation).WithMany(p => p.Sancions)
                .HasForeignKey(d => d.NumPrestamo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("sancion_ibfk_1");
        });

        modelBuilder.Entity<Socio>(entity =>
        {
            entity.HasKey(e => e.NumSocio).HasName("PRIMARY");

            entity.ToTable("socio");

            entity.HasIndex(e => e.MatriculaCredencial, "fk_socio_credencial");

            entity.Property(e => e.NumSocio).HasColumnName("numSocio");
            entity.Property(e => e.CorreoElectronico)
                .HasMaxLength(100)
                .HasColumnName("correoElectronico");
            entity.Property(e => e.Direccion)
                .HasMaxLength(200)
                .HasColumnName("direccion");
            entity.Property(e => e.MatriculaCredencial).HasColumnName("matriculaCredencial");
            entity.Property(e => e.NombCompleto)
                .HasMaxLength(100)
                .HasColumnName("nombCompleto");

            entity.HasOne(d => d.MatriculaCredencialNavigation).WithMany(p => p.Socios)
                .HasForeignKey(d => d.MatriculaCredencial)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_socio_credencial");
        });

        modelBuilder.Entity<Telefono>(entity =>
        {
            entity.HasKey(e => e.IdTelefono).HasName("PRIMARY");

            entity.ToTable("telefono");

            entity.HasIndex(e => e.NumSocio, "numSocio");

            entity.Property(e => e.IdTelefono).HasColumnName("idTelefono");
            entity.Property(e => e.NumSocio).HasColumnName("numSocio");
            entity.Property(e => e.Numero)
                .HasMaxLength(15)
                .HasColumnName("numero");

            entity.HasOne(d => d.NumSocioNavigation).WithMany(p => p.Telefonos)
                .HasForeignKey(d => d.NumSocio)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("telefono_ibfk_1");
        });

        modelBuilder.Entity<Tema>(entity =>
        {
            entity.HasKey(e => e.IdTema).HasName("PRIMARY");

            entity.ToTable("tema");

            entity.Property(e => e.IdTema).HasColumnName("idTema");
            entity.Property(e => e.NombTema)
                .HasMaxLength(100)
                .HasColumnName("nombTema");
        });

        modelBuilder.Entity<Ubicacion>(entity =>
        {
            entity.HasKey(e => e.IdUbicacion).HasName("PRIMARY");

            entity.ToTable("ubicacion");

            entity.HasIndex(e => e.IdPasillo, "fk_ubicacion_pasillo");

            entity.HasIndex(e => e.IdTema, "fk_ubicacion_tema");

            entity.Property(e => e.IdUbicacion).HasColumnName("idUbicacion");
            entity.Property(e => e.IdPasillo).HasColumnName("idPasillo");
            entity.Property(e => e.IdTema).HasColumnName("idTema");
            entity.Property(e => e.Piso)
                .HasMaxLength(50)
                .HasColumnName("piso");

            entity.HasOne(d => d.IdPasilloNavigation).WithMany(p => p.Ubicacions)
                .HasForeignKey(d => d.IdPasillo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_ubicacion_pasillo");

            entity.HasOne(d => d.IdTemaNavigation).WithMany(p => p.Ubicacions)
                .HasForeignKey(d => d.IdTema)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_ubicacion_tema");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
