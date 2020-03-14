using System.Windows.Forms;

namespace BakeryX.Common.MonitorManagment
{
    public static class MonitorProperties
    {
        public static int DisplayWidth
        {
            get
            {
                return Screen.PrimaryScreen.Bounds.Width;
            }
        }

        public static int DisplayHeight
        {
            get
            {
                return Screen.PrimaryScreen.Bounds.Height;
            }
        }
    }
}
