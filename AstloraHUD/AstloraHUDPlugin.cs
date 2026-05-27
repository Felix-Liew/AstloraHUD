using Rocket.Core;
using Rocket.Core.Logging;
using Rocket.Core.Plugins;
using Rocket.Unturned;
using Rocket.Unturned.Player;
using SDG.Unturned;
using System.Timers;

namespace FelixLiew.AstloraHUD
{
    public class AstloraHUDPlugin : RocketPlugin<AstloraHUDConfiguration>
    {
        private const ushort EffectId = 32502;
        private const short EffectKey = 301;
        private Timer timer;
        protected override void Load()
        {
            U.Events.OnPlayerConnected += OnPlayerConnected;
            U.Events.OnPlayerDisconnected += OnPlayerDisconnected;
            EffectManager.onEffectButtonClicked += OnEffectButtonClicked;
            Logger.Log($"{Name} {Assembly.GetName().Version.ToString(3)} has been loaded!");

            timer = new Timer(60000);
            timer.Elapsed += (sender, args) =>
            {
                UpdateTimeUI();
            };
            timer.Start();
        }
        protected override void Unload()
        {
            U.Events.OnPlayerConnected -= OnPlayerConnected;
            U.Events.OnPlayerDisconnected -= OnPlayerDisconnected;
            EffectManager.onEffectButtonClicked -= OnEffectButtonClicked;
            timer?.Stop();
            timer?.Dispose();
            Logger.Log($"{Name} has been unloaded!");
        }

        private void OnPlayerConnected(UnturnedPlayer player)
        {
            EffectManager.sendUIEffect(EffectId, EffectKey, player.CSteamID, true);
            this.UpdatePlayersUI();
            this.UpdateTimeUI();
        }
        private void OnPlayerDisconnected(UnturnedPlayer player)
        {
            this.UpdatePlayersUI();
        }
        private void UpdatePlayersUI()
        {
            foreach (SteamPlayer steamPlayer in Provider.clients)
            {
                EffectManager.sendUIEffectText(EffectKey, steamPlayer.transportConnection, true, "Players", string.Format("{0}/{1}", Provider.clients.Count, Provider.maxPlayers));
            }
        }

        private void UpdateTimeUI()
        {
            uint time = LightingManager.time;

            string formattedTime = $"{time / 100:D2}:{time % 100:D2}";

            foreach (SteamPlayer steamPlayer in Provider.clients)
            {
                EffectManager.sendUIEffectText(
                    EffectKey,
                    steamPlayer.transportConnection,
                    true,
                    "Time",
                    formattedTime
                );
            }
        }
        private void OnEffectButtonClicked(Player player, string buttonName)
        {
            switch (buttonName)
            {
                case "R1":
                    R1(player);
                    break;

                case "R2":
                    R2(player);
                    break;

                case "R3":
                    R3(player);
                    break;
                case "R4":
                    R4(player);
                    break;

                case "L1":
                    L1(player);
                    break;
                case "L2":
                    L2(player);
                    break;
                case "L3":
                    L3(player);
                    break;
                case "L4":
                    L4(player);
                    break;
                case "L5":
                    L5(player);
                    break;
            }
        }

        private void R1(Player player)
        {
            player.sendBrowserRequest("欢迎访问星洛拉官网!", "https://rp.astlora.cn");
        }

        private void R2(Player player)
        {
            player.sendBrowserRequest("欢迎加入我们的社群!", "https://qm.qq.com/q/5TQ0da0gXC");
        }

        private void R3(Player player)
        {
            player.sendBrowserRequest("欢迎加入我们的论坛!", "https://un.astlora.cn");
        }
        private void R4(Player player)
        {
            player.sendBrowserRequest("欢迎访问星洛云官网!", "https://astlora.cn");
        }
        private void L1(Player player)
        {
            UnturnedPlayer uPlayer = UnturnedPlayer.FromPlayer(player);
            R.Commands.Execute(uPlayer, "akit");
        }
        private void L2(Player player)
        {
            UnturnedPlayer uPlayer = UnturnedPlayer.FromPlayer(player);
            R.Commands.Execute(uPlayer, "go");
        }
        private void L3(Player player)
        {
            UnturnedPlayer uPlayer = UnturnedPlayer.FromPlayer(player);
            R.Commands.Execute(uPlayer, "ranking");
        }
        private void L4(Player player)
        {
            player.sendBrowserRequest("星落拉·纯净工艺 支持中心!", "https://rp.astlora.cn/submitticket");
        }
        private void L5(Player player)
        {
            UnturnedPlayer uPlayer = UnturnedPlayer.FromPlayer(player);
            R.Commands.Execute(uPlayer, "music");
        }

    }
}