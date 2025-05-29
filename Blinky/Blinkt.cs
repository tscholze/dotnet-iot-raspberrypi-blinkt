using System.Device.Gpio;

namespace Blinky;

/// <summary>
/// Represents a controller for the Pimoroni Blinkt! LED board,
/// which contains 8 RGB LEDs driven by the APA102 protocol.
/// </summary>
public class Blinkt : IDisposable
{
    /// <summary>
    /// Hardware configuration constants.
    /// </summary>
    private const int NUM_LEDS = 8;

    /// <summary>
    /// DATA_PIN is used for sending pixel data,
    /// </summary>
    private const int DATA_PIN = 23;

    /// <summary>
    /// CLOCK_PIN is used for clock signal to synchronize data transmission.
    /// </summary>
    private const int CLOCK_PIN = 24;

    /// <summary>
    /// Represents a controller for the Pimoroni Blinkt! LED board.
    /// This class provides methods to control the 8 RGB LEDs using the APA102 protocol.
    /// It allows setting individual pixel colors, brightness, and updating the display.
    /// </summary>
    private readonly GpioController _controller;

    /// <summary>
    /// Buffer to hold pixel data for the LEDs.
    /// Each LED is represented by 4 bytes:
    ///  - 1 byte for brightness (5 bits used, 3 bits unused)
    ///  - 1 byte for blue component
    ///  - 1 byte for green component
    ///  - 1 byte for red component
    /// </summary>
    private readonly byte[] _pixels;

    /// <summary>
    /// Initializes a new instance of the Blinkt controller.
    /// Sets up GPIO pins and initializes the LED buffer.
    /// </summary>
    public Blinkt()
    {
        _controller = new GpioController();
        _controller.OpenPin(DATA_PIN, PinMode.Output);
        _controller.OpenPin(CLOCK_PIN, PinMode.Output);
        
        _pixels = new byte[NUM_LEDS * 4];  // 4 bytes per LED (brightness, blue, green, red)

        Clear();
    }

    /// <summary>
    /// Sets the color and brightness of an individual LED.
    /// </summary>
    /// <param name="pixel">The LED index (0-7)</param>
    /// <param name="r">Red component (0-255)</param>
    /// <param name="g">Green component (0-255)</param>
    /// <param name="b">Blue component (0-255)</param>
    /// <param name="brightness">Brightness level (0.0-1.0)</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when pixel index is invalid</exception>
    public void SetPixel(int pixel, byte r, byte g, byte b, float brightness = 0.2f)
    {
        if (pixel < 0 || pixel >= NUM_LEDS)
            throw new ArgumentOutOfRangeException(nameof(pixel));

        // Clamp brightness between 0 and 1, then scale to 5-bit value (0-31)
        brightness = Math.Max(0, Math.Min(1.0f, brightness));
        byte bright = (byte)(brightness * 31);

        // Store pixel data in buffer (APA102 format)
        int index = pixel * 4;
        _pixels[index] = bright;          // 5-bit brightness value
        _pixels[index + 1] = b;           // Blue
        _pixels[index + 2] = g;           // Green
        _pixels[index + 3] = r;           // Red
    }

    /// <summary>
    /// Clears all LEDs by setting them to black (off).
    /// </summary>
    public void Clear()
    {
        Array.Clear(_pixels, 0, _pixels.Length);
    }

    /// <summary>
    /// Updates the LED display with the current pixel buffer contents.
    /// Follows the APA102 protocol: start frame, LED data, end frame.
    /// </summary>
    public void Show()
    {
        // Start frame: 32 zero bits
        WriteByte(0x00);
        WriteByte(0x00);
        WriteByte(0x00);
        WriteByte(0x00);

        // LED frames: [111XXXXX, Blue, Green, Red] for each LED
        // Where XXXXX is 5-bit brightness value
        for (int i = 0; i < NUM_LEDS * 4; i += 4)
        {
            WriteByte((byte)(0xE0 | _pixels[i]));     // 111XXXXX brightness value
            WriteByte(_pixels[i + 1]);                // Blue
            WriteByte(_pixels[i + 2]);                // Green
            WriteByte(_pixels[i + 3]);                // Red
        }

        // End frame: 32 one bits
        WriteByte(0xFF);
        WriteByte(0xFF);
        WriteByte(0xFF);
        WriteByte(0xFF);
    }

    /// <summary>
    /// Writes a single byte to the LED strip following the APA102 protocol.
    /// </summary>
    /// <param name="value">The byte to write</param>
    private void WriteByte(byte value)
    {
        // Send each bit MSB first
        for (int i = 0; i < 8; i++)
        {
            _controller.Write(DATA_PIN, (value & (1 << (7 - i))) > 0);
            _controller.Write(CLOCK_PIN, true); 
            _controller.Write(CLOCK_PIN, false); 
        }
    }

    /// <summary>
    /// Cleans up resources and turns off all LEDs.
    /// </summary>
    public void Dispose()
    {
        Clear();
        Show();
        _controller?.Dispose();
    }
}