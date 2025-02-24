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
public class AlgorithmViewModel : ViewModelBase{
    public ICommand NavigateHomeCommand { get; set; }
    public ICommand NavigateNaiveCommand { get; set; }
    public ICommand NavigateKMPCommand { get; set; }
    public ICommand NavigateRabinKarpCommand { get; set; }
    public ICommand NavigateBoyerMooreCommand { get; set; }

    public AlgorithmViewModel(NavigationStore navigationStore) {
        NavigateHomeCommand = new NavigateCommand<HomeViewModel>(navigationStore, () => new HomeViewModel(navigationStore));

        NavigateNaiveCommand = new NavigateCommand<NaiveViewModel>(navigationStore, () => new NaiveViewModel(navigationStore));

        NavigateKMPCommand = new NavigateCommand<KMPViewModel>(navigationStore, () => new KMPViewModel(navigationStore));

        NavigateBoyerMooreCommand = new NavigateCommand<BoyerMooreViewModel>(navigationStore, () => new BoyerMooreViewModel(navigationStore));

        NavigateRabinKarpCommand = new NavigateCommand<RabinKarpVIewModel>(navigationStore, () => new RabinKarpVIewModel(navigationStore));
    
    }

}
