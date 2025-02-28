using Accessibility;
using NavigationMVVM.ViewModels;
using string_matching_algorithm.Commands;
using string_matching_algorithm.Stores;
using System.Windows;
using System.Windows.Input;

namespace string_matching_algorithm.ViewModels;
public class KMPViewModel : ViewModelBase {
    //param goc luu tru du lieu
    private string _nameTemp;
    //param su dung thuc te
    public string NameTemp {
        get => _nameTemp;
        set {
            _nameTemp = value;
            OnPropertyChanged();
        }
    }

    private string _patternString = "asd";
    public string PatternString
    {
        get => _patternString;
        set
        {
            _patternString = value;
            OnPropertyChanged();
        }
    }
    private string _textString = "jklsdfhjksdhjklaffhjklasd";
    public string TextString
    {
        get => _textString;
        set
        {
            _textString = value;
            OnPropertyChanged();
        }
    }

public ICommand NavigateAlgorithmCommand { get; set; }
    //command
    public ICommand Click1Command { get; set; }
    //trigger
    public ICommand Click2Command { get; set; }
    public ICommand Click3Command { get; set; }
    public KMPViewModel(NavigationStore navigationStore) {
        NavigateAlgorithmCommand = new NavigateCommand<AlgorithmViewModel>(navigationStore, () => new AlgorithmViewModel(navigationStore));
        
        //param binding 
        NameTemp = "honguyen tai loi";
        //function binding tu command 
        Click1Command = new RelayCommand<object>(Click1);
        //object la mot doi tuong can truyen vao 
        //truyen vao mot ham Click va thuc hien tai ham click

        //trigger
        Click2Command = new RelayCommand<object>(Click2);

        Click3Command = new RelayCommand<object>(Click3);
    }
    public void Click1(object sender) {
        //code command
        MessageBox.Show("Command da duoc thuc hien");
    }
    public void Click2(object sender) {
        //code trigger
        MessageBox.Show("Toi nhan duoc mot trigger");
    }
    public void Click3(object sender)
    {
        //code command
        int res = KMP(TextString, PatternString);
        MessageBox.Show(res.ToString()); 
    }

    void longestPrefixSuffix(string pattern, ref List<int> lps)
    {
        int length = 0;
        int i = 1;
        int m = pattern.Length;
        // algorithm that find matches between prefix and suffix
        // how many times they appear in pattern string
        while (i < m)
        {
            if (pattern[i] == pattern[length])
            {
                length++;
                lps[i] = length;
                i++;
            }
            else
            {
                if (length != 0) length = lps[length - 1];
                else
                {
                    lps[i] = 0;
                    i++;
                }
            }
        }
    }
     int KMP(string text, string pattern)
    {
        int res =0;
        int i = 0;
        int j = 0;
        int textLength = text.Length;
        int patternLength = pattern.Length;
        List<int> lps = new List<int>(new int[patternLength]);
        //find lps table
        longestPrefixSuffix(pattern, ref lps);
        while (i < textLength)
        {
            if (pattern[j] == text[i])
            {
                i++;
                j++;
                // found 
                if (j == patternLength)
                {
                    res = i - j;
                    return res;
                }
            }
            else
            {
                // if j is not initial value (0), move j to value in lps table
                if (j != 0) j = lps[j - 1];
                else i++;
            }
        }
        // not found
        return res;
    }
}
