using System;

namespace Snaps.Services
{
    public class AppMenuAction : AppMenu
    {
        internal AppMenuAction(IntPtr menuHandle, string name, int id, int order, uint position,
            Action<AppMenuAction> callback) : base(menuHandle,
            name, id, order, position)
        {
            this.Callback = callback;
        }

        public Action<AppMenuAction> Callback { get; }
    }
}