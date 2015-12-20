﻿namespace PersonalOrganiser.ViewModels.Tasker
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Windows.UI.Xaml.Navigation;
    using Template10.Services.NavigationService;

    using Contracts;
    using Models;
    using Mvvm;
    using Data;
    public class TaskDetailsPageViewModel : ViewModelBase
    {
        public TaskDetailsPageViewModel()
        {
            this.ContentViewModel = new ContentViewModel();
            this.ContentViewModel.Tasks = DbInstance.GetAllTasksAsync().Result;
        }

        IContentViewModel contentViewModel;
        public IContentViewModel ContentViewModel { get { return contentViewModel; } set { Set(ref contentViewModel, value); } }

        private string _Value = "Default";
        public string Value { get { return _Value; } set { Set(ref _Value, value); } }

        private TaskModel currentTask;
        public TaskModel CurrentTask { get { return currentTask; } set { Set(ref currentTask, value); } }

        public override void OnNavigatedTo(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            if (state.ContainsKey(nameof(Value)))
            {
                Value = state[nameof(Value)]?.ToString();
                state.Clear();
            }
            else
            {
                Value = parameter?.ToString();
                currentTask = DbInstance.GetAllTasksAsync().Result.FirstOrDefault(t => t.Id == int.Parse(Value));
            }
        }

        public override async Task OnNavigatedFromAsync(IDictionary<string, object> state, bool suspending)
        {
            if (suspending)
                state[nameof(Value)] = Value;
            await Task.Yield();
        }

        public override void OnNavigatingFrom(NavigatingEventArgs args)
        {
            args.Cancel = false;
        }

        public void DeleteTask()
        {
            NavigationService.Navigate(typeof(Views.TaskerPage));
        }
    }
}
