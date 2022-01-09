using SimpleMVVM;
using System.Windows.Input;

namespace SimpleMVVMDemo.ViewModel
{
    public class SignInUserControlViewModel : ViewModelBase
    {

        private SimpleCommand signIn;
        public ICommand SignIn => signIn ??= new SimpleCommand(PerformSignIn);

        private void PerformSignIn()
        {
        }

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


    }
}
