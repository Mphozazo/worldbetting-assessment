
using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using Xunit;

namespace WorldBetting.Assessment.WebAPI.Tests
{
    public class AutoMoqDataAttribute : AutoDataAttribute
    {
        public AutoMoqDataAttribute() : base(() => new Fixture().Customize(new AutoMoqCustomization()) )
            {}
    }
}