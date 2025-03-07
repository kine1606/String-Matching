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

    public string AboutMe {
        get => _aboutMe;
        set {
            _aboutMe = value;
        }
    }
    private string _aboutMe;
    public ICommand NavigateHomeCommand { get; }

    public AboutViewModel(NavigationStore navigationStore) {
        NavigateHomeCommand = new NavigateCommand<HomeViewModel>(navigationStore, () => new HomeViewModel(navigationStore));
        AboutMe = "Nguyễn Trung Kiên - 23520802\nHồ Nguyễn Tài Lợi - 23520869\nĐội ngũ phát triển ứng dụng đa nền tảng";
    }

}
