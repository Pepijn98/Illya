using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace Illya_Chan
{
    class MyBot
    {
        DiscordClient discord;
        CommandService commands;

        Random rand;

        string[] Selfies;
        string[] randomTexts;
        public MyBot()
        {
            rand = new Random();

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

            randomTexts = new string[]
            {
                "SirZechs is a poopyhead LUUL",
                "Ohh baby fuck me in the ass",
                "Senpai desu~"
            };

            discord = new DiscordClient(x =>
            {
                x.LogLevel = LogSeverity.Info;
                x.LogHandler = Log;
            });

            discord.UsingCommands(x =>
            {
                x.PrefixChar = '-';
                x.AllowMentionPrefix = true;
            });

            commands = discord.GetService<CommandService>();

            commands.CreateCommand("ban")
                .Parameter("a", ParameterType.Unparsed)
                    .Alias(new string[] { "b" }) //add alias
                    .Do(async (e) =>
                    {

                        try
                        {
                            Role Elders = e.Server.FindRoles("Elders").FirstOrDefault();
                            Role Judge = e.Server.FindRoles("Judge").FirstOrDefault();
                            if (e.User.HasRole(Elders))
                            {
                                await e.Channel.SendMessage(e.GetArg("a"));
                                if (e.Message.MentionedUsers.FirstOrDefault() == null)
                                {
                                    await e.Channel.SendMessage(e.User.Mention + " That's not a valid user!");
                                }
                                else
                                {
                                    try
                                    {
                                        await e.Server.Ban(e.Message.MentionedUsers.FirstOrDefault());
                                        await e.Channel.SendMessage(e.GetArg("Ban") + " was banned!");
                                    }
                                    catch (Exception p)
                                    {
                                        await e.Channel.SendMessage(e.User.Mention + " I do not have permission to ban that user!");
                                    }
                                }
                            }
                            else if (e.User.HasRole(Judge))
                            {
                                await e.Channel.SendMessage(e.GetArg("a"));
                                if (e.Message.MentionedUsers.FirstOrDefault() == null)
                                {
                                    await e.Channel.SendMessage(e.User.Mention + " That's not a valid user!");
                                }
                                else
                                {
                                    try
                                    {
                                        await e.Server.Ban(e.Message.MentionedUsers.FirstOrDefault());
                                        await e.Channel.SendMessage(e.GetArg("Ban") + " was banned!");
                                    }
                                    catch (Exception p)
                                    {
                                        await e.Channel.SendMessage(e.User.Mention + " I do not have permission to ban that user!");
                                    }
                                }
                            }
                            else
                            {
                                await e.Channel.SendMessage(e.User.Mention + " You're not a Judge+!");
                            }
                        }
                        catch
                        {
                            await e.Channel.SendMessage("This server does not have the `Elders` role! Please add it for this command to work");
                        }
                    });

            commands.CreateCommand("kick")
                .Parameter("a", ParameterType.Unparsed)
                    .Alias(new string[] { "k" }) //add alias
                    .Do(async (e) =>
                    {
                        try
                        {
                            Role Elders = e.Server.FindRoles("Elders").FirstOrDefault();
                            Role Judge = e.Server.FindRoles("Judge").FirstOrDefault();
                            Role Veteran = e.Server.FindRoles("Veteran").FirstOrDefault();
                            if (e.User.HasRole(Elders))
                            {
                                await e.Channel.SendMessage(e.GetArg("a"));
                                if (e.Message.MentionedUsers.FirstOrDefault() == null)
                                {
                                    await e.Channel.SendMessage(e.User.Mention + " That's not a valid user!");
                                }
                                else
                                {
                                    try
                                    {
                                        await e.Message.MentionedUsers.FirstOrDefault().Kick();
                                        await e.Channel.SendMessage(e.GetArg("Kick") + " was kicked!");
                                    }
                                    catch (Exception p)
                                    {
                                        await e.Channel.SendMessage(e.User.Mention + " I do not have permission to kick that user!");
                                    }
                                }
                            }
                            else if (e.User.HasRole(Judge))
                            {
                                await e.Channel.SendMessage(e.GetArg("a"));
                                if (e.Message.MentionedUsers.FirstOrDefault() == null)
                                {
                                    await e.Channel.SendMessage(e.User.Mention + " That's not a valid user!");
                                }
                                else
                                {
                                    try
                                    {
                                        await e.Message.MentionedUsers.FirstOrDefault().Kick();
                                        await e.Channel.SendMessage(e.GetArg("Kick") + " was kicked!");
                                    }
                                    catch (Exception p)
                                    {
                                        await e.Channel.SendMessage(e.User.Mention + " I do not have permission to kick that user!");
                                    }
                                }
                            }
                            else if (e.User.HasRole(Veteran))
                            {
                                await e.Channel.SendMessage(e.GetArg("a"));
                                if (e.Message.MentionedUsers.FirstOrDefault() == null)
                                {
                                    await e.Channel.SendMessage(e.User.Mention + " That's not a valid user!");
                                }
                                else
                                {
                                    try
                                    {
                                        await e.Message.MentionedUsers.FirstOrDefault().Kick();
                                        await e.Channel.SendMessage(e.GetArg("Kick") + " was kicked!");
                                    }
                                    catch (Exception p)
                                    {
                                        await e.Channel.SendMessage(e.User.Mention + " I do not have permission to kick that user!");
                                    }
                                }
                            }
                            else
                            {
                                await e.Channel.SendMessage(e.User.Mention + " You're not a Veteran+!");
                            }
                        }
                        catch
                        {
                            await e.Channel.SendMessage("This server does not have the `Elders` role! Please add it for this command to work");
                        }
                    });

            commands.CreateCommand("Do you love me?")
                    .Do(async (e) =>
                    {
                        try
                        {
                            Role Senpai = e.Server.FindRoles("Senpai").FirstOrDefault();
                            if (e.User.HasRole(Senpai))
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
                            await e.Channel.SendMessage("Who are you!? What is this!? Where am I!? Why am I here!????");
                        }
                    });

        commands.CreateCommand("ping")
                .Do(async (e) =>
                {
                    await e.Channel.SendMessage("pong!");
                });

        commands.CreateCommand("*Gives you a hug*")
                    .Alias(new string[] { "*Hugs*" }) //add alias
                    .Alias(new string[] { "*Hugs you*" }) //add alias
                    .Do(async (e) =>
                    {
                        try
                        {
                            Role Senpai = e.Server.FindRoles("Senpai").FirstOrDefault();
                            if (e.User.HasRole(Senpai))
                            {
                                await e.Channel.SendMessage("Ohh pls Senpai stop it >///<");
                            }
                            else
                            {
                                await e.Channel.SendMessage(e.User.Mention + " Only senpai can hug me, leave me alone...");
                            }
                        }
                        catch
                        {
                            await e.Channel.SendMessage("Aaaah a stranger is hugging me!! SENPAI HELP!! (This server does not have the `Senpai` role)");
                        }
                    });

            RegisterSelfieCommand();
            RegisterRandomtextCommand();
            RegisterPurgeCommand();
            RegisterTestCommand();

            discord.ExecuteAndWait(async () =>
            {
                await discord.Connect("Bot MjIzNDY3MzE1MzE5MDEzMzc2.CrNzWw.tOtNQi3m4LWd0MqV_1-IWh92ipE");
            });
        }

        private void RegisterSelfieCommand()
        {
            commands.CreateCommand("selfie")
                .Do(async (e) =>
                {
                    int randomSelfieIndex = rand.Next(Selfies.Length);
                    string selfieToPost = Selfies[randomSelfieIndex];
                    await e.Channel.SendFile(selfieToPost);
                });
        }

        private void RegisterRandomtextCommand()
        {
            commands.CreateCommand("text")
                .Do(async (e) =>
                {
                    int randomTextIndex = rand.Next(randomTexts.Length);
                    string textToPost = randomTexts[randomTextIndex];
                    await e.Channel.SendMessage(textToPost);
                });
        }

        private void RegisterPurgeCommand()
        {
            commands.CreateCommand("purge")
                .Alias(new string[] { "prg" }) //add alias
                .Do(async (e) =>
                {
                    try
                    {
                        Role Elders = e.Server.FindRoles("Elders").FirstOrDefault();
                        Role Judge = e.Server.FindRoles("Judge").FirstOrDefault();
                        Role Veteran = e.Server.FindRoles("Veteran").FirstOrDefault();
                        Role Learning_Veteran = e.Server.FindRoles("Learning_Veteran").FirstOrDefault();
                        if (e.User.HasRole(Elders))
                        {
                            Message[] messagesToDelete;
                            messagesToDelete = await e.Channel.DownloadMessages(5);

                            await e.Channel.DeleteMessages(messagesToDelete);
                        }
                        else if (e.User.HasRole(Judge))
                        {
                            Message[] messagesToDelete;
                            messagesToDelete = await e.Channel.DownloadMessages(5);

                            await e.Channel.DeleteMessages(messagesToDelete);
                        }
                        else if (e.User.HasRole(Veteran))
                        {
                            Message[] messagesToDelete;
                            messagesToDelete = await e.Channel.DownloadMessages(5);

                            await e.Channel.DeleteMessages(messagesToDelete);
                        }
                        else if (e.User.HasRole(Learning_Veteran))
                        {
                            Message[] messagesToDelete;
                            messagesToDelete = await e.Channel.DownloadMessages(5);

                            await e.Channel.DeleteMessages(messagesToDelete);
                        }
                        else
                        {
                            await e.Channel.SendMessage(e.User.Mention + " You're not a Learning_Veteran+!");
                        }
                    }
                    catch
                    {
                        await e.Channel.SendMessage("This server does not have the `Elders` role! Please add it for this command to work");
                    }
                });
        }

        private void RegisterTestCommand()
        {
            commands.CreateCommand("Test")
                .Do(async (e) =>
                {
                    await e.Channel.SendMessage(e.User.Mention + " What dafuq do you want!?!?");
                });
        }

        private void Log(object sender, LogMessageEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
