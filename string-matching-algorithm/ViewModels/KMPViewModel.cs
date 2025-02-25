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

    public ICommand NavigateAlgorithmCommand { get; set; }
    //command
    public ICommand Click1Command { get; set; }
    //trigger
    public ICommand Click2Command { get; set; }
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

    }
    public void Click1(object sender) {
        //code command
        MessageBox.Show("Command da duoc thuc hien");
    }
    public void Click2(object sender) {
        //code trigger
        MessageBox.Show("Toi nhan duoc mot trigger");
    }
}
