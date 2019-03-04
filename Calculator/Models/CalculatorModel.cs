using System;

namespace Calculator.Models
{
    public class CalculatorModel
    {
        private double num1, num2;
        private string operation;
        private double result;

        public double NumberOne
        {
            get { return num1; }
            set { num1 = value; }
        }
        public double NumberTwo
        {
            get { return num2; }
            set { num2 = value; }
        }
        public string Operation
        {
            get { return operation; }
            set { operation = value; }
        }
        public double Result
        {
            get { return result; }
        }

        public void DoTheMath()
        {
            switch (operation)
            {
                case ("+"):
                    result = num1 + num2;
                    break;
                case ("-"):
                    result = num1 - num2;
                    break;
                case ("*"):
                    result = num1 * num2;
                    break;
                case ("/"):
                    if (num2 == 0) throw new Exception("Деление на ноль");
                    result = num1 / num2;
                    break;
                case ("%"):
                    result = (num1 / 100) * num2;
                    break;
                case ("sqrt"):
                    if (num2 < 0) throw new Exception("Корень из отриц числа");
                    result = Math.Sqrt(num2);
                    break;
                case ("power2"):
                    result = Math.Pow(num2, 2);
                    break;
                case ("power3"):
                    result = Math.Pow(num2, 3);
                    break;
                case ("inverse"):
                    result = 1 / num2;
                    break;
            }
        }
    }
}
