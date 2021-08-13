using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using Workout_Mobile_App.Models;

namespace Workout_Mobile_App.Data
{
    public class DayDatabase
    {
        readonly SQLiteAsyncConnection database;

        public DayDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Day>().Wait();
        }

        public Task<List<Day>> GetDaysAsync(int id)
        {
            return database.Table<Day>().Where(i => i.WorkoutKey == id).ToListAsync();
        }

        public Task<List<Day>> GetDaysWithHigherIDAsync(Day day)
        {
            return database.Table<Day>().Where(i => i.WorkoutKey == day.WorkoutKey && i.ID > day.ID).ToListAsync();
        }

        public Task<Day> GetDayAsync(int id)
        {
            return database.Table<Day>()
                            .Where(i => i.ID == id)
                            .FirstOrDefaultAsync();
        }

        public Task<int> SaveDayAsync(Day day)
        {
            return database.InsertAsync(day);
        }

        public Task<int> DeleteDayAsync(Day day)
        {
            return database.DeleteAsync(day);
        }

        public Task<int> GetLastInsertID(int id)
        {
            return database.Table<Day>().Where(i => i.WorkoutKey == id).CountAsync();
        }
    }
}
