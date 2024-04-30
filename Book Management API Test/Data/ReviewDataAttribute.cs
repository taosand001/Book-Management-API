using AutoFixture;
using AutoFixture.Xunit2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Management_API_Test.Data
{
    public class ReviewDataAttribute : AutoDataAttribute
    {
        public ReviewDataAttribute() : base(() =>
        {
            var fixture = new Fixture();
            fixture.Customizations.Add(new ReviewSpecimenBuilder());
            return fixture;
        })
        { }


    }
}
