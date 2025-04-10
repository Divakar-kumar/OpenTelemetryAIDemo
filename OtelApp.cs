using Microsoft.Extensions.Logging;
using OpenTelemetryAIDemo.Modules;
using OpenTelemetryAIDemo.Services;

namespace OpenTelemetryAIDemo
{
   public class OtelApp
   {
      private readonly ILogger<OtelApp> _logger;
      private readonly ModuleFactory _moduleFactory;
      private readonly ITelemetryToggleService _telemetryToggleService;

      public OtelApp(ILogger<OtelApp> logger, ModuleFactory moduleFactory, ITelemetryToggleService telemetryToggleService)
      {
         _logger = logger;
         _moduleFactory = moduleFactory;
         _telemetryToggleService = telemetryToggleService;
      }

      public async Task Run()
      {
         string[] options = new[]
      {
            "💬 Chat Completion",
            "🛠️ Chat Completion with Function Calling",
            "🧠 Multi-Agent Interactions",
            "📡 Toggle Automatic Telemetry",
            "❌ Quit"
        };

         int selectedIndex = 0;

         Console.Clear();
         PrintHeader();

         ConsoleKey key;
         do
         {
            DisplayMenu(options, selectedIndex);

            key = Console.ReadKey(true).Key;

            switch (key)
            {
               case ConsoleKey.UpArrow:
                  do { selectedIndex = (selectedIndex - 1 + options.Length) % options.Length; }
                  while (string.IsNullOrWhiteSpace(options[selectedIndex]));
                  break;

               case ConsoleKey.DownArrow:
                  do { selectedIndex = (selectedIndex + 1) % options.Length; }
                  while (string.IsNullOrWhiteSpace(options[selectedIndex]));
                  break;

               case ConsoleKey.Enter:
                  Console.Clear();
                  PrintHeader();
                  Console.ForegroundColor = ConsoleColor.Green;
                  Console.WriteLine($"\n🎯 You selected: {options[selectedIndex]}");
                  Console.ResetColor();
                  if (options[selectedIndex].Contains("Toggle Automatic Telemetry"))
                  {
                     _telemetryToggleService.Toggle();
                     Console.Write("\n📡 Automatic Telemetry is now: ");
                     if (_telemetryToggleService.IsEnabled)
                     {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("[ENABLED]");
                     }
                     else
                     {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("[DISABLED]");
                     }
                     Console.ResetColor();
                  }
                  else
                  {
                     var module = _moduleFactory.GetModule(selectedIndex);
                     if (module != null)
                     {
                        await module.RunApp();
                     }
                     else
                     {
                        Console.WriteLine("\n⚠️ Module not implemented.");
                     }
                  }
                  Console.WriteLine("\nPress any key to return to the main menu...");
                  Console.ReadKey(true);
                  Console.Clear();
                  PrintHeader();
                  break;

               case ConsoleKey.Escape:
                  return;
            }

         } while (key != ConsoleKey.Escape);
      }

      static void DisplayMenu(string[] options, int selectedIndex)
      {
         Console.SetCursorPosition(0, 12);
         Console.ResetColor();

         for (int i = 0; i < options.Length; i++)
         {
            if (string.IsNullOrWhiteSpace(options[i]))
            {
               Console.WriteLine();
               continue;
            }

            if (i == selectedIndex)
            {
               Console.BackgroundColor = ConsoleColor.Yellow;
               Console.ForegroundColor = ConsoleColor.Black;
               Console.WriteLine($"👉 {options[i]}");
            }
            else
            {
               Console.ResetColor();
               Console.ForegroundColor = ConsoleColor.Gray;
               Console.WriteLine($"   {options[i]}");
            }
         }

         Console.ResetColor();
         Console.ForegroundColor = ConsoleColor.DarkGray;
         Console.WriteLine($"\n{new string('─', Console.WindowWidth - 1)}");
         Console.WriteLine($"🔼 Use arrow keys to navigate • ⏎ Enter to select • ⎋ Esc to quit");
         Console.ResetColor();
      }

      static void PrintHeader()
      {
         Console.ForegroundColor = ConsoleColor.Green;
         Console.WriteLine(@"   ____ _____ _____ _         ____  _____ __  __  ___  ");
         Console.WriteLine(@"  / __ \_   _| ____| |       |  _ \| ____|  \/  |/ _ \ ");
         Console.WriteLine(@" | |  | || | |  _| | |       | | | |  _| | |\/| | | | |");
         Console.WriteLine(@" | |__| || | | |___| |___    | |_| | |___| |  | | |_| |");
         Console.WriteLine(@"  \____/ |_| |_____|_____|   |____/|_____|_|  |_|\___/  ");

         Console.WriteLine();
         Console.ForegroundColor = ConsoleColor.Cyan;
         for (int i = 0; i < Console.WindowWidth - 1; i++)
         {
            Console.Write(i % 2 == 0 ? '•' : '─');
         }

         Console.WriteLine();
         Console.WriteLine();
         Console.ResetColor();
         Console.ForegroundColor = ConsoleColor.Magenta;
         Console.WriteLine(" 🌐 Welcome to the OpenTelemetry AI Console!");
         Console.ForegroundColor = ConsoleColor.Yellow;
         Console.WriteLine(" 🧩 Purpose : Demo OpenTelemetry capabilities with Multi-Agent Interactions");
         Console.WriteLine(" 🛠️ Version : 1.0.0     👨‍💻 Author: Divakar Kumar");

         Console.ForegroundColor = ConsoleColor.Cyan;
         for (int i = 0; i < Console.WindowWidth - 1; i++)
         {
            Console.Write(i % 2 == 0 ? '•' : '─');
         }
         Console.WriteLine();
         Console.WriteLine();
         Console.ResetColor();
      }
   }
}
