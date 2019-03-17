namespace Snaps.Services
{
    using System;
    using System.Reactive.Subjects;
    using Native;

    public class ActiveWindowWatcher : IDisposable
    {
        private readonly IntPtr hook;

        public ISubject<IntPtr> Windows = new Subject<IntPtr>();

        private Imports.WinEventDelegate winEvent;

        public ActiveWindowWatcher()
        {
            this.winEvent = this.WinEventProc;

            this.hook = Imports.SetWinEventHook(Imports.EventSystemForeground, Imports.EventSystemForeground,
                IntPtr.Zero, this.winEvent, 0, 0, Imports.WinEventOutOfContext);
        }

        protected bool Disposed { get; private set; }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing && this.Disposed == false)
            {
                this.Disposed = true;

                Imports.UnhookWinEvent(this.hook);

                this.Windows.OnCompleted();

                this.winEvent = null;
            }
        }

        private void WinEventProc(IntPtr hWinEventHook, uint eventType, IntPtr hWnd, int @object, int child,
            uint dwEventThread, uint dwmsEventTime)
        {
            this.Windows.OnNext(hWnd);
        }
    }
}