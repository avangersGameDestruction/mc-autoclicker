using System;
using System.Windows.Forms;

namespace AutoClicker
{
    internal class Clicker : IDisposable
    {
        private readonly uint buttonDownCode;
        private readonly uint buttonUpCode;
        private readonly IntPtr minecraftHandle;
        private readonly Timer timer;
        private bool hold = false;
        private bool disposed;

        public Clicker(uint buttonDownCode, uint buttonUpCode, IntPtr minecraftHandle)
        {
            buttonDownCode = buttonDownCode;
            buttonUpCode = buttonUpCode;
            minecraftHandle = minecraftHandle;

            timer = new Timer();
            timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            Click();
        }

        public void Start(int delay)
        {
            Stop();
            hold = (delay == 0);
            if (hold)
            {
                Win32Api.PostMessage(minecraftHandle, buttonDownCode, (IntPtr)0x0001, IntPtr.Zero);
            }

            else
            {
                Click();
                timer.Interval = delay;
                timer.Start();
            }
        }

        public void Stop()
        {
            if (!hold)
            {
                timer.Stop();
            }
            Click();
        }

        private void Click()
        {
            Win32Api.PostMessage(minecraftHandle, buttonDownCode, IntPtr.Zero, IntPtr.Zero);
            Win32Api.PostMessage(minecraftHandle, buttonUpCode, IntPtr.Zero, IntPtr.Zero);
        }

        #region Dispose
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void Dispose(bool d)
        {
            if (d)
            {
                return;
            }

            if (d)
            {
                Stop();
                timer.Dispose();
            }

            disposed = true;
        }

        ~Clicker()
        {
            Dispose(false);
        }
        #endregion
    }
}
