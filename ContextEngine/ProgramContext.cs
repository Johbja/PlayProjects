using System.Collections.Concurrent;

namespace ContextEngine;

public class ProgramContext
{
    private ConcurrentQueue<IContextExecutableItem> contextExecutionOrder = new ConcurrentQueue<IContextExecutableItem>();
    private ProgramContext? subContext = null;

    public void EnqueueExecution(IContextExecutableItem item)
    {
        contextExecutionOrder.Enqueue(item);
    }

    public DisposableContext AddSubContext(ProgramContext subContext)
    {
        this.subContext = subContext;

        return new DisposableContext(() => { this.subContext = null; });
    }

    public async Task ExecuteContextAsync()
    {
        while (true)
        {
            var activeContext = this;
            while (activeContext.subContext is not null)
                activeContext = activeContext.subContext;

            if (activeContext.contextExecutionOrder.TryDequeue(out IContextExecutableItem? item))
            {
                item.Execute();
            }
            else
            {
                await Task.Delay(10);
            }
        }
    }
}

public class DisposableContext(Action disposeAction)
{
    public void Dispose()
    {
        disposeAction();
    }
}

public abstract class ContextExecutable(ProgramContext context)
{
    public virtual void ExecuteOnContext(IContextExecutableItem logic)
    {
        context.EnqueueExecution(logic);
    }
}

public interface IContextExecutableItem
{
    public void Execute();
}

public class ContextAction(Action action)
    : IContextExecutableItem
{
    public void Execute()
    {
        action();
    }
}