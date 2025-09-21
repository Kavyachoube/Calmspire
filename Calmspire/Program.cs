using CalmSpire.Data;
using CalmSpire.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllersWithViews();

// EF Core
builder.Services.AddDbContext<CalmSpireDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Sessions
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromHours(2);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddHttpClient();
builder.Services.AddScoped<SuggestionEngineService>();
builder.Services.AddScoped<AuthenticationService>();
builder.Services.AddScoped<AIChatService>();

var app = builder.Build();

// Error handling
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Ensure DB exists & seed
using (var scope = app.Services.CreateScope())
{
    var ctx = scope.ServiceProvider.GetRequiredService<CalmSpireDbContext>();
    ctx.Database.EnsureCreated();

    if (!ctx.Assessments.Any())
    {
        var stress = new CalmSpire.Models.Assessment
        {
            Title = "Stress Level Assessment",
            Description = "A quick assessment to evaluate your current stress levels and coping strategies.",
            QuestionsJson = System.Text.Json.JsonSerializer.Serialize(new[]
            {
                new { Question = "Do you often feel overwhelmed by daily tasks?", Type = "radio", Options = new[] { "Never", "Sometimes", "Often", "Always" } },
                new { Question = "How well do you sleep at night?", Type = "radio", Options = new[] { "Very well", "Well", "Poorly", "Very poorly" } },
                new { Question = "Do you experience physical tension (headaches, muscle pain)?", Type = "radio", Options = new[] { "Never", "Rarely", "Sometimes", "Frequently" } },
                new { Question = "How often do you feel anxious or worried?", Type = "radio", Options = new[] { "Never", "Rarely", "Sometimes", "Frequently" } },
                new { Question = "Do you have difficulty concentrating?", Type = "radio", Options = new[] { "Never", "Rarely", "Sometimes", "Frequently" } }
            }),
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        var mood = new CalmSpire.Models.Assessment
        {
            Title = "Mood Check-In",
            Description = "A brief assessment to understand your current emotional state and mood patterns.",
            QuestionsJson = System.Text.Json.JsonSerializer.Serialize(new[]
            {
                new { Question = "How would you describe your overall mood this week?", Type = "radio", Options = new[] { "Excellent", "Good", "Fair", "Poor" } },
                new { Question = "Do you feel hopeful about the future?", Type = "radio", Options = new[] { "Very hopeful", "Somewhat hopeful", "Not very hopeful", "Not at all hopeful" } },
                new { Question = "How often do you enjoy activities you used to like?", Type = "radio", Options = new[] { "Always", "Often", "Sometimes", "Never" } },
                new { Question = "Do you feel supported by friends and family?", Type = "radio", Options = new[] { "Very supported", "Somewhat supported", "Not very supported", "Not at all supported" } },
                new { Question = "How satisfied are you with your life currently?", Type = "radio", Options = new[] { "Very satisfied", "Satisfied", "Dissatisfied", "Very dissatisfied" } }
            }),
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        ctx.Assessments.AddRange(stress, mood);
        ctx.SaveChanges();
    }
}

app.Run();
