using FilmesApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FilmesApi.Data;

public class FilmeContext : DbContext
{
	public FilmeContext(DbContextOptions<FilmeContext> opts) : base(opts)
	{

	}

	protected override void OnModelCreating(ModelBuilder builder)
	{
		// A sessao tem como chave FilmeId e CinemaId
		builder.Entity<Sessao>()
			.HasKey(sessao => new { sessao.FilmeId, sessao.CinemaId });

		// Uma sessao tem um cinema e um cinema tem muitas sessoes
		builder.Entity<Sessao>()
			.HasOne(sessao => sessao.Cinema)
			.WithMany(cinema => cinema.Sessoes)
			.HasForeignKey(sessao => sessao.CinemaId);

		// Uma sessao tem um filme e um filme tem varias sessoes
		builder.Entity<Sessao>()
			.HasOne(sessao => sessao.Filme)
			.WithMany(filme => filme.Sessoes)
			.HasForeignKey(sessao => sessao.FilmeId);

		// Ao deletar um endereço nao deletar em cascata o cinema e filme
		builder.Entity<Endereco>()
			.HasOne(endereco => endereco.Cinema)
			.WithOne(cinema => cinema.Endereco)
			.OnDelete(DeleteBehavior.Restrict);
	}

	public DbSet<Filme> Filmes { get; set; }
	public DbSet<Cinema> Cinemas { get; set; }
	public DbSet<Endereco> Enderecos { get; set; }
	public DbSet<Sessao> Sessoes { get; set; }
}
