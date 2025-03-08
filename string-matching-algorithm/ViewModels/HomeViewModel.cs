using NavigationMVVM.ViewModels;
using string_matching_algorithm.Commands;
using string_matching_algorithm.Stores;
using System.Windows.Input;

namespace string_matching_algorithm.ViewModels; 
public class HomeViewModel : ViewModelBase {
    public ICommand NavigateAlgorithmCommand { get; }
    public ICommand NavigateAnalysisCommand { get; }
    public ICommand NavigateAboutCommand { get; }
    public ICommand NavigateGuideCommand { get; }

    public HomeViewModel(NavigationStore navigationStore) {
        NavigateAboutCommand = new NavigateCommand<AboutViewModel>(navigationStore, () => new AboutViewModel(navigationStore));
        NavigateGuideCommand = new NavigateCommand<GuideViewModel>(navigationStore, () => new GuideViewModel(navigationStore));
        NavigateAlgorithmCommand = new NavigateCommand<AlgorithmViewModel>(navigationStore, () => new AlgorithmViewModel(navigationStore));
    }
}
