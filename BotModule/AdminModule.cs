using System;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;

namespace ExampleBot.BotModule
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
            else if (amount == 0)
            {
                await ctx.RespondAsync(embed: new DiscordEmbedBuilder
                {
                    Title = "ExampleBot",
                    Description = $"You need to add an amount of numbers to purge messages. :warning:\nIf you need help, use the `??help purge` command.",
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
                DiscordMember mem = ctx.Member;
                var messages = await ctx.Channel.GetMessagesAsync(amount + 1);
                await ctx.Channel.DeleteMessagesAsync(messages);
                await ctx.RespondAsync(embed: new DiscordEmbedBuilder
                {
                    Title = "ExampleBot",
                    Description = "**" + amount.ToString() + $"** messages were deleted by {mem.Mention}.",
                    Timestamp = DateTime.Now,
                    Color = DiscordColor.Blurple,
                    Footer = new DiscordEmbedBuilder.EmbedFooter
                    {
                        Text = "ExampleBot by sEbi3",
                        IconUrl = Program.discord.CurrentUser.AvatarUrl
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
            else if (member == null)
            {
                await ctx.RespondAsync(embed: new DiscordEmbedBuilder
                {
                    Title = "ExampleBot",
                    Description = $"You need to add an `user` as parameter to kick someone. :warning:\nIf you need help, use the `??help kick` command.",
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
            else if (member.Id == ctx.User.Id)
            {
                await ctx.RespondAsync(embed: new DiscordEmbedBuilder
                {
                    Title = "ExampleBot",
                    Description = $"You can't kick yourself. :x:\nIf you need help, use the `??help kick` command.",
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
            else
            {
                await ctx.RespondAsync(embed: new DiscordEmbedBuilder
                {
                    Title = "ExampleBot",
                    Thumbnail = new DiscordEmbedBuilder.EmbedThumbnail { Url = Program.discord.CurrentUser.AvatarUrl },
                    Description = $"{member.Mention} has been kicked from the server by {ctx.Member.Mention}.\n\n**Reason for the kick**\n {reason}",
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

        [Command("ban"), Description("Use this command to ban an user from the current server.")]
        public async Task Bann(CommandContext ctx, [Description("The member to ban.")] DiscordMember member, [RemainingText] [Description("Reason for the ban. (optional)")]string reason)
        {
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
            else if (member == null)
            {
                await ctx.RespondAsync(embed: new DiscordEmbedBuilder
                {
                    Title = "ExampleBot",
                    Description = $"You need to add an `user` as parameter to ban someone. :warning:\nIf you need help, use the `??help ban` command.",
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
            else if (member.Id == ctx.User.Id)
            {
                await ctx.RespondAsync(embed: new DiscordEmbedBuilder
                {
                    Title = "ExampleBot",
                    Description = $"You can't ban yourself. :x:\nIf you need help, use the `??help ban` command.",
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
            else
            {
                await member.BanAsync(7, null);
                await ctx.Message.DeleteAsync();
                await member.SendMessageAsync(embed: new DiscordEmbedBuilder
                {
                    Title = "ExampleBot",
                    Thumbnail = new DiscordEmbedBuilder.EmbedThumbnail { Url = Program.discord.CurrentUser.AvatarUrl },
                    Description = $"You were banned by **{ctx.Member.Username}#{ctx.User.Discriminator}**.\n\n**Reason for the ban**\n{reason}.\n\n**Information of the server**\nServer: *{ctx.Guild.Name}*\nRegion: *{ctx.Guild.VoiceRegion.Name}*",
                    Timestamp = DateTime.Now,
                    Color = DiscordColor.Red,
                    Footer = new DiscordEmbedBuilder.EmbedFooter
                    {
                        Text = "ExampleBot",
                        IconUrl = Program.discord.CurrentUser.AvatarUrl
                    }
                });
                await ctx.RespondAsync(embed: new DiscordEmbedBuilder {
                    Title = "ExampleBot",
                    Thumbnail = new DiscordEmbedBuilder.EmbedThumbnail { Url = Program.discord.CurrentUser.AvatarUrl },
                    Description = $"{member.Mention} has been banned from this server by {ctx.Member.Mention}.\n\n**Reason for the ban**\n {reason}",
                    Timestamp = DateTime.Now,
                    Color = DiscordColor.Blurple,
                    Footer = new DiscordEmbedBuilder.EmbedFooter
                    {
                        Text = "ExampleBot by sEbi3",
                        IconUrl = Program.discord.CurrentUser.AvatarUrl
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
                    Title = "ExampleBot",
                    Description = $"You don't have enough permissions to use this command. :x:",
                    Color = DiscordColor.Red,
                    Timestamp = DateTime.Now,
                    Footer = new DiscordEmbedBuilder.EmbedFooter
                    {
                        Text = "ExampleBot by sEbi3",
                        IconUrl = Program.discord.CurrentUser.AvatarUrl
                    }
                });
                return;
            }
            else if (member == null)
            {
                await context.RespondAsync(embed: new DiscordEmbedBuilder
                {
                    Title = "ExampleBot",
                    Description = $"You need to add an `user` as parameter to unban someone. :warning:\nIf you need help, use the `??help unban` command.",
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
            else if (user == null)
            {
                await context.Message.DeleteAsync();
                await context.RespondAsync(embed: new DiscordEmbedBuilder
                {
                    Title = "ExampleBot",
                    Description = $"You need to add an **valid** `user` as parameter to unban someone. :warning:\nIf you need help, use the `??help unban` command.",
                    Color = DiscordColor.Orange,
                    Timestamp = DateTime.Now,
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
                await context.Guild.UnbanMemberAsync(user, $"{context.User.Username}#{context.User.Discriminator}");
                await context.Message.DeleteAsync();
                await context.RespondAsync(embed: new DiscordEmbedBuilder
                {
                    Title = "ExampleBot",
                    Thumbnail = new DiscordEmbedBuilder.EmbedThumbnail { Url = Program.discord.CurrentUser.AvatarUrl },
                    Description = $"Unbanned {user.Mention} successfully.",
                    Color = DiscordColor.Blurple,
                    Timestamp = DateTime.Now,
                    Footer = new DiscordEmbedBuilder.EmbedFooter
                    {
                        Text = "ExampleBot by sEbi3",
                        IconUrl = Program.discord.CurrentUser.AvatarUrl
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
                    Title = "ExampleBot",
                    Description = $"You don't have enough permissions to use this command. :x:",
                    Color = DiscordColor.Red,
                    Timestamp = DateTime.Now,
                    Footer = new DiscordEmbedBuilder.EmbedFooter
                    {
                        Text = "ExampleBot by sEbi3",
                        IconUrl = Program.discord.CurrentUser.AvatarUrl
                    }
                });
                return;
            }
            else if (id == 0)
            {
                await context.Message.DeleteAsync();
                await context.RespondAsync(embed: new DiscordEmbedBuilder
                {
                    Title = "ExampleBot",
                    Description = $"You need to add an `user` as parameter to unban someone. :warning:\nIf you need help, use the `??help unban` command.",
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
            else if (user == null)
            {
                await context.Message.DeleteAsync();
                await context.RespondAsync(embed: new DiscordEmbedBuilder
                {
                    Title = "ExampleBot",
                    Description = $"You need to add an **valid** `user` as parameter to unban someone. :warning:\nIf you need help, use the `??help unban` command.",
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
                await context.Guild.UnbanMemberAsync(user, $"{context.User.Username}#{context.User.Discriminator}");
                await context.Message.DeleteAsync();
                await context.RespondAsync(embed: new DiscordEmbedBuilder
                {
                    Title = "ExampleBot",
                    Thumbnail = new DiscordEmbedBuilder.EmbedThumbnail { Url = Program.discord.CurrentUser.AvatarUrl },
                    Description = $"Unbanned {user.Mention} successfully.",
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
}
