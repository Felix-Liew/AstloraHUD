using Rocket.API;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using System.Collections.Generic;
using UnityEngine;

namespace FelixLiew.AstloraHUD
{
    public class CommandAUI : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Player;
        public string Name => "aui";
        public string Help => "Turn on or off the Astlora HUD panel.";
        public string Syntax => "<on/off>";
        public List<string> Aliases => new List<string>();
        public List<string> Permissions => new List<string> { "astlora.aui" };

        public void Execute(IRocketPlayer caller, string[] command)
        {
            UnturnedPlayer player = (UnturnedPlayer)caller;
            ulong steamId = player.CSteamID.m_SteamID;

            if (command.Length != 1)
            {
                UnturnedChat.Say(caller, AstloraHUDPlugin.Instance.Translate("command_aui_usage"), Color.red);
                return;
            }

            string arg = command[0].ToLower();

            if (arg == "on")
            {
                if (AstloraHUDPlugin.Instance.HiddenHUDPlayers.Contains(steamId))
                {
                    AstloraHUDPlugin.Instance.HiddenHUDPlayers.Remove(steamId);
                }

                AstloraHUDPlugin.Instance.ShowHUD(player);
                UnturnedChat.Say(caller, AstloraHUDPlugin.Instance.Translate("command_aui_on"), Color.green);
            }
            else if (arg == "off")
            {
                if (!AstloraHUDPlugin.Instance.HiddenHUDPlayers.Contains(steamId))
                {
                    AstloraHUDPlugin.Instance.HiddenHUDPlayers.Add(steamId);
                }

                AstloraHUDPlugin.Instance.HideHUD(player);
                UnturnedChat.Say(caller, AstloraHUDPlugin.Instance.Translate("command_aui_off"), Color.yellow);
            }
            else
            {
                UnturnedChat.Say(caller, AstloraHUDPlugin.Instance.Translate("command_aui_usage"), Color.red);
            }
        }
    }
}