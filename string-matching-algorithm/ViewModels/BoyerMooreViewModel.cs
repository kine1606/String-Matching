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
public class BoyerMooreViewModel : ViewModelBase {
    #region Properties
    public int NO_OF_CHARS = 256;

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
    public ICommand NavigateAlgorithmCommand { get; set; }
    public ICommand ResultCommand { get; set; }
    public ICommand StepCommand { get; set; }
    public BoyerMooreViewModel(NavigationStore navigationStore) {
        NavigateAlgorithmCommand = new NavigateCommand<AlgorithmViewModel>(navigationStore, () => new AlgorithmViewModel(navigationStore));
        
        ResultCommand = new RelayCommand<object>(result);
    }
    public void result(object? parameter = null) {
        search(Txt.ToCharArray(), Pattern.ToCharArray());
    }
    public int max(int a, int b) { return (a > b) ? a : b; }
    public void badCharHeuristic(char[] str, int size,
                                int[] badchar) {
        int i;
        for (i = 0; i < NO_OF_CHARS; i++)
            badchar[i] = -1;

        for (i = 0; i < size; i++)
            badchar[(int)str[i]] = i;
    }
    public void search(char[] txt, char[] pat) {
        int m = pat.Length;
        int n = txt.Length;
        int[] badchar = new int[NO_OF_CHARS];
        badCharHeuristic(pat, m, badchar);
        int s = 0;
        while (s <= (n - m)) {
            int j = m - 1;
            while (j >= 0 && pat[j] == txt[s + j]) j--;
            if (j < 0) {
                //find match
                s += (s + m < n) ? m - badchar[txt[s + m]] : 1;
            }
            else
                //mismatch
                s += max(1, j - badchar[txt[s + j]]);
        }
    }
}
