using NavigationMVVM.ViewModels;
using string_matching_algorithm.Commands;
using string_matching_algorithm.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace string_matching_algorithm.ViewModels; 
//brute force : vet' can.
public class NaiveViewModel : ViewModelBase {
    public string TextTemp {
        get => _textTemp;
        set {
            if (_textTemp != value) {
                _textTemp = value;
                OnPropertyChanged();
            }
        }
    }
    private string _textTemp;
    private ObservableCollection<string> _textList = new();
    public ObservableCollection<string> TextList {
        get => _textList;
        set { _textList = value; OnPropertyChanged(nameof(TextList)); }
    }
    public ICommand NavigateAlgorithmCommand { get; set; }
    public ICommand SearchCommand { get; set; }

    public NaiveViewModel(NavigationStore navigationStore) {
        NavigateAlgorithmCommand = new NavigateCommand<AlgorithmViewModel>(navigationStore, () => new AlgorithmViewModel(navigationStore));
        SearchCommand = new RelayCommand<object>(AddingTextBlock);
    }
    public void AddingTextBlock(object? sender = null) {
        TextList = new ObservableCollection<string>(TextTemp.Select(c => c.ToString()));
    }
}
