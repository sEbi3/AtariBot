using System;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using Test_Bot;

namespace AI_Bot.BotModule
{
    public class MiscModule : BaseCommandModule
    {
        [Command("msg"), Aliases("echo", "say"), Description("Use this command to let the bot repeat your message.")]
        public async Task Repeat(CommandContext ctx, [Description("Write a custom message for the bot to respond with.")] string msg)
        {
            await ctx.Message.DeleteAsync();
            if (!ctx.Member.PermissionsIn(ctx.Channel).HasPermission(Permissions.BanMembers))
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
            else if (msg == null)
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
                    Description = $"You need to add a message as argument.\n```{Program.InternalPrefix}help msg\n{Program.InternalPrefix}msg [message]```",
                    Timestamp = DateTime.Now,
                    Color = DiscordColor.Orange,
                    Footer = new DiscordEmbedBuilder.EmbedFooter
                    {
                        Text = $"{Program.BotName} by sEbi3",
                    }
                });
                return;
            }
            else if (string.IsNullOrWhiteSpace(msg))
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
                    Description = $"You need to add a message as argument.\n```{Program.InternalPrefix}help msg\n{Program.InternalPrefix}msg [message]```",
                    Timestamp = DateTime.Now,
                    Color = DiscordColor.Orange,
                    Footer = new DiscordEmbedBuilder.EmbedFooter
                    {
                        Text = $"{Program.BotName} by sEbi3",
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

        [Command("embed"), Aliases("em"), Description("Use this command to let the bot repeat your message as embed.")]
        public async Task EmbedRepeat(CommandContext ctx, [Description("Write a custom message for the bot to respond with as embed.")] string msg)
        {
            await ctx.Message.DeleteAsync();
            if (!ctx.Member.PermissionsIn(ctx.Channel).HasPermission(Permissions.BanMembers))
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
            else if (msg == null)
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
                    Description = $"You need to add a message as argument.\n```{Program.InternalPrefix}help embed\n{Program.InternalPrefix}embed [message]```",
                    Timestamp = DateTime.Now,
                    Color = DiscordColor.Orange,
                    Footer = new DiscordEmbedBuilder.EmbedFooter
                    {
                        Text = $"{Program.BotName} by sEbi3",
                    }
                });
                return;
            }
            else if (string.IsNullOrWhiteSpace(msg))
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
                    Description = $"You need to add a message as argument.\n```{Program.InternalPrefix}help embed\n{Program.InternalPrefix}embed [message]```",
                    Timestamp = DateTime.Now,
                    Color = DiscordColor.Orange,
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
                    Description = string.Join(" ", msg),
                    Color = DiscordColor.Blurple,
                    Footer = new DiscordEmbedBuilder.EmbedFooter
                    {
                        Text = $"{Program.BotName} by sEbi3",
                    }
                });
            }
        }

        [Command("dm"), Description("Use this command to let the bot repeat your message to an user per dm.")]
        public async Task DM(CommandContext ctx, [Description("The member for the bot to respond.")] DiscordMember mem, [Description("Write a custom message for the bot to respond with.")] string msg)
        {
            await ctx.Message.DeleteAsync();
            if (!ctx.Member.PermissionsIn(ctx.Channel).HasPermission(Permissions.BanMembers))
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
            else if (mem == null)
            {
                await ctx.RespondAsync(embed: new DiscordEmbedBuilder
                {
                    Author = new DiscordEmbedBuilder.EmbedAuthor
                    {
                        IconUrl = Program.discord.CurrentUser.AvatarUrl,
                        Name = Program.discord.CurrentUser.Username
                    },
                    Title = "Warning",
                    Description = $"You need to add an `user` as argument.\n```{Program.InternalPrefix}help dm\n{Program.InternalPrefix}dm [member] [message]```",
                    Timestamp = DateTime.Now,
                    Color = DiscordColor.Orange,
                    Footer = new DiscordEmbedBuilder.EmbedFooter
                    {
                        Text = $"{Program.BotName} by sEbi3",
                    }
                });
                return;
            }
            else if (string.IsNullOrWhiteSpace(msg))
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
                    Description = $"You need to add a message as argument.\n```{Program.InternalPrefix}help dm\n{Program.InternalPrefix}dm [message]```",
                    Timestamp = DateTime.Now,
                    Color = DiscordColor.Orange,
                    Footer = new DiscordEmbedBuilder.EmbedFooter
                    {
                        Text = $"{Program.BotName} by sEbi3",
                    }
                });
                return;
            }
            else if (msg == null)
            {
                await ctx.RespondAsync(embed: new DiscordEmbedBuilder
                {
                    Author = new DiscordEmbedBuilder.EmbedAuthor
                    {
                        IconUrl = Program.discord.CurrentUser.AvatarUrl,
                        Name = Program.discord.CurrentUser.Username
                    },
                    Title = "Warning",
                    Description = $"You need to add a message as argument.\n```{Program.InternalPrefix}help dm\n{Program.InternalPrefix}dm [member] [message]```",
                    Timestamp = DateTime.Now,
                    Color = DiscordColor.Orange,
                    Footer = new DiscordEmbedBuilder.EmbedFooter
                    {
                        Text = $"{Program.BotName} by sEbi3",
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
                Author = new DiscordEmbedBuilder.EmbedAuthor
                {
                    IconUrl = Program.discord.CurrentUser.AvatarUrl,
                    Name = Program.discord.CurrentUser.Username
                },
                Description = $"pong!\n **{ ctx.Client.Ping }ms**",
                Timestamp = DateTime.Now,
                Color = DiscordColor.Blurple,
                Footer = new DiscordEmbedBuilder.EmbedFooter
                {
                    Text = $"{Program.BotName} by sEbi3",
                }
            });
        }

        [Command("setup"), Description("Use this command for help to set up and use the bot.")]
        public async Task Tutorial(CommandContext ctx)
        {
            await ctx.RespondAsync(embed: new DiscordEmbedBuilder
            {
                Author = new DiscordEmbedBuilder.EmbedAuthor
                {
                    IconUrl = Program.discord.CurrentUser.AvatarUrl,
                    Name = Program.discord.CurrentUser.Username
                },
                Title = "How to use the Bot?",
                Thumbnail = new DiscordEmbedBuilder.EmbedThumbnail { Url = Program.discord.CurrentUser.AvatarUrl },
                Description = 
                $"Hello, I am the {Program.BotName}.\n" +
                $"I'll help you to use my commands.\n\n" +

                "**__Where can I find all commands?__**" +
                $"**1.** If you need help with finding the **Universal Commands**,\nuse the `{Program.InternalPrefix}help` command in the current chat.\n\n" +
                $"**2.** If you're not sure for what a specific command is or how to use that command, use the `{Program.InternalPrefix}help` `<command>` command.\n\n" +

                "**__Where can I find help?__**" +
                $"[[Visit the wiki for help and setup]](https://sebi3.gitbook.io/ai-bot/)\n" +
                $"[[Take a look into our FAQ for known issues and general questions]](https://sebi3.gitbook.io/ai-bot/wiki/frequently-asked-questions)\n\n",
                Timestamp = DateTime.Now,
                Color = DiscordColor.Blurple,
                Footer = new DiscordEmbedBuilder.EmbedFooter
                {
                    Text = $"{Program.BotName} by sEbi3",
                }
            });
        }
    }
}