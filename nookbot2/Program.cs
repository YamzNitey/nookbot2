using System;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;

namespace nookbot2
{
    class Program
    {
        private DiscordSocketClient Client;
        private CommandHandler CmdHandler;

        static void Main()
            => new Program().StartAsync().GetAwaiter().GetResult();

        public async Task StartAsync()
        {
            Console.WriteLine("nookbot2 by Yamz9983");

            if(!Config.CreateConfig())
            {
                Console.WriteLine("Config file created. Edit the config JSON in config/config.json to include your token, then start this program again.");
                return;
            }

            if (string.IsNullOrWhiteSpace(Config.Bot.Token))
            {
                Console.WriteLine("The token is blank. Exiting...");
                return;
            }

            Client = new DiscordSocketClient(new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Verbose,
                MessageCacheSize = 10000,
            });

            Client.Log += Client_Log;

            await Client.LoginAsync(TokenType.Bot, Config.Bot.Token);
            await Client.StartAsync();
            CmdHandler = new CommandHandler();
            await CmdHandler.InitAsync(Client);
            await Task.Delay(-1);
        }

        private async Task Client_Log(LogMessage logMessage)
        {
            await Console.Out.WriteLineAsync(logMessage.ToString());
        }
    }
}
