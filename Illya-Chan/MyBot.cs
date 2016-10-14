using System;
using System.Linq;
using Discord;
using Discord.Commands;
using System.Xml;

namespace Illya_Chan
{
    class MyBot
    {
        DiscordClient discord;
        CommandService commands;

        Random rand;

        //Define everything we need for later
        string cmds;
        string token;
        public const string AppName = "Illya-Chan";
        public const string AppUrl = "http://illya-chan.xyz";
        public const string AppVersion = "0.7.1 beta";
        string[] Selfies;
        string[] randomTexts;
        public MyBot()
        {
            rand = new Random();

            //Adds images for the selfie commands
            Selfies = new string[]
            {
                "selfie/Illya_1.jpg",
                "selfie/Illya_2.jpg",
                "selfie/Illya_3.jpg",
                "selfie/Illya_4.jpg",
                "selfie/Illya_5.jpg",
                "selfie/Illya_6.jpg",
                "selfie/Illya_7.jpg",
                "selfie/Illya_8.jpg",
                "selfie/Illya_9.jpg",
                "selfie/Illya_10.jpg",
                "selfie/Illya_11.jpg",
                "selfie/Illya_12.jpg",
                "selfie/Illya_13.jpg",
                "selfie/Illya_14.jpg",
                "selfie/Illya_15.jpg",
                "selfie/Illya_16.jpg",
                "selfie/Illya_17.jpg",
                "selfie/Illya_18.jpg",
                "selfie/Illya_19.jpg",
                "selfie/Illya_20.jpg",
                "selfie/Illya_21.jpg",
                "selfie/Illya_22.jpg",
                "selfie/Illya_23.jpg",
                "selfie/Illya_24.jpg",
                "selfie/Illya_25.jpg"
            };

            //Adds texts for the randome text command
            randomTexts = new string[]
            {
                "would you like your eggs scrambled or fried?",
                "I am jelly ;-;",
                "Carpet.",
                "I hid the body 👍",
                "UNICORNS POOPED IN MY BED!",
                "whatever you do, don't turn off the light tonight!",
                "Do you have a pickle?",
                "Sorry, I'm to busy giving my unicorn a bath.",
                "Go to the bathroom and lock the door if u hear anything run!!",
                "I'm pregnant, I think you're the dad."
            };

            //Console title
            Console.Title = $"{AppName} (App v{AppVersion}) (Discord.Net v{DiscordConfig.LibVersion})";

            discord = new DiscordClient(x =>
            {
                x.AppName = AppName;
                x.AppUrl = AppUrl;
                x.AppVersion = AppVersion;
                x.LogLevel = LogSeverity.Info;
                x.LogHandler = Log;
            });

            //Select what prefix to use
            discord.UsingCommands(x =>
            {
                x.PrefixChar = '_';
                x.AllowMentionPrefix = true;
                x.HelpMode = HelpMode.Public;
            });

            //Gets the command service
            commands = discord.GetService<CommandService>();
            
            //Register all commands
            RegisterPingCommand();
            RegisterSelfieCommand();
            RegisterRandomtextCommand();
            RegisterLoveCommand();
            RegisterPurgeCommand();
            RegisterKickCommand();
            RegisterBanCommand();
            //RegisterRestartCommand();
            RegisterCommandsCommand();
            RegisterInviteCommand();
            RegisterGameCommand();
            RegisterResetGameCommand();

            //Reads token from .xml file and puts it in the token string we made earlier
            using (XmlReader reader = XmlReader.Create("token.xml"))
            {
                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        reader.ReadToFollowing("token");
                        token = reader.ReadInnerXml();
                    }
                }
            }

            //Reads the commmand list from .xml file
            using (XmlReader reader = XmlReader.Create("commands.xml"))
            {
                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        reader.ReadToFollowing("cmds");
                        cmds = reader.ReadInnerXml();
                    }
                }
            }

            //Logs in to discord with the given token in the string
            discord.ExecuteAndWait(async () =>
            {
                await discord.Connect(token, TokenType.Bot);
                discord.SetGame(new Game($"_commands [bot v{AppVersion} | Discord.Net v{DiscordConfig.LibVersion}]"));
            });
        }


        //Give all registered commands something to do
        private void RegisterPingCommand()
        {
            commands.CreateCommand("ping")
                .Description("Bot reacts with `pong!` to check if it's working.")
                .Do(async (e) =>
                {
                    await e.Channel.SendMessage("pong!");
                });
        }

        private void RegisterSelfieCommand()
        {
            commands.CreateCommand("selfie")
                .Description("Send a cute selfie.")
                .Do(async (e) =>
                {
                    int randomSelfieIndex = rand.Next(Selfies.Length);
                    string selfieToPost = Selfies[randomSelfieIndex];
                    await e.Channel.SendFile(selfieToPost);
                });
        }

        private void RegisterRandomtextCommand()
        {
            commands.CreateCommand("randomtext")
                .Alias(new string[] { "rt" }) //add alias
                .Description("Post a random line of text.")
                .Do(async (e) =>
                {
                    int randomTextIndex = rand.Next(randomTexts.Length);
                    string textToPost = randomTexts[randomTextIndex];
                    await e.Channel.SendMessage(textToPost);
                });
        }

        private void RegisterLoveCommand()
        {
            commands.CreateCommand("Do you love me?")
                .Alias(new string[] { "Do you love me" }) //add alias
                .Description("Reacts with an answer.")
                .Do(async (e) =>
                {
                    try
                    {
                        if (e.User.Id == 93973697643155456)
                        {
                            await e.Channel.SendMessage("Of course Senpai!");
                        }
                        else
                        {
                            await e.Channel.SendMessage(e.User.Mention + " NO! I only love my Senpai!");
                        }
                    }
                    catch
                    {
                        await e.Channel.SendMessage("Who are you!? What is this!? Where am I!? Why am I here!??");
                    }
               });
        }

        private void RegisterPurgeCommand()
        {
            commands.CreateCommand("purge")
                .Alias(new string[] { "prune", "clr" }) //add alias
                .Description("Deletes 50 messages.")
                .AddCheck((cm, u, ch) => u.ServerPermissions.ManageMessages)
                .Do(async (e) =>
                {
                    Message[] messagesToDelete;
                    messagesToDelete = await e.Channel.DownloadMessages(50);

                    await e.Channel.DeleteMessages(messagesToDelete);
                });
        }

        private void RegisterKickCommand()
        {
            commands.CreateCommand("kick")
                .Parameter("a", ParameterType.Unparsed)
                .Alias(new string[] { "k" }) //add alias
                .Description("Kick a user")
                .AddCheck((cm, u, ch) => u.ServerPermissions.KickMembers)
                .Do(async (e) =>
                {
                    await e.Channel.SendMessage(e.GetArg("a"));
                    if (e.Message.MentionedUsers.Count() < 1)
                    {
                        await e.Channel.SendMessage(e.User.Mention + " That's not a valid user!");
                    }
                    else
                    {
                        try
                        {
                            await e.Message.MentionedUsers.FirstOrDefault().Kick();
                            await e.Channel.SendMessage(e.Message.MentionedUsers.FirstOrDefault() + " was kicked!");
                        }
                        catch
                        {
                            await e.Channel.SendMessage(e.User.Mention + " I do not have permission to kick that user!");
                        }
                    }
                });
        }

        private void RegisterBanCommand()
        {
            commands.CreateCommand("ban")
                .Parameter("a", ParameterType.Unparsed)
                .Alias(new string[] { "b" }) //add alias
                .Description("Ban a user.")
                .AddCheck((cm, u, ch) => u.ServerPermissions.BanMembers)
                .Do(async (e) =>
                {
                    await e.Channel.SendMessage(e.GetArg("a"));
                    if (e.Message.MentionedUsers.Count() < 1)
                    {
                        await e.Channel.SendMessage(e.User.Mention + " That's not a valid user!");
                    }
                    else
                    {
                        try
                        {
                            await e.Server.Ban(e.Message.MentionedUsers.FirstOrDefault());
                            await e.Channel.SendMessage(e.Message.MentionedUsers.FirstOrDefault() + " was banned!");
                        }
                        catch
                        {
                            await e.Channel.SendMessage(e.User.Mention + " I do not have permission to ban that user!");
                        }
                    }
                });
        }

        private void RegisterCommandsCommand()
        {
            commands.CreateCommand("commands")
                .Alias(new string[] { "cmds" }) //add alias
                .Description("Show all the commands the bot can do.")
                .Do(async (e) =>
                {
                    await e.Channel.SendMessage(cmds);
                });
        }

        private void RegisterGameCommand()
        {
            commands.CreateCommand("setgame")
                .Alias(new string[] { "set", "newgame" }) //add alias
                .Description("Set new playing status for the bot.")
                .Parameter("NewGame", ParameterType.Unparsed)
                .Do(async (e) =>
                {
                    if (e.User.Id == 93973697643155456)
                    {
                        discord.SetGame(new Game(e.GetArg("NewGame")));
                        await e.Channel.SendMessage("New playing status as been set!");
                    }
                    else
                    {
                        await e.Channel.SendMessage("This command is only for the bot owner sorry.");
                    }
                });
        }

        private void RegisterResetGameCommand()
        {
            commands.CreateCommand("resetgame")
                .Alias(new string[] { "reset" }) //add alias
                .Description("Resets the playing status of the bot.")
                .Do(async (e) =>
                {
                    if (e.User.Id == 93973697643155456)
                    {
                        discord.SetGame(new Game($"_commands [bot v{AppVersion} | Discord.Net v{DiscordConfig.LibVersion}]"));
                        await e.Channel.SendMessage("Playing status has been reset!");
                    }
                    else
                    {
                        await e.Channel.SendMessage("This command is only for the bot owner sorry.");
                    }
                });
        }

        private void RegisterInviteCommand()
        {
            commands.CreateCommand("invite")
                .Alias(new string[] { "inv" }) //add alias
                .Description("Sends an invite link of the bot.")
                .Do(async (e) =>
                {
                    await e.Channel.SendMessage("Bots invite link: https://discordapp.com/oauth2/authorize?&client_id=223467315319013376&scope=bot&permissions=00000008");
                });
        }

        //Show info on the console
        private void Log(object sender, LogMessageEventArgs e)
        {
            Console.WriteLine($"[{e.Severity}] [{e.Source}] {e.Message}");
        }
    }
}
