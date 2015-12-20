namespace PersonalOrganiser.Views.Tasker
{
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Navigation;

    using ViewModels.Tasker;

    public sealed partial class AddNewTaskPage : Page
    {
        public AddNewTaskPage()
        {
            this.InitializeComponent();
            NavigationCacheMode = NavigationCacheMode.Disabled;
        }
        public AddNewTaskPageViewModel ViewModel => this.DataContext as AddNewTaskPageViewModel;
    }
}
