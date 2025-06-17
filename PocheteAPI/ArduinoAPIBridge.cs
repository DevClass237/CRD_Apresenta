using System.IO.Ports;
using System.Text.Json;
using System.Text;
using PocheteAPI.DTO;
using PocheteAPI.Services;

namespace ArduinoAPIBridge
{
    public class BridgeArduino
    {
        private static SerialPort serialPort = new SerialPort("/dev/ttyACM0", 9600);

        public static async Task Main()
        {
            serialPort.Open();
            Console.WriteLine("Serial port opened.");

            using var httpClient = new HttpClient { BaseAddress = new Uri("http://localhost:5288") };
            var service = new MovimentacaoService(httpClient);

            while (true)
            {
                try
                {
                    string line = serialPort.ReadLine()?.Trim();
                    Console.WriteLine("Received: " + line);

                    if (string.IsNullOrWhiteSpace(line)) continue;

                    // Deserialize JSON
                    var dto = JsonSerializer.Deserialize<MovimentacaoDTO>(line, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    if (dto is not null)
                    {
                        var (success, message) = await service.RegistrarRetiradaAsync(dto);
                        Console.WriteLine(message);

                        // Optionally confirm back to Arduino
                        serialPort.WriteLine(success ? "OK" : "FAIL");
                    }
                    else
                    {
                        Console.WriteLine("❌ Failed to parse JSON.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("❌ Error: " + ex.Message);
                    serialPort.WriteLine("FAIL");
                }
            }
        }
    }
}
