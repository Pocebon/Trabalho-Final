using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Trab_Final.Services.DTOs;

namespace Trab_Final.BaseDados.Models;

public partial class ApiJulianoContext : DbContext
{
    public ApiJulianoContext()
    {
    }

    public ApiJulianoContext(DbContextOptions<ApiJulianoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Autor> Autors { get; set; }

    public virtual DbSet<Editora> Editoras { get; set; }

    public virtual DbSet<Emprestimo> Emprestimos { get; set; }

    public virtual DbSet<EmprestimoLivro> EmprestimoLivros { get; set; }

    public virtual DbSet<Livro> Livros { get; set; }

    public virtual DbSet<Pessoa> Pessoas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost; Database=API_JULIANO; Username=postgres; Password=28102004Lipe#");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Autor>(entity =>
        {
            entity.HasKey(e => e.IdAutor).HasName("autor_pkey");

            entity.ToTable("autor");

            entity.Property(e => e.IdAutor).HasColumnName("id_autor");
            entity.Property(e => e.DataNascimento).HasColumnName("data_nascimento");
            entity.Property(e => e.Nacionalidade)
                .HasMaxLength(50)
                .HasColumnName("nacionalidade");
            entity.Property(e => e.NomeAutor)
                .HasMaxLength(100)
                .HasColumnName("nome_autor");
        });

        modelBuilder.Entity<Editora>(entity =>
        {
            entity.HasKey(e => e.IdEditora).HasName("editora_pkey");

            entity.ToTable("editora");

            entity.Property(e => e.IdEditora).HasColumnName("id_editora");
            entity.Property(e => e.CnpjEditora)
                .HasMaxLength(15)
                .HasColumnName("cnpj_editora");
            entity.Property(e => e.NomeEditora)
                .HasMaxLength(100)
                .HasColumnName("nome_editora");
        });

        modelBuilder.Entity<Emprestimo>(entity =>
        {
            entity.HasKey(e => e.IdEmprestimo).HasName("emprestimo_pkey");

            entity.ToTable("emprestimo");

            entity.Property(e => e.IdEmprestimo).HasColumnName("id_emprestimo");
            entity.Property(e => e.DataDevolucao).HasColumnName("data_devolucao");
            entity.Property(e => e.DataEmprestimo).HasColumnName("data_emprestimo");
            entity.Property(e => e.IdPessoa).HasColumnName("id_pessoa");
            entity.Property(e => e.IdLivro).HasColumnName("id_livro");


            entity.Property(e => e.EmprestimoDevolvido)
               .HasMaxLength(1)
               .HasColumnName("emprestimodevolvido");

            entity.HasOne(d => d.IdLivroNavigation).WithMany(p => p.Emprestimo)
               .HasForeignKey(d => d.IdLivro)
               .HasConstraintName("emprestimo_id_livro_fkey");


            entity.HasOne(d => d.IdPessoaNavigation).WithMany(p => p.Emprestimo)
                .HasForeignKey(d => d.IdPessoa)
                .HasConstraintName("emprestimo_id_pessoa_fkey");
        });

        modelBuilder.Entity<EmprestimoLivro>(entity =>
        {
            entity.HasKey(e => e.IdEmprestimoLivro).HasName("emprestimo_livro_pkey");

            entity.ToTable("emprestimo_livro");

            entity.Property(e => e.IdEmprestimoLivro).HasColumnName("id_emprestimo_livro");
            entity.Property(e => e.IdEmprestimo).HasColumnName("id_emprestimo");
            entity.Property(e => e.IdLivro).HasColumnName("id_livro");

            entity.HasOne(d => d.IdEmprestimoNavigation).WithMany(p => p.EmprestimoLivros)
                .HasForeignKey(d => d.IdEmprestimo)
                .HasConstraintName("emprestimo_livro_id_emprestimo_fkey");

            entity.HasOne(d => d.IdLivroNavigation).WithMany(p => p.EmprestimoLivros)
                .HasForeignKey(d => d.IdLivro)
                .HasConstraintName("emprestimo_livro_id_livro_fkey");
        });

        modelBuilder.Entity<Livro>(entity =>
        {
            entity.HasKey(e => e.IdLivro).HasName("livro_pkey");

            entity.ToTable("livro");

            entity.Property(e => e.IdLivro).HasColumnName("id_livro");
            entity.Property(e => e.AnoPublicacao).HasColumnName("ano_publicacao");
            entity.Property(e => e.IdEditora).HasColumnName("id_editora");
            entity.Property(e => e.Id_autor).HasColumnName("id_autor");
            entity.Property(e => e.Estante).HasColumnName("estante");
            entity.Property(e => e.Prateleira)
                .HasMaxLength(10)
                .HasColumnName("prateleira");

            entity.Property(e => e.NomeLivro)
                .HasMaxLength(100)
                .HasColumnName("nome_livro");

            entity.HasOne(d => d.IdEditoraNavigation).WithMany(p => p.Livros)
                .HasForeignKey(d => d.IdEditora)
                .HasConstraintName("livro_id_editora_fkey");

            entity.HasOne(d => d.IdAutorNavigation).WithMany(p => p.Livros)
    .HasForeignKey(d => d.Id_autor)
    .HasConstraintName("livro_id_autor_fkey");
        });

        modelBuilder.Entity<Pessoa>(entity =>
        {
            entity.HasKey(e => e.IdPessoa).HasName("pessoas_pkey");

            entity.ToTable("pessoas");

            entity.Property(e => e.IdPessoa).HasColumnName("id_pessoa");
            entity.Property(e => e.CpfPessoa)
                .HasMaxLength(15)
                .HasColumnName("cpf_pessoa");
            entity.Property(e => e.DataNascimento).HasColumnName("data_nascimento");
            entity.Property(e => e.NomePessoa)
                .HasMaxLength(100)
                .HasColumnName("nome_pessoa");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
