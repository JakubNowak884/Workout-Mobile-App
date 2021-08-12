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

        public Task<List<Workout>> GetNotesAsync()
        {
            //Get all notes.
            return database.Table<Workout>().ToListAsync();
        }

        public Task<Workout> GetNoteAsync(int id)
        {
            // Get a specific note.
            return database.Table<Workout>()
                            .Where(i => i.ID == id)
                            .FirstOrDefaultAsync();
        }

        public Task<int> SaveNoteAsync(Workout note)
        {
            return database.InsertAsync(note);
        }

        public Task<int> DeleteNoteAsync(Workout note)
        {
            // Delete a note.
            return database.DeleteAsync(note);
        }
    }
}
