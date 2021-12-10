using System;
using System.Collections.Generic;
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
        public Command TypeEquals { get; }
        public Command<string> TypeOperation { get; }

        private List<string> components = new List<string>();

        private string numberString = "";

        public string EquationValue => string.Join(null, components) + numberString;

        public string ResultValue { get; set; }

        public MainWindowViewModel()
        {
            TypeNumber = new Command<string>(_TypeNumber);
            TypeCancel = new Command(_TypeCancel);
            TypeEquals = new Command(_TypeEquals);
            TypeOperation = new Command<string>(_TypeOperation);
        }

        private void _TypeOperation(object obj)
        {
            if (numberString != "")
            {
                components.Add(numberString);
                numberString = "";
            }

            components.Add((string)obj);
            RaisePropertyChanged(nameof(EquationValue));
        }

        private void _TypeEquals()
        {
            if (numberString != "")
            {
                components.Add(numberString);
                numberString = "";
            }
            ResultValue = GetResult();
            RaisePropertyChanged(nameof(ResultValue));
        }

        private string GetResult()
        {
            //ONP :) 
            var operatorList = new List<string> { "+", "-", "*", "/", "(", ")" };
            var componentsOnp = new List<string>();
            var stack = new Stack<string>();

            foreach (var c in components)
            {
                if (!operatorList.Contains(c))
                {
                    componentsOnp.Add(c);
                    continue;
                }
                else if (c == "(")
                {
                    stack.Push(c);
                    continue;
                }
                else if (c == ")")
                {
                    while (stack.Count > 0)
                    {
                        var op = stack.Pop();
                        if (op == "(") break;
                        componentsOnp.Add(op);
                    }
                }
                else
                {
                    var stack_p = -1;
                    var c_p = c == "+" || c == "-" || c == "(" ? 0 : 1;

                    if (stack.Count != 0)
                    {
                        var s = stack.Peek();
                        stack_p = s == "+" || s == "-" || s == "(" ? 0 : 1;
                    }

                    if (c_p <= stack_p)
                    {
                        while (stack.Count > 0)
                        {
                            var s = stack.Peek();
                            stack_p = s == "+" || s == "-" || s == "(" ? 0 : 1;
                            if (stack_p >= c_p)
                            {
                                if (s != "(" && s != ")")
                                {
                                    componentsOnp.Add(stack.Pop());
                                }
                                else break;
                            }
                            else break;
                        }
                    }
                    stack.Push(c);
                }
            }

            while(stack.Count > 0)
            {
                componentsOnp.Add(stack.Pop());
            }

            stack.Clear();

            foreach(var c in componentsOnp)
            {
                if(!operatorList.Contains(c))
                {
                    stack.Push(c);
                    continue;
                }
                else
                {
                    var a = Convert.ToDouble(stack.Pop());
                    var b = Convert.ToDouble(stack.Pop());
                    switch(c)
                    {
                        case "+": stack.Push((b + a).ToString()); break;
                        case "-": stack.Push((b - a).ToString()); break;
                        case "*": stack.Push((b * a).ToString()); break;
                        case "/": stack.Push((b / a).ToString()); break;
                    }
                }
            }
            return stack.Pop();
        }

        private void _TypeCancel()
        {
            if (ResultValue != "")
            {
                components.Clear();
                ResultValue = "";
            }
            else if (numberString != "")
            {
                numberString = numberString.Substring(0, numberString.Length - 1);
            }
            else if (components.Count > 0)
            {
                components.RemoveAt(components.Count - 1);
            }

            RaisePropertyChanged(nameof(EquationValue));
            RaisePropertyChanged(nameof(ResultValue));
        }

        private void _TypeNumber(object obj)
        {
            numberString += (string)obj;
            RaisePropertyChanged(nameof(EquationValue));
        }
    }

    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged([CallerMemberName] string name = null)
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