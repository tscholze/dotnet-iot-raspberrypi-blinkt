# .NET IoT Raspberry Pi Blinkt!

A C# .NET implementation for controlling the [Pimoroni Blinkt!](https://shop.pimoroni.com/products/blinkt) LED board on a Raspberry Pi.

## What does it?
The project demonstrates controlling the Blinkt! LED board using C# and .NET IoT libraries. Examples include:
- Setting individual LED colors
- Creating rainbow effects
- Controlling LED brightness

## Hardware Requirements

- [Raspberry Pi](https://www.raspberrypi.org/) (any model but not a Zero)
- [Pimoroni Blinkt!](https://shop.pimoroni.com/products/blinkt) RGB LED board

## Software Requirements

- [.NET SDK](https://learn.microsoft.com/en-us/dotnet/iot/deployment) installed on Raspberry Pi
- SPI interface enabled (`sudo raspi-config`)

## How to run

1. Clone this repository:
```bash
git clone https://github.com/tscholze/dotnet-iot-raspberrypi-blinkt.git
cd dotnet-iot-raspberrypi-blinkt/Blinky
```

2. Build and run
```bash
dotnet run
```

## Contributing

Feel free to improve the quality of the code. It would be great to learn more from experienced C# and IoT developers.

## Authors

Just me, [Tobi]([https://tscholze.github.io).

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE.md) file for details.
Dependencies or assets maybe licensed differently.