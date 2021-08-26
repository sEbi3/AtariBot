using System.Collections.Generic;
using System.Linq;
using System.Text;
using DSharpPlus;
using System;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Converters;
using DSharpPlus.CommandsNext.Entities;
using DSharpPlus.Entities;
using Test_Bot;

namespace AI_Bot.HelpModule
{
    public sealed class Help : BaseHelpFormatter
    {
        private readonly DiscordEmbedBuilder _output;
        private string _description;
        private string _name;
        private string _name2;

        public Help(CommandContext ctx) : base(ctx)
        {
            _output = new DiscordEmbedBuilder()
            .WithColor(DiscordColor.Blurple)
            .WithTimestamp(DateTime.Now)
            .WithFooter($"{Program.BotName} by sEbi3")
            .WithThumbnail(Program.discord.CurrentUser.AvatarUrl)
            .WithTitle("Help");
        }

        public override CommandHelpMessage Build()
        {
            var desc = 
                $"Hello, I am the {Program.BotName}!\nHere is a list with all universal commands.\nUse **{Program.InternalPrefix}help <command>** for more details on a command.\n" + 
                $"Use **{Program.InternalPrefix}setup** for helping you to setup and use the bot.\n\n" +
                $"**Note:** This bot is still in an early alpha build and development.\nYou may wait a bit for the first public release.\n\n" +
                $"[[Visit the wiki for help and setup]](https://sebi3.gitbook.io/ai-bot/)\n" +
                $"[[Take a look into our FAQ for known issues]](https://sebi3.gitbook.io/ai-bot/wiki/frequently-asked-questions)\n\n";

            if (!string.IsNullOrWhiteSpace(_name))
            {
                _output.WithTitle(_name);
                desc = _description ?? "```No description provided.```";
            }

            _output.WithDescription(desc);
            _output.AddField(
               "**__Fun Commands__**",
               $"```\n" +
               $"{Program.InternalHelpPrefix}cat\n" +
               $"{Program.InternalHelpPrefix}dog\n" +
               $"{Program.InternalHelpPrefix}avatar\n" +
               $"{Program.InternalHelpPrefix}covid\n" +
               $"{Program.InternalHelpPrefix}servericon\n" +
               $"{Program.InternalHelpPrefix}msg\n" +
               $"{Program.InternalHelpPrefix}dm\n" +
               $"{Program.InternalHelpPrefix}whois```", true);
            _output.AddField(
               "**__Info Commands__**",
               $"```\n" + 
               $"{Program.InternalHelpPrefix}uptime\n" +
               $"{Program.InternalHelpPrefix}bot\n" +
               $"{Program.InternalHelpPrefix}setup\n" +
               $"{Program.InternalHelpPrefix}invite\n" +
               $"{Program.InternalHelpPrefix}info\n" +
               $"{Program.InternalHelpPrefix}help\n" +
               $"{Program.InternalHelpPrefix}ping```", true);
            _output.AddField(
              "**__Admin Commands__**",
              $"```\n" + 
              $"{Program.InternalHelpPrefix}kick\n" +
              $"{Program.InternalHelpPrefix}ban\n" +
              $"{Program.InternalHelpPrefix}unban\n" +
              $"{Program.InternalHelpPrefix}purge\n" +
              $"{Program.InternalHelpPrefix}embed\n" +
              $"{Program.InternalHelpPrefix}warn\n" +
              $"{Program.InternalHelpPrefix}warns```", true);
            return new CommandHelpMessage(embed: _output);
        }

        public override BaseHelpFormatter WithCommand(Command cmd)
        {
            _name = (cmd is CommandGroup ? "Group: " : "Command: ") + cmd.QualifiedName;
            _name2 = (cmd is CommandGroup ? "" : "") + cmd.QualifiedName;
            _description = cmd.Description;

            if (cmd.Aliases?.Any() ?? false)
                _output.AddField("**__Aliases__**", string.Join(", ", cmd.Aliases.Select(Formatter.InlineCode)), false);

            if (!(cmd.Overloads?.Any() ?? false)) return this;
            foreach (var overload in cmd.Overloads.OrderByDescending(o => o.Priority))
            {
                if (overload.Arguments.Count == 0) continue;

                var args = new StringBuilder();
                foreach (var arg in overload.Arguments)
                {
                    args.Append(Formatter.InlineCode($"[{CommandsNext.GetUserFriendlyTypeName(arg.Type)}]"));
                    args.Append(' ');
                    args.Append(arg.Description ?? "```This command has no description.```");
                    if (arg.IsOptional)
                    {
                        args.Append(" (def: ")
                            .Append(Formatter.InlineCode(arg.DefaultValue is null
                                ? "None"
                                : arg.DefaultValue.ToString())).Append(')');
                        args.Append(" (optional)");
                    }

                    args.AppendLine();
                }

                var usage = new StringBuilder();
                foreach (var usa in overload.Arguments)
                {
                    usage.Append($"[{CommandsNext.GetUserFriendlyTypeName(usa.Type)}]");
                    usage.Append(' ');
                }

                _output.AddField($"{(cmd.Overloads.Count > 1 ? $"**__Overload__** #{overload.Priority}" : "**__Arguments__**")}",
                    args.ToString() ?? "```This command has no arguments.```", false);

                _output.AddField("**__How to use this command__**", $"```{Program.InternalPrefix + _name2}" + " " + usage.ToString() + "```" ?? "No usage.", false);
            }
            return this;
        }

        public override BaseHelpFormatter WithSubcommands(IEnumerable<Command> subcommands)
        {
            var enumerable = subcommands.ToList();
            return this;
        }
    }
}
