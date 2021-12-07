using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Calculator
{
    public class MainWindowViewModel : ViewModelBase
    {
        public string EquationValue => string.Join(null, components) + numberString;
        public string ResultValue { get; set; }
        public Command<string> TypeNumber { get; private set; }
        public Command<string> TypeOperation { get; }
        public Command TypeCancel { get; }
        public Command TypeEquals { get; }

        private List<string> components = new();

        private string numberString = "";

        public MainWindowViewModel()
        {
            TypeNumber = new Command<string>(_TypeNumber);
            TypeOperation = new Command<string>(_TypeOperation);
            TypeCancel = new Command(_TypeCancel);
            TypeEquals = new Command(_TypeEquals);
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

        private void _TypeCancel()
        {
            if(ResultValue!="")
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
            var operatorList = new List<string> { "+", "-", "/", "*", "(", ")" };
            var componentsRnp = new List<string>();
            var stack = new Stack<string>();
            foreach (var c in components)
            {
                if (!operatorList.Contains(c))
                {
                    componentsRnp.Add(c);
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
                        componentsRnp.Add(op);
                    }
                }
                else
                {
                    var s_priority = -1;
                    var c_priority = c == "+" || c == "-" || c == "(" ? 0 : 1;

                    if (stack.Count != 0)
                    {
                        var s = stack.Peek();
                        s_priority = s == "+" || s == "-" || s == "(" ? 0 : 1;
                    }

                    if (c_priority <= s_priority)
                    {
                        while (stack.Count > 0)
                        {
                            var s = stack.Peek();
                            s_priority = s == "+" || s == "-" || s == "(" ? 0 : 1;
                            if (s_priority >= c_priority)
                            {
                                if (s != "(" && s != ")")
                                {
                                    componentsRnp.Add(stack.Pop());
                                }
                                else
                                {
                                    break;
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    stack.Push(c);
                }
            }

            while (stack.Count > 0)
            {
                componentsRnp.Add(stack.Pop());
            }

            stack.Clear();

            foreach (var c in componentsRnp)
            {
                if (!operatorList.Contains(c))
                {
                    stack.Push(c);
                    continue;
                }
                else
                {
                    var a = Convert.ToDouble(stack.Pop());
                    var b = Convert.ToDouble(stack.Pop());
                    switch (c)
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