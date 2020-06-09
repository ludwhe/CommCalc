using System.ServiceModel;

namespace CommCalc.Contracts
{
    [ServiceContract]
    public interface ICalcService
    {
        [OperationContract]
        double Add(double a, double b);

        [OperationContract]
        double Multiply(double a, double b);
    }
}