using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader.Config;

namespace JustEnoughSickles.NewContent.Configs
{
    public class Config : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;

        [Header("Config")]
        [Label("Toggle UI")]
        [Tooltip("Toggle User Interface, or like this bars that's showing you how many souls you have.")]
        [DefaultValue(true)]
        public bool EnableUI;

        [Label("UI Position X")]
        [Tooltip("Um, it's UI position, like coordinate plate, x y geometry.")]
        [Range(0, 1920)]
        [DefaultValue(1920)]
        public int UIXPos;

        [Label("UI Position Y")]
        [Tooltip("Um, it's UI position, like coordinate plate, x y geometry.")]
        [Range(0, 1080)]
        [DefaultValue(1080)]
        public int UIYPos;
    }
}
