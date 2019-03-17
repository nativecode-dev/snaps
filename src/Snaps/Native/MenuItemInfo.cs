namespace Snaps.Native
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public class MenuItemInfo
    {
        public int cbSize = Marshal.SizeOf(typeof(MenuItemInfo));
        public uint cch;
        public IntPtr dwItemData;
        public string dwTypeData = null;
        public int fMask;
        public uint fState;
        public uint fType;
        public IntPtr hbmpChecked;
        public IntPtr hbmpItem;
        public IntPtr hbmpUnchecked;
        public IntPtr hSubMenu;
        public uint wID;

        public MenuItemInfo()
        {
        }

        public MenuItemInfo(int pfMask)
        {
            this.fMask = pfMask;
        }
    }
}