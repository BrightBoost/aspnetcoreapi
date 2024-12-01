### **Exercise: Build a Fun "Quote of the Day" Minimal API**

Create a Minimal API that:
- Returns a random inspirational quote from a predefined list.
- Allows users to add new quotes via POST requests.
- Includes a route to get all quotes and another route to search for quotes by keyword.
- Bonus: Add Swagger for testing and documentation.

---

#### **Requirements**
1. **Basic API Endpoints**:
   - `GET /quote`: Returns a random quote.
   - `GET /quotes`: Returns all quotes.
   - `GET /quotes/search?keyword=...`: Searches for quotes containing the specified keyword.
   - `POST /quotes`: Allows users to add a new quote.

2. **Data Structure**:
   - Use an in-memory list of quotes for simplicity. 

3. **Enhancements**:
   - Add Swagger for documentation.
   - Return meaningful HTTP status codes (e.g., `201` for created, `404` for not found).

---

#### **Learning Outcomes**
1. You will learn how to:
   - Create and run a Minimal API.
   - Work with `MapGet`, `MapPost`, and query parameters.
   - Use in-memory data structures.
2. You will practice using HTTP methods (`GET`, `POST`) and routes.

---

### **Follow these steps:**

#### **Step 1: Create a New ASP.NET Core Project**
1. Open a terminal or command prompt.
2. Run the following command to create a new ASP.NET Core project:
   ```bash
   dotnet new web -n QuoteApi
   cd QuoteApi
   ```
   - This creates a new folder named `QuoteApi` with a basic ASP.NET Core web project.

3. Open the project in your preferred code editor (e.g., Visual Studio Code).

---

#### **Step 2: Add Required NuGet Packages**
1. Add the **SwaggerGen** package to enable Swagger support:
   ```bash
   dotnet add package Swashbuckle.AspNetCore --version 6.5.0
   ```

2. Wait for the dependencies to restore. You’re now ready to start coding.

---

#### **Step 3: Register the Quotes Service**
1. Open the `Program.cs` file.
2. Add the following code to register an in-memory list of quotes as a singleton service:
   ```csharp
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
   ```

---

#### **Step 4: Enable Swagger**
1. In `Program.cs`, enable Swagger for API documentation:
   ```csharp
   builder.Services.AddEndpointsApiExplorer();
   builder.Services.AddSwaggerGen();

   app.UseSwagger();
   app.UseSwaggerUI();
   ```

2. This allows Swagger to automatically generate API documentation and provide an interactive UI for testing endpoints.

---

#### **Step 5: Create the Quote API Endpoints**
Add the following endpoints to `Program.cs`:

1. **Get a Random Quote**:
   ```csharp
   app.MapGet("/quote", ([FromServices] List<Quote> quotes) =>
   {
       var random = new Random();
       return quotes[random.Next(quotes.Count)];
   });
   ```

2. **Get All Quotes**:
   ```csharp
   app.MapGet("/quotes", ([FromServices] List<Quote> quotes) => quotes);
   ```

3. **Search for Quotes by Keyword**:
   ```csharp
   app.MapGet("/quotes/search", (string keyword, [FromServices] List<Quote> quotes) =>
   {
       var filteredQuotes = quotes
           .Where(q => q.Content.Contains(keyword, StringComparison.OrdinalIgnoreCase))
           .ToList();

       return filteredQuotes.Any()
           ? Results.Ok(filteredQuotes)
           : Results.NotFound("No quotes found matching your keyword.");
   });
   ```

4. **Add a New Quote**:
   ```csharp
   app.MapPost("/quotes", (Quote newQuote, [FromServices] List<Quote> quotes) =>
   {
       var nextId = quotes.Max(q => q.Id) + 1;
       var quoteToAdd = new Quote(nextId, newQuote.Content, newQuote.Author);
       quotes.Add(quoteToAdd);

       return Results.Created($"/quotes/{nextId}", quoteToAdd);
   });
   ```

---

#### **Step 6: Define the Quote Record**
At the bottom of `Program.cs`, add the `Quote` record:
```csharp
public record Quote(int Id, string Content, string Author);
```

---

#### **Step 7: Run the Application**
1. Start the application:
   ```bash
   dotnet run
   ```

2. Open Swagger UI in your browser:
   - Navigate to `http://localhost:<port>/swagger` (replace `<port>` with the actual port displayed in your terminal).

3. Test the following endpoints:
   - `GET /quote`: Fetches a random quote.
   - `GET /quotes`: Lists all available quotes.
   - `GET /quotes/search?keyword=<term>`: Searches for quotes containing a specific keyword.
   - `POST /quotes`: Adds a new quote (use a JSON body like below):
     ```json
     {
         "content": "Programming isn't about what you know; it's about what you can figure out.",
         "author": "Chris Pine"
     }
     ```

---

### **Final Code**
Here’s how the full `Program.cs` file should look:

```csharp
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
```

---

### **Bonus Challenge**
- **Add Validation**: Ensure `newQuote` in `POST /quotes` has non-empty `Content` and `Author`.
- **Frontend Integration**: Build a small JavaScript app to display random quotes using the API.

