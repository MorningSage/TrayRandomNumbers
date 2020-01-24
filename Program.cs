using System;
using System.Windows.Forms;

namespace Random_Number_Tray
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            /***
            * While at work, I realized I needed a random number
            * generator quite frequently.  There are options
            * online for this, but I figured it would be a good
            * exercise to demonstrate how to properly handle a
            * windowless application's runtime loop without
            * manually looping yourself.
            *
            * By default, the ApplicationContext subscribes to
            * the main form's Closed event at which time it
            * exits the message loop.  In this example, that
            * behavior is redefined and we manually exit the
            * message loop when the Close option is selected
            * by the user.
            ***/

            // Remember Pre-Windows XP controls?
            // Yeah, me too...
            Application.EnableVisualStyles();

            // True to use Graphics class; false to use GDI TextRenderer
            Application.SetCompatibleTextRenderingDefault(false);

            // Running a class inheriting ApplicationContext, not a winform
            Application.Run(new Context());
        }
    }
}
