
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;
using System.Windows.Controls;
    using System.ComponentModel;
    using System.Windows.Input;

namespace MyCalculatorv1.ViewModel
{
    public class CalculatorViewModel : ObservableObject
    {
        public CalculatorViewModel()
        {          

           // windowCalculator = new CalculatorButtons();
            foreach (Window window in Application.Current.Windows.OfType<CalculatorButtons>())
            {
                if (((CalculatorButtons)window).Name == "calculatorButtons")
                    windowCalculator = (CalculatorButtons)window;
            }

            title = string.Format("Simple calculator ");
        }

        private CalculatorButtons windowCalculator;

        private string resultValue;
        public string ResultValue
        {
            get
            {
                return resultValue;
            }
            set
            {
                resultValue = value;
                OnPropertyChanged("ResultValue");
            }
        }

        private string title;
        public string Title { 
            get {
                return title;
            } 
            set {
                title = value;
                OnPropertyChanged("Title");
            } 
        }

        private ICommand _shutDown;
        public ICommand ShutDown
        {
            get
            {
                if (_shutDown == null)
                {
                    _shutDown = new RelayCommand(param => Application.Current.Shutdown(), param => true);
                }
                return _shutDown;
            }
        }
        private ICommand _delete;
        public ICommand Delete
        {
            get
            {
                if (_delete == null)
                {
                    _delete = new RelayCommand((x) => { ResultValue = string.Empty; Title = "Clear all operations!"; }, param => true);
                }
                return _delete;
            }
        }

        private ICommand _pressNumber;
        public ICommand PressNumber
        {
            get
            {
                if (_pressNumber == null)
                {
                    _pressNumber = new RelayCommand((x) => { ResultValue += x; }, x => IsAlreadyComputed);
                }
                return _pressNumber;
            }
        }
        bool IsAlreadyComputed
        {
            get
            {
                // string operation = operatorType as string;
                if (windowCalculator.DisplayResultTextBox.Text == string.Empty)
                    return true;
                else
                    return  !windowCalculator.DisplayResultTextBox.Text.Contains("=");
            }
        }

        private ICommand _difference;
        public ICommand Difference
        {
            get
            {
                if (_difference == null)
                {
                    _difference = new RelayCommand((x) => { ResultValue += x; }, x => IsValid);
                }
                return _difference;
            }
        }

        private ICommand _multiply;
        public ICommand Multiply
        {
            get
            {
                if (_multiply == null)
                {
                    _multiply = new RelayCommand((x) => { ResultValue += x; },x => IsValid);
                }
                return _multiply;
            }
        }
        bool IsValid
        {
            get
            {
               // string operation = operatorType as string;

                return !windowCalculator.DisplayResultTextBox.Text.Contains("*")
                    && !windowCalculator.DisplayResultTextBox.Text.Contains("/")
                     && !windowCalculator.DisplayResultTextBox.Text.Contains("+")
                      && !windowCalculator.DisplayResultTextBox.Text.Contains("-");
            }
        }

        private ICommand _sum;
        public ICommand Sum
        {
            get
            {
                if (_sum == null)
                {
                    _sum = new RelayCommand((x) => { ResultValue += x; }, x => IsValid);
                }
                return _sum;
            }
        }

        private ICommand _substract;
        public ICommand Substract
        {
            get
            {
                if (_substract == null)
                {
                    _substract = new RelayCommand((x) => { ResultValue += x; }, x => IsValid);
                }
                return _substract;
            }
        }

        private ICommand _result;
        public ICommand Result
        {
            get
            {
                if (_result == null)
                {
                    _result = new RelayCommandGeneric<object>(ExecuteComputeResult, CanComputeResult);
                }
                return _result;
            }
        }

        private void ExecuteComputeResult(object param)
        {
            try
            {
                ComputeResult();
                Title = string.Format("Simple calculator like : {0}", windowCalculator.DisplayResultTextBox.Text);
            }
            catch (Exception exc)
            {
                ResultValue = Title = "Error!";
                MessageBox.Show("Error in calculations: " + exc.Message, "ERROR in COMPUTING", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        private bool CanComputeResult(object param)
        {
            TextBlock text = param as TextBlock;

            return text!= null ? text.Text.Length > 0 : false;            
        }

        private double lastCalculatedValue = 0;
        private void ComputeResult()
        {
            String operation;
            int iOperator = 0;
            string typedStrings = ResultValue;

            ResultValue = string.Empty;
            
            if (typedStrings.Contains("="))
            {
                ResultValue = string.Empty;
                return;
            }
            else if (typedStrings.Contains("+"))
            {
                iOperator = typedStrings.IndexOf("+");
            }
            else if (typedStrings.Contains("-"))
            {
                iOperator = typedStrings.IndexOf("-");
            }
            else if (typedStrings.Contains("*"))
            {
                iOperator = typedStrings.IndexOf("*");
            }
            else if (typedStrings.Contains("/"))
            {
                iOperator = typedStrings.IndexOf("/");
            }
            else
            {
                Title = "error in calculation!";
                return;
            }

            operation = typedStrings.Substring(iOperator, 1);
            double op1 = Convert.ToDouble(typedStrings.Substring(0, iOperator));
            double op2 = Convert.ToDouble(typedStrings.Substring(iOperator + 1, typedStrings.Length - iOperator - 1));

            if (operation == "+")
            {
                typedStrings += "=" + (op1 + op2);
            }
            else if (operation == "-")
            {
                typedStrings += "=" + (op1 - op2);
            }
            else if (operation == "*")
            {
                typedStrings += "=" + (op1 * op2);
            }
            else
            {
                typedStrings += "=" + (op1 / op2);
            }

            lastCalculatedValue = op1 + op2;
            ResultValue = typedStrings;

        }

    }
}
