using AutoFixture;
using AutoFixture.Xunit2;

namespace Book_Management_API_Test.Data
{
    public class UserDataAttribute : AutoDataAttribute
    {
        public UserDataAttribute() : base(() =>
        {
            var fixture = new Fixture();
            fixture.Customizations.Add(new UserSpecimenBuilder());
            return fixture;
        })
        { }
    }
}
