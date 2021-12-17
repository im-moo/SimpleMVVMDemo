using System.Windows;
using System.Windows.Input;

namespace SimpleMVVMDemo.Views
{
    /// <summary>
    /// SignIn.xaml 的交互逻辑
    /// </summary>
    public partial class SignIn : Window
    {
        public SignIn()
        {
            InitializeComponent();
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
