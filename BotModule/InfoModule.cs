using System;
using DSharpPlus;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using ExampleBot;

namespace ExampleBot.BotModule
{
    public class InforCommands : BaseCommandModule
    {
        [Command("ExampleBot"), Description("Use this command to see information of the ExampleBot."), Cooldown(1, 10, CooldownBucketType.User)]
        public async Task Test(CommandContext ctx)
        {
            var guilds = Program.discord.Guilds;
            await ctx.RespondAsync(embed: new DiscordEmbedBuilder
            {
                Title = "ExampleBot",
                Thumbnail = new DiscordEmbedBuilder.EmbedThumbnail { Url = Program.discord.CurrentUser.AvatarUrl },
                Description = 
                $"Hello, I am the **ExampleBot**.\n" +
                $"If you need help with finding my commands,\nuse `{Program.InternalPrefix}commands` for the universal commands list.\n\n" +
                $"If you want me to join on your server as well,\nuse the `{Program.InternalPrefix}invite` command and click on the url.\n\n" +
                $"If you don't know how to start with using my commands,\nuse the `{Program.InternalPrefix}setup` command.\n\n" +
                $"**Bot version**: {Program.BotVersion}\n**Guilds**: {guilds.Count.ToString()}\n\n" +
                $"**:warning: This bot is still in an alpha build and development.\nYou may wait a bit for the first public release.**",
                Timestamp = DateTime.Now,
                Color = DiscordColor.Blurple,
                Footer = new DiscordEmbedBuilder.EmbedFooter
                {
                    Text = "ExampleBot by sEbi3",
                    IconUrl = Program.discord.CurrentUser.AvatarUrl
                }
            });
        }

        [Command("uptime"), Description("Use this command to see information of the ExampleBot.")]
        public async Task Uptime(CommandContext ctx)
        {
            TimeSpan diff = DateTime.Now - Program.startupTime;
            await ctx.RespondAsync(embed: new DiscordEmbedBuilder
            {
                Title = "ExampleBot",
                Description =
                "\nThe **ExampleBot** is running since **" + $"{diff.Hours} hours {diff.Minutes} minutes {diff.Seconds} seconds.**",
                Timestamp = DateTime.Now,
                Color = DiscordColor.Blurple,
                Footer = new DiscordEmbedBuilder.EmbedFooter
                {
                    Text = "ExampleBot by sEbi3",
                    IconUrl = Program.discord.CurrentUser.AvatarUrl
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
                    Title = "ExampleBot",
                    Description = $"You don't have enough permissions to use this command. :x:",
                    Timestamp = DateTime.Now,
                    Color = DiscordColor.Red,
                    Footer = new DiscordEmbedBuilder.EmbedFooter
                    {
                        Text = "ExampleBot by sEbi3",
                        IconUrl = Program.discord.CurrentUser.AvatarUrl
                    }
                });
                return;
            }
            else if (Program.IsPrivate == true)
            {
                await ctx.RespondAsync(embed: new DiscordEmbedBuilder
                {
                    Title = "ExampleBot",
                    Description = $"{Program.ShowNotification}",
                    Timestamp = DateTime.Now,
                    Color = DiscordColor.Orange,
                    Footer = new DiscordEmbedBuilder.EmbedFooter
                    {
                        Text = "ExampleBot by sEbi3",
                        IconUrl = Program.discord.CurrentUser.AvatarUrl
                    }
                });
                return;
            }
            else
            {
                await ctx.RespondAsync(embed: new DiscordEmbedBuilder
                {
                    Title = "ExampleBot",
                    Thumbnail = new DiscordEmbedBuilder.EmbedThumbnail { Url = Program.discord.CurrentUser.AvatarUrl },
                    Description = 
                    $"Hello, you can use this link to invite the bot on your server.\n\n" +
                    "[[Invite the ExampleBot to your server]](YOUR INVITE LINK)\n",
                    Timestamp = DateTime.Now,
                    Color = DiscordColor.Blurple,
                    Footer = new DiscordEmbedBuilder.EmbedFooter
                    {
                        Text = "ExampleBot by sEbi3",
                        IconUrl = Program.discord.CurrentUser.AvatarUrl
                    }
                });
            }
        }

        [Command("Info"), Aliases("ServerInfo", "ServerStats"), Description("Use this command to see the information of the current server.")]
        public async Task ServerStats(CommandContext ctx)
        {
            var info = new DiscordEmbedBuilder
            {
                Title = "ExampleBot",
                Thumbnail = new DiscordEmbedBuilder.EmbedThumbnail { Url = ctx.Guild.IconUrl },
                Description = "Listening server information about **" + ctx.Guild.Name.ToString() + "**.",
                Timestamp = DateTime.Now,
                Color = DiscordColor.Blurple,
                Footer = new DiscordEmbedBuilder.EmbedFooter
                {
                    Text = "ExampleBot by sEbi3",
                    IconUrl = Program.discord.CurrentUser.AvatarUrl
                }
            };
            info.AddField(
                "Server Information",
                "Owner: " + ctx.Guild.Owner.Mention +
                "\nRegion: " + ctx.Guild.VoiceRegion.Name, true);
            info.AddField("General Information",
                "\nTotal members: " + ctx.Guild.MemberCount +
                "\nTotal channels: " + ctx.Guild.Channels.Count, true);
            info.AddField("Other Information",
                "\nRoles: " + ctx.Guild.Roles.Count +
                "\nEmojis: " + ctx.Guild.Emojis.Count, true);
            info.AddField("The server was created on",
               "" + ctx.Guild.CreationTimestamp.DateTime, true);
            await ctx.RespondAsync(embed: info);
        }

        [Command("members"), Description("Use this command to see the the amount of members of the current server."),]
        public async Task Members(CommandContext ctx)
        {
            var mems = ctx.Guild.MemberCount;
            await ctx.RespondAsync(embed: new DiscordEmbedBuilder
            {
                Title = "ExampleBot",
                Description = $"I found** {mems} **members.",
                Timestamp = DateTime.Now,
                Color = DiscordColor.Blurple,
                Footer = new DiscordEmbedBuilder.EmbedFooter
                {
                    Text = "ExampleBot by sEbi3",
                    IconUrl = Program.discord.CurrentUser.AvatarUrl
                }
            });
        }
    }
}
