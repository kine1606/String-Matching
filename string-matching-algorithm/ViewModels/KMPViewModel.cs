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
public class KMPViewModel : ViewModelBase {
    public ICommand NavigateAlgorithmCommand { get; set; }

    public KMPViewModel(NavigationStore navigationStore) {
        NavigateAlgorithmCommand = new NavigateCommand<AlgorithmViewModel>(navigationStore, () => new AlgorithmViewModel(navigationStore));

    }

}
