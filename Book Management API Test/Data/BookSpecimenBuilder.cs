using AutoFixture.Kernel;
using Book_Management_API.Dto;
using Book_Management_API.Model;

namespace Book_Management_API_Test.Data
{
    public class BookSpecimenBuilder : ISpecimenBuilder
    {
        public object Create(object request, ISpecimenContext context)
        {
            if (request is Type t && t == typeof(Book))
            {
                return new Book
                {
                    Id = 1,
                    Author = "Author",
                    Title = "Title",
                    Genre = "Genre",
                    Year = 2021
                };
            }

            if (request is Type t2 && t2 == typeof(BookDto))
            {
                return new BookDto
                (
                    Author: "Author",
                    Title: "Title",
                    Genre: "Genre",
                    Year: 2021
                );
            }
            return new NoSpecimen();
        }
    }
}
