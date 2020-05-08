using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace AutoClicker
{
    public static class Extensions
    {
        public static IEnumerable<T> AllControls<T>(this Control SP) where T : Control
        {
            var h = SP is T;

            if (h)
            {
                yield return (T) SP;
            }

            foreach (var c in SP.Controls.Cast<Control>())
            {
                foreach (var i in AllControls<T>(c))
                {
                    yield return i;
                }
            }
        }
    }
}
