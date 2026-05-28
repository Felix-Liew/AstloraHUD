using Rocket.Core;
using Rocket.Core.Plugins;
using Rocket.Unturned;
using Rocket.Unturned.Chat; 
using Rocket.Unturned.Player;
using SDG.Unturned;
using System.Collections;
using UnityEngine;

namespace FelixLiew.AstloraHUD
{
    public class AstloraHUDPlugin : RocketPlugin<AstloraHUDConfiguration>
    {
        private const ushort EffectId = 32502;
        private const short EffectKey = 301;
        private Coroutine _uiUpdateCoroutine;

        protected override void Load()
        {
            U.Events.OnPlayerConnected += OnPlayerConnected;
            EffectManager.onEffectButtonClicked += OnEffectButtonClicked;

            _uiUpdateCoroutine = StartCoroutine(UpdateUILoop());

            Rocket.Core.Logging.Logger.Log($"{Name} {System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString(3)} has been loaded!");
        }

        protected override void Unload()
        {
            U.Events.OnPlayerConnected -= OnPlayerConnected;
            EffectManager.onEffectButtonClicked -= OnEffectButtonClicked;

            if (_uiUpdateCoroutine != null)
            {
                StopCoroutine(_uiUpdateCoroutine);
            }

            Rocket.Core.Logging.Logger.Log($"{Name} has been unloaded!");
        }

        private void OnPlayerConnected(UnturnedPlayer player)
        {
            EffectManager.sendUIEffect(EffectId, EffectKey, player.CSteamID, true);

            EffectManager.sendUIEffectImageURL(EffectKey, player.CSteamID, true, "Logo", Configuration.Instance.LogoUrl);
            EffectManager.sendUIEffectImageURL(EffectKey, player.CSteamID, true, "Polosa", Configuration.Instance.PolosaUrl);
            EffectManager.sendUIEffectImageURL(EffectKey, player.CSteamID, true, "R1", Configuration.Instance.R1Url);
            EffectManager.sendUIEffectImageURL(EffectKey, player.CSteamID, true, "R2", Configuration.Instance.R2Url);
            EffectManager.sendUIEffectImageURL(EffectKey, player.CSteamID, true, "R3", Configuration.Instance.R3Url);
            EffectManager.sendUIEffectImageURL(EffectKey, player.CSteamID, true, "R4", Configuration.Instance.R4Url);
            EffectManager.sendUIEffectImageURL(EffectKey, player.CSteamID, true, "L1", Configuration.Instance.L1Url);
            EffectManager.sendUIEffectImageURL(EffectKey, player.CSteamID, true, "L2", Configuration.Instance.L2Url);
            EffectManager.sendUIEffectImageURL(EffectKey, player.CSteamID, true, "L3", Configuration.Instance.L3Url);
            EffectManager.sendUIEffectImageURL(EffectKey, player.CSteamID, true, "L4", Configuration.Instance.L4Url);
            EffectManager.sendUIEffectImageURL(EffectKey, player.CSteamID, true, "L5", Configuration.Instance.L5Url);

            EffectManager.sendUIEffectText(EffectKey, player.CSteamID, true, "Name", Configuration.Instance.ServerNameText);
            EffectManager.sendUIEffectText(EffectKey, player.CSteamID, true, "Description", Configuration.Instance.ServerDescriptionText);
        }

        private IEnumerator UpdateUILoop()
        {
            while (true)
            {
                yield return new WaitForSeconds(1f);

                float timeFraction = (float)LightingManager.time / (float)LightingManager.cycle;
                int hours = (int)(timeFraction * 24);
                int minutes = (int)((timeFraction * 24 - hours) * 60);

                string gameTimeText = $"{hours:00}:{minutes:00}";
                string playersText = $"{Provider.clients.Count}/{Provider.maxPlayers}";

                foreach (SteamPlayer steamPlayer in Provider.clients)
                {
                    EffectManager.sendUIEffectText(EffectKey, steamPlayer.transportConnection, true, "Players", playersText);
                    EffectManager.sendUIEffectText(EffectKey, steamPlayer.transportConnection, true, "Time", gameTimeText);
                }
            }
        }
        private void OnEffectButtonClicked(Player player, string buttonName)
        {
            UnturnedPlayer uPlayer = UnturnedPlayer.FromPlayer(player);
            if (uPlayer == null) return;

            switch (buttonName)
            {
                case "R1": ExecuteButtonAction(uPlayer, Configuration.Instance.R1Type, Configuration.Instance.R1Value, Configuration.Instance.R1Desc); break;
                case "R2": ExecuteButtonAction(uPlayer, Configuration.Instance.R2Type, Configuration.Instance.R2Value, Configuration.Instance.R2Desc); break;
                case "R3": ExecuteButtonAction(uPlayer, Configuration.Instance.R3Type, Configuration.Instance.R3Value, Configuration.Instance.R3Desc); break;
                case "R4": ExecuteButtonAction(uPlayer, Configuration.Instance.R4Type, Configuration.Instance.R4Value, Configuration.Instance.R4Desc); break;
                case "L1": ExecuteButtonAction(uPlayer, Configuration.Instance.L1Type, Configuration.Instance.L1Value, Configuration.Instance.L1Desc); break;
                case "L2": ExecuteButtonAction(uPlayer, Configuration.Instance.L2Type, Configuration.Instance.L2Value, Configuration.Instance.L2Desc); break;
                case "L3": ExecuteButtonAction(uPlayer, Configuration.Instance.L3Type, Configuration.Instance.L3Value, Configuration.Instance.L3Desc); break;
                case "L4": ExecuteButtonAction(uPlayer, Configuration.Instance.L4Type, Configuration.Instance.L4Value, Configuration.Instance.L4Desc); break;
                case "L5": ExecuteButtonAction(uPlayer, Configuration.Instance.L5Type, Configuration.Instance.L5Value, Configuration.Instance.L5Desc); break;
            }
        }
        private void ExecuteButtonAction(UnturnedPlayer player, ButtonFunctionType type, string value, string desc)
        {
            if (string.IsNullOrEmpty(value)) return;

            switch (type)
            {
                case ButtonFunctionType.Web:
                    player.Player.sendBrowserRequest(desc, value);
                    break;

                case ButtonFunctionType.Command:
                    R.Commands.Execute(player, value);
                    break;

                case ButtonFunctionType.Message:
                    UnturnedChat.Say(player, value, Color.green);
                    break;
                case ButtonFunctionType.UIEffect:
        
                    if (ushort.TryParse(value, out ushort uiId))
                    {         
                        EffectManager.sendUIEffect(uiId, (short)uiId, player.CSteamID, true);
                    }
                    break;
            }
        }
    }
}