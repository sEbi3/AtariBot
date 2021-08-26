using System;
using DSharpPlus;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using Test_Bot;

namespace AI_Bot.BotModule
{
    public class InforCommands : BaseCommandModule
    {
        [Command("uptime"), Description("Use this command to see information of the AI Bot.")]
        public async Task Uptime(CommandContext ctx)
        {
            TimeSpan diff = DateTime.Now - Program.startupTime;
            await ctx.RespondAsync(embed: new DiscordEmbedBuilder
            {
                Author = new DiscordEmbedBuilder.EmbedAuthor
                {
                    IconUrl = Program.discord.CurrentUser.AvatarUrl,
                    Name = Program.discord.CurrentUser.Username
                },
                Title = "Uptime",
                Description = $"\nThe **{Program.BotName}** is running since** " + $"{diff.Hours} hours {diff.Minutes} minutes {diff.Seconds} seconds.**\n\n**__Quick Links__**\n[Wiki](https://sebi3.gitbook.io/ataribot/) | [Setup](https://app.gitbook.com/@sebi3/s/ai-bot/wiki/getting-started) | [FAQ](https://app.gitbook.com/@sebi3/s/ai-bot/wiki/frequently-asked-questions)",
                Timestamp = DateTime.Now,
                Color = DiscordColor.Blurple,
                Footer = new DiscordEmbedBuilder.EmbedFooter
                {
                    Text = $"{Program.BotName} by sEbi3",
                }
            });
        }

        [Command("Invite"), Description("Use this command to invite the bot on your server."), Cooldown(1, 10, CooldownBucketType.User)]
        public async Task Invite(CommandContext ctx)
        {
            if (!ctx.Member.PermissionsIn(ctx.Channel).HasPermission(Permissions.Administrator))
            {
                await ctx.RespondAsync(embed: new DiscordEmbedBuilder
                {
                    Author = new DiscordEmbedBuilder.EmbedAuthor
                    {
                        IconUrl = Program.discord.CurrentUser.AvatarUrl,
                        Name = Program.discord.CurrentUser.Username
                    },
                    Title = "Warning",
                    Thumbnail = new DiscordEmbedBuilder.EmbedThumbnail { Url = Program.discord.CurrentUser.AvatarUrl },
                    Description = $"You don't have enough permissions to use this command.\nMake sure that you have enough permissions to use this command.",
                    Timestamp = DateTime.Now,
                    Color = DiscordColor.IndianRed,
                    Footer = new DiscordEmbedBuilder.EmbedFooter
                    {
                        Text = $"{Program.BotName} by sEbi3",
                    }
                });
                return;
            }
            else if (Program.IsPrivate == true)
            {
                await ctx.RespondAsync(embed: new DiscordEmbedBuilder
                {
                    Author = new DiscordEmbedBuilder.EmbedAuthor
                    {
                        IconUrl = Program.discord.CurrentUser.AvatarUrl,
                        Name = Program.discord.CurrentUser.Username
                    },
                    Title = "Warning",
                    Thumbnail = new DiscordEmbedBuilder.EmbedThumbnail { Url = Program.discord.CurrentUser.AvatarUrl },
                    Description = $"{Program.ShowNotification}",
                    Timestamp = DateTime.Now,
                    Color = DiscordColor.IndianRed,
                    Footer = new DiscordEmbedBuilder.EmbedFooter
                    {
                        Text = $"{Program.BotName} by sEbi3",
                    }
                });
                return;
            }
            else
            {
                await ctx.RespondAsync(embed: new DiscordEmbedBuilder
                {
                    Author = new DiscordEmbedBuilder.EmbedAuthor
                    {
                        IconUrl = Program.discord.CurrentUser.AvatarUrl,
                        Name = Program.discord.CurrentUser.Username
                    },
                    Title = "Add me to your server!",
                    Thumbnail = new DiscordEmbedBuilder.EmbedThumbnail { Url = Program.discord.CurrentUser.AvatarUrl },
                    Description = 
                    $"Hello, you can use this link to invite the bot on your server.\n\n" +
                    "[[Invite the bot to your server]](https://discord.com/oauth2/authorize?client_id=711550600286044201&scope=bot&permissions=8)\n" +
                    $"**Note: This bot is still in an early alpha build and development.\nYou may wait a bit for the first public release.\n\n",
                    Timestamp = DateTime.Now,
                    Color = DiscordColor.Blurple,
                    Url = "https://sites.google.com/view/ai-bot-discord/home",
                    Footer = new DiscordEmbedBuilder.EmbedFooter
                    {
                        Text = $"{Program.BotName} by sEbi3",
                    }
                });
            }
        }

        [Command("Info"), Aliases("ServerInfo", "ServerStats"), Description("Use this command to see the information of the current server.")]
        public async Task ServerStats(CommandContext ctx)
        {
            var info = new DiscordEmbedBuilder
            {
                Author = new DiscordEmbedBuilder.EmbedAuthor
                {
                    IconUrl = Program.discord.CurrentUser.AvatarUrl,
                    Name = Program.discord.CurrentUser.Username
                },
                Title = "Server Information",
                Thumbnail = new DiscordEmbedBuilder.EmbedThumbnail { Url = ctx.Guild.IconUrl },
                Description = "Listening server information about **" + ctx.Guild.Name.ToString() + "**.",
                Timestamp = DateTime.Now,
                Color = DiscordColor.Blurple,
                Footer = new DiscordEmbedBuilder.EmbedFooter
                {
                    Text = $"{Program.BotName} by sEbi3",
                }
            };
            info.AddField(
                "**__Server Information__**",
                "Owner: " + ctx.Guild.Owner.DisplayName +
                "\nRegion: " + ctx.Guild.VoiceRegion.Name + "", true);
            info.AddField("**__General Information__**",
                "\nTotal members: " + ctx.Guild.MemberCount +
                "\nTotal channels: " + ctx.Guild.Channels.Count + "", true);
            info.AddField("**__Other Information__**",
                "Roles: " + ctx.Guild.Roles.Count +
                "\nEmojis: " + ctx.Guild.Emojis.Count + "", true);
            info.AddField("**__The server was created on__**",
               "```" + ctx.Guild.CreationTimestamp.DateTime + "```", true);
            await ctx.RespondAsync(embed: info);
        }

        [Command("bot"), Aliases("botstats"), Description("Use this command to see the information about the bot.")]
        public async Task BotStats(CommandContext ctx)
        {
            var guilds = Program.discord.Guilds;
            var info = new DiscordEmbedBuilder
            {
                Author = new DiscordEmbedBuilder.EmbedAuthor
                {
                    IconUrl = Program.discord.CurrentUser.AvatarUrl,
                    Name = Program.discord.CurrentUser.Username
                },
                Title = $"{Program.BotName} Information",
                Thumbnail = new DiscordEmbedBuilder.EmbedThumbnail { Url = Program.discord.CurrentUser.AvatarUrl },
                Description = $"Listening information about the **{Program.BotName}**.",
                Timestamp = DateTime.Now,
                Color = DiscordColor.Blurple,
                Footer = new DiscordEmbedBuilder.EmbedFooter
                {
                    Text = $"{Program.BotName} by sEbi3",
                }
            };
            info.AddField(
                "**__General Information__**",
                "Guilds: " + guilds.Count +
                "\nServer Region: Europe\n" + "", true);
            info.AddField("**__Bot Details__**",
                "\nVersion: " + Program.BotVersion +
                "\nDeveloper: sEbi3#0001\n" + "", true);
            info.AddField("**__The bot was created on__**",
               "```01/04/2021 2:11:34 PM" + "```", false);
            await ctx.RespondAsync(embed: info);
        }
    }
}
