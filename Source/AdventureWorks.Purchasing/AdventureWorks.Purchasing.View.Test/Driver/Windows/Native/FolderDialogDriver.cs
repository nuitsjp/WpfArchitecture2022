﻿using Codeer.Friendly.Windows;
using Codeer.Friendly.Windows.Grasp;
using Codeer.Friendly.Windows.NativeStandardControls;
using Codeer.TestAssistant.GeneratorToolKit;
using System;
using System.Linq;
using System.Threading;

namespace Driver.Windows.Native
{
    public class FolderDialogDriver
    {
        public WindowControl Core { get; private set; }
        public NativeTree Tree => new NativeTree(Core.IdentifyFromWindowClass("SysTreeView32"));
        public NativeButton Button_OK => new NativeButton(Core.IdentifyFromWindowText("OK"));
        public NativeButton Button_Cancel => new NativeButton(Core.IdentifyFromWindowText("キャンセル"));

        public FolderDialogDriver(WindowControl core)
        {
            Core = core;
        }

        public void EmulateSelect(params string[] names)
        {
            for (int i = 1; i <= names.Length; i++)
            {
                var current = names.Take(i).ToArray();
                while (true)
                {
                    var item = Tree.FindNode(current);
                    if (item != IntPtr.Zero)
                    {
                        Tree.EnsureVisible(item);
                        Tree.EmulateSelectItem(item);
                        if (i != names.Length) Tree.EmulateExpand(item, true);
                        break;
                    }
                    else Thread.Sleep(10);
                }
            }
        }
    }

    public static class FolderDialogDriver_Extensions
    {
        [WindowDriverIdentify(CustomMethod = "TryAttach")]
        public static FolderDialogDriver Attach_Dlg_Folder(this WindowsAppFriend app, string text)
            => new FolderDialogDriver(WindowControl.WaitForIdentifyFromWindowText(app, text));

        public static bool TryAttach(WindowControl window, out string title)
        {
            title = null;
            if (window.GetFromWindowClass("SysTreeView32").Length != 1 ||
            window.GetFromWindowText("OK").Length != 1 ||
            window.GetFromWindowText("キャンセル").Length != 1) return false;
            title = window.GetWindowText();
            return true;
        }
    }
}
