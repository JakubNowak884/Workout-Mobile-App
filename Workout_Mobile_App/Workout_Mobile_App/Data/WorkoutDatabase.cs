using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using Workout_Mobile_App.Models;

namespace Workout_Mobile_App.Data
{
    public class WorkoutDatabase
    {
        readonly SQLiteAsyncConnection database;

        public WorkoutDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Workout>().Wait();
        }

        public Task<List<Workout>> GetWorkoutsAsync()
        {
            //Get all notes.
            return database.Table<Workout>().ToListAsync();
        }

        public Task<Workout> GetWorkoutAsync(int id)
        {
            // Get a specific note.
            return database.Table<Workout>()
                            .Where(i => i.ID == id)
                            .FirstOrDefaultAsync();
        }

        public Task<int> UpdateWorkoutAsync(Workout workout)
        {
            return database.UpdateAsync(workout);
        }

        public Task<int> SaveWorkoutAsync(Workout workout)
        {
            return database.InsertAsync(workout);
        }

        public Task<int> DeleteWorkoutAsync(Workout workout)
        {
            return database.DeleteAsync(workout);
        }

        public Task<int> CountWorkouts()
        {
            return database.Table<Workout>().CountAsync();
        }
    }
}
