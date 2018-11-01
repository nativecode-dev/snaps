using System;
using System.Collections.Generic;
using Snaps.Native;

namespace Snaps.Services
{
    public class AppWindowInstance : IDisposable
    {
        public AppWindowInstance(IntPtr windowHandle)
        {
            this.MenuHandle = Imports.GetSystemMenu(windowHandle, false);
            this.WindowHandle = windowHandle;
        }

        protected bool Disposed { get; private set; }

        protected ICollection<AppMenu> Items { get; } = new List<AppMenu>();

        protected int MenuCount => Imports.GetMenuItemCount(this.MenuHandle);

        protected IntPtr MenuHandle { get; }

        protected IntPtr WindowHandle { get; }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void AddMenu(string text, int id, Action<AppMenuAction> callback)
        {
            var order = this.MenuCount + this.Items.Count + 1;
            this.Items.Add(new AppMenuAction(this.MenuHandle, text, id, order, Imports.MF_BYPOSITION, callback));
        }

        public void AddSeparator(int id)
        {
            var order = this.MenuCount + this.Items.Count + 1;
            var position = Imports.MF_BYPOSITION | Imports.MF_SEPARATOR;
            this.Items.Add(new AppMenu(this.MenuHandle, string.Empty, id, order, position));
        }

        public static bool CreateInstance(IntPtr windowHandle, out AppWindowInstance instance)
        {
            var menuHandle = Imports.GetSystemMenu(windowHandle, false);
            var count = Imports.GetMenuItemCount(menuHandle);

            if (count > 0)
            {
                instance = new AppWindowInstance(windowHandle);
                return true;
            }

            instance = null;

            return false;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing && this.Disposed == false)
            {
                this.Disposed = true;

                foreach (var item in this.Items)
                {
                    item.Dispose();
                }

                this.Items.Clear();

                Imports.GetSystemMenu(this.WindowHandle, true);
            }
        }
    }
}