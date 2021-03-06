// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PokemonTrainerAPI.Repository;

namespace PokemonTrainerAPI.Migrations
{
    [DbContext(typeof(PkTrainerContext))]
    [Migration("20220210213635_InicialMySql")]
    partial class InicialMySql
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.10");

            modelBuilder.Entity("PokemonTrainerAPI.Domain.Pokemon", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("idTrainer")
                        .HasColumnType("int");

                    b.Property<string>("nome")
                        .HasColumnType("longtext");

                    b.HasKey("id");

                    b.HasIndex("idTrainer");

                    b.ToTable("tb_Pokemons");
                });

            modelBuilder.Entity("PokemonTrainerAPI.Domain.Usuario", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("email")
                        .HasColumnType("longtext");

                    b.Property<string>("username")
                        .HasColumnType("longtext");

                    b.HasKey("id");

                    b.ToTable("tb_usuarios");
                });

            modelBuilder.Entity("PokemonTrainerAPI.Domain.Pokemon", b =>
                {
                    b.HasOne("PokemonTrainerAPI.Domain.Usuario", "trainer")
                        .WithMany("ListaDePokemons")
                        .HasForeignKey("idTrainer")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("trainer");
                });

            modelBuilder.Entity("PokemonTrainerAPI.Domain.Usuario", b =>
                {
                    b.Navigation("ListaDePokemons");
                });
#pragma warning restore 612, 618
        }
    }
}
