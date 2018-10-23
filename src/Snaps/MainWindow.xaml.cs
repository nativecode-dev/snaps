using System;
using System.Collections.Concurrent;
using System.Windows;
using Snaps.Native;

namespace Snaps
{
    public sealed partial class MainWindow : Window, IObserver<IntPtr>, IDisposable
    {
        private readonly ConcurrentDictionary<IntPtr, MenuHolder>
            menus = new ConcurrentDictionary<IntPtr, MenuHolder>();

        public MainWindow(ActiveWindowWatcher watcher)
        {
            this.InitializeComponent();
            watcher.Windows.Subscribe(this);
        }

        public void OnCompleted()
        {
        }

        public void OnError(Exception error)
        {
        }

        public void OnNext(IntPtr value)
        {
            if (this.menus.ContainsKey(value))
            {
                return;
            }

            try
            {
                var menu = Imports.GetSystemMenu(value, false);
                var count = Imports.GetMenuItemCount(menu);

                if (count > 0)
                {
                    var holder = new MenuHolder(menu);

                    holder.MenuItems.Add(new MenuItem(holder)
                    {
                        Id = 10000,
                        Order = count + 1,
                        Position = Imports.MF_BYPOSITION | Imports.MF_SEPARATOR,
                        Text = string.Empty
                    });

                    holder.MenuItems.Add(new MenuItem(holder)
                    {
                        Id = 10001,
                        Order = count + 2,
                        Text = "Align: Bottom"
                    });

                    holder.MenuItems.Add(new MenuItem(holder)
                    {
                        Id = 10002,
                        Order = count + 3,
                        Text = "Align: Top"
                    });

                    if (this.menus.TryAdd(menu, holder))
                    {
                        holder.Create();
                    }
                }
            }
            catch
            {
                //
            }
        }

        public void Dispose()
        {
            foreach (var kvp in this.menus)
            {
                kvp.Value.Dispose();
            }

            this.menus.Clear();
        }
    }
}