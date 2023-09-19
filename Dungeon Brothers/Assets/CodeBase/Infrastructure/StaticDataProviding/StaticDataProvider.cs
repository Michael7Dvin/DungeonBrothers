using CodeBase.Infrastructure.Addressable;
using CodeBase.Infrastructure.Configs.Character;

namespace CodeBase.Infrastructure.StaticDataProviding
{
    public class StaticDataProvider : IStaticDataProvider
    {
        public StaticDataProvider(AllStaticData
            allStaticData)
        {
            AllStaticData = allStaticData;
        }

        public AllStaticData AllStaticData { get; }
    }
}