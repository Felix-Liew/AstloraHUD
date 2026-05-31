using Rocket.API;

namespace FelixLiew.AstloraHUD
{
    public enum ButtonFunctionType
    {
        Web,
        Command,
        Message,
        UIEffect
    }

    public class AstloraHUDConfiguration : IRocketPluginConfiguration
    {
        public string LogoUrl { get; set; }
        public string PolosaUrl { get; set; }
        public string ServerNameText { get; set; }
        public string ServerDescriptionText { get; set; }
        public string R1Url { get; set; }
        public string R2Url { get; set; }
        public string R3Url { get; set; }
        public string R4Url { get; set; }
        public string L1Url { get; set; }
        public string L2Url { get; set; }
        public string L3Url { get; set; }
        public string L4Url { get; set; }
        public string L5Url { get; set; }

        public ButtonFunctionType R1Type { get; set; }
        public string R1Value { get; set; }
        public string R1Desc { get; set; }

        public ButtonFunctionType R2Type { get; set; }
        public string R2Value { get; set; }
        public string R2Desc { get; set; }

        public ButtonFunctionType R3Type { get; set; }
        public string R3Value { get; set; }
        public string R3Desc { get; set; }

        public ButtonFunctionType R4Type { get; set; }
        public string R4Value { get; set; }
        public string R4Desc { get; set; }

        public ButtonFunctionType L1Type { get; set; }
        public string L1Value { get; set; }
        public string L1Desc { get; set; }

        public ButtonFunctionType L2Type { get; set; }
        public string L2Value { get; set; }
        public string L2Desc { get; set; }

        public ButtonFunctionType L3Type { get; set; }
        public string L3Value { get; set; }
        public string L3Desc { get; set; }

        public ButtonFunctionType L4Type { get; set; }
        public string L4Value { get; set; }
        public string L4Desc { get; set; }

        public ButtonFunctionType L5Type { get; set; }
        public string L5Value { get; set; }
        public string L5Desc { get; set; }

        public void LoadDefaults()
        {
            LogoUrl = "https://i.ibb.co/twLwRstt/astlora.png";
            PolosaUrl = "https://i.ibb.co/CCDBHbV/polosa.png";

            R1Url = "https://i.ibb.co/twLwRstt/astlora.png";
            R2Url = "https://i.ibb.co/ns00Y7Wf/website.png";
            R3Url = "https://i.ibb.co/LXG1Vz3p/forum.png";
            R4Url = "https://i.ibb.co/Fbm6Bwf8/cloud.png";

            L1Url = "https://i.ibb.co/V0GcC3Rw/kit.png";
            L2Url = "https://i.ibb.co/VY7ksxCB/ranking.png";
            L3Url = "https://i.ibb.co/WppbJ60T/go.png";
            L4Url = "https://i.ibb.co/6cR1DRr0/report.png";
            L5Url = "https://i.ibb.co/qFyk9wmk/music.png";

            ServerNameText = "Astlora";
            ServerDescriptionText = "Welcome to Astlora!";

            R1Type = ButtonFunctionType.Web;
            R1Value = "https://discord.gg/bJrDAUVMr";
            R1Desc = "Test";

            R2Type = ButtonFunctionType.Web;
            R2Value = "https://discord.gg/bJrDAUVMr";
            R2Desc = "Test";

            R3Type = ButtonFunctionType.Web;
            R3Value = "https://discord.gg/bJrDAUVMr";
            R3Desc = "Test";

            R4Type = ButtonFunctionType.Web;
            R4Value = "https://discord.gg/bJrDAUVMr";
            R4Desc = "test";

            L1Type = ButtonFunctionType.Command;
            L1Value = "kit new";
            L1Desc = "";

            L2Type = ButtonFunctionType.Command;
            L2Value = "kit test";
            L2Desc = "";

            L3Type = ButtonFunctionType.Command;
            L3Value = "kit astlora";
            L3Desc = "";

            L4Type = ButtonFunctionType.Command;
            L4Value = "kit ammo";
            L4Desc = "";

            L5Type = ButtonFunctionType.Command;
            L5Value = "kit event";
            L5Desc = "";
        }
    }
}