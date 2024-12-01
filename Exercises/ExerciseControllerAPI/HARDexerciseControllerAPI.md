### Hard: Create a Product CRUD API in ASP.NET Core

Build a fully functional Product CRUD API using ASP.NET Core. The API should support the following operations:
- Create, Read, Update, Delete products.
- Add validations to ensure proper data input.
- Enable Swagger for API testing and include example values and descriptions.
- **Bonus**: Implement advanced features such as file upload and mixed parameters.

---

#### **Requirements**
1. **Set up the Project**:
   - Create an ASP.NET Core Web API project named `ProductCrudApi`.
   - Add Swagger for API documentation.

2. **Create a Product Data Transfer Object (DTO)**:
   - Add a `ProductDto` class to represent the data structure.
   - Include the following fields with appropriate data annotations:
     - `Id` (int, auto-generated).
     - `Name` (string, required, max 100 characters).
     - `Price` (decimal, required, positive value).
     - `Description` (string, optional, max 500 characters).

3. **Build the `ProductsController`**:
   - Implement endpoints for the following:
     - `GET /api/products`: Retrieve all products.
     - `GET /api/products/{id}`: Retrieve a single product by ID.
     - `POST /api/products`: Add a new product.
     - `PUT /api/products/{id}`: Update an existing product.
     - `DELETE /api/products/{id}`: Delete a product by ID.
   - Use in-memory storage (e.g., a static list) to simulate a database.

4. **Test the API**:
   - Use Swagger UI to test all endpoints.

5. **Bonus Features**:
   - **Add Example Values and Descriptions**:
     - Annotate the `ProductDto` class to include example values and field descriptions in Swagger.
   - **File Upload**:
     - Add an endpoint that accepts a file upload.
   - **Mix Parameters**:
     - Create an endpoint that accepts a combination of query, path, and body parameters.


At the end of this exercise, you should have a complete Product CRUD API.

Good luck! 🚀