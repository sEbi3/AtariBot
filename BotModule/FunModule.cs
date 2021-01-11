using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using ExampleBot;

namespace ExampleBot.BotModule
{
    public class FunModule : BaseCommandModule
    {
        [Command("cat"), Aliases("kittie"), Description("Use this command to see a cute kittie."), Cooldown(1, 10, CooldownBucketType.User)]
        public async Task Cat(CommandContext ctx)
        {
            var url = "https://api.thecatapi.com/v1/images/search";
            using (WebClient client = new WebClient())
            {
                var content = client.DownloadString(url);
                content = content.Substring(1);
                content = content.Remove(content.Length - 1);
                JObject json = JObject.Parse(content);
                string cat = Convert.ToString(json["url"]);

                await ctx.RespondAsync(embed: new DiscordEmbedBuilder
                {
                    Title = "Found one!",
                    Description = "Awww <3",
                    ImageUrl = cat,
                    Color = DiscordColor.Blurple,
                    Timestamp = DateTimeOffset.Now,
                    Footer = new DiscordEmbedBuilder.EmbedFooter
                    {
                        Text = "ExampleBot by sEbi3",
                        IconUrl = Program.discord.CurrentUser.AvatarUrl
                    }
                });
            }
        }

        [Command("dog"), Aliases("doggo"), Description("Use this command to see a cute doggo."), Cooldown(1, 10, CooldownBucketType.User)]
        public async Task Dog(CommandContext ctx)
        {
            var url = "https://api.thedogapi.com/v1/images/search";
            using (WebClient client = new WebClient())
            {
                var content = client.DownloadString(url);
                content = content.Substring(1);
                content = content.Remove(content.Length - 1);
                JObject json = JObject.Parse(content);
                string cat = Convert.ToString(json["url"]);

                await ctx.RespondAsync(embed: new DiscordEmbedBuilder
                {
                    Title = "Found a doggo!",
                    Description = "Awww <3",
                    ImageUrl = cat,
                    Color = DiscordColor.Blurple,
                    Timestamp = DateTimeOffset.Now,
                    Footer = new DiscordEmbedBuilder.EmbedFooter
                    {
                        Text = "ExampleBot by sEbi3",
                        IconUrl = Program.discord.CurrentUser.AvatarUrl
                    }
                });
            }
        }

        [Command("getpb"), Description("Use this commend to reply with the user's avatar."), Aliases("pfp"), RequireGuild]
        public async Task AvatarAsync(CommandContext context, [RemainingText, Description("The member whose avatar you want.")] DiscordMember member)
        {
            if (member == null) member = context.Member;
            string imageUrl = member.GetAvatarUrl(member.AvatarHash.StartsWith("a_") ? ImageFormat.Gif : ImageFormat.Png);
            await context.RespondAsync(embed: new DiscordEmbedBuilder()
                .WithTitle("ExampleBot")
                .WithImageUrl(imageUrl)
                .WithTimestamp(DateTime.Now)
                .WithColor(DiscordColor.Blurple)
                .WithFooter("ExampleBot by sEbi3")
                .Build()); ;        
        }

        [Command("servericon"), Description("Use this command to get the server icon.")]
        public async Task si(CommandContext ctx)
        {
            await ctx.RespondAsync(embed: new DiscordEmbedBuilder
            {
                Title = "ExampleBot",
                Description = "Current icon of the server **" + ctx.Guild.Name + "**",
                ImageUrl = "" + ctx.Guild.IconUrl,
                Timestamp = DateTime.Now,
                Color = DiscordColor.Blurple,
                Footer = new DiscordEmbedBuilder.EmbedFooter
                {
                    Text = "ExampleBot by sEbi3",
                    IconUrl = Program.discord.CurrentUser.AvatarUrl
                }
            });
        }

        [Command("whois"), Description("Use this command to get information of an user.")]
        public async Task WhoIs(CommandContext ctx, [Description("The member whose information you want.")] DiscordUser member)
        {
            var user = (await ctx.Guild.GetMemberAsync(member.Id));
            var roles2 = new List<string>();
            foreach (var role in user.Roles)
                roles2.Add(role.Mention);
            var roles = string.Join(", ", roles2);

            var em = new DiscordEmbedBuilder
            {
                Author = new DiscordEmbedBuilder.EmbedAuthor
                {
                    IconUrl = member.AvatarUrl,
                    Name = member.Username + "#" + member.Discriminator
                },
                Thumbnail = new DiscordEmbedBuilder.EmbedThumbnail
                {
                    Url = member.AvatarUrl
                },
                Color = DiscordColor.Blurple,
                Timestamp = DateTime.Now,
                Footer = new DiscordEmbedBuilder.EmbedFooter
                {
                    Text = "ExampleBot by sEbi3",
                    IconUrl = ExampleBot.Program.discord.CurrentUser.AvatarUrl
                }
            }
                            .AddField("Joined", user.JoinedAt.DateTime.ToString(), true)
                            .AddField("Registered", user.CreationTimestamp.ToString(), true)
                            .AddField($"Roles [{user.Roles.Count()}]", roles, false);
            await ctx.RespondAsync(embed: em);
        }

        [Command("whois"), Description("Use this command to get information of an user.")]
        public async Task WhoIsNoParamenter(CommandContext ctx)
        {
            var user = ctx.Member;
            var member = (await ctx.Guild.GetMemberAsync(user.Id));
            var roles2 = new List<string>();
            foreach (var role in member.Roles)
                roles2.Add(role.Mention);
            var roles = string.Join(", ", roles2);

            var em = new DiscordEmbedBuilder
            {
                Author = new DiscordEmbedBuilder.EmbedAuthor
                {
                    IconUrl = user.AvatarUrl,
                    Name = user.Username + "#" + user.Discriminator
                },
                Thumbnail = new DiscordEmbedBuilder.EmbedThumbnail
                {
                    Url = user.AvatarUrl
                },
                Color = DiscordColor.Blurple,
                Timestamp = DateTime.Now,
                Footer = new DiscordEmbedBuilder.EmbedFooter
                {
                    Text = "ExampleBot by sEbi3",
                    IconUrl = ExampleBot.Program.discord.CurrentUser.AvatarUrl
                }
            }
                            .AddField("Joined", member.JoinedAt.DateTime.ToString(), true)
                            .AddField("Registered", member.CreationTimestamp.ToString(), true)
                            .AddField($"Roles [{member.Roles.Count()}]", roles, false);
            await ctx.RespondAsync(embed: em);
        }

        [Command("covid"), Description("Use this command to show the corona statitics.")]
        public async Task Corona(CommandContext ctx, [Description("The country you want to look up for covid statitics.")] string country)
        {
            using (WebClient client = new WebClient())
            {
                var response = client.DownloadString("https://api.covid19api.com/summary");
                var stuff = JsonConvert.DeserializeObject<ExampleBot.Utils.Welcome>(response);
                foreach (var x in stuff.Countries)
                {
                    if (x.CountryCountry == country || x.CountryCode == country)
                    {
                        var ok = x.TotalConfirmed - x.TotalRecovered;
                        var color = "#19ff0d";

                        if (ok < 500)
                        {
                            color = "#0dff00";
                        }
                        else if (ok >= 500 && ok <= 500)
                        {
                            color = "#f2ff00";
                        }
                        else if (ok >= 5001 && ok <= 20000)
                        {
                            color = "#c7a418";
                        }
                        else if (ok >= 20001)
                        {
                            color = "#ff002b";
                        }
                        var CoronaEmbed = new DiscordEmbedBuilder
                        {
                            Thumbnail = new DiscordEmbedBuilder.EmbedThumbnail { Url = "https://www.countryflags.io/" + x.CountryCode + "/flat/64.png" },
                            Title = $"Corona Statistic of {country}",
                            Color = new DiscordColor(color),
                            Description = "Live Statistics",
                            Timestamp = DateTime.Now,
                            Footer = new DiscordEmbedBuilder.EmbedFooter
                            {
                                Text = "ExampleBot by sEbi3",
                                IconUrl = ExampleBot.Program.discord.CurrentUser.AvatarUrl
                            }
                        };
                        CoronaEmbed.AddField("New Confirmed", x.NewConfirmed.ToString(), true);
                        CoronaEmbed.AddField("Total Confirmed", x.TotalConfirmed.ToString(), true);
                        CoronaEmbed.AddField("New Deaths", x.NewDeaths.ToString(), true);
                        CoronaEmbed.AddField("Total Deaths", x.TotalDeaths.ToString(), true);
                        CoronaEmbed.AddField("New Recovered", x.NewRecovered.ToString(), true);
                        CoronaEmbed.AddField("Total Recovered", x.TotalRecovered.ToString(), true);
                        CoronaEmbed.AddField("Now infected", ok.ToString(), true);
                        await ctx.RespondAsync(embed: CoronaEmbed);
                    }
                }
            }
        }
    }
}
