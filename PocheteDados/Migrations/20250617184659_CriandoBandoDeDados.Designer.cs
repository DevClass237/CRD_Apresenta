﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PocheteDados.Data;

#nullable disable

namespace PocheteDados.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250617184659_CriandoBandoDeDados")]
    partial class CriandoBandoDeDados
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.15")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("PocheteModelos.Modelo.Admin", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Admins");
                });

            modelBuilder.Entity("PocheteModelos.Modelo.Curso", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("Nome")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Cursos");
                });

            modelBuilder.Entity("PocheteModelos.Modelo.DadosTemporarios", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("Conteudo1")
                        .HasColumnType("int");

                    b.Property<long?>("Conteudo2")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CriadoEm")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("DadosTemporarios");
                });

            modelBuilder.Entity("PocheteModelos.Modelo.Movimentacao", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("DataDevolucao")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DataRetirada")
                        .HasColumnType("datetime(6)");

                    b.Property<long>("PocheteId")
                        .HasColumnType("bigint");

                    b.Property<int>("ProfessorMatricula")
                        .HasColumnType("int");

                    b.Property<int?>("TurnoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PocheteId");

                    b.HasIndex("ProfessorMatricula");

                    b.HasIndex("TurnoId");

                    b.ToTable("Movimentacoes");
                });

            modelBuilder.Entity("PocheteModelos.Modelo.Pochete", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("IdToken")
                        .HasColumnType("longtext");

                    b.Property<long>("SalaId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("SalaId");

                    b.ToTable("Pochetes");
                });

            modelBuilder.Entity("PocheteModelos.Modelo.Professor", b =>
                {
                    b.Property<int>("Matricula")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int?>("TurnoId")
                        .HasColumnType("int");

                    b.HasKey("Matricula");

                    b.HasIndex("TurnoId");

                    b.ToTable("Professores");
                });

            modelBuilder.Entity("PocheteModelos.Modelo.Sala", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int?>("TurnoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TurnoId");

                    b.ToTable("Salas");
                });

            modelBuilder.Entity("PocheteModelos.Modelo.Turno", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Turnos");
                });

            modelBuilder.Entity("PocheteModelos.Modelo.Usuario", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("Turma", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<long>("CursoId")
                        .HasColumnType("bigint");

                    b.Property<string>("Identificador")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Turno")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int?>("TurnoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CursoId");

                    b.HasIndex("TurnoId");

                    b.ToTable("Turmas");
                });

            modelBuilder.Entity("PocheteModelos.Modelo.Movimentacao", b =>
                {
                    b.HasOne("PocheteModelos.Modelo.Pochete", "Pochete")
                        .WithMany()
                        .HasForeignKey("PocheteId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("PocheteModelos.Modelo.Professor", "Professor")
                        .WithMany()
                        .HasForeignKey("ProfessorMatricula")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("PocheteModelos.Modelo.Turno", null)
                        .WithMany("Movimentacoes")
                        .HasForeignKey("TurnoId");

                    b.Navigation("Pochete");

                    b.Navigation("Professor");
                });

            modelBuilder.Entity("PocheteModelos.Modelo.Pochete", b =>
                {
                    b.HasOne("PocheteModelos.Modelo.Sala", "Sala")
                        .WithMany()
                        .HasForeignKey("SalaId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Sala");
                });

            modelBuilder.Entity("PocheteModelos.Modelo.Professor", b =>
                {
                    b.HasOne("PocheteModelos.Modelo.Turno", null)
                        .WithMany("Professores")
                        .HasForeignKey("TurnoId");
                });

            modelBuilder.Entity("PocheteModelos.Modelo.Sala", b =>
                {
                    b.HasOne("PocheteModelos.Modelo.Turno", null)
                        .WithMany("Salas")
                        .HasForeignKey("TurnoId");
                });

            modelBuilder.Entity("Turma", b =>
                {
                    b.HasOne("PocheteModelos.Modelo.Curso", "Curso")
                        .WithMany()
                        .HasForeignKey("CursoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PocheteModelos.Modelo.Turno", null)
                        .WithMany("Turmas")
                        .HasForeignKey("TurnoId");

                    b.Navigation("Curso");
                });

            modelBuilder.Entity("PocheteModelos.Modelo.Turno", b =>
                {
                    b.Navigation("Movimentacoes");

                    b.Navigation("Professores");

                    b.Navigation("Salas");

                    b.Navigation("Turmas");
                });
#pragma warning restore 612, 618
        }
    }
}
