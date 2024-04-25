namespace Book_Management_API.Dto
{
    public record BookDto(
       string Author,
       string Title,
       string Genre,
       int Year
    );
}
