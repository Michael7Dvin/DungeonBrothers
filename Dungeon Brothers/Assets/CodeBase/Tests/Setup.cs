using CodeBase.Infrastructure.Services.Logging;
using NSubstitute;

namespace CodeBase.Tests
{
    public class Setup
    {
        public static HEALTH Health(float initialValue)
        {
            var health = Create.Health();

            ICustomLogger logger = Substitute.For<ICustomLogger>();
            I_SOME_SERVICE someService = Substitute.For<I_SOME_SERVICE>();

            health.InjectDependencies(logger, someService);
            health.Construct(initialValue);
            return health;
        }
    }
}