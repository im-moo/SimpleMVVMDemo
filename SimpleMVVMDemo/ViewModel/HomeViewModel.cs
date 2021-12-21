using MaterialDesignThemes.Wpf;
using SimpleMVVM;
using SimpleMVVMDemo.Models;
using SimpleMVVMDemo.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SimpleMVVMDemo.ViewModel
{
    public class HomeViewModel : ViewModelBase
    {
        public HomeViewModel()
        {

            AllItems = GenerateDemoItems();
            FilterItems(null);
        }
        private object uC;

        public object UC { get => uC; set => SetProperty(ref uC, value); }

        private SimpleCommand runExtendedDialogCommand;
        public ICommand RunExtendedDialogCommand => runExtendedDialogCommand ??= new SimpleCommand(async () => { await RunExtendedDialog(); });

        private async Task RunExtendedDialog()
        {
            //let's set up a little MVVM, cos that's what the cool kids are doing:
            var view = new SampleDialog
            {
                DataContext = new SampleDialogViewModel()
            };

            //show the dialog
            var result = await DialogHost.Show(view, "RootDialog", ExtendedOpenedEventHandler, ExtendedClosingEventHandler);

            //check the result...
            Debug.WriteLine("Dialog was closed, the CommandParameter used to close it was: " + (result ?? "NULL"));
        }
        private void ExtendedOpenedEventHandler(object sender, DialogOpenedEventArgs eventargs)
            => Debug.WriteLine("You could intercept the open and affect the dialog using eventArgs.Session.");

        private void ExtendedClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            if (eventArgs.Parameter is bool parameter &&
                parameter == false) return;

            //OK, lets cancel the close...
            eventArgs.Cancel();

            //...now, lets update the "session" with some new content!
            eventArgs.Session.UpdateContent(new SampleProgressDialog());
            //note, you can also grab the session when the dialog opens via the DialogOpenedEventHandler

            //lets run a fake operation for 3 seconds then close this baby.
            Task.Delay(TimeSpan.FromSeconds(3))
                .ContinueWith((t, _) => eventArgs.Session.Close(false), null,
                    TaskScheduler.FromCurrentSynchronizationContext());
        }

        private DemoItem selectedItem;

        public DemoItem SelectedItem
        {
            get => selectedItem;
            set
            {
                if (value == null || value.Equals(selectedItem)) return;

                selectedItem = value;
                OnPropertyChanged();
            }
        }

        private int selectedIndex;

        public int SelectedIndex { get => selectedIndex; set => SetProperty(ref selectedIndex, value); }

        private ObservableCollection<DemoItem> demoItems;

        public ObservableCollection<DemoItem> DemoItems
        {
            get => demoItems;
            set
            {
                demoItems = value;
                OnPropertyChanged();
            }
        }

        public string SearchKeyword { get => _searchKeyword; set { _searchKeyword = value; OnPropertyChanged(); FilterItems(_searchKeyword); } }

        private string _searchKeyword;

        private ObservableCollection<DemoItem> _allItems;

        public ObservableCollection<DemoItem> AllItems { get => _allItems; set => _allItems = value; }

        private void FilterItems(string keyword)
        {
            var filteredItems =
                string.IsNullOrWhiteSpace(keyword) ?
                AllItems :
                AllItems.Where(i => i.Name.ToLower().Contains(keyword.ToLower()));

            DemoItems = new ObservableCollection<DemoItem>(filteredItems);
        }

        private ObservableCollection<DemoItem> GenerateDemoItems()
        {
            var items = new ObservableCollection<DemoItem>();

            var item = new DemoItem("SettingUserControl", new SettingUserControl(), new[]{ DocumentationLink.DemoPageLink<object>() ,
                    DocumentationLink.WikiLink("Brush-Names", "Brushes"),
                        DocumentationLink.WikiLink("Custom-Palette-Hues", "Custom Palettes"),
                        DocumentationLink.WikiLink("Swatches-and-Recommended-Colors", "Swatches"),});
            var item1 = new DemoItem("SignInUserControl", new SignInUserControl(), new[]{ DocumentationLink.DemoPageLink<object>() ,
                    DocumentationLink.WikiLink("Brush-Names", "Brushes"),
                        DocumentationLink.WikiLink("Custom-Palette-Hues", "Custom Palettes"),
                        DocumentationLink.WikiLink("Swatches-and-Recommended-Colors", "Swatches"),});
            items.Add(item);
            items.Add(item1);
            return items;
        }

        private SimpleCommand head;
        public ICommand Head => head ??= new SimpleCommand(PerformHead);

        private void PerformHead()
        {
        }
    }
}
