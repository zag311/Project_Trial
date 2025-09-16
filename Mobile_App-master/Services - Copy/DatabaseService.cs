using Recraft.Models;
using SQLite;

namespace Recraft.Services
{
    public class DatabaseService
    {
        private readonly SQLiteAsyncConnection _database;

        public DatabaseService()
        {
            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "Ideass.db3");
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Idea>().Wait();
        }

        public Task<int> SaveIdeaAsync(Idea idea)
        {
            return _database.InsertAsync(idea);
        }

        public Task<List<Idea>> GetIdeasAsync() => _database.Table<Idea>().ToListAsync();

        public Task<List<Idea>> SearchIdeasAsync(string keyword) => _database.Table<Idea>()
            .Where(i => i.Title.ToLower().Contains(keyword.ToLower()) || i.ItemType.ToLower().Contains(keyword.ToLower()))
            .ToListAsync();
    }
}