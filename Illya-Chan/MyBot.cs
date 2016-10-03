using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.Audio;
using System.Xml;

namespace Illya_Chan
{
    class MyBot
    {
        DiscordClient discord;
        CommandService commands;

        Random rand;

        //Makes token string for later and 2 strings for some random commands
        public string token;
        public Game CurrentGame { get; private set; }
        public const string AppName = "Illya-Chan (bot)";
        public const string AppUrl = "http://haremkingonline.xyz";
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
                "Sorry. I'm to busy giving my unicorn a bath.",
                "Go to the bathroom and lock the door if u hear anything run!!",
                "I'm pregnant, I think you're the dad."
            };

            Console.Title = $"{AppName} (Discord.Net v{DiscordConfig.LibVersion})";

            discord = new DiscordClient(x =>
            {
                x.AppName = AppName;
                x.AppUrl = AppUrl;
                x.AppVersion = "0.6";
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

            //Registers all commands
            RegisterPingCommand();
            RegisterSelfieCommand();
            RegisterRandomtextCommand();
            RegisterLoveCommand();
            RegisterPurgeCommand();
            RegisterKickCommand();
            RegisterBanCommand();

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

            //Logs in to discord with the given token in the string
            discord.ExecuteAndWait(async () =>
            {
                await discord.Connect(token);
            });
        }



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
                                Role Owner = e.Server.FindRoles("Owner").FirstOrDefault();
                                if (e.User.HasRole(Owner))
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

    private void Log(object sender, LogMessageEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
