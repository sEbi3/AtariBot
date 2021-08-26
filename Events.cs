using System.Threading.Tasks;
using DSharpPlus.EventArgs;
using DSharpPlus.Entities;
using System.Threading;
using System;
using DSharpPlus;

namespace AI_Bot
{
    public class Events
    {
        public static async Task Messagecreated(MessageCreateEventArgs e)
        {  
        }

        public static async Task TypingStarted(TypingStartEventArgs e)
        {
        }

        public static async Task UpdateRichPrsence()
        {
            while (true)
            {
                var guilds = Program.discord.Guilds;
                await Program.discord.UpdateStatusAsync(new DiscordActivity("??help", ActivityType.Watching), UserStatus.Online, DateTimeOffset.Now);
                Thread.Sleep(30 * 1000);
            }
        }

        public static async Task ThreadWorker(ReadyEventArgs e)
        {
           await Task.Run(UpdateRichPrsence().Start);
        }
    }
}