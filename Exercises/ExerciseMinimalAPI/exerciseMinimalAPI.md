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
