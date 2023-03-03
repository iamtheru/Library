using FluentValidation;
using FluentValidation.AspNetCore;
using Library.Validation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Repository;
using Repository.Interfaces;
using Service;
using Service.Interfaces;
using Shared.Configuration;
using Shared.DTO.Incoming;
using System.Xml.Serialization;

namespace Library
{
	public static class ServiceProviderExtensions
	{
		public static void AddBookService(this IServiceCollection services)
		{
			services.AddTransient<IBookService, BookService>();
		}

		public static void AddRecomendedService(this IServiceCollection services)
		{
			services.AddTransient<IRecomendedService, RecomendedService>();
		}

		public static void AddRepository(this IServiceCollection services)
		{
			services.AddTransient<IBookRepository, BookRepository>();
		}

		public static void AddAutomapper(this IServiceCollection services)
		{
			services.AddAutoMapper(typeof(AppMappingProfile));
		}

		public static void AddValidation(this IServiceCollection services)
		{
			services.AddFluentValidationAutoValidation();
		}

		public static void AddValidationRuls(this IServiceCollection services)
		{
			services.AddScoped<IValidator<BookPost>, BookPostValidator>();
			services.AddScoped<IValidator<RatingPut>, RatingPutValidator>();
			services.AddScoped<IValidator<ReviewPut>, ReviewPutValidator>();
		}

		public static void ConfigureConfig(this IServiceCollection services, WebApplicationBuilder builder)
		{
			services.Configure<Config>(builder.Configuration.GetSection("Configuration"));
		}

		public static void AddCorsWithOpt(this IServiceCollection services)
		{
			services.AddCors(options => {
				options.AddPolicy(
					name: "CORSOpenPolicy",
					builder => builder.WithOrigins("*").AllowAnyHeader().AllowAnyMethod()
					);
			});
		}
	}
}
