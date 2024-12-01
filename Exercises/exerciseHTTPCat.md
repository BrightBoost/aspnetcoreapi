### Exercise: Calling the http.cat API in C#
- Learn how to make HTTP requests in C#.
- Understand HTTP status codes.
- Display cat images for specific status codes.
- How to handle responses and errors.
- Basic file handling in C#.

---

### Steps for the exercise

1. Create a new console app
2. Add the `System.Net.Http.Json` package
3. In the `Program.cs` file, do the following:
    - Ask for the input of which status code the user wants to see
    - Make a request to `https://http.cat/{statusCode}`
    - Get the image and save it in a local file
4. Run the program

### Bonus tasks

1. What happens if you enter an invalid HTTP status code? Add error handling for cases like `abc` or unsupported codes.
2. Modify the code to open the downloaded image automatically
3. Let users enter multiple status codes in one session:
4. Provide a predefined list of status codes for students to test (`200`, `404`, `500`, etc.).
5. For advanced folks, enhance the exercise by using a GUI framework (e.g., WPF or Windows Forms) to display the cat images directly in a window.


