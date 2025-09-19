using CalmSpire.Data;
using CalmSpire.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configure Entity Framework
builder.Services.AddDbContext<CalmSpireDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add session support
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromHours(2);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Add custom services
builder.Services.AddScoped<AuthenticationService>();
builder.Services.AddScoped<ChatService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
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

// Ensure database is created (no migrations, just check existence)
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<CalmSpireDbContext>();
    context.Database.EnsureCreated();   // ✅ This will only create DB if missing

    // Seed initial assessments if none exist
    if (!context.Assessments.Any())
    {
        var stressAssessment = new CalmSpire.Models.Assessment
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

        var moodAssessment = new CalmSpire.Models.Assessment
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

        context.Assessments.AddRange(stressAssessment, moodAssessment);
        context.SaveChanges();
    }
}

app.Run();
