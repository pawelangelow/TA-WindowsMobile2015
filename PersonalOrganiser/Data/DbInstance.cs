namespace PersonalOrganiser.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.IO;
    using System.Threading.Tasks;

    using SQLite.Net;
    using SQLite.Net.Async;
    using SQLite.Net.Platform.WinRT;

    using Windows.Storage;

    using Models;

    public static class DbInstance
    {
        private static SQLiteAsyncConnection connection;

        private static async void InitAsync()
        {
            connection = GetDbConnectionAsync();
            var result = connection.CreateTablesAsync<ScheduledEventModel, TaskDataModel>();
            while (!result.IsCompleted)
            {

            }
        }

        private static SQLiteAsyncConnection GetDbConnectionAsync()
        {
            var dbFilePath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "eventScheduler.data");

            var connectionFactory =
                 new Func<SQLiteConnectionWithLock>(
                     () =>
                     new SQLiteConnectionWithLock(
                         new SQLitePlatformWinRT(),
                         new SQLiteConnectionString(dbFilePath, storeDateTimeAsTicks: false)));

            var asyncConnection = new SQLiteAsyncConnection(connectionFactory);

            return asyncConnection;
        }

        public static SQLiteAsyncConnection Get()
        {
            if (connection == null)
            {
                InitAsync();
            }

            return connection;
        }

        public static async Task<List<TaskModel>> GetAllTasksAsync()
        {
            var connection = Get();
            var data = await connection.Table<TaskDataModel>().ToListAsync().ConfigureAwait(false);
            var result = new List<TaskModel>();

            var importances = new TaskModel().Importances;

            foreach (var task in data)
            {
                var curTask = new TaskModel()
                {
                    Id = task.Id,
                    Title = task.Title,
                    IsDone = task.IsDone,
                    TaskDateTime = task.Date,
                    TaskImportance = importances.Where(t => t.Id == task.TaskImportanceId).FirstOrDefault()
                };

                result.Add(curTask);
            }

            return result;
        }
    }
}
