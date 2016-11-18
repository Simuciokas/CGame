using Rocket.API;
using Rocket.Core.Logging;
using Rocket.Core.Plugins;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Events;
using Rocket.Unturned.Player;
using System;
using UnityEngine;
using SDG.Unturned;
using fr34kyn01535.Uconomy;
using Freenex.FeexRanks;
using Rocket.API.Collections;
using Logger = Rocket.Core.Logging.Logger;

namespace Simuciokas.SimuciokasCGames
{
    public class SimuciokasCGames : RocketPlugin<SimuciokasCGamesConfiguration>
    {
        public DateTime? lastmessage = null;
        int gameanswer;
        int rInt;
        bool gameinp;
        public override TranslationList DefaultTranslations
        {
            get
            {
                return new TranslationList(){
                    {"Answered","Player {0} answered correctly!"},
                    {"AnsweredU1","You answered correctly and got {0}$"},
                    {"AnsweredU2","You answered correctly and got {0}$ with {1} point(-s)!"},
                    {"QuestionPart1","What's {0}"},
                    {"QuestionPart2","{0} equal to?"}
                };
            }
        }

        protected override void Load()
        {
            UnturnedPlayerEvents.OnPlayerChatted += Events_PlayerChatted;
            Logger.Log("Successfully loaded CGames v1.0");
            if (Configuration.Instance.Use_Feex_Points == true)
            {
                if (IsDependencyLoaded("FeexRanks"))
                {
                    Logger.Log("CGames successfully loaded with FeexRanks", ConsoleColor.Magenta);
                }
                else
                {
                    Logger.LogWarning("You don't have FeexRanks enabled to use the point system!");
                }
            }
        }
        public void Events_PlayerChatted(UnturnedPlayer player, ref Color color, string message, EChatMode chatMode, ref bool cancel)
        {
            int j;
            int reward_money = 0;
            int reward_points = 0;
            if (Int32.TryParse(message, out j)){
                if (j == gameanswer && gameinp == true)
                {
                    gameanswer = 0;
                    gameinp = false;
                    UnturnedChat.Say(Translations.Instance.Translate("Answered", player.DisplayName));
                    if (player.HasPermission("cgames.extra"))
                    {
                        if (rInt == 1)
                        {
                            if (Configuration.Instance.Custom_Extra_Amount_Enabled == true)
                                reward_money = Configuration.Instance.Extra_Amount_For_Sum;
                            else
                                reward_money = Configuration.Instance.Default_Amount_For_Sum * 2;
                        }
                        else if (rInt == 2)
                        {
                            if (Configuration.Instance.Custom_Extra_Amount_Enabled == true)
                                reward_money = Configuration.Instance.Extra_Amount_For_Minus;
                            else
                                reward_money = Configuration.Instance.Default_Amount_For_Minus * 2;
                        }
                        else if (rInt == 3)
                        {
                            if (Configuration.Instance.Custom_Extra_Amount_Enabled == true)
                                reward_money = Configuration.Instance.Extra_Amount_For_Multiplication;
                            else
                                reward_money = Configuration.Instance.Default_Amount_For_Multiplication * 2;
                        }
                        else if (rInt == 4)
                        {
                            if (Configuration.Instance.Custom_Extra_Amount_Enabled == true)
                                reward_money = Configuration.Instance.Extra_Amount_For_Division;
                            else
                                reward_money = Configuration.Instance.Default_Amount_For_Division * 2;
                        }
                    }
                    else
                    {
                        if (rInt == 1)
                        {
                            reward_money = Configuration.Instance.Default_Amount_For_Sum;
                        }
                        else if (rInt == 2)
                        {
                            reward_money = Configuration.Instance.Default_Amount_For_Minus;
                        }
                        else if (rInt == 3)
                        {
                            reward_money = Configuration.Instance.Default_Amount_For_Multiplication;
                        }
                        else if (rInt == 4)
                        {
                             reward_money = Configuration.Instance.Default_Amount_For_Division;
                        }
                    }
                    if (Configuration.Instance.Use_Feex_Points == true)
                    {
                        if (IsDependencyLoaded("FeexRanks"))
                        {
                            if (rInt == 1)
                            {
                                reward_points = Configuration.Instance.Feex_Points_For_Sum;
                            }
                            else if (rInt == 2)
                            {
                                reward_points = Configuration.Instance.Feex_Points_For_Minus;
                            }
                            else if (rInt == 3)
                            {
                                reward_points = Configuration.Instance.Feex_Points_For_Multiplication;
                            }
                            else if (rInt == 4)
                            {
                                reward_points = Configuration.Instance.Feex_Points_For_Division;
                            }
                            FeexRanks.Instance.UpdatePoints(player, reward_points);
                            UnturnedChat.Say(player, Translations.Instance.Translate("AnsweredU2", reward_money, reward_points));
                        } else
                        {
                            Logger.LogWarning("You don't have FeexRanks enabled to use the point system!");
                        }
                    } else
                    UnturnedChat.Say(player, Translations.Instance.Translate("AnsweredU1", reward_money));
                    Uconomy.Instance.Database.IncreaseBalance(player.CSteamID.ToString(), reward_money);
                }
            }
        }
        protected override void Unload()
        {
            UnturnedPlayerEvents.OnPlayerChatted -= Events_PlayerChatted;
        }

        public void printMessage()
        {
            if (Provider.clients.Count > 0)
            {
                if (State == PluginState.Loaded && (lastmessage == null || ((DateTime.Now - lastmessage.Value).TotalSeconds > Configuration.Instance.Every_NumberOfSeconds)))
                {
                    gameinp = true;
                    System.Random r = new System.Random();
                    int rInt1 = r.Next(1, 101);
                    int rInt2 = r.Next(1, 101);
                    rInt = r.Next(1, 5);
                    string text = "dank";
                    if (rInt == 1)
                    {
                        while (rInt1 + rInt2 <10 && rInt1 + rInt2 > -1)
                        {
                            rInt1 = r.Next(1, 101);
                            rInt2 = r.Next(1, 101);
                        }
                        gameanswer = rInt1 + rInt2;
                        text = Translations.Instance.Translate("QuestionPart1", rInt1) + " + " + Translations.Instance.Translate("QuestionPart2", rInt2);
                    }
                    else if (rInt == 2)
                    {
                        while (rInt1 - rInt2 < 10 && rInt1 - rInt2 > -1)
                        {
                            rInt1 = r.Next(1, 101);
                            rInt2 = r.Next(1, 101);
                        }
                        gameanswer = rInt1 - rInt2;
                        text = Translations.Instance.Translate("QuestionPart1", rInt1) + " - " + Translations.Instance.Translate("QuestionPart2", rInt2);
                    }
                    else if (rInt == 3)
                    {
                        rInt1 = r.Next(-14, 15);
                        rInt2 = r.Next(-14, 15);
                        while (rInt1 * rInt2 < 10 && rInt1 * rInt2 > -1)
                        {
                            rInt1 = r.Next(-14, 15);
                            rInt2 = r.Next(-14, 15);
                        }
                        gameanswer = rInt1 * rInt2;
                        text = Translations.Instance.Translate("QuestionPart1", rInt1) + " x " + Translations.Instance.Translate("QuestionPart2", rInt2);
                    }
                    else if (rInt == 4)
                    {
                        rInt1 = r.Next(-100, 101);
                        rInt2 = 102;
                        while (rInt1 % rInt2 != 0 || (rInt1 / rInt2 < 10 && rInt1 / rInt2 > -1))
                        {
                            rInt2 = r.Next(-10, 11);
                            while (rInt2 == 0)
                            {
                                rInt2 = r.Next(-100, 101);
                            }
                        }
                        gameanswer = rInt1 / rInt2;
                        text = Translations.Instance.Translate("QuestionPart1", rInt1) + " / " + Translations.Instance.Translate("QuestionPart2", rInt2);
                    }
                    UnturnedChat.Say(text);
                    lastmessage = DateTime.Now;
                }
            }
        }
        private void FixedUpdate()
        {
            printMessage();
        }
    }
}
