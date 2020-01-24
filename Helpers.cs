using System.Windows.Forms;
using System.Drawing;
using System.Reflection;
using System.Linq;
using System;


/// <summary>
/// A static class that houses things we will need during runtime
/// </summary>
public static class Helpers
{
    /// <summary>
    /// Gets the embedded ico resource
    /// </summary>
    public static Icon GetIco()
    {
        // Find Us
        var assembly = Assembly.GetExecutingAssembly();
        // Find the name of the ico we have as an embedded resource (see project file)
        var resourceName = assembly.GetManifestResourceNames().Single(x => x.EndsWith(".ico"));

        // Using statement (new to C# 8.0) containing the stream to the ico file
        using var stream = assembly.GetManifestResourceStream(resourceName);
        // Returns the new Icon object from the stream
        return new Icon(stream);
    }

    /// <summary>
    /// The winform used to prompt the user for the
    /// upper bounds of the random number
    /// </summary>
    public class PromptForm : Form
    {
        /// <summary>
        /// Gets the raw text from the user
        /// </summary>
        private string FinalNumber = string.Empty;

        /// <summary>
        /// Called when creating a new instance of this winform
        /// </summary>
        public PromptForm()
        {
            // Set the settings for the winform itself
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Size = new Size(500, 200);
            StartPosition = FormStartPosition.CenterScreen;
            MinimizeBox = false;
            MaximizeBox = false;
            Text = "How high shall we go?";
            Icon = GetIco();

            // Create the label containing the prompt
            var questionLabel = new Label()
            {
                AutoSize = false,
                Text = "What's the highest number that should be returned?",
                Dock = DockStyle.Top,
                Height = 50,
                Padding = new Padding(10, 10, 0, 0)
            };
            // Create a box for the user to type in
            var MaxNumberTextBox = new TextBox()
            {
                Location = new Point(10, 50 + 10),
                Width = ClientSize.Width - 20 // 20 because of 10px on each side (see 10 above)
            };
            // Create a "submit" button
            var OKButton = new Button()
            {
                Text = "Generate",
                Location = new Point(
                    (ClientSize.Width - 90) / 2, // Centered
                    50 + 10 + 45 // 45px below the textbox (see above)
                ),
                Size = new Size(90, 30)
            };

            // Fires when the user presses something on their keyboard
            MaxNumberTextBox.KeyDown += (sender, e) => 
            {
                /***
                * Note that we could also filter out non-numeric keypresses
                * but that is more than we care to do in this example
                ***/
                switch (e.KeyData)
                {
                    case Keys.Escape:
                        // Also closes the winform
                        DialogResult = DialogResult.Cancel;
                        break;
                    case Keys.Enter:
                        FinalNumber = MaxNumberTextBox.Text;
                        // Also closes the winform
                        DialogResult = DialogResult.OK;
                        break;
                    default:
                        break; 
                }
            };

            // Fired when the user clicks the "submit" button
            OKButton.Click += (sender, e) =>
            {
                FinalNumber = MaxNumberTextBox.Text;
                // Also closes the winform
                DialogResult = DialogResult.OK;
            };

            // Add the controls to the form
            // TabIndex should follow this order by default
            Controls.Add(questionLabel);
            Controls.Add(MaxNumberTextBox);
            Controls.Add(OKButton);
        }

        /// <summary>
        /// Gets the user's response as an <see cref="int"/> type.
        /// </summary>
        /// <exception cref="System.Exception">Thrown when the user entered a value that is not a number</exception>
        /// <exception cref="System.Exception">Thrown when the user entered a value that is less than one</exception>
        public int GetFinalNumber()
        {
            /***
            * Ideally, you would throw custom exceptions that could be caught individually
            ***/

            // Attempt to parse as an int or fail
            if (!int.TryParse(FinalNumber, out int parsed)) throw new Exception("Value cannot be converted to a valid number");
            // Ensure that the number is at least 1
            if (parsed < 1) throw new Exception("Numbers must be greater than zero");

            // Houston, we don't have any problems
            return parsed;
        }

        /// <summary>
        /// Gets the raw input from the user as a <see cref="string"/>
        /// </summary>
        public string GetFinalNumberString() => FinalNumber;
    }
}
