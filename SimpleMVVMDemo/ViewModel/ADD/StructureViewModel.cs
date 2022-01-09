using SimpleMVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SimpleMVVMDemo.ViewModel
{
    public class StructureViewModel : ViewModelBase
    {

        private SimpleCommand clickSetting;
        public ICommand ClickSetting => clickSetting ??= new SimpleCommand(PerformClickSetting);

        private void PerformClickSetting()
        {
        }

        private System.Collections.IEnumerable movieCategories;

        public System.Collections.IEnumerable MovieCategories { get => movieCategories; set => SetProperty(ref movieCategories, value); }

        private SimpleCommand addCommand;
        public ICommand AddCommand => addCommand ??= new SimpleCommand(Add);

        private void Add()
        {
        }

        private SimpleCommand removeSelectedItemCommand;
        public ICommand RemoveSelectedItemCommand => removeSelectedItemCommand ??= new SimpleCommand(RemoveSelectedItem);

        private void RemoveSelectedItem()
        {
        }
    }
}
