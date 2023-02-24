using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.RepositoryEntities;

namespace Repository.EntitiesConfiguration
{
	internal class BookConfiguration : IEntityTypeConfiguration<Book>
	{
		public void Configure(EntityTypeBuilder<Book> builder)
		{
			builder
			.HasKey(b => b.Id);

			builder
				.HasMany(b => b.Reviews)
				.WithOne(r => r.Book)
				.HasForeignKey(r => r.BookId);

			builder
				.HasMany(b => b.Ratings)
				.WithOne(r => r.Book)
				.HasForeignKey(r => r.BookId);
		}
	}
}
