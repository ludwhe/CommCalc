using CommCalc.Core.Abstractions;

namespace CommCalc.Core.Operators
{
    public class Multiplier : IMultiplier
    {
        public double Multiply(double a, double b)
        {
            return a * b;
        }
    }
}
