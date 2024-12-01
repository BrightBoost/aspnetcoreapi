using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register the in-memory quotes list as a singleton service
builder.Services.AddSingleton(new List<Quote>
{
    new Quote(1, "Code is like humor. When you have to explain it, it’s bad.", "Cory House"),
    new Quote(2, "Any fool can write code that a computer can understand. Good programmers write code that humans can understand.", "Martin Fowler"),
    new Quote(3, "It’s not a bug – it’s an undocumented feature.", "Anonymous"),
    new Quote(4, "Talk is cheap. Show me the code.", "Linus Torvalds"),
    new Quote(5, "First, solve the problem. Then, write the code.", "John Johnson"),
    new Quote(6, "Simplicity is the soul of efficiency.", "Austin Freeman"),
    new Quote(7, "Before software can be reusable, it first has to be usable.", "Ralph Johnson"),
    new Quote(8, "Make it work, make it right, make it fast.", "Kent Beck")
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/quote", ([FromServices] List<Quote> quotes) =>
{
    var random = new Random();
    return quotes[random.Next(quotes.Count)];
});

app.MapGet("/quotes", ([FromServices] List<Quote> quotes) => quotes);

app.MapGet("/quotes/search", (string keyword, [FromServices] List<Quote> quotes) =>
{
    var filteredQuotes = quotes
        .Where(q => q.Content.Contains(keyword, StringComparison.OrdinalIgnoreCase))
        .ToList();

    return filteredQuotes.Any()
        ? Results.Ok(filteredQuotes)
        : Results.NotFound("No quotes found matching your keyword.");
});

app.MapPost("/quotes", (Quote newQuote, [FromServices] List<Quote> quotes) =>
{
    var nextId = quotes.Max(q => q.Id) + 1;
    var quoteToAdd = new Quote(nextId, newQuote.Content, newQuote.Author);
    quotes.Add(quoteToAdd);

    return Results.Created($"/quotes/{nextId}", quoteToAdd);
});

app.Run();

public record Quote(int Id, string Content, string Author);
