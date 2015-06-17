using SimpleContract;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvCalculator
{
    [Export(typeof(ICalculator))]
    public class Calculator : ICalculator
    {

        public IList<IOperation> GetOperations()
        {
            return new List<IOperation>()
            {
                new Operation{Name="+",NumberOperands=2},
                new Operation{Name="-",NumberOperands=2},
                new Operation{Name="*",NumberOperands=2},
                new Operation{Name="/",NumberOperands=2},
                new Operation{Name="%",NumberOperands=2},
                new Operation{Name="++",NumberOperands=1},
                new Operation{Name="--",NumberOperands=1}
            };
        }

        public double Operate(IOperation operation, double[] operands)
        {
            double result = 0;
            switch (operation.Name)
            {
                case "+":
                    result = operands[0] + operands[1];
                    break;
                case "-":
                    result = operands[0] - operands[1];
                    break;
                case "*":
                    result = operands[0] * operands[1];
                    break;
                case "/":
                    result = operands[0] / operands[1];
                    break;
                case "%":
                    result = operands[0] - operands[1];
                    break;
                case "--":
                    result = --operands[0];
                    break;
                case "++":
                    result = ++operands[0];
                    break;
                default:
                    throw new InvalidOperationException(
                        String.Format("invalid operation {0}", operation.Name));
            }

            return result;
        }
    }
}
