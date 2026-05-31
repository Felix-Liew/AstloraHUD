using Rocket.API.Collections;
using Rocket.Core;
using Rocket.Core.Plugins;
using Rocket.Unturned;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using SDG.Unturned;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FelixLiew.AstloraHUD
{
    public class AstloraHUDPlugin : RocketPlugin<AstloraHUDConfiguration>
    {
        public static AstloraHUDPlugin Instance { get; private set; }

        public List<ulong> HiddenHUDPlayers { get; private set; } = new List<ulong>();

        public const ushort EffectId = 32502;
        private const short EffectKey = 301;
        private Coroutine _uiUpdateCoroutine;

        protected override void Load()
        {
            Instance = this;
            U.Events.OnPlayerConnected += OnPlayerConnected;
            U.Events.OnPlayerDisconnected += OnPlayerDisconnected;
            EffectManager.onEffectButtonClicked += OnEffectButtonClicked;

            _uiUpdateCoroutine = StartCoroutine(UpdateUILoop());

            string version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString(3);
            Rocket.Core.Logging.Logger.Log("=========================================");
            Rocket.Core.Logging.Logger.Log($"[Astlora] HUD System Loaded Successfully");
            Rocket.Core.Logging.Logger.Log($"[Astlora] Version : {version}");
            Rocket.Core.Logging.Logger.Log($"[Astlora] Support : Discord -> felix_liew");
            Rocket.Core.Logging.Logger.Log("=========================================");
        }

        protected override void Unload()
        {
            U.Events.OnPlayerConnected -= OnPlayerConnected;
            U.Events.OnPlayerDisconnected -= OnPlayerDisconnected;
            EffectManager.onEffectButtonClicked -= OnEffectButtonClicked;

            if (_uiUpdateCoroutine != null)
            {
                StopCoroutine(_uiUpdateCoroutine);
            }

            Instance = null;
            Rocket.Core.Logging.Logger.LogError("=========================================");
            Rocket.Core.Logging.Logger.LogError($"[Astlora] HUD System unLoaded Successfully!");
            Rocket.Core.Logging.Logger.LogError($"[Astlora] Please check configurations or");
            Rocket.Core.Logging.Logger.LogError($"[Astlora] contact Developer via Discord: felix_liew");
            Rocket.Core.Logging.Logger.LogError("=========================================");
        }

        private void OnPlayerConnected(UnturnedPlayer player)
        {
            ShowHUD(player);
        }

        private void OnPlayerDisconnected(UnturnedPlayer player)
        {
            if (HiddenHUDPlayers.Contains(player.CSteamID.m_SteamID))
            {
                HiddenHUDPlayers.Remove(player.CSteamID.m_SteamID);
            }
        }

        public void ShowHUD(UnturnedPlayer player)
        {
            StartCoroutine(SendPlayerUIWithDelay(player));
        }

        public void HideHUD(UnturnedPlayer player)
        {
            EffectManager.askEffectClearByID(EffectId, player.Player.channel.owner.transportConnection);
        }

        private IEnumerator SendPlayerUIWithDelay(UnturnedPlayer player)
        {
            yield return new WaitForSeconds(1.5f);

            if (player == null || player.Player == null) yield break;

            var connection = player.Player.channel.owner.transportConnection;

            EffectManager.sendUIEffect(EffectId, EffectKey, connection, true);

            yield return new WaitForSeconds(0.2f);

            EffectManager.sendUIEffectImageURL(EffectKey, connection, true, "Logo", Configuration.Instance.LogoUrl, true, true);
            EffectManager.sendUIEffectImageURL(EffectKey, connection, true, "Polosa", Configuration.Instance.PolosaUrl, true, true);
            EffectManager.sendUIEffectImageURL(EffectKey, connection, true, "R1", Configuration.Instance.R1Url, true, true);
            EffectManager.sendUIEffectImageURL(EffectKey, connection, true, "R2", Configuration.Instance.R2Url, true, true);
            EffectManager.sendUIEffectImageURL(EffectKey, connection, true, "R3", Configuration.Instance.R3Url, true, true);
            EffectManager.sendUIEffectImageURL(EffectKey, connection, true, "R4", Configuration.Instance.R4Url, true, true);
            EffectManager.sendUIEffectImageURL(EffectKey, connection, true, "L1", Configuration.Instance.L1Url, true, true);
            EffectManager.sendUIEffectImageURL(EffectKey, connection, true, "L2", Configuration.Instance.L2Url, true, true);
            EffectManager.sendUIEffectImageURL(EffectKey, connection, true, "L3", Configuration.Instance.L3Url, true, true);
            EffectManager.sendUIEffectImageURL(EffectKey, connection, true, "L4", Configuration.Instance.L4Url, true, true);
            EffectManager.sendUIEffectImageURL(EffectKey, connection, true, "L5", Configuration.Instance.L5Url, true, true);

            EffectManager.sendUIEffectText(EffectKey, connection, true, "Name", Configuration.Instance.ServerNameText);
            EffectManager.sendUIEffectText(EffectKey, connection, true, "Description", Configuration.Instance.ServerDescriptionText);
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
                    if (HiddenHUDPlayers.Contains(steamPlayer.playerID.steamID.m_SteamID))
                        continue;

                    EffectManager.sendUIEffectText(EffectKey, steamPlayer.transportConnection, true, "Players", playersText);
                    EffectManager.sendUIEffectText(EffectKey, steamPlayer.transportConnection, true, "Time", gameTimeText);
                }
            }
        }
        public override TranslationList DefaultTranslations => new TranslationList
        {
            { "command_aui_usage", "Correct usage: /aui on or /aui off" },
            { "command_aui_on", "HUD panel has been enabled!" },
            { "command_aui_off", "HUD panel has been disabled!" }
        };

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
                        EffectManager.sendUIEffect(uiId, (short)uiId, player.Player.channel.owner.transportConnection, true);
                    }
                    break;
            }
        }
    }
}