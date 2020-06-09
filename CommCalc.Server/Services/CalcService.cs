using System;
using CommCalc.Contracts;
using CommCalc.Core.Abstractions;
using CommCalc.Core.Operators;

namespace CommCalc.Server.Services
{
    public class CalcService : ICalcService
    {
        private readonly IAdder _adder;
        private readonly IMultiplier _multiplier;

        public CalcService()
        {
            _adder = new Adder();
            _multiplier = new Multiplier();
        }

        public double Add(double a, double b)
        {
            Console.WriteLine($"Adding {a} and {b}.");
            return _adder.Add(a, b);
        }

        public double Multiply(double a, double b)
        {
            Console.WriteLine($"Multiplying {a} and {b}.");
            return _multiplier.Multiply(a, b);
        }
    }
}