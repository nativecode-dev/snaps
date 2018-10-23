using System;
using System.Collections.Generic;
using System.Linq;
using Snaps.Native;

namespace Snaps
{
    public class MenuHolder : IDisposable
    {
        public MenuHolder(IntPtr handle)
        {
            this.Handle = handle;
        }

        public IntPtr Handle { get; }

        public IList<MenuItem> MenuItems { get; } = new List<MenuItem>();

        protected bool Disposed { get; private set; }

        public void Dispose()
        {
        }

        public void Create()
        {
            foreach (var item in this.MenuItems)
            {
                item.Create();
            }
        }

        public void Destroy()
        {
            foreach (var item in this.MenuItems)
            {
                item.Destroy();
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing == false && this.Disposed)
            {
                return;
            }

            this.Disposed = true;

            foreach (var item in this.MenuItems)
            {
                item.Destroy();
            }

            this.MenuItems.Clear();
        }

        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if (msg == Imports.WM_SYSCOMMAND)
            {
                var item = this.MenuItems.SingleOrDefault(x => x.Id == wParam.ToInt32());

                if (item != null)
                {
                    handled = item.Click();
                }

                return this.Handle;
            }

            return IntPtr.Zero;
        }
    }
}