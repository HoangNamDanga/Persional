using DemoCtyLamHai.Domain;
using System.Text.Json;

namespace DemoCtyLamHai.Infrastructure
{
    public class UserRepository
    {
        private readonly string _filePath = Path.Combine("Data", "users.json");

        public UserRepository()
        {
            if (!File.Exists(_filePath))
                File.WriteAllText(_filePath, "[]");
        }

        public List<User> GetAll()
        {
            var json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<User>>(json) ?? new List<User>();
        }

        public void SaveAll(List<User> users)
        {
            var json = JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_filePath, json);
        }
    }
}
