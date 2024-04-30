using AutoFixture;
using AutoFixture.Xunit2;

namespace Book_Management_API_Test.Data
{
    public class UserDataFailureAttribute : AutoDataAttribute
    {
        public UserDataFailureAttribute() : base(() =>
        {
            var fixture = new Fixture();
            fixture.Customizations.Add(new UserFailureSpecimenBuilder());
            fixture.Inject<string>(null);
            return fixture;
        })
        { }
    }
}
