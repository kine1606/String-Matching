using NavigationMVVM.ViewModels;
using string_matching_algorithm.Stores;
using string_matching_algorithm.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace string_matching_algorithm.Commands; 
public class NavigateCommand<TViewModel> : CommandBase
    where TViewModel : ViewModelBase
    {
    private readonly NavigationStore _navigationStore;
    private readonly Func<TViewModel> _createViewModel;
    public NavigateCommand(NavigationStore navigationStore, Func<TViewModel> createViewModel) {
        _navigationStore = navigationStore;
        _createViewModel = createViewModel;
    }

    public override void Execute(object parameter) {
        _navigationStore.CurrentViewModel = _createViewModel();
    }
}
