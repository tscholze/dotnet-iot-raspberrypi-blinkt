namespace Blinky.Web;

public class BlinktService
{
    private readonly Blinkt _blinkt;
    private readonly Samples _samples;

    public BlinktService()
    {
        _blinkt = new Blinkt();
        _samples = new Samples(_blinkt);
    }

    public void SetColor(int pixel, byte r, byte g, byte b, float brightness = 0.2f)
    {
        Console.WriteLine($"Setting pixel {pixel} to color R:{r}, G:{g}, B:{b} with brightness {brightness}");
        _blinkt.SetPixel(pixel, r, g, b, brightness);
        _blinkt.Show();
    }

    public void RunSimpleRgbExample()
    {
        Console.WriteLine("Running Simple RGB Example");
        _samples.SimpleRgbExample();
    }
    public void RunMovingRainbowExample()
    {
        Console.WriteLine("Running Moving Rainbow Example");
        _samples.MovingRainbowExample();
    }

    public void Clear()
    {
        _blinkt.Clear();
        _blinkt.Show();
    }
}