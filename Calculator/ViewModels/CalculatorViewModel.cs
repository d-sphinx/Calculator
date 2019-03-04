using System;
using System.Windows.Input;
using Calculator.Models;

namespace Calculator.ViewModels
{
    public class CalculatorCommand<T> : ICommand
    {
        private Action<T> exec;
        public event EventHandler CanExecuteChanged;
        public CalculatorCommand(Action<T> ex) { exec = ex; }
        public bool CanExecute(object parameter) { return true; }
        public void Execute(object parameter) { exec((T)parameter); }
    }

    public class CalculatorViewModel : ViewModelBase
    {
        private CalculatorModel Calc;

        private string tablo, prevOp;
        private bool firstNumberIsNull = true;
        private bool clearTablo = false;

        private CalculatorCommand<string> NumeralCommand;
        private CalculatorCommand<string> MathOpCommand;
        private CalculatorCommand<string> EditCommand;
        private CalculatorCommand<string> SpecialCommand;

        public ICommand Numeral { get { return NumeralCommand; } }
        public ICommand MathOp { get { return MathOpCommand; } }
        public ICommand Edit { get { return EditCommand; } }
        public ICommand Special { get { return SpecialCommand; } }

        public CalculatorViewModel()
        {
            Calc = new CalculatorModel();

            NumeralCommand = new CalculatorCommand<string>(NumeralClick);
            MathOpCommand = new CalculatorCommand<string>(MathOpClick);
            EditCommand = new CalculatorCommand<string>(EditClick);
            SpecialCommand = new CalculatorCommand<string>(SpecialClick);

            tablo = "0";
        }

        public string Tablo
        {
            get { return tablo; }
            set
            {
                tablo = value;
                OnPropertyChanged("Tablo");
            }
        }

        public string NumberOne
        {
            get { return Calc.NumberOne + ""; }
            set { Calc.NumberOne = Convert.ToDouble(value); }
        }

        public string NumberTwo
        {
            get { return Calc.NumberTwo + ""; }
            set { Calc.NumberTwo = Convert.ToDouble(value); }
        }

        public string Operation
        {
            get { return Calc.Operation; }
            set { Calc.Operation = value; }
        }

        public string Result
        {
            get { return Calc.Result + ""; }
        }

        public void NumeralClick(string num)
        {
            Tablo = (tablo == "0" || clearTablo) ?
                num : tablo + num;
            clearTablo = false;
        }

        public void MathOpClick(string op)
        {
            try
            {
                if (prevOp == "=" || firstNumberIsNull)
                {
                    NumberOne = tablo;
                    prevOp = op;
                    firstNumberIsNull = false;
                }
                else
                {
                    if (op == "%")
                    {
                        NumberTwo = Tablo;
                        Operation = op;
                        Calc.DoTheMath();
                        prevOp = "=";
                    }
                    else
                    {
                        NumberTwo = tablo;
                        Operation = prevOp;
                        Calc.DoTheMath();
                        prevOp = op;
                    }
                    Tablo = Result;
                    NumberOne = tablo;
                }
            }
            catch (Exception ex)
            {
                Tablo = ex.Message;
                firstNumberIsNull = true;
            }
            finally
            {
                clearTablo = true;
            }
        }

        public void EditClick(string cmd)
        {
            switch (cmd)
            {
                case "ce":
                    Tablo = "0";
                    break;
                case "c":
                    Tablo = "0";
                    firstNumberIsNull = true;
                    prevOp = string.Empty;
                    break;
                case "back":
                    Tablo = (tablo.Length > 1) ?
                        tablo.Substring(0, tablo.Length - 1) : "0";
                    break;
            }
        }

        public void SpecialClick(string cmd)
        {
            try
            {
                switch (cmd)
                {
                    case "sqrt":
                    case "power2":
                    case "power3":
                    case "inverse":
                        NumberTwo = tablo;
                        Operation = cmd;
                        Calc.DoTheMath();
                        Tablo = Result;
                        break;
                    case "+-":
                        Tablo = (tablo.Contains("-") || tablo == "0") ?
                            tablo.Remove(tablo.IndexOf("-"), 1) : "-" + tablo;
                        break;
                    case ",":
                        if (!tablo.Contains(","))
                        {
                            Tablo = tablo + ",";
                        }
                        break;

                }
            }
            catch (Exception ex)
            {
                Tablo = ex.Message;
                firstNumberIsNull = true;
                clearTablo = true;
            }
        }
    }
}
