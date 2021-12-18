using SimpleMVVM;
using SimpleMVVMDemo.Views;
using System.Windows.Input;

namespace SimpleMVVMDemo.ViewModel
{
    public class SignInViewModel : ViewModelBase
    {

        private string account;

        public string Account { get => account; set => SetProperty(ref account, value); }

        private string password;

        public string Password
        {
            get => password;
            set
            {
                password = value;
                OnPropertyChanged();
            }
        }

        private SimpleCommand signIn;
        public ICommand SignIn => signIn ??= new SimpleCommand(PerformSignIn);

        private void PerformSignIn()
        {
            if (Account == "admin" && Password == "admin")
            {
                Home home = new Home();
                home.ShowDialog();
                WinSta = System.Windows.WindowState.Minimized;
            }
        }

        private System.Windows.WindowState winSta;

        public System.Windows.WindowState WinSta { get => winSta; set => SetProperty(ref winSta, value); }

        private SimpleCommand clickSetting;
        public ICommand ClickSetting => clickSetting ??= new SimpleCommand(PerformClickSetting);

        private void PerformClickSetting()
        {
            SettingWindow setting = new();
            setting.ShowDialog();
            WinSta = System.Windows.WindowState.Minimized;
        }
    }
}
