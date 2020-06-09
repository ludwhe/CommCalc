using System;
using System.IO;
using System.Net;
using System.ServiceModel;
using System.Text;
using CommCalc.Contracts;

namespace CommCalc.Client
{
    public static class Program
    {
        private const string _basicHttpEndPointAddress = "http://localhost:8080/basichttp";
        private const string _soapEnvelopeContent = "<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\"><soapenv:Body><Echo xmlns = \"http://tempuri.org/\" ><text>Hello</text></Echo></soapenv:Body></soapenv:Envelope>";
        private static readonly Random _random = new Random();

        static void Main(string[] args)
        {
            CallUsingWcf();
            //CallUsingWebRequest();

            Console.WriteLine("Hit enter to exit");
            Console.ReadLine();
        }

        private static void CallUsingWcf()
        {
            var a = _random.NextDouble();
            var b = _random.NextDouble();
            var factory = new ChannelFactory<ICalcService>(new BasicHttpBinding(), new EndpointAddress(_basicHttpEndPointAddress));
            factory.Open();
            var channel = factory.CreateChannel();
            ((IClientChannel)channel).Open();
            Console.WriteLine($"http Add({a}, {b}) => {channel.Add(a, b)}");
            ((IClientChannel)channel).Close();
            factory.Close();

            a = _random.NextDouble();
            b = _random.NextDouble();
            factory = new ChannelFactory<ICalcService>(new NetTcpBinding(SecurityMode.None), new EndpointAddress("net.tcp://localhost:8808/nettcp"));
            factory.Open();
            channel = factory.CreateChannel();
            ((IClientChannel)channel).Open();
            Console.WriteLine($"net.tcp Add({a}, {b}) => {channel.Add(a, b)}");
            ((IClientChannel)channel).Close();
            factory.Close();

            a = _random.NextDouble();
            b = _random.NextDouble();
            factory = new ChannelFactory<ICalcService>(new BasicHttpBinding(), new EndpointAddress(_basicHttpEndPointAddress));
            factory.Open();
            channel = factory.CreateChannel();
            ((IClientChannel)channel).Open();
            Console.WriteLine($"http Multiply({a}, {b}) => {channel.Multiply(a, b)}");
            ((IClientChannel)channel).Close();
            factory.Close();

            a = _random.NextDouble();
            b = _random.NextDouble();
            factory = new ChannelFactory<ICalcService>(new NetTcpBinding(SecurityMode.None), new EndpointAddress("net.tcp://localhost:8808/nettcp"));
            factory.Open();
            channel = factory.CreateChannel();
            ((IClientChannel)channel).Open();
            Console.WriteLine($"net.tcp Multiply({a}, {b}) => {channel.Multiply(a, b)}");
            ((IClientChannel)channel).Close();
            factory.Close();
        }

        private static void CallUsingWebRequest()
        {
            // 
            // The following sample, creates a basic web request to the specified endpoint, sends the SOAP request and reads the response
            // 

            // Prepare the raw content
            var utf8Encoder = new UTF8Encoding();
            var bodyContentBytes = utf8Encoder.GetBytes(_soapEnvelopeContent);

            // Create the web request
            var webRequest = WebRequest.Create(new Uri(_basicHttpEndPointAddress));
            webRequest.Headers.Add("SOAPAction", "http://tempuri.org/IEchoService/Echo");
            webRequest.ContentType = "text/xml";
            webRequest.Method = "POST";
            webRequest.ContentLength = bodyContentBytes.Length;

            // Append the content
            var requestContentStream = webRequest.GetRequestStream();
            requestContentStream.Write(bodyContentBytes, 0, bodyContentBytes.Length);

            // Send the request and read the response
            using Stream responseStream = webRequest.GetResponse().GetResponseStream();
            using StreamReader responsereader = new StreamReader(responseStream);
            var soapResponse = responsereader.ReadToEnd();
            Console.WriteLine($"Http SOAP Response: {soapResponse}");
        }
    }
}
