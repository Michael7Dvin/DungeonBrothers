using CodeBase;
using CodeBase.Infrastructure.Services.Logging;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

public class HEALTH_TESTS_FOR_DEBUG
{
    [Test]
    public void WhenTakingDamage_AndDamageMoreOrEqualCurrentHealth_ThenIsDeadShouldBeTrue()
    {
        // Arrange.
        ICustomLogger logger = Substitute.For<ICustomLogger>();
        I_SOME_SERVICE_FOR_UNIT_TEST_DEBUG someService = Substitute.For<I_SOME_SERVICE_FOR_UNIT_TEST_DEBUG>();
        
        HEALTH_FOR_UNIT_TEST_DEBUG health = new(logger, someService);
        health.Construct(100);

        // Act.
        health.TakeDamage(100);
        
        // Assert.
        health.IsDead.Should().Be(true);
    }
}
