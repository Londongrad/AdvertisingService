using AdvertisingService.Repositories;

namespace AdvertisingService.Services
{
    public class PlatformService(PlatformRepository repository)
    {
        private readonly PlatformRepository _repository = repository;

        public void LoadFromFile(string filePath)
        {
            var locationMap = new Dictionary<string, HashSet<string>>();

            foreach (var line in File.ReadAllLines(filePath))
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                var parts = line.Split(':');
                if (parts.Length != 2) continue;

                var name = parts[0].Trim();
                var locations = parts[1]
                    .Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => x.Trim());

                foreach (var loc in locations)
                {
                    if (!locationMap.ContainsKey(loc))
                        locationMap[loc] = new HashSet<string>();

                    locationMap[loc].Add(name);
                }
            }

            _repository.SetPlatforms(locationMap);
        }

        public List<string> Search(string location) =>
            _repository.FindByLocation(location);
    }
}
