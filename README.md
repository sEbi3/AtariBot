# AtariBot for Discord (Build 1.0.44)
AtariBot is a multi-purpose bot with many features for moderation, fun and administration.<br>
- <a href="https://sebi3.de/docs/AtariBot/getting-started">Click here for more information about the AtariBot.</a><br>
- <a href="https://sebi3.de/docs/AtariBot/fun-commands">Click here for a list with all commands.</a>
 
 
## This bot was built with:
- <a href="https://dsharpplus.github.io/">DSharpPlus v4.0.1</a><br><br>

--------

## Current Features
- Every command has a description for the <code>function</code>, <code>arguments</code>, <code>permissions</code>, <code>aliases</code> and on how to use the commands.
- Permissions are set for each command.<br>
  ○ In this case normal users can't execute commands used by moderators or administartors.<br><br>
- A lot of embeds were used for the commands in order to make it looks more clean and easier to read.
- This bot adds many commands for moderation, administration, information and fun.<br>
  ○ Take a look at the <code>DSharpPlus Documentation</code> in order to create your own commands.<br>
  ○ You can also add commands for youself easily by taking a look at the default commands in order to see how they were made.<br><br>

--------

## How to create your bot application and invite it to your server using this source code?
<b>1.)</b> Download the source code of the bot <a href="https://github.com/sEbi3/AtariBot">[here]</a>.<br>
<b>2.)</b> Visit the <a href="https://discord.com/developers/applications">Discord Developer Portal</a> and go to <code>Applications</code>.<br>
<b>3.)</b> Create a new Application and open it.<br>
<b>4.)</b> Go to the <code>Bot</code> tab on the left side and <code>add a new Bot</code>.<br>
<b>5.)</b> Copy the <code>Token</code> and paste it into the <code>program.cs</code> class here:

```cs
discord = new DiscordClient(new DiscordConfiguration
            {
                Token = "Paste your Token here",
                TokenType = TokenType.Bot, MinimumLogLevel = Microsoft.Extensions.Logging.LogLevel.Debug,
            });
```
<b>6.)</b> Go back to the <a href="https://discord.com/developers/applications">Discord Developer Portal</a> and go into the <code>General Information</code> tab.</p>
<b>7.)</b> Now copy the <code>Application ID</code> and open <a href="https://discord.com/oauth2/authorize?client_id=711550600286044201&scope=bot&permissions=8">[this URL]</a> on a new tab and change the <code>Application ID</code> from <code>711550600286044201</code> to the one you copied.<br>
<b>8.)</b> Refresh the page and choose your discord server in the dropdown list. The bot will now join your server.<br><br>

--------
 
## How to create a bot using this source code and running it on your localhost?
<b>1.)</b> Create a new <code>Console App (.NET Core)</code> project.<br>
<b>2.)</b> Add the source code into your project by dragging and dropping all files and folders into your project directory.<br>
<b>3.)</b> Add the <a href="https://dsharpplus.github.io/">DSharpPlus</a> API to your project. (Use the <code>NuGet Manager</code> for adding the API.)<br>
<b>4.)</b> Change everything to your liking. (Have a look above on how to create a <code>bot application</code>)<br>
<b>5.)</b> Now you only need to <code>compile the program</code>. In order to start your bot, go into your project files and start the <code>AtariBot.exe</code>.

The bot is only online when the <code>AtariBot.exe</code> is running. If you want to have the bot online 24/7, you need to rent a server and start the <code>AtariBot.exe</code> from the server. (For uploading the files, you need to use a program that supports FTP.)<br><br>

--------

## How to change/add permissions to a specific command?
<b>1.)</b> Go into one of the Modules. (In this case I use the <code>MiscModule</code>.).<br>
<b>2.)</b> Now find the command where you want to change the permissions. (In this case I use the <code>msg</code> command.).<br>
<b>3.)</b> After you have found the command, find the following <code>if statement</code>:

```cs
if (!ctx.Member.PermissionsIn(ctx.Channel).HasPermission(Permissions.BanMembers))
{
//If the user has not enough permissions, this block of code runs.
}
else
//If the user has enough permissions, this block of code runs.
}
```
<b>6.)</b> The only thing you need to change are the permissions in the statement.<br>(For example: from <code>Permissions.BanMembers</code> to <code>Permissions.Administrator</code>.)<br>
<b>7.)</b> If you don't want any permissions to be required in order to use a command, just leave the <code>if statement</code> away.<br><br>

--------

## Support
If you like my work and want access to early versions, please consider supporting me on [**Patreon**](https://www.patreon.com/sEbi3). 

--------

## Terms
You can use and edit this code to your liking. Don't ever claim it to be your own code and provide credit if you are using this code for your project. You are not allowed to reupload the exact same code. This means you are now allowed to copy the classes for changing text for translations or dialogs.

Using the code means parts of the source code in order to implement them for your own projects. You are not allowed to reupload the exact same classes without any big changes except for using the code for personal perposes only.

You can find the full license here: https://sebi3.de/EULA
