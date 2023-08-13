using Microsoft.EntityFrameworkCore;
using WebServer.Entity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDataContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("ProductsDb")));
builder.Services.AddCors();

var app = builder.Build();
app.UseCors(builder => builder.AllowAnyMethod().AllowAnyOrigin().AllowAnyHeader());

app.MapGet("api/nomenklatures", async (AppDataContext context) => await context.Products.ToListAsync());

app.MapPost("api/nomenklatures", async(Product productToAdd, AppDataContext context) =>  
{
    await context.Products.AddAsync(productToAdd);
    await context.SaveChangesAsync();
});

app.MapGet("/api/nomenklatures/{id:int}", async (int id, AppDataContext context) =>
{
    var product = await context.Products.FindAsync(id);
    var nomenklature = new
    {
        id = product.id,
        name = product.name,
        price = product.price,
        quantity = 1,
        children = await GetChildren(context, id)
    };

    List<object> dataToReturn = new List<object>
    {
        nomenklature
    };

    if (nomenklature == null) 
        return Results.NotFound(new { message = "Пользователь не найден" });

    return Results.Json(dataToReturn);
});

async Task<List<object>> GetChildren(AppDataContext context, int id)
{
    var links = await context.LinkData.Where(l => l.parent_id == id).ToListAsync();
    var children = new List<object>();
    foreach (var link in links)
    {
        var product = await context.Products.FindAsync(link.product_id);
        if (product != null)
        {
            var child = new
            {
                id = product.id,
                name = product.name,
                price = product.price,
                quantity = link.quantity,
                children = await GetChildren(context, product.id)
            };
            children.Add(child);
        }
    }
    return children;
}
app.Run();
