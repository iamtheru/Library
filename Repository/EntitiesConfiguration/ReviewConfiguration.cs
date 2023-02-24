using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.RepositoryEntities;

namespace Repository.EntitiesConfiguration
{
	internal class ReviewConfiguration : IEntityTypeConfiguration<Review>
	{
		public void Configure(EntityTypeBuilder<Review> builder)
		{
			builder
				.HasKey(r => r.Id);

			builder
				.HasOne(r => r.Book)
				.WithMany(b => b.Reviews)
				.HasForeignKey(r => r.BookId)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}
