namespace PersonalOrganiser.Models
{
    using System;
    using System.Collections.ObjectModel;

    using PersonalOrganiser.Mvvm;

    public class TaskModel : ViewModelBase
    {
        private int id;
        private string title;
        private bool isDone;
        private DateTime taskDateTime;
        private TaskImportanceModel taskImportance;

        public ObservableCollection<TaskImportanceModel> importances = new ObservableCollection<TaskImportanceModel>()
        {
            new TaskImportanceModel(1, "Trivial", "Cyan", 5),
            new TaskImportanceModel(2, "Can Wait", "Lime", 4),
            new TaskImportanceModel(3, "Soon", "Yellow", 3),
            new TaskImportanceModel(4, "Important", "DarkOrange", 2),
            new TaskImportanceModel(5, "Urgent", "Red", 1)
        };

        public TaskModel()
            : this(string.Empty, DateTime.Now, null)
        {
        }

        public TaskModel(string title, DateTime taskDateTime, TaskImportanceModel taskImportance)
        {
            this.Title = title;
            this.TaskDateTime = taskDateTime;
            this.TaskImportance = taskImportance;
            this.IsDone = false;
        }

        public TaskModel(TaskModel newTask)
            : this(newTask.Title, newTask.TaskDateTime, newTask.taskImportance)
        {
        }

        public int Id
        {
            get
            {
                return this.id;
            }
            set
            {
                Set(ref id, value);
            }
        }

        public string Title
        {
            get
            {
                return this.title;
            }
            set
            {
                Set(ref title, value);
            }
        }

        public TaskImportanceModel TaskImportance
        {
            get
            {
                return this.taskImportance;
            }
            set
            {
                if (value == null)
                {
                    value = importances[0];
                }
                Set(ref taskImportance, value);
            }
        }

        public bool IsDone
        {
            get
            {
                return this.isDone;
            }
            set
            {

                Set(ref isDone, value);
            }
        }

        public DateTime TaskDateTime
        {
            get
            {
                return this.taskDateTime;
            }
            set
            {
                Set(ref taskDateTime, value);
            }
        }

        public TimeSpan TaskDateTimeTimeSpanProxy
        {
            get
            {
                return taskDateTime - taskDateTime.Date;
            }
            set
            {
                if (TaskDateTimeTimeSpanProxy != value)
                {
                    TaskDateTime = taskDateTime.Date.Add(value);
                    RaisePropertyChanged("TaskDateTimeTimeSpanProxy");
                }
            }
        }

        public ObservableCollection<TaskImportanceModel> Importances
        {
            get
            {
                return this.importances;
            }
        }
    }
}