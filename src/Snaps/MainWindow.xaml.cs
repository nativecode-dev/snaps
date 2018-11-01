using System;
using System.Collections.Concurrent;
using System.Windows;
using Snaps.Services;

namespace Snaps
{
    public sealed partial class MainWindow : Window, IObserver<IntPtr>, IDisposable
    {
        private readonly ConcurrentDictionary<IntPtr, AppWindowInstance> instances =
            new ConcurrentDictionary<IntPtr, AppWindowInstance>();

        public MainWindow(ActiveWindowWatcher watcher)
        {
            this.InitializeComponent();
            watcher.Windows.Subscribe(this);
        }

        public void Dispose()
        {
            foreach (var kvp in this.instances)
            {
                kvp.Value.Dispose();
            }

            this.instances.Clear();
        }

        public void OnCompleted()
        {
        }

        public void OnError(Exception error)
        {
        }

        public void OnNext(IntPtr value)
        {
            var cached = this.instances.ContainsKey(value);

            if (cached)
            {
                return;
            }

            if (AppWindowInstance.CreateInstance(value, out var instance) == false)
            {
                return;
            }

            if (this.instances.TryAdd(value, instance))
            {
                instance.AddSeparator(10001);
                instance.AddMenu("Align Top", 10002, AlignTop);
            }
        }

        private static void AlignTop(AppMenuAction action)
        {
        }
    }
}