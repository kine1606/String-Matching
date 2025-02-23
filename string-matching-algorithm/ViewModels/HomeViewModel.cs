using NavigationMVVM.ViewModels;
using string_matching_algorithm.Commands;
using string_matching_algorithm.Stores;
using System.Windows.Input;

namespace string_matching_algorithm.ViewModels; 
public class HomeViewModel : ViewModelBase {
    public string WelcomeMessage => "Hello home";
    public ICommand NavigateAccountCommand { get; }
    public ICommand NavigateAboutCommand { get; }

    public HomeViewModel(NavigationStore navigationStore) {
        NavigateAboutCommand = new NavigateCommand<AboutViewModel>(navigationStore, () => new AboutViewModel(navigationStore));
    }
}
