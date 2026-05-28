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
            // The URL used to load the image
            LogoUrl = "https://astlora.cn/logo.png";
            PolosaUrl = "https://astlora.cn/polosa.png";
            // R1-R4 are the four top-right buttons from left to right.
            R1Url = "https://astlora.cn/r1.png";
            R2Url = "https://astlora.cn/r2.png";
            R3Url = "https://astlora.cn/r3.png";
            R4Url = "https://astlora.cn/r4.png";
            // L1-L5 represent the five buttons on the left side, ordered from top to bottom.
            L1Url = "https://astlora.cn/l1.png";
            L2Url = "https://astlora.cn/l2.png";
            L3Url = "https://astlora.cn/l3.png";
            L4Url = "https://astlora.cn/l4.png";
            L5Url = "https://astlora.cn/l5.png";

            ServerNameText = "Astlora";
            ServerDescriptionText = "Welcome to Astlora!";

            // Freely choose between Web, Command, Message, or UIEffect.
            R1Type = ButtonFunctionType.Web;
            R1Value = "https://rp.astlora.cn";
            R1Desc = "欢迎访问星洛拉官网!";

            R2Type = ButtonFunctionType.Web;
            R2Value = "https://qm.qq.com/q/5TQ0da0gXC";
            R2Desc = "欢迎加入我们的社群!";

            R3Type = ButtonFunctionType.Web;
            R3Value = "https://un.astlora.cn";
            R3Desc = "欢迎加入我们的论坛!";

            R4Type = ButtonFunctionType.Web;
            R4Value = "https://astlora.cn";
            R4Desc = "欢迎访问星洛云官网!";

            L1Type = ButtonFunctionType.Command;
            L1Value = "kit new";
            L1Desc = "";

            L2Type = ButtonFunctionType.Command;
            L2Value = "go";
            L2Desc = "";

            L3Type = ButtonFunctionType.Command;
            L3Value = "ranking";
            L3Desc = "";

            L4Type = ButtonFunctionType.Web;
            L4Value = "https://rp.astlora.cn/submitticket";
            L4Desc = "星落拉·纯净工艺 支持中心!";

            L5Type = ButtonFunctionType.UIEffect;
            L5Value = "32201";
            L5Desc = "";
        }
    }
}