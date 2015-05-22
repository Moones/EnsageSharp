#region 

using System.Windows.Forms;

#endregion

namespace MoonesComboScript
{
    public static class Extensions
    {
        public static void Start(this Timer timer, double time)
        {
            timer.Interval += (int)time;
            timer.Start();
        }
    }
}