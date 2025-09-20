using AdvertisingService.Repositories;
using AdvertisingService.Services;

namespace AdvertisingService.IntegrationTests
{
    public class PlatformServiceTests
    {
        [Fact]
        public void LoadFromFile_ShouldParsePlatformsCorrectly()
        {
            var repo = new PlatformRepository();
            var service = new PlatformService(repo);

            var filePath = Path.GetTempFileName();
            File.WriteAllLines(filePath, new[]
            {
                "Яндекс.Директ:/ru",
                "Ревдинский рабочий:/ru/svrd/revda"
            });

            service.LoadFromFile(filePath);

            var result = service.Search("/ru/svrd/revda");

            Assert.Contains("Яндекс.Директ", result);
            Assert.Contains("Ревдинский рабочий", result);
        }

        [Fact]
        public void LoadFromFile_ShouldSkipInvalidLines()
        {
            var repo = new PlatformRepository();
            var service = new PlatformService(repo);

            var filePath = Path.GetTempFileName();
            File.WriteAllLines(filePath, new[]
            {
                "Некорректная строка без двоеточия",
                "Газета:/ru/msk"
            });

            service.LoadFromFile(filePath);

            var result = service.Search("/ru/msk");

            Assert.Single(result);
            Assert.Contains("Газета", result);
        }
    }
}
