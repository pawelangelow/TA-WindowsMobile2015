namespace PersonalOrganiser.Views
{
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Input;

    using Models;
    using Views.Tasker;
    using Data;
    using System.Linq;
    using ViewModels.Tasker;

    public sealed partial class TaskerPage : Page
    {
        public TaskerPage()
        {
            InitializeComponent();
            NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Disabled;
        }

        public TaskerPageViewModel ViewModel => this.DataContext as TaskerPageViewModel;

        private void GotoTaskDetails(object sender, TappedRoutedEventArgs e)
        {
            var task = (sender as Grid).Tag as TaskModel;
            Frame.Navigate(typeof(TaskDetailsPage), task.Id);
        }

        private void DeleteTask(object sender, RoutedEventArgs e)
        {
            var taskToDelete = (sender as Button).Parent;
        }

        private async void CheckBox_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var id = (sender as CheckBox).Tag as int?;

            var connection = DbInstance.Get();

            var finishedTask = DbInstance.GetAllTasksAsync().Result.Where(t => t.Id == id).FirstOrDefault();

            if (finishedTask != null)
            {
                finishedTask.IsDone = !finishedTask.IsDone;

                var taskToUpdate = new TaskDataModel()
                {
                    Id = finishedTask.Id,
                    Title = finishedTask.Title,
                    Date = finishedTask.TaskDateTime,
                    TaskImportanceId = finishedTask.TaskImportance.Id,
                    IsDone = finishedTask.IsDone
                };

                var result = await connection.UpdateAsync(taskToUpdate);
            }
            else
            {
                return;
            }
        }
    }
}
