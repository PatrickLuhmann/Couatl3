﻿// <auto-generated />
using Couatl3_Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace Couatl3_Model.Migrations
{
    [DbContext(typeof(CouatlContext))]
    [Migration("20171125182738_Migration_1")]
    partial class Migration_1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125");

            modelBuilder.Entity("Couatl3_Model.Account", b =>
                {
                    b.Property<int>("AccountId")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Closed");

                    b.Property<string>("Institution");

                    b.Property<string>("Name");

                    b.HasKey("AccountId");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("Couatl3_Model.LotAssignment", b =>
                {
                    b.Property<int>("LotAssignmentId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("BuyTransactionTransactionId");

                    b.Property<decimal>("Quantity");

                    b.Property<int?>("SellTransactionTransactionId");

                    b.HasKey("LotAssignmentId");

                    b.HasIndex("BuyTransactionTransactionId");

                    b.HasIndex("SellTransactionTransactionId");

                    b.ToTable("LotAssignments");
                });

            modelBuilder.Entity("Couatl3_Model.Price", b =>
                {
                    b.Property<int>("PriceId")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Amount");

                    b.Property<bool>("Closing");

                    b.Property<DateTime>("Date");

                    b.HasKey("PriceId");

                    b.ToTable("Prices");
                });

            modelBuilder.Entity("Couatl3_Model.Security", b =>
                {
                    b.Property<int>("SecurityId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<string>("Symbol");

                    b.HasKey("SecurityId");

                    b.ToTable("Securities");
                });

            modelBuilder.Entity("Couatl3_Model.Transaction", b =>
                {
                    b.Property<int>("TransactionId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccountId");

                    b.Property<DateTime>("Date");

                    b.Property<decimal>("Fee");

                    b.Property<decimal>("Quantity");

                    b.Property<int>("SecurityId");

                    b.Property<int>("Type");

                    b.Property<decimal>("Value");

                    b.HasKey("TransactionId");

                    b.HasIndex("AccountId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("Couatl3_Model.LotAssignment", b =>
                {
                    b.HasOne("Couatl3_Model.Transaction", "BuyTransaction")
                        .WithMany()
                        .HasForeignKey("BuyTransactionTransactionId");

                    b.HasOne("Couatl3_Model.Transaction", "SellTransaction")
                        .WithMany()
                        .HasForeignKey("SellTransactionTransactionId");
                });

            modelBuilder.Entity("Couatl3_Model.Transaction", b =>
                {
                    b.HasOne("Couatl3_Model.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
