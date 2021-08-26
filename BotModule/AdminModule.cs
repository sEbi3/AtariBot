using System;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using Test_Bot;

namespace AI_Bot.BotModule
{
    public class AdminModule : BaseCommandModule
    {
        [Command("purge"), Description("Use this command to purge an amount of messages in the current channel.")]
        public async Task Purge(CommandContext ctx, [Description("Write an amount of messages for the bot to delete.")] int amount)
        {
            if (!ctx.Member.PermissionsIn(ctx.Channel).HasPermission(Permissions.ManageMessages))
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
            else if (amount == 0)
            {
                await ctx.RespondAsync(embed: new DiscordEmbedBuilder
                {
                    Author = new DiscordEmbedBuilder.EmbedAuthor
                    {
                        IconUrl = Program.discord.CurrentUser.AvatarUrl,
                        Name = Program.discord.CurrentUser.Username
                    },
                    Thumbnail = new DiscordEmbedBuilder.EmbedThumbnail { Url = Program.discord.CurrentUser.AvatarUrl },
                    Title = "Warning",
                    Description = $"You need to add an amount of numbers to purge messages.\n```{Program.InternalPrefix}help purge\n{Program.InternalPrefix}purge [amount]```",
                    Timestamp = DateTime.Now,
                    Color = DiscordColor.IndianRed,
                    Footer = new DiscordEmbedBuilder.EmbedFooter
                    {
                        Text = $"{Program.BotName} by sEbi3",
                    }
                });
                return;
            }
            else if (amount > 100)
            {
                await ctx.RespondAsync(embed: new DiscordEmbedBuilder
                {
                    Author = new DiscordEmbedBuilder.EmbedAuthor
                    {
                        IconUrl = Program.discord.CurrentUser.AvatarUrl,
                        Name = Program.discord.CurrentUser.Username
                    },
                    Thumbnail = new DiscordEmbedBuilder.EmbedThumbnail { Url = Program.discord.CurrentUser.AvatarUrl },
                    Title = "Warning",
                    Description = $"The limit for purging messages is **100**.",
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
                DiscordMember mem = ctx.Member;
                var messages = await ctx.Channel.GetMessagesAsync(amount + 1);
                await ctx.Channel.DeleteMessagesAsync(messages);
                await ctx.RespondAsync(embed: new DiscordEmbedBuilder
                {
                    Author = new DiscordEmbedBuilder.EmbedAuthor
                    {
                        IconUrl = Program.discord.CurrentUser.AvatarUrl,
                        Name = Program.discord.CurrentUser.Username
                    },
                    Title = "Messages were purged",
                    Description = "**" + amount.ToString() + $"** messages were deleted by {mem.Mention}.",
                    Timestamp = DateTime.Now,
                    Color = DiscordColor.Green,
                    Footer = new DiscordEmbedBuilder.EmbedFooter
                    {
                        Text = $"{Program.BotName} by sEbi3",
                    }
                });
                await ctx.Message.DeleteAsync();
            }
        }

        [Command("kick"), Description("Use this command to kick an user from the current server."), Aliases("k")]
        public async Task KickAsync(CommandContext ctx, [Description("The member to kick.")] DiscordMember member, [RemainingText, Description("Reason for the kick. (optional)")] string reason)
        {
            if (!ctx.Member.PermissionsIn(ctx.Channel).HasPermission(Permissions.KickMembers))
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
            else if (member == null)
            {
                await ctx.RespondAsync(embed: new DiscordEmbedBuilder
                {
                    Author = new DiscordEmbedBuilder.EmbedAuthor
                    {
                        IconUrl = Program.discord.CurrentUser.AvatarUrl,
                        Name = Program.discord.CurrentUser.Username
                    },
                    Thumbnail = new DiscordEmbedBuilder.EmbedThumbnail { Url = Program.discord.CurrentUser.AvatarUrl },
                    Title = "Warning",
                    Description = $"You need to add an `user` as argument to kick someone.\n```{Program.InternalPrefix}help kick\n{Program.InternalPrefix}kick [member] [reason]```",
                    Timestamp = DateTime.Now,
                    Color = DiscordColor.IndianRed,
                    Footer = new DiscordEmbedBuilder.EmbedFooter
                    {
                        Text = $"{Program.BotName} by sEbi3",
                    }
                });
                return;
            }
            else if (member.Id == ctx.User.Id)
            {
                await ctx.RespondAsync(embed: new DiscordEmbedBuilder
                {
                    Author = new DiscordEmbedBuilder.EmbedAuthor
                    {
                        IconUrl = Program.discord.CurrentUser.AvatarUrl,
                        Name = Program.discord.CurrentUser.Username
                    },
                    Thumbnail = new DiscordEmbedBuilder.EmbedThumbnail { Url = Program.discord.CurrentUser.AvatarUrl },
                    Title = "Warning",
                    Description = $"You can't kick yourself.\n\n```{Program.InternalPrefix}help kick\n{Program.InternalPrefix}kick [member] [reason]```",
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
                    Title = "User has been kicked",
                    Thumbnail = new DiscordEmbedBuilder.EmbedThumbnail { Url = Program.discord.CurrentUser.AvatarUrl },
                    Description = $"{member.Mention} has been kicked from the server by {ctx.Member.Mention}.\n\n**Reason for the kick**\n {reason}",
                    Timestamp = DateTime.Now,
                    Color = DiscordColor.Green,
                    Footer = new DiscordEmbedBuilder.EmbedFooter
                    {
                        Text = $"{Program.BotName} by sEbi3",
                    }
                });
            }
        }

        [Command("ban"), Description("Use this command to ban an user from the current server.")]
        public async Task Bann(CommandContext ctx, [Description("The member to ban.")] DiscordMember member, [RemainingText] [Description("Reason for the ban. (optional)")]string reason)
        {
            if (!ctx.Member.PermissionsIn(ctx.Channel).HasPermission(Permissions.BanMembers))
            {
                await ctx.RespondAsync(embed: new DiscordEmbedBuilder
                {
                    Author = new DiscordEmbedBuilder.EmbedAuthor
                    {
                        IconUrl = Program.discord.CurrentUser.AvatarUrl,
                        Name = Program.discord.CurrentUser.Username
                    },
                    Thumbnail = new DiscordEmbedBuilder.EmbedThumbnail { Url = Program.discord.CurrentUser.AvatarUrl },
                    Title = "Warning",
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
            else if (member == null)
            {
                await ctx.RespondAsync(embed: new DiscordEmbedBuilder
                {
                    Author = new DiscordEmbedBuilder.EmbedAuthor
                    {
                        IconUrl = Program.discord.CurrentUser.AvatarUrl,
                        Name = Program.discord.CurrentUser.Username
                    },
                    Thumbnail = new DiscordEmbedBuilder.EmbedThumbnail { Url = Program.discord.CurrentUser.AvatarUrl },
                    Title = "Warning",
                    Description = $"You need to add an `user` as argument to ban someone.\n\n```{Program.InternalPrefix}help ban\n{Program.InternalPrefix}ban [member] [reason]```",
                    Timestamp = DateTime.Now,
                    Color = DiscordColor.IndianRed,
                    Footer = new DiscordEmbedBuilder.EmbedFooter
                    {
                        Text = $"{Program.BotName} by sEbi3",
                    }
                });
                return;
            }
            else if (member.Id == ctx.User.Id)
            {
                await ctx.RespondAsync(embed: new DiscordEmbedBuilder
                {
                    Author = new DiscordEmbedBuilder.EmbedAuthor
                    {
                        IconUrl = Program.discord.CurrentUser.AvatarUrl,
                        Name = Program.discord.CurrentUser.Username
                    },
                    Thumbnail = new DiscordEmbedBuilder.EmbedThumbnail { Url = Program.discord.CurrentUser.AvatarUrl },
                    Title = "Warning",
                    Description = $"You can't ban yourself.\n\n```{Program.InternalPrefix}help ban\n{Program.InternalPrefix}ban [member] [reason]```",
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
                await member.BanAsync(7, null);
                await ctx.Message.DeleteAsync();
                await member.SendMessageAsync(embed: new DiscordEmbedBuilder
                {
                    Author = new DiscordEmbedBuilder.EmbedAuthor
                    {
                        IconUrl = Program.discord.CurrentUser.AvatarUrl,
                        Name = Program.discord.CurrentUser.Username
                    },
                    Thumbnail = new DiscordEmbedBuilder.EmbedThumbnail { Url = Program.discord.CurrentUser.AvatarUrl },
                    Title = "You were banned",
                    Description = $"You were banned by **{ctx.Member.Username}#{ctx.User.Discriminator}**.\n\n**Reason for the ban**\n{reason}.\n**Information of the server**\nServer: *{ctx.Guild.Name}*\nRegion: *{ctx.Guild.VoiceRegion.Name}*",
                    Timestamp = DateTime.Now,
                    Color = DiscordColor.Red,
                    Footer = new DiscordEmbedBuilder.EmbedFooter
                    {
                        Text = $"{Program.BotName} by sEbi3",
                    }
                });
                await ctx.RespondAsync(embed: new DiscordEmbedBuilder {
                    Author = new DiscordEmbedBuilder.EmbedAuthor
                    {
                        IconUrl = Program.discord.CurrentUser.AvatarUrl,
                        Name = Program.discord.CurrentUser.Username
                    },
                    Thumbnail = new DiscordEmbedBuilder.EmbedThumbnail { Url = Program.discord.CurrentUser.AvatarUrl },
                    Title = "User has been banned",
                    Description = $"{member.Mention} has been banned from this server by {ctx.Member.Mention}.\n\n**Reason for the ban**\n {reason}",
                    Timestamp = DateTime.Now,
                    Color = DiscordColor.Green,
                    Footer = new DiscordEmbedBuilder.EmbedFooter
                    {
                        Text = $"{Program.BotName} by sEbi3",
                    }
                });
                return;
            }
        }

        [Command("unban"), Description("Use this command to unban an user from the current server."), RequirePermissions(Permissions.BanMembers), RequireGuild]
        public async Task UnbanAsync2(CommandContext context, [Description("The member to unban.")] DiscordMember member)
        {
            DiscordUser user;
            user = await context.Client.GetUserAsync(member.Id);
            if (!context.Member.PermissionsIn(context.Channel).HasPermission(Permissions.BanMembers))
            {
                await context.RespondAsync(embed: new DiscordEmbedBuilder
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
            else if (member == null)
            {
                await context.RespondAsync(embed: new DiscordEmbedBuilder
                {
                    Author = new DiscordEmbedBuilder.EmbedAuthor
                    {
                        IconUrl = Program.discord.CurrentUser.AvatarUrl,
                        Name = Program.discord.CurrentUser.Username
                    },
                    Title = "Warning",
                    Thumbnail = new DiscordEmbedBuilder.EmbedThumbnail { Url = Program.discord.CurrentUser.AvatarUrl },
                    Description = $"You need to add an `user` or `user ID` as argument to unban someone.\n```{Program.InternalPrefix}help unban\n{Program.InternalPrefix}unban [member] or [member ID]```",
                    Timestamp = DateTime.Now,
                    Color = DiscordColor.IndianRed,
                    Footer = new DiscordEmbedBuilder.EmbedFooter
                    {
                        Text = $"{Program.BotName} by sEbi3",
                    }
                });
                return;
            }
            else if (user == null)
            {
                await context.Message.DeleteAsync();
                await context.RespondAsync(embed: new DiscordEmbedBuilder
                {
                    Author = new DiscordEmbedBuilder.EmbedAuthor
                    {
                        IconUrl = Program.discord.CurrentUser.AvatarUrl,
                        Name = Program.discord.CurrentUser.Username
                    },
                    Title = "Warning",
                    Thumbnail = new DiscordEmbedBuilder.EmbedThumbnail { Url = Program.discord.CurrentUser.AvatarUrl },
                    Description = $"You need to add an **valid** `user` or `user ID` as argument to unban someone.\n```{Program.InternalPrefix}help unban\n{Program.InternalPrefix}unban [member] or [member ID]```",
                    Color = DiscordColor.IndianRed,
                    Timestamp = DateTime.Now,
                    Footer = new DiscordEmbedBuilder.EmbedFooter
                    {
                        Text = $"{Program.BotName} by sEbi3",
                    }
                });
                return;
            }
            else
            {
                await context.Guild.UnbanMemberAsync(user, $"{context.User.Username}#{context.User.Discriminator}");
                await context.Message.DeleteAsync();
                await context.RespondAsync(embed: new DiscordEmbedBuilder
                {
                    Author = new DiscordEmbedBuilder.EmbedAuthor
                    {
                        IconUrl = Program.discord.CurrentUser.AvatarUrl,
                        Name = Program.discord.CurrentUser.Username
                    },
                    Title = "User has been unbanned",
                    Thumbnail = new DiscordEmbedBuilder.EmbedThumbnail { Url = Program.discord.CurrentUser.AvatarUrl },
                    Description = $"{context.Member.Mention} successfully unbanned {user.Mention}.",
                    Color = DiscordColor.Green,
                    Timestamp = DateTime.Now,
                    Footer = new DiscordEmbedBuilder.EmbedFooter
                    {
                        Text = $"{Program.BotName} by sEbi3",
                    }
                });
            }
        }

        [Command("unban"), Description("Use this command to unban an user from the current server."), RequirePermissions(Permissions.BanMembers), RequireGuild]
        public async Task UnbanAsync(CommandContext context, [Description("The user ID to unban.")] ulong id)
        {
            DiscordUser user;
            user = await context.Client.GetUserAsync(id);
            if (!context.Member.PermissionsIn(context.Channel).HasPermission(Permissions.BanMembers))
            {
                await context.RespondAsync(embed: new DiscordEmbedBuilder
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
            else if (id == 0)
            {
                await context.Message.DeleteAsync();
                await context.RespondAsync(embed: new DiscordEmbedBuilder
                {
                    Author = new DiscordEmbedBuilder.EmbedAuthor
                    {
                        IconUrl = Program.discord.CurrentUser.AvatarUrl,
                        Name = Program.discord.CurrentUser.Username
                    },
                    Title = "Warning",
                    Thumbnail = new DiscordEmbedBuilder.EmbedThumbnail { Url = Program.discord.CurrentUser.AvatarUrl },
                    Description = $"You need to add an `user` or `user ID` as argument to unban someone.\n```{Program.InternalPrefix}help unban\n{Program.InternalPrefix}unban [member] or [member ID]```",
                    Timestamp = DateTime.Now,
                    Color = DiscordColor.IndianRed,
                    Footer = new DiscordEmbedBuilder.EmbedFooter
                    {
                        Text = $"{Program.BotName} by sEbi3",
                    }
                });
                return;
            }
            else if (user == null)
            {
                await context.Message.DeleteAsync();
                await context.RespondAsync(embed: new DiscordEmbedBuilder
                {
                    Author = new DiscordEmbedBuilder.EmbedAuthor
                    {
                        IconUrl = Program.discord.CurrentUser.AvatarUrl,
                        Name = Program.discord.CurrentUser.Username
                    },
                    Title = "Warning",
                    Thumbnail = new DiscordEmbedBuilder.EmbedThumbnail { Url = Program.discord.CurrentUser.AvatarUrl },
                    Description = $"You need to add an **valid** `user` or `user ID` as argument to unban someone.\n```{Program.InternalPrefix}help unban\n{Program.InternalPrefix}unban [member] or [member ID]```",
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
                await context.Guild.UnbanMemberAsync(user, $"{context.User.Username}#{context.User.Discriminator}");
                await context.Message.DeleteAsync();
                await context.RespondAsync(embed: new DiscordEmbedBuilder
                {
                    Author = new DiscordEmbedBuilder.EmbedAuthor
                    {
                        IconUrl = Program.discord.CurrentUser.AvatarUrl,
                        Name = Program.discord.CurrentUser.Username
                    },
                    Title = "User has been unbanned",
                    Thumbnail = new DiscordEmbedBuilder.EmbedThumbnail { Url = Program.discord.CurrentUser.AvatarUrl },
                    Description = $"{context.Member.Mention} successfully unbanned {user.Mention}.",
                    Timestamp = DateTime.Now,
                    Color = DiscordColor.Green,
                    Footer = new DiscordEmbedBuilder.EmbedFooter
                    {
                        Text = $"{Program.BotName} by sEbi3",
                    }
                });
            }
        }
    }
}
