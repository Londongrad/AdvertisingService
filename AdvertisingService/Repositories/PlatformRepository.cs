namespace AdvertisingService.Repositories
{
    public class PlatformRepository
    {
        private readonly Dictionary<string, HashSet<string>> _locationMap = new();

        public void SetPlatforms(Dictionary<string, HashSet<string>> platforms)
        {
            _locationMap.Clear();
            foreach (var kv in platforms)
                _locationMap[kv.Key] = new(kv.Value);
        }

        public List<string> FindByLocation(string location)
        {
            var result = new HashSet<string>();

            var parts = location.Split('/', StringSplitOptions.RemoveEmptyEntries);

            // идём от самой глубокой локации к корневой
            for (int i = parts.Length; i > 0; i--)
            {
                var prefix = "/" + string.Join("/", parts.Take(i));

                if (_locationMap.TryGetValue(prefix, out var platforms))
                {
                    result.UnionWith(platforms);
                }
            }

            return result.ToList();
        }
    }
}
