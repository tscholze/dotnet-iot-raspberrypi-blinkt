using System;

namespace Blinky;

/// <summary>
/// This class contains methods to demonstrate various LED effects
/// such as a moving rainbow and simple RGB color changes.
/// It requires an instance of the Blinkt class to control the LEDs.
/// </summary>
/// <param name="blinkt">An instance of the Blinkt class.</param>
public class Samples(Blinkt blinkt)
{
    /// <summary>
    /// Demonstrates a moving rainbow effect on the Blinkt! LEDs.
    /// Each LED will cycle through colors based on its position.
    /// </summary>
    public void MovingRainbowExample()
    {
        Console.WriteLine("Moving Rainbow Example");

        // Create a moving rainbow effect
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                // Calculate the color based on the position
                var r = (byte)((Math.Sin((i + j) * 0.5) + 1) * 127.5);
                var g = (byte)((Math.Sin((i + j) * 0.5 + Math.PI / 3) + 1) * 127.5);
                var b = (byte)((Math.Sin((i + j) * 0.5 + Math.PI * 2 / 3) + 1) * 127.5);

                blinkt.SetPixel(j, r, g, b, 0.2f);
            }

            blinkt.Show();
            Thread.Sleep(500);
        }

        // Clear the LEDs after the effect
        Console.WriteLine("Turn LEDs off");
        blinkt.Clear();
        blinkt.Show();
    }

    /// <summary>
    /// Demonstrates a simple RGB color change on all Blinkt! LEDs.
    /// Each LED will be set to red, green, and blue in sequence.
    /// </summary>
    public void SimpleRgbExample()
    {
        Console.WriteLine("Simple RGB Example");

        // Set all LEDs to red
        Console.WriteLine("Set to red");
        SetColor(255, 0, 0);
        Thread.Sleep(1000);

        // Set all LEDs to green
        Console.WriteLine("Set to green");
        SetColor(0, 255, 0);
        Thread.Sleep(1000);

        // Set all LEDs to blue
        Console.WriteLine("Set to blue");
        SetColor(0, 0, 255);
        Thread.Sleep(1000);

        // Clear the LEDs again
        Console.WriteLine("Turn LEDs off");
        blinkt.Clear();
        blinkt.Show();
    }

    #region Blinkt! Helper Methods

    /// <summary>
    /// Sets the color of all Blinkt! LEDs.
    /// </summary>
    /// <param name="r">Red component (0-255)</param>
    /// <param name="g">Green component (0-255)</param>
    /// <param name="b">Blue component (0-255)</param>
    /// <param name="brightness">Brightness (0.0-1.0, default is 0.2)</param>
    void SetColor(byte r, byte g, byte b, float brightness = 0.2f)
    {
        for (int i = 0; i < 8; i++)
        {
            blinkt.SetPixel(i, r, g, b, brightness);
        }

        blinkt.Show();
    }

    #endregion
}