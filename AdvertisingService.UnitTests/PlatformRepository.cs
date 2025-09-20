using AdvertisingService.Repositories;

namespace AdvertisingService.UnitTests
{
    public class PlatformRepositoryTests
    {
        [Fact]
        public void FindByLocation_ShouldReturnPlatformsFromAllPrefixes()
        {
            var repo = new PlatformRepository();
            repo.SetPlatforms(new Dictionary<string, HashSet<string>>
            {
                ["/ru"] = new() { "Яндекс.Директ" },
                ["/ru/svrd"] = new() { "Крутая реклама" },
                ["/ru/svrd/revda"] = new() { "Ревдинский рабочий" }
            });

            var result = repo.FindByLocation("/ru/svrd/revda");

            Assert.Contains("Яндекс.Директ", result);
            Assert.Contains("Крутая реклама", result);
            Assert.Contains("Ревдинский рабочий", result);
        }

        [Fact]
        public void FindByLocation_ShouldReturnEmpty_WhenNoMatches()
        {
            var repo = new PlatformRepository();
            repo.SetPlatforms(new Dictionary<string, HashSet<string>>());

            var result = repo.FindByLocation("/us");

            Assert.Empty(result);
        }
    }
}
