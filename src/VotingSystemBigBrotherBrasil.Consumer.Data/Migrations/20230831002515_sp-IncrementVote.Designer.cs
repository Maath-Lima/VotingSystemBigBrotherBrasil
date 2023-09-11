﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VotingSystemBigBrotherBrasil.Consumer.Data;

#nullable disable

namespace VotingSystemBigBrotherBrasil.Consumer.Data.Migrations
{
    [DbContext(typeof(VotingSystemContext))]
    [Migration("20230831002515_sp-IncrementVote")]
    partial class spIncrementVote
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("VotingSystemBigBrotherBrasil.Consumer.Data.Vote", b =>
                {
                    b.Property<int>("VoteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("VoteId"));

                    b.Property<string>("ParticipantName")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<int>("Votes")
                        .HasColumnType("int");

                    b.HasKey("VoteId");

                    b.ToTable("Votes");
                });
#pragma warning restore 612, 618
        }
    }
}