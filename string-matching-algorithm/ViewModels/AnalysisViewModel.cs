using NavigationMVVM.ViewModels;
using string_matching_algorithm.Commands;
using string_matching_algorithm.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace string_matching_algorithm.ViewModels; 
public class AnalysisViewModel : ViewModelBase {
    public ICommand NavigateHomeCommand { get; set; }

    public AnalysisViewModel(NavigationStore navigationStore) {
        NavigateHomeCommand = new NavigateCommand<HomeViewModel>(navigationStore, () => new HomeViewModel(navigationStore));

    }
}
