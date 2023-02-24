using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.RepositoryEntities;

namespace Repository.EntitiesConfiguration
{
	internal class RatingConfiguration : IEntityTypeConfiguration<Rating>
	{
		public void Configure(EntityTypeBuilder<Rating> builder)
		{
			builder
				.HasKey(r => r.Id);

			builder
				.HasOne(r => r.Book)
				.WithMany(b => b.Ratings)
				.HasForeignKey(r => r.BookId)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}
