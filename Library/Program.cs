using Library.Areas.Identity.Data;
using Library.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("ApplicationDbContextConnection") ??
                       throw new InvalidOperationException(
                           "Connection string 'ApplicationDbContextConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "BookManager",
        pattern: "{area:exists}/{controller=BookManager}/{action=Index}/{id?}");

    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
        
    endpoints.MapRazorPages();
});

// Seed the database
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        
        var user1 = userManager.FindByIdAsync("1").Result;
        var user2 = userManager.FindByIdAsync("2").Result;

        if (user1 != null && user2 != null)
        {
            SeedData(context, user1, user2);
        }
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding the database.");
    }
}

app.Run();


void SeedData(ApplicationDbContext context, ApplicationUser user1, ApplicationUser user2)
{
    if (!context.Books.Any())
    {
        var author1 = new Author { FirstName = "John", LastName = "Doe" };
        var author2 = new Author { FirstName = "Jane", LastName = "Smith" };
        context.Authors.AddRange(author1, author2);
        
        var publisher1 = new Publisher { Name = "ABC Publications" };
        var publisher2 = new Publisher { Name = "XYZ Books" };
        context.Publishers.AddRange(publisher1, publisher2);

        var book1 = new Book { Title = "Book 1", Author = author1, Publisher = publisher1 };
        var book2 = new Book { Title = "Book 2", Author = author2, Publisher = publisher2 };
        context.Books.AddRange(book1, book2);
        
        var loan1 = new Loan { Book = book1, User = user1, LoanDate = DateTime.Now, ReturnDate = DateTime.Now.AddDays(14) };
        var loan2 = new Loan { Book = book2, User = user2, LoanDate = DateTime.Now, ReturnDate = DateTime.Now.AddDays(21) };
        context.Loans.AddRange(loan1, loan2);

        context.SaveChanges();
    }
}