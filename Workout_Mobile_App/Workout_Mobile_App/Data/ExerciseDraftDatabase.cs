using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using Workout_Mobile_App.Models;

namespace Workout_Mobile_App.Data
{
    public class ExerciseDraftDatabase
    {
        readonly SQLiteAsyncConnection database;

        public ExerciseDraftDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<ExerciseDraft>().Wait();
        }

        public Task<List<ExerciseDraft>> GetDraftExercisesAsync()
        {
            return database.Table<ExerciseDraft>().ToListAsync();
        }

        public Task<ExerciseDraft> GetDraftExerciseAsync(int id)
        {
            return database.Table<ExerciseDraft>()
                            .Where(i => i.ID == id)
                            .FirstOrDefaultAsync();
        }

        public Task<int> UpdateDraftExerciseAsync(ExerciseDraft exercise)
        {
            return database.UpdateAsync(exercise);
        }

        public Task<int> SaveDraftExerciseAsync(ExerciseDraft exercise)
        {
            return database.InsertAsync(exercise);
        }

        public Task<int> DeleteDraftExerciseAsync(ExerciseDraft exercise)
        {
            return database.DeleteAsync(exercise);
        }
    }
}
