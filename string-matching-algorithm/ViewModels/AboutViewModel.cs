using NavigationMVVM.ViewModels;
using string_matching_algorithm.Commands;
using string_matching_algorithm.Stores;
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
        AboutMe = @"
        Tên đồ án: AlgoMatch Application - Ứng dụng trực quan thuật toán đối sánh chuỗi
        Môn học: Advanced Data Structures & Algorithms
        Lớp học: CS523.P21
        Giảng viên hướng dẫn: ThS. Nguyễn Thanh Sơn
        Sinh viên 1: Nguyễn Trung Kiên - 23520802
        Sinh viên 2: Hồ Nguyễn Tài Lợi - 23520869
        ";
    }

}
