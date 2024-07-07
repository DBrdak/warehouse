using System;

namespace Warehouse.UI.Observers;

public class ExceptionHandler : IObserver<Exception>
{
    public void OnNext(Exception value)
    {
        Console.WriteLine();
        //if (Debugger.IsAttached)
        //    Debugger.Break();

        //RxApp.MainThreadScheduler.Schedule(() => { throw value; });
    }

    public void OnError(Exception error)
    {
        Console.WriteLine();
        //if (Debugger.IsAttached)
        //    Debugger.Break();

        //RxApp.MainThreadScheduler.Schedule(() => { throw error; });
    }

    public void OnCompleted()
    {
        Console.WriteLine();
        //if (Debugger.IsAttached)
        //    Debugger.Break();
        //RxApp.MainThreadScheduler.Schedule(() => { throw new NotImplementedException(); });
    }
}