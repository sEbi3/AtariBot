using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using System;
using System.Collections.Generic;
using AI_Bot.HelpModule;
using DSharpPlus.EventArgs;

namespace AI_Bot
{
    public static class Program
    {
        public static DiscordClient discord;
        public static CommandContext ctx;
        public static CommandsNextExtension commands;
        public static DateTime startupTime = DateTime.Now;
        public static Dictionary<ulong, string> guildPrefixes = new Dictionary<ulong, string>();

        public static string InternalPrefix = "??";
        public static string InternalHelpPrefix = "";
        public static string BotVersion = "v.1.0.44";
        public static string BotName = "AtariBot";
        public static string ShowNotification = 
            "This command has been set to private or was disabled by the developer." + 
            "Try to use the command later again.\n\nThe reason for this could due to an important issue that we we need to fix," +
            "or we are investigate and work on this command.\n\n" +
            "[[More information can be found in our FAQ]](https://sebi3.gitbook.io/ai-bot/wiki/frequently-asked-questions)";
        public static bool IsPrivate = true;

        static void Main(string[] args)
        {
            MainAsync(args).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        static async Task MainAsync(string[] args)
        {
            discord = new DiscordClient(new DiscordConfiguration
            {
                Token = "Your Token",
                TokenType = TokenType.Bot, MinimumLogLevel = Microsoft.Extensions.Logging.LogLevel.Debug,
            });
            commands = discord.UseCommandsNext(new CommandsNextConfiguration
            {
                StringPrefixes = new string[] { "??" },
                CaseSensitive = false,
                EnableDms = false,
            });

            commands.RegisterCommands<AI_Bot.BotModule.AdminModule>();
            commands.RegisterCommands<AI_Bot.BotModule.FunModule>();
            commands.RegisterCommands<AI_Bot.BotModule.MiscModule>();
            commands.RegisterCommands<AI_Bot.BotModule.InforCommands>();

            commands.SetHelpFormatter<Help>();

            await discord.ConnectAsync();
            await Task.Delay(-1);
            discord.Ready += Discord_Ready;
        }

        public static Task Discord_Ready(DiscordClient client, ReadyEventArgs e)
        {
            Task.Run(Events.UpdateRichPrsence().Start);
            Task.Run(Events.ThreadWorker(e).Start);
            return Task.CompletedTask;
        }
    }
}