using Blinky;

#region Main Program

Console.WriteLine("Blinkt! Example");
using var blinkt = new Blinkt();

try
{
    var samples = new Samples(blinkt);
    samples.SimpleRgbExample();
    Thread.Sleep(2000);
    samples.MovingRainbowExample();
}
finally
{
    // Ensure the LEDs are turned off when exiting
    blinkt.Clear();
    blinkt.Show();
}

#endregion