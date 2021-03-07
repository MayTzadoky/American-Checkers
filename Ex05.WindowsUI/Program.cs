using System.Windows.Forms;


// $G$ SFN-008 (+11) Bonus: Events in the Logic layer are handled by the UI.

namespace Ex05.WindowsUI
{
    public class Program
    {
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new DamkaBoard());
        }
    }
}
