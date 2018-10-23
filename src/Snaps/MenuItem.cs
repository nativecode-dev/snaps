using System;
using Snaps.Native;

namespace Snaps
{
    public class MenuItem
    {
        public MenuItem(MenuHolder holder)
        {
            this.Holder = holder;
        }

        public Func<bool> ClickHandler { get; set; }

        public uint Order { get; set; }

        public uint Position { get; set; } = Imports.MF_BYPOSITION;

        public string Text { get; set; }

        protected MenuHolder Holder { get; }

        public uint Id { get; set; }

        public void Create()
        {
            var created = Imports.InsertMenu(this.Holder.Handle, this.Order, this.Position, this.Id, this.Text);

            if (created == false)
            {
                throw new InvalidOperationException(
                    $"Failed to create menu item {this.Id} for handle {this.Holder.Handle}");
            }
        }

        public void Destroy()
        {
            Imports.RemoveMenu(this.Holder.Handle, this.Order, this.Position);
        }

        public bool Click()
        {
            if (this.ClickHandler != null)
            {
                return this.ClickHandler();
            }

            return false;
        }
    }
}