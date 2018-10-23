using System;
using System.Runtime.InteropServices;

namespace Snaps.Native
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public class MenuItemInfo
    {
        public MenuItemInfo()
        {
        }

        public MenuItemInfo(int pfMask)
        {
            this.fMask = pfMask;
        }

        public int cbSize = Marshal.SizeOf(typeof(MenuItemInfo));
        public int fMask;
        public uint fType;
        public uint fState;
        public uint wID;
        public IntPtr hSubMenu;
        public IntPtr hbmpChecked;
        public IntPtr hbmpUnchecked;
        public IntPtr dwItemData;
        public string dwTypeData = null;
        public uint cch;
        public IntPtr hbmpItem;
    }
}
