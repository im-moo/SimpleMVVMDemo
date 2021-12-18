﻿using SimpleMVVM;

namespace SimpleMVVMDemo.ViewModel
{
    public class SampleDialogViewModel : ViewModelBase
    {
        private string? _name;

        public string? Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }
    }
}
