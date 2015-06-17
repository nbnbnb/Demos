using Service.Interface;
using Service.Interface.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.Text;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ChannelFactory<ICalculator> factory =
                new ChannelFactory<ICalculator>("httpPoint"))
            {
                ICalculator proxy = factory.CreateChannel();

                try
                {
                    Console.WriteLine(proxy.Divide(1, 0));
                }
                catch (WebException ex)
                {
                    string message = ex.Message;
                }
                catch (FaultException<ExceptionDetail> ex)
                {
                    string message = ex.Message;
                }
                catch (FaultException<CalculationError> ex)
                {
                    string message = ex.Message;
                }
                catch (FaultException ex)
                {
                    string message = ex.Message;
                }
                
                
                Console.Read();
            }
        }
    }
}
