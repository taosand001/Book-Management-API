using AutoFixture.Kernel;
using Book_Management_API.Dto;
using Book_Management_API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Book_Management_API_Test.Data
{
    internal class ReviewSpecimenBuilder : ISpecimenBuilder
    {
        public object Create(object request, ISpecimenContext context)
        {
            if (request is Type type && type == typeof(Review))
            {
                return new Review
                {
                    Id = 1,
                    Rating = 1,
                    Comment = "Test",
                    BookId = 1,
                    UserId = "vis"
                };
            }

            if( request is Type type1 && type1 == typeof(ReviewDto))
            {
                return new ReviewDto
                     (BookId: 1,
                       Rating: 1,
                       Comment: "Test"
                    );
            }

            return new NoSpecimen();
        }

       
    }
}
