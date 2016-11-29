using System.Threading.Tasks;
using System.Windows.Input;

namespace Tcoc.Blog.Async.Command
{
    // Artikel von Stephen Cleary
    // https://msdn.microsoft.com/en-us/magazine/dn630647.aspx

    interface IAsyncCommand : ICommand
    {
        Task ExecuteAsync(object parameter);
    }
}
