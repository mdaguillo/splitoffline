﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BudgetAPI.Migrations
{
    [DbContext(typeof(BudgetAppDbContext))]
    partial class BudgetAppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BudgetAPI.EntityFramework.Models.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("BudgetAPI.EntityFramework.Models.Expense", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(9,2)");

                    b.Property<Guid?>("CategoryID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryID");

                    b.ToTable("Expenses");
                });

            modelBuilder.Entity("BudgetAPI.EntityFramework.Models.ExpenseParticipant", b =>
                {
                    b.Property<Guid>("ExpenseID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ParticipantId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("AmountOwed")
                        .HasColumnType("decimal(9,2)");

                    b.Property<decimal>("Share")
                        .HasColumnType("decimal(3,2)");

                    b.HasKey("ExpenseID", "ParticipantId");

                    b.HasIndex("ParticipantId");

                    b.ToTable("ExpenseParticipants");
                });

            modelBuilder.Entity("BudgetAPI.EntityFramework.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("BudgetAPI.EntityFramework.Models.Expense", b =>
                {
                    b.HasOne("BudgetAPI.EntityFramework.Models.Category", "Category")
                        .WithMany("Expenses")
                        .HasForeignKey("CategoryID")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Category");
                });

            modelBuilder.Entity("BudgetAPI.EntityFramework.Models.ExpenseParticipant", b =>
                {
                    b.HasOne("BudgetAPI.EntityFramework.Models.Expense", "Expense")
                        .WithMany("ExpenseParticipants")
                        .HasForeignKey("ExpenseID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BudgetAPI.EntityFramework.Models.User", "User")
                        .WithMany("Expenses")
                        .HasForeignKey("ParticipantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Expense");

                    b.Navigation("User");
                });

            modelBuilder.Entity("BudgetAPI.EntityFramework.Models.Category", b =>
                {
                    b.Navigation("Expenses");
                });

            modelBuilder.Entity("BudgetAPI.EntityFramework.Models.Expense", b =>
                {
                    b.Navigation("ExpenseParticipants");
                });

            modelBuilder.Entity("BudgetAPI.EntityFramework.Models.User", b =>
                {
                    b.Navigation("Expenses");
                });
#pragma warning restore 612, 618
        }
    }
}
