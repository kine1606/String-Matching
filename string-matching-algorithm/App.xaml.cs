using string_matching_algorithm.Stores;
using string_matching_algorithm.ViewModels;
using System.Windows;

namespace string_matching_algorithm
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e) {

            NavigationStore navigationStore = new NavigationStore();

            navigationStore.CurrentViewModel = new HomeViewModel(navigationStore);

            MainWindow = new MainWindow() {
                DataContext = new MainViewModel(navigationStore)
            };
            MainWindow.Show();

            base.OnStartup(e);
        }
    }

}
