﻿using Codeer.Friendly.Windows;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using System.Diagnostics;

namespace Driver.TestController
{
    // ReSharper disable once UnusedMember.Global
    public static class LifeCycleClassTest
    {
        static WindowsAppFriend _app;

        // ReSharper disable once UnusedMember.Global
        public static WindowsAppFriend Start()
        {
            if (_app != null)
            {
                bool exist = false;
                try
                {
                    // ReSharper disable once ConditionIsAlwaysTrueOrFalse
                    exist = Process.GetProcessById(_app.ProcessId) != null;
                }
                catch
                {
                    // ignored
                }

                if (!exist) _app = null;
            }

            if (_app == null) _app = ProcessController.Start();
            else _app.ResetTimeout();

            return _app;
        }

        // ReSharper disable once UnusedMember.Global
        public static void Clear()
        {
            if (_app == null) return;

            if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
            {
                End();
                return;
            }

            _app.ClearTimeout();
        }

        public static void End()
        {
            _app.Kill();
            _app = null;
        }
    }
}
