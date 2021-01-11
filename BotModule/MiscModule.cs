using System;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using ExampleBot;

namespace ExampleBot.BotModule
{
    public class MiscModule : BaseCommandModule
    {
        [Command("msg"), Aliases("echo", "say"), Description("Use this command to let the bot repeat your message.")]
        public async Task Repeat(CommandContext ctx, [Description("Write a custom message for the bot to respond with.")] params string[] msg)
        {
            await ctx.Message.DeleteAsync();
            if (!ctx.Member.PermissionsIn(ctx.Channel).HasPermission(Permissions.BanMembers))
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
            if (msg == null)
            {
                await ctx.RespondAsync(embed: new DiscordEmbedBuilder
                {
                    Title = "ExampleBot",
                    Description = $"You need to add a message as parameter. :warning:\nIf you need help, use the `??help msg` command.",
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
                await ctx.TriggerTypingAsync();
                await ctx.RespondAsync(string.Join(" ", msg));
            }
        }

        [Command("dm"), Description("Use this command to let the bot repeat your message to an user per dm.")]
        public async Task DM(CommandContext ctx, [Description("The member for the bot to respond.")] DiscordMember mem, [Description("Write a custom message for the bot to respond with.")] params string[] msg)
        {
            await ctx.Message.DeleteAsync();
            if (!ctx.Member.PermissionsIn(ctx.Channel).HasPermission(Permissions.BanMembers))
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
            if (msg == null)
            {
                await ctx.RespondAsync(embed: new DiscordEmbedBuilder
                {
                    Title = "ExampleBot",
                    Description = $"You need to add a message as parameter. :warning:\nIf you need help, use the `??help msg` command.",
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
                await mem.SendMessageAsync(string.Join(" ", msg));
            }
        }

        [Command("ping"), Description("Use this command to check the ping of the AI Bot.")]
        public async Task PingAsync(CommandContext ctx)
        {
            await ctx.RespondAsync(embed: new DiscordEmbedBuilder
            {
                Title = "ExampleBot",
                Thumbnail = new DiscordEmbedBuilder.EmbedThumbnail { Url = Program.discord.CurrentUser.AvatarUrl },
                Description = $"pong!\n **{ ctx.Client.Ping }ms**",
                Timestamp = DateTime.Now,
                Color = DiscordColor.Blurple,
                Footer = new DiscordEmbedBuilder.EmbedFooter
                {
                    Text = "ExampleBot",
                    IconUrl = Program.discord.CurrentUser.AvatarUrl
                }
            });
        }

        [Command("setstatus"), Description("Use this command to set up the status of the AI Bot.")]
        public async Task Status(CommandContext ctx)
        {
            if (!ctx.Member.PermissionsIn(ctx.Channel).HasPermission(Permissions.ManageRoles))
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
            await Program.discord.UpdateStatusAsync(new DiscordActivity("at GitHub.com", ActivityType.Watching), UserStatus.Online, DateTimeOffset.Now);
            await ctx.RespondAsync(embed: new DiscordEmbedBuilder
            {
                Title = "ExampleBot",
                Description = $"Updated the status of the ExampleBot to the default preset.",
                Timestamp = DateTime.Now,
                Color = DiscordColor.Blurple,
                Footer = new DiscordEmbedBuilder.EmbedFooter
                {
                    Text = "ExampleBot by sEbi3",
                    IconUrl = Program.discord.CurrentUser.AvatarUrl
                }
            });
        }

        [Command("commands"), Aliases("c"), Description("Use this command to see a list with universal commands of the ExampleBot.")]
        public async Task commands(CommandContext ctx)
        {
            await ctx.RespondAsync(embed: new DiscordEmbedBuilder
            {
                Title = "Universal Commands",
                Description = 
                $"The following commands are the **Universal Commands** on every server.\n" + 
                $"You can find all commands on your current server with `{Program.InternalPrefix}help`.\n " +
                $"Use `{Program.InternalPrefix}help <command>` for more details of a command." +

                $"\n\n**Fun Commands**\n`{Program.InternalPrefix}cat` or `{Program.InternalPrefix}kittie` - Responds with a random cat picture.\n" +
                $"`{Program.InternalPrefix}dog` or `{Program.InternalPrefix}doggo` - Responds with a random dog picture.\n" +
                $"`{Program.InternalPrefix}getpb` `[@user]` - Responds with the picture of an user.\n" +
                $"`{Program.InternalPrefix}covid` `[country]` - Shows corona statistics from a country.\n" +
                $"`{Program.InternalPrefix}servericon` - Responds with the server icon of the current server.\n" +
                $"`{Program.InternalPrefix}whois` `[@user]` - Responds with information about the pinged user.\n\n**Info Commands**\n" +
                $"`{Program.InternalPrefix}ExampleBot` - Responds with information about the **ExampleBot**.\n" +
                $"`{Program.InternalPrefix}invite` - Responds with the **AI Bot** invite for your server.\n" +
                $"`{Program.InternalPrefix}info` - Responds with information about current server.\n" +
                $"`{Program.InternalPrefix}help` `<command>` - Responds with information about a command.\n" +
                $"`{Program.InternalPrefix}uptime` - Responds with the uptime of the **ExampleBot**.\n" +
                $"`{Program.InternalPrefix}members` - Responds with the current amount of members.\n\n**Misc Commands**\n" +
                $"`{Program.InternalPrefix}msg` or `echo` `[message]` - Responds with the used [message].\n" +
                $"`{Program.InternalPrefix}ping` - Responds with ping of the Discord API...oh and pong!\n" +
                $"`{Program.InternalPrefix}commands` - Responds with the current command.\n\n**Admin Commands**\n" +
                $"`{Program.InternalPrefix}kick` `[@user]` - kicking a user from the current discord server.\n" +
                $"`{Program.InternalPrefix}ban` `[@user]` - banning a user from the current discord server.\n" +
                $"`{Program.InternalPrefix}unban` `[user id]` - unbanning a user from the current discord server.\n" +
                $"`{Program.InternalPrefix}purge` `[amount]` - purge the chat with the amount of numbers.\n",
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