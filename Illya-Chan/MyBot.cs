using System;
using System.Linq;
using Discord;
using Discord.Commands;
using System.Xml;
using System.Threading.Tasks;

namespace Illya_Chan
{
    class MyBot
    {
        DiscordClient discord;
        CommandService commands;

        Random rand;

        // Define everything we need for later
        string token;
        char prefix = '.';
        public const string AppName = "Illya-Chan";
        public const string AppUrl = "http://illya-chan.xyz";
        public const string AppVersion = "0.9.2 beta";
        string[] Selfies;
        string[] randomTexts;
        public MyBot()
        {
            rand = new Random();

            // Adds images for the selfie commands
            Selfies = new string[]
            {
                "./selfie/Illya_1.jpg",
                "./selfie/Illya_2.jpg",
                "./selfie/Illya_3.jpg",
                "./selfie/Illya_4.jpg",
                "./selfie/Illya_5.jpg",
                "./selfie/Illya_6.jpg",
                "./selfie/Illya_7.jpg",
                "./selfie/Illya_8.jpg",
                "./selfie/Illya_9.jpg",
                "./selfie/Illya_10.jpg",
                "./selfie/Illya_11.jpg",
                "./selfie/Illya_12.jpg",
                "./selfie/Illya_13.jpg",
                "./selfie/Illya_14.jpg",
                "./selfie/Illya_15.jpg",
                "./selfie/Illya_16.jpg",
                "./selfie/Illya_17.jpg",
                "./selfie/Illya_18.jpg",
                "./selfie/Illya_19.jpg",
                "./selfie/Illya_20.jpg",
                "./selfie/Illya_21.jpg",
                "./selfie/Illya_22.jpg",
                "./selfie/Illya_23.jpg",
                "./selfie/Illya_24.jpg",
                "./selfie/Illya_25.jpg"
            };

            // Adds texts for the randome text command
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
            // Console title
            Console.Title = $"{AppName} (App v{AppVersion}) (Discord.Net v{DiscordConfig.LibVersion})";

            discord = new DiscordClient(x =>
            {
                x.AppName = AppName;
                x.AppUrl = AppUrl;
                x.AppVersion = AppVersion;
                x.ReconnectDelay = 5;
                x.LogLevel = LogSeverity.Info;
                x.LogHandler = Log;
            });

            // Select what prefix to use
            discord.UsingCommands(x =>
            {
                
                x.PrefixChar = prefix;
                x.AllowMentionPrefix = true;
                x.HelpMode = HelpMode.Public;
                
            });

            // Gets the command service
            commands = discord.GetService<CommandService>();

            // Register all commands
            RegisterPingCommand();
            RegisterSelfieCommand();
            RegisterRandomtextCommand();
            RegisterLoveCommand();
            RegisterPurgeCommand();
            RegisterKickCommand();
            RegisterBanCommand();
            RegisterInviteCommand();
            RegisterDisconnectCommand();
            RegisterSetGroup();
            RegisterSayCommand();
            RegisterInfoGroup();
            RegisterLeaveCommand();

            // Reads token from .xml file and puts it in the token string we made earlier
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

            // Logs in to discord with the given token in the string
            discord.ExecuteAndWait(async () =>
            {
                await discord.Connect(token, TokenType.Bot);
                Console.WriteLine($"Current prefix is: [{prefix}]");
                discord.SetGame(new Game($"Icommands [bot v{AppVersion} | Discord.Net v{DiscordConfig.LibVersion}]"));
            });
        }

        // Give all registered commands something to do
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
                    Console.WriteLine("[" + e.User.Name + "] Used the Iselfie command.");
                    int randomSelfieIndex = rand.Next(Selfies.Length);
                    //var fileStream = new FileStream(Selfies[randomSelfieIndex], FileMode.Open);
                    string selfieToPost = Selfies[randomSelfieIndex];
                    //await e.Channel.SendFile(fileStream);
                    await e.Channel.SendFile(selfieToPost);
                });
        }

        private void RegisterRandomtextCommand()
        {
            commands.CreateCommand("randomtext")
                .Alias(new string[] { "randtxt", "rtxt" }) // add alias
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
                .Alias(new string[] { "Do you love me" }) // add alias
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
                .Parameter("number", ParameterType.Unparsed)
                .Alias(new string[] { "prune" })
                .Description("Deletes the number of messages specified.")
                .AddCheck((cm, u, ch) => u.ServerPermissions.ManageMessages)
                .Do(async (e) =>
                {
                    int number = int.Parse(e.Args[0]);
                    Message[] messagesToDelete;
                    messagesToDelete = await e.Channel.DownloadMessages(number + 1);

                    await e.Channel.DeleteMessages(messagesToDelete);
                });
        }

        private void RegisterKickCommand()
        {
            commands.CreateCommand("kick")
                .Parameter("a", ParameterType.Unparsed)
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

        private void RegisterInviteCommand()
        {
            commands.CreateCommand("invite")
                .Alias(new string[] { "inv" }) // add alias
                .Description("Sends an invite link of the bot.")
                .Do(async (e) =>
                {
                    await e.Channel.SendMessage("Bots invite link: https://discordapp.com/oauth2/authorize?&client_id=223467315319013376&scope=bot&permissions=8");
                });
        }

        private void RegisterDisconnectCommand()
        {
            commands.CreateCommand("stop")
                .Do(async (e) =>
                {
                    if (e.User.Id == 93973697643155456)
                    {
                        try
                        {
                            Task sendMsg = e.Channel.SendMessage("Disconnecting now! Cya later senpai :heart:"); // currently not sending the message
                            sendMsg.Wait(100);
                            if (sendMsg.IsCompleted)
                            {
                                Console.WriteLine("Ready to disconnect.");
                                await discord.Disconnect();
                            }
                            else
                            {
                                Console.WriteLine("Timed out before sendMsg completed.");
                            }
                        }
                        catch
                        {
                            await e.Channel.SendMessage("Could not disconnect.");
                        }
                    }
                    else
                    {
                        await e.Channel.SendMessage("This command is for the bot owner only.");
                    }
                    
                });
        }

        private void RegisterSetGroup()
        {
            // Create a group of sub-commands `set`
            commands.CreateGroup("set", (b) =>
            {
                // The command text `set nick <name>`
                b.CreateCommand("nick")
                    .Description("Change your nickname.")
                    .Parameter("myname", ParameterType.Unparsed)
                    .Do(async (e) =>
                    {
                        try
                        {
                            if (e.User.ServerPermissions.ChangeNickname)
                            {
                                string myname = e.Args[0];
                                 var user = e.User;                     // Get the object of the user that executed the command.
                                 await user.Edit(nickname: myname);     // Edit the user's nickname.
                                 await e.Channel.SendMessage($"{user.Mention} I changed your name to **{myname}**");
                            }
                            else
                            {
                                await e.Channel.SendMessage("You do not have permission to change your nickname");
                            }
                        }
                        catch
                        {
                            await e.Channel.SendMessage($"{e.User.Mention} I cannot change your nickname :frowning:");
                        }
                    });

                // The command text `set botnick <name>`
                b.CreateCommand("botnick")
                    .Description("Change the bot's nickname.")
                    .Parameter("name", ParameterType.Unparsed)
                    .Do(async (e) =>
                    {
                        try
                        {
                            if (e.User.ServerPermissions.Administrator)
                            {
                                string name = e.Args[0];
                                var bot = e.Server.CurrentUser;     // Get the bot's user object for this server.
                                await bot.Edit(nickname: name);     // Edit the user's nickname.
                                await e.Channel.SendMessage($"I changed my name to **{name}**");
                            }
                            else
                            {
                                await e.Channel.SendMessage("You need administrator permissions to use this command.");
                            }
                        }
                        catch
                        {
                            await e.Channel.SendMessage("I cannot change my nickname :frowning:");
                        }
                    });

                b.CreateCommand("game")
                   .Alias(new string[] { "newgame" }) // add alias
                   .Description("Set new playing status for the bot.")
                   .Parameter("NewGame", ParameterType.Unparsed)
                   .Do(async (e) =>
                   {
                       if (e.User.Id == 93973697643155456)
                       {
                           string NewGame = e.Args[0];
                           discord.SetGame(new Game(NewGame));
                           await e.Channel.SendMessage($"New playing status as been set to: **{NewGame}**");
                       }
                       else
                       {
                           await e.Channel.SendMessage("This command is only for the bot owner sorry.");
                       }
                   });

                b.CreateCommand("resetgame")
                    .Alias(new string[] { "reset" }) // add alias
                    .Description("Resets the playing status of the bot.")
                    .Do(async (e) =>
                    {
                        if (e.User.Id == 93973697643155456)
                        {
                            string game = $"Icommands [bot v{AppVersion} | Discord.Net v{DiscordConfig.LibVersion}]";
                            discord.SetGame(new Game(game));
                            await e.Channel.SendMessage($"Playing status has been reset to: **{game}**");
                        }
                        else
                        {
                            await e.Channel.SendMessage("This command is only for the bot owner sorry.");
                        }
                    });
            });
        }

        private void RegisterSayCommand()
        {
            commands.CreateCommand("say")
                .Description("Make the bot say something.")
                .Parameter("text", ParameterType.Unparsed)
                .Do(async (e) =>
                {
                    string text = e.Args[0];
                    await e.Channel.SendMessage(text);
                });
        }

        private void RegisterInfoGroup()
        {
            commands.CreateGroup("info", (i) =>
            {
                i.CreateCommand("user")
                    .Description("Show the info of the user.")
                    .Alias(new string[] { "u" })
                    .Do(async (e) =>
                    {
                        var userName = e.User.Name;
                        var userDis = e.User.Discriminator;
                        var userId = e.User.Id;
                        var userOn = e.User.LastOnlineAt;
                        var userJoin = e.User.JoinedAt;
                        var userAva = e.User.AvatarUrl;
                        var lines = $"`NameDiscrim: `**{userName}#{userDis}**\n"+
                                    $"`Id: `**{userId}**\n"+
                                    $"`Last online: `**{userOn}**\n"+
                                    $"`Joined at: `**{userJoin}**\n"+
                                    $"`AvatarUrl: `**{userAva}**";

                            await e.Channel.SendMessage(lines);
                    });
                i.CreateCommand("id")
                    .Description("Show the ID of the user.")
                    .Do(async (e) =>
                    {
                        var userId = e.User.Id;
                        await e.Channel.SendMessage(e.User.Mention + $" Your ID is: **{userId}**");
                    });
                i.CreateCommand("server")
                    .Description("Show the info of the server.")
                    .Alias(new string[] { "s" })
                    .Do(async (e) =>
                    {
                        var serverName = e.Server.Name;
                        var serverOwner = e.Server.Owner;
                        var serverUcount = e.Server.UserCount;
                        var serverIconurl = e.Server.IconUrl;
                        var serverId = e.Server.Id;
                        var lines = $"`Server name: `**{serverName}**\n"+
                                    $"`Owner: `**{serverOwner}**\n"+
                                    $"`Member count: `**{serverUcount}**\n"+
                                    $"`IconUrl: `**{serverIconurl}**\n"+
                                    $"`Server Id: `**{serverId}**\n";

                            await e.Channel.SendMessage(lines);
                    });
            });
        }

        private void RegisterLeaveCommand()
        {
            commands.CreateCommand("leave")
                .Description("Makes Illya leave the server.")
                .Do(async (e) =>
                {
                    if (e.User.ServerPermissions.Administrator)
                    {
                        var serverName = e.Server.Name;

                        Task sendMsg = e.Channel.SendMessage("It's not like I wanted to be here anyways b-baka!"); // currently not sending the message
                        sendMsg.Wait(100);
                        if (sendMsg.IsCompleted)
                        {
                            Console.WriteLine($"succesfully left server: {serverName}");
                            await e.Server.Leave();
                        }
                        else
                        {
                            await e.Channel.SendMessage($"Failed to leave **{serverName}**");
                            Console.WriteLine($"Failed to leave {serverName}");
                        }
                    }
                    else
                    {
                        await e.Channel.SendMessage("This command is for admins only.");
                    }
                });
        }

        // Show info on the console
        private void Log(object sender, LogMessageEventArgs e)
        {
            Console.WriteLine($"[{e.Severity}] [{e.Source}] {e.Message}");
        }
    }
}
