namespace PersonalOrganiser.ViewModels.Tasker
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Data;
    using Mvvm;
    using Models;

    public class MainPageAllTasksViewModel : ViewModelBase
    {
        public MainPageAllTasksViewModel()
        {
        }

        public IEnumerable<TaskModel> Tasks
        {
            get
            {
                return DbInstance.GetAllTasksAsync().Result;
            }
        }
    }

    public class MainPageTodayTasksViewModel : ViewModelBase
    {
        public MainPageTodayTasksViewModel()
        {        
        }

        public IEnumerable<TaskModel> Tasks
        {
            get
            {
                var tempViewModel = DbInstance.GetAllTasksAsync().Result.Where(t => t.TaskDateTime.DayOfYear == DateTime.Now.DayOfYear).ToList();
                return tempViewModel;
            }
        }
    }


    public class MainPageTomorrowTasksViewModel : ViewModelBase
    {
        public MainPageTomorrowTasksViewModel()
        {    
        }

        public IEnumerable<TaskModel> Tasks
        {
            get
            {
                var tempViewModel = DbInstance.GetAllTasksAsync().Result.Where(t => t.TaskDateTime.DayOfYear == DateTime.Now.DayOfYear + 1).ToList();
                return tempViewModel;
            }
        }
    }

    public class TaskerPageViewModel : ViewModelBase
    {
        public MainPageAllTasksViewModel MainPageAllTasksViewModel { get; set; } = new MainPageAllTasksViewModel();
        public MainPageTodayTasksViewModel MainPageTodayTasksViewModel { get; set; } = new MainPageTodayTasksViewModel();
        public MainPageTomorrowTasksViewModel MainPageTomorrowTasksViewModel { get; set; } = new MainPageTomorrowTasksViewModel();

        public TaskerPageViewModel()
        {
        }

        public ContentViewModel ContentViewModel
        {
            get
            {
                return App.ContentViewModel;
            }
            set
            {
                Set(ref App.ContentViewModel, value);
            }
        }

        public string Title
        {
            get
            {
                return "Tasks";
            }
        }

        public void GotoAddNewTaskPage()
        {
            NavigationService.Navigate(typeof(Views.Tasker.AddNewTaskPage));
        }

        public void GotoPrivacy()
        {
            NavigationService.Navigate(typeof(Views.SettingsPage), 1);
        }

        public void GotoAbout()
        {
            NavigationService.Navigate(typeof(Views.SettingsPage), 2);
        }
    }
}
