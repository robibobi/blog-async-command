using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Tcoc.Blog.Async.Command;

namespace Tcoc.Blog.Async
{
    class MainViewModel : ViewModelBase
    {
        public AsyncCommand<string> CalculatePrimesCmd { get; }

        public List<int> Primes { get; set; }

        public MainViewModel()
        {
            CalculatePrimesCmd = new AsyncCommand<string>(OnCalculatePrimes, CanExecuteCalculation);
        }

        private bool CanExecuteCalculation(string countString)
        {
            return !String.IsNullOrEmpty(countString) && Regex.IsMatch(countString, @"^\d+$");
        }

        private Task OnCalculatePrimes(CancellationToken t, string countString)
        {
            return Task.Factory.StartNew(() =>
            {
                int count = Convert.ToInt32(countString);
                Primes = GeneratePrimesNaive(t, count)
                            .Take(1000)
                            .ToList();
                RaisePropertyChanged(nameof(Primes));
            });
        }

        private static List<int> GeneratePrimesNaive(CancellationToken t, int n)
        {
            List<int> primes = new List<int>();
            primes.Add(2);
            int nextPrime = 3;
            while (primes.Count < n && !t.IsCancellationRequested)
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
