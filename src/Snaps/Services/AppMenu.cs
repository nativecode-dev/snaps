using System;
using Snaps.Native;

namespace Snaps.Services
{
    public class AppMenu : IDisposable
    {
        internal AppMenu(IntPtr menuHandle, string name, int id, int order, uint position)
        {
            this.Id = id;
            this.MenuHandle = menuHandle;
            this.Name = name;
            this.Order = order;
            this.Position = position;

            Imports.InsertMenu(this.MenuHandle, (uint) this.Order, this.Position, (uint) this.Id, this.Name);
        }

        public IntPtr MenuHandle { get; }

        public int Id { get; }

        public string Name { get; }

        public int Order { get; }

        public uint Position { get; }

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

                Imports.RemoveMenu(this.MenuHandle, (uint) this.Order, this.Position);
            }
        }
    }
}