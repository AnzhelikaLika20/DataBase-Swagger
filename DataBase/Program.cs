using DataBaseLibrary;

namespace DataBase
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                var filePath = Path.Combine(System.AppContext.BaseDirectory, "database.xml");
                c.IncludeXmlComments(filePath);
            });
            builder.Services.AddControllers();

            builder.Services.AddSingleton<IDataBase, DataBaseLibrary.DataBase>();

            builder.Services.AddSingleton<IDataAccessLayer, DataBaseLibrary.DataAccessLayer>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            
            app.UseAuthorization();
            

            app.MapControllers();

            app.Run();
        }
    }
}