using Microsoft.AspNetCore.Identity;

namespace QuoteCrudApi.Models {
    public class QuoteDto {
        public int Id {get; set; }
        public string Text { get; set; }

        public string Author { get; set; }

        public QuoteDto(int id, string text, string author) 
        {
            Id = id;
            Text = text;
            Author = author;
        }

        public QuoteDto() {}

    }
}

