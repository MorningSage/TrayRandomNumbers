using System;
using System.Windows.Forms;

/// <summary>
/// Contains the logic for the message loop
/// </summary>
class Context : ApplicationContext
{
    
    /// <summary>
    /// Gets an object for generating random numbers
    /// </summary>
    Random rnd = new Random();

    /// <summary>
    /// Prompts the user for the upper bounds of their number
    /// range -- generating and displaying the number 
    /// </summary>
    private void Generate()
    {
        // Using statement (new to C# 8.0) containing the winform for prompting the user
        using var frm = new Helpers.PromptForm();
    
        // Prompt the user and stop running if applicable
        if (frm.ShowDialog() != DialogResult.OK) return;

        try
        {
            // Attempt to parse the user's input as an int
            var maxNumber = frm.GetFinalNumber();

            /***
            * Note that in the following, we are adding one
            * because this number is considered exclusive
            * upper bound.  Meaning that it's generated up
            * to but not including this number.  In this
            * example, we want to include it
            ***/

            // Generate and display the random number to the user
            MessageBox.Show($"Random Number between 1-{maxNumber}:\n\n{rnd.Next(1, maxNumber + 1)}", "Random Number Generator", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            /***
            * Catch the general exception.  Ideally, we'd
            * have specific Exceptions in case we want to
            * react differently in certain circumstances.
            * For instance, we could loop until the user
            * enters a valid number or flat out cancels
            * the operation
            ***/

            // Let the user know that something went awry
            MessageBox.Show($"Unable to generate number.\n\nNumber Input: \"{frm.GetFinalNumberString()}\"\nError: {ex.Message}", "Random Number Generator", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }


    /// <summary>
    /// Called when creating a new instance of this class
    /// </summary>
    public Context()
    {
        /***
        * Since this program does not operate in a visual window,
        * we can't rely on that for our message loop.
        * The alternative is to use an ApplicationContext class
        * to redefine the message loop.  You'll see how it works
        * below.  If this code were to exist in the entry point,
        * execution would end immediately since there's nothing
        * handling the message loop.  Now, we don't need to use
        * a while loop to continue execution.
        ***/

        // Create the icon to display in the system tray
        var notify = new NotifyIcon()
        {
            Text = "Generate a random number",
            ContextMenu = new ContextMenu(new MenuItem[]
            {
                /***
                * Create three menu items to be displayed
                * when RightClicked by the user.  We are
                * also defining an action to perform when
                * each MenuItem is clicked.
                *
                * Note that to stop execution, there's no
                * Form to close, etc.  In this case we call
                * Application.Exit().  Specific actions to
                * wrap up execution will be handled below.
                ***/
                new MenuItem("Generate", (sender, e) => Generate()),
                new MenuItem("-"),
                new MenuItem("Exit", (sender, e) => Application.Exit()),
            }),
            Visible = true,
            Icon = Helpers.GetIco()
        };

        /***
        * There's a Click event and a MouseClick event. The
        * difference being that the Click event is more
        * general and might be fired when a user hits
        * Enter, etc.  MouseClick contains more information
        * that we could use if we needed to like which
        * button, how many clicks, XY location, and the like
        ***/

        // Fired when the user actually clicks on the icon
        notify.MouseClick += (sender, e) =>
        {
            // Generate only when Left-Clicked
            if (e.Button == MouseButtons.Left) Generate();

            /***
            * Note that Right-Clicks are still handled
            * by the system and the ContextMenu is still
            * displayed
            ***/
        };

        // Fired when Application.Exit() is called
        Application.ApplicationExit += (sender, e) =>
        {
            // The only thing we need to do is hide the icon
            notify.Visible = false;
        };
    }
}

