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
public class AboutViewModel : ViewModelBase {

    public string AboutMe => "Hello i am ho nguyen tai loi";
    public ICommand NavigateHomeCommand { get; }

    public AboutViewModel(NavigationStore navigationStore) {
        NavigateHomeCommand = new NavigateCommand<HomeViewModel>(navigationStore, () => new HomeViewModel(navigationStore));

    }

}
