using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using Workout_Mobile_App.Models;

namespace Workout_Mobile_App.Data
{
    public class ExerciseDatabase
    {
        readonly SQLiteAsyncConnection database;

        public ExerciseDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Exercise>().Wait();
        }

        public Task<List<Exercise>> GetExercisesAsync(int dayId)
        {
            return database.Table<Exercise>().Where(i => i.DayKey == dayId).ToListAsync();
        }

        public Task<Exercise> GetExerciseAsync(int id)
        {
            return database.Table<Exercise>()
                            .Where(i => i.ID == id)
                            .FirstOrDefaultAsync();
        }

        public Task<int> UpdateExerciseAsync(Exercise exercise)
        {
            return database.UpdateAsync(exercise);
        }

        public Task<int> SaveExerciseAsync(Exercise exercise)
        {
            return database.InsertAsync(exercise);
        }

        public Task<int> DeleteExerciseAsync(Exercise exercise)
        {
            return database.DeleteAsync(exercise);
        }

        public Task<int> GetLastInsertedID()
        {
            return database.Table<Exercise>().CountAsync();
        }
    }
}
