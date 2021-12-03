using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Calculator
{
    public class MainWindowViewModel : ViewModelBase
    {
        public string DisplayValue { get; set; } = "";
        public double CurrentValue { get; set; }
        public string CurrentMode { get; set; } = "";
        public Command<string> TypeNumber { get; private set; }
        public Command TypeCancel { get; }
        public Command TypeAdd { get; }
        public Command TypeEquals { get; }

        public MainWindowViewModel()
        {
            TypeNumber = new Command<string>(_TypeNumber);
            TypeCancel = new Command(_TypeCancel);
            TypeAdd = new Command(_TypeAdd);
            TypeEquals = new Command(_TypeEquals);
        }

        private void _TypeEquals()
        {
            switch(CurrentMode)
            {
                case "+":
                    {
                        CurrentValue = CurrentValue + Convert.ToDouble(DisplayValue);
                        break;
                    }
            }
            CurrentMode = "";
            DisplayValue = CurrentValue.ToString();
            RaisePropertyChanged(nameof(DisplayValue));
        }

        private void _TypeAdd()
        {
            if(CurrentMode!="+")
            {
                CurrentMode = "+";
                CurrentValue = Convert.ToDouble(DisplayValue); 
            }

            DisplayValue = "";
            RaisePropertyChanged(nameof(DisplayValue));
        }

        private void _TypeCancel()
        {
            DisplayValue = "";
            RaisePropertyChanged(nameof(DisplayValue));
            CurrentMode = "";
        }

        private void _TypeNumber(object obj)
        {
            DisplayValue = DisplayValue + obj;
            RaisePropertyChanged(nameof(DisplayValue));
        }
    }

    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged([CallerMemberName] string name=null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }


    public class Command : ICommand
    {
        private readonly Action action;

        public event EventHandler CanExecuteChanged;

        public Command(Action action)
        {
            this.action = action;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            action?.Invoke();
        }
    }

    public class Command<T> : ICommand
    {
        private readonly Action<object> action;

        public event EventHandler CanExecuteChanged;

        public Command(Action<object> action)
        {
            this.action = action;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            action?.Invoke(parameter);
        }
    }

} 