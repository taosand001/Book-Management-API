using AutoFixture;
using AutoFixture.Xunit2;

namespace Book_Management_API_Test.Data
{
    public class BookDataAttribute : AutoDataAttribute
    {
        public BookDataAttribute() : base(() =>
        {
            var fixture = new Fixture();
            fixture.Customizations.Add(new BookSpecimenBuilder());
            return fixture;
        })
        { }
    }
}
