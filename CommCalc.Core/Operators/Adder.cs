using CommCalc.Core.Abstractions;

namespace CommCalc.Core.Operators
{
    public class Adder : IAdder
    {
        public double Add(double a, double b)
        {
            return a + b;
        }
    }
}
