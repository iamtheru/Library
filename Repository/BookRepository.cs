using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Repository.EntitiesConfiguration;
using Repository.Interfaces;
using Shared.Configuration;
using Shared.RepositoryEntities;
using Order = Shared.SortState.BookOrder;

namespace Repository
{
	public class BookRepository : DbContext, IBookRepository
	{
		private readonly string _connection;

		private DbSet<Review> Reviews { get; set; }

		private DbSet<Book> Books { get; set; }

		private DbSet<Rating> Ratings { get; set; }



		public BookRepository(IOptions<Config> options)
		{
			_connection = options.Value.DefaultConnection;
		}

		public async Task<List<Book>> GetAllBooksAsync(Order order)
		{
			var query = Books
				.Include(b => b.Ratings)
				.Include(b => b.Reviews)
				.AsQueryable();

			switch (order)
			{
				case Order.Title:
					query = query.OrderBy(b => b.Title);
					break;
				case Order.Author:
					query = query.OrderBy(b => b.Author);
					break;
				default:
					throw new ArgumentException($"Invalid orderBy value '{order}'.", nameof(order));
			}

			return await query.ToListAsync();
		}

		public async Task<List<Book>> GetRecommendedBooksAsync(string genre, int bookCount, int reviewCount)
		{
			var query = Books
			.Include(b => b.Ratings)
			.Include(b => b.Reviews)
			.Where(b => b.Genre == genre)
			.Where(b => b.Reviews.Count > reviewCount)
			.OrderByDescending(b => b.Ratings.Average(r => r.Score))
			.Take(bookCount);

			return await query.ToListAsync();
		}

		public async Task<Book> GetBookDetailsAsync(int id)
		{
			var query = Books
				.Include(b => b.Ratings)
				.Include(b => b.Reviews)
				.Where(b => b.Id == id);

			return await query.FirstOrDefaultAsync();
		}

		public async Task<int> DeleteBookAsync(int id)
		{
			var book = await Books.FirstOrDefaultAsync(b => b.Id == id);

			if (book == null)
			{
				return -1;
			}

			Books.Remove(book);

			return await SaveAsync();
		}

		public async Task<Book> FindBookAsync(int id)
		{
			return await Books.FirstOrDefaultAsync(b => b.Id == id);
		}

		public async Task<int> AddBookAsync(Book book)
		{	
			await Books.AddAsync(book);
			return 1;
		}

		public async Task<int> UpdateBook(Book book)
		{
			Books.Update(book);
			return await SaveAsync();
		}

		public async Task<int> AddRatingAsync(Rating rating)
		{
			await Ratings.AddAsync(rating);
			return await SaveAsync();
		}

		public async Task<int> AddReviewAsync(Review review)
		{
			await Reviews.AddAsync(review);
			return await SaveAsync();
		}

		public async Task<int> SaveAsync()
		{ 
			return await SaveChangesAsync();
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseNpgsql(_connection);
			//optionsBuilder.UseInMemoryDatabase("LibraryDB");
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(new BookConfiguration());
			modelBuilder.ApplyConfiguration(new ReviewConfiguration());
			modelBuilder.ApplyConfiguration(new RatingConfiguration());
		}
	}
}
