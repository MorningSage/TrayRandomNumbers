# TrayRandomNumbers
The general functionality of this project is simple: to generate random numbers between 1 and N.  It resides in the system tray as an icon the user can interact with and prompts them for N.  It was written to serve a purpose at my workplace.

Sure, there are plenty of alternatives as Random Numbers are not revolutionary.  However, this project exists to demonstrate that you don't need to use an invisible form or a `while(true)` loop with `DoEvents`.  These have the potential to introduce problems like a flickering form at startup or worse, the deathtrap of using `DoEvents`.

Instead, you can inherit `ApplicationContext` in a class and pass a new instance of that to `Application.Run()` just like you would a Form.  This avoids having more running than you need to and you avoid the need to write extra loops.  Check it out in the code.
