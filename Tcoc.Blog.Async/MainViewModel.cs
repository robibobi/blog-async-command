using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tcoc.Blog.Async.Command;

namespace Tcoc.Blog.Async
{
    class MainViewModel
    {

        private AsyncCommand<double> _calculatePrimesCmd;

        public AsyncCommand<double> CalculatePrimesCmd => _calculatePrimesCmd; 

        public MainViewModel()
        {
            _calculatePrimesCmd = new AsyncCommand<double>(OnCalculatePrimes);
        }

        private Task OnCalculatePrimes(CancellationToken t, double primeCount)
        {
            return Task.Factory.StartNew(() =>
            {
                var primes = GeneratePrimesNaive((int)primeCount);
                Console.WriteLine($"Calculated {primes.Count} primes");
            });
        }


        private static List<int> GeneratePrimesNaive(int n)
        {
            List<int> primes = new List<int>();
            primes.Add(2);
            int nextPrime = 3;
            while (primes.Count < n)
            {
                int sqrt = (int)Math.Sqrt(nextPrime);
                bool isPrime = true;
                for (int i = 0; (int)primes[i] <= sqrt; i++)
                {
                    if (nextPrime % primes[i] == 0)
                    {
                        isPrime = false;
                        break;
                    }
                }
                if (isPrime)
                {
                    primes.Add(nextPrime);
                }
                nextPrime += 2;
            }
            return primes;
        }
    }
}
