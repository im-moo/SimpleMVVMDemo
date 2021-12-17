using SimpleMVVM;
using System.Security;
using System.Windows;

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

        public static SecureString GetEncryptedPassword(DependencyObject obj)
        {
            return (SecureString)obj.GetValue(EncryptedPasswordProperty);
        }

        public static void SetEncryptedPassword(DependencyObject obj, SecureString value)
        {
            obj.SetValue(EncryptedPasswordProperty, value);
        }

        // Using a DependencyProperty as the backing store for EncryptedPassword.  This enables animation, styling, binding, etc...
        public static DependencyProperty EncryptedPasswordProperty =
            DependencyProperty.RegisterAttached("EncryptedPassword", typeof(SecureString), typeof(SignInViewModel));
    }
}
