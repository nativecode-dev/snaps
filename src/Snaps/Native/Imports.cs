namespace Snaps.Native
{
    using System;
    using System.Runtime.InteropServices;

    public static class Imports
    {
        public delegate void WinEventDelegate(IntPtr hook, uint eventType, IntPtr hWnd, int @object, int child,
            uint eventThread, uint eventTime);

        public const uint EventSystemForeground = 3;

        public const uint WinEventOutOfContext = 0;

        public const uint MIIM_STATE = 0x00000001;
        public const uint MIIM_ID = 0x00000002;
        public const uint MIIM_SUBMENU = 0x00000004;
        public const uint MIIM_TYPE = 0x00000010;
        public const uint MIIM_DATA = 0x00000020;
        public const uint MIIM_STRING = 0x00000040;
        public const uint MIIM_BITMAP = 0x00000080;
        public const uint MIIM_FTYPE = 0x00000100;
        public const uint MF_ENABLED = 0x00000000;

        public const uint WM_SYSCOMMAND = 0x112;
        public const uint MF_SEPARATOR = 0x800;
        public const uint MF_BYPOSITION = 0x400;
        public const uint MF_STRING = 0x0;

        private const string UserDll = "user32.dll";

        [DllImport(UserDll)]
        public static extern int GetMenuItemCount(IntPtr menu);

        [DllImport(UserDll)]
        public static extern bool InsertMenu(IntPtr menu, uint position, uint flags, uint newItem, string text);

        [DllImport(UserDll)]
        public static extern bool RemoveMenu(IntPtr menu, uint position, uint flags);

        [DllImport(UserDll)]
        public static extern IntPtr SetWinEventHook(uint min, uint max, IntPtr proc, WinEventDelegate @delegate,
            uint process, uint thread, uint flags);

        [DllImport(UserDll)]
        public static extern bool UnhookWinEvent(IntPtr hook);
    }
}