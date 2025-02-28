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
using static System.Net.Mime.MediaTypeNames;

namespace string_matching_algorithm.ViewModels; 
public class NaiveViewModel : ViewModelBase {

    #region Properties
    public string Txt {
        get => _txt;
        set {
            if (_txt != value) {
                _txt = value;
                OnPropertyChanged();
            }
        }
    }
    private string _txt;
    public string Pattern {
        get => _pattern;
        set {
            if (_pattern != value) {
                _pattern = value;
                OnPropertyChanged();
            }
        }
    }
    private string _pattern;

    #endregion
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

    public void NaiveAlogorithm(object? parameter = null) {
        
        int M = Txt.Length;
        int N = Pattern.Length;

        for (int i = 0; i <= N - M; i++) {
            int j;

            for (j = 0; j < M; j++) {
                if (Txt[i + j] != Pattern[j]) {
                    break;
                }
            }

            // If Txttern matches at index i
            if (j == M) {
                //find
            }
        }
    }
}
