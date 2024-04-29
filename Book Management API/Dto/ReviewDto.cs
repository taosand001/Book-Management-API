namespace Book_Management_API.Dto
{
   
   public record ReviewDto(
   int BookId,
   int Rating,
   string Comment);
}
