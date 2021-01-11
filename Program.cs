using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using System;
using System.Collections.Generic;

namespace ExampleBot
{
    public static class Program
    {
        public static DiscordClient discord;
        public static CommandContext ctx;
        public static CommandsNextExtension commands;
        public static DateTime startupTime = DateTime.Now;
        public static Dictionary<ulong, string> guildPrefixes = new Dictionary<ulong, string>();

        public static string InternalPrefix = "PREFIX (??)";
        public static string BotVersion = "BOT VERSION";
        public static string MysqlConnectionRootString = "CONNECTION-STRING"; //There is currently no MySQL stuff in this open source project.
        public static string ShowNotification = 
            "This command has been set to private or was disabled by the developer. :warning:" + 
            "\nTry to use the command later again.\n\nThe reason for this could due to an important issue that we we need to fix,\n" +
            "or we are investigate and work on this command.\n\n";
        public static bool IsPrivate = true;

        static void Main(string[] args)
        {
            MainAsync(args).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        static async Task MainAsync(string[] args)
        {
            discord = new DiscordClient(new DiscordConfiguration
            {
                Token = "YOUR BOT TOKEN",
                TokenType = TokenType.Bot, LogLevel = LogLevel.Debug
            });
            commands = discord.UseCommandsNext(new CommandsNextConfiguration
            {
                //You can edit the prefix for your liking.
                StringPrefixes = new string[] { "??" },
                CaseSensitive = false,
                EnableDms = false,
            });

            //Register all your command classes here.
            commands.RegisterCommands<ExampleBot.BotModule.AdminModule>();
            commands.RegisterCommands<ExampleBot.BotModule.FunModule>();
            commands.RegisterCommands<ExampleBot.BotModule.MiscModule>();
            commands.RegisterCommands<ExampleBot.BotModule.InforCommands>();

            discord.MessageCreated += Events.Messagecreated;
            discord.TypingStarted += Events.TypingStarted;

            await discord.ConnectAsync();
            await Task.Delay(-1);
        }
    }
}