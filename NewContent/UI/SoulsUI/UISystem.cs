using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace JustEnoughSickles.NewContent.UI.SoulsUI
{
    public class UISystem : ModSystem
    {
        public UserInterface StaticUI;
        public SoulsUI SoulsUI;
        public UserInterface DynamicUI;
        public SoulBarsUI SoulBarsUI;
        public override void Load()
        {
            SoulsUI = new SoulsUI();
            SoulsUI.Activate();
            StaticUI = new UserInterface();
            StaticUI.SetState(SoulsUI);
            SoulBarsUI = new SoulBarsUI();
            SoulBarsUI.Activate();
            DynamicUI = new UserInterface();
            DynamicUI.SetState(SoulBarsUI);
        }
        public override void UpdateUI(GameTime gameTime)
        {
            if (Main.gameMenu)
                return;

            StaticUI.Update(gameTime);
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            layers.Insert(0, new LegacyGameInterfaceLayer("Dynamic UI", DrawDynamicUI, InterfaceScaleType.Game));
            layers.Insert(1, new LegacyGameInterfaceLayer("Static UI", DrawStaticUI, InterfaceScaleType.Game));
        }

        private bool DrawStaticUI()
        {
            if (Main.gameMenu || !ModContent.GetInstance<Configs.Config>().EnableUI)
                return true;
            if (SoulBarsUI.SoulBars[0].Top != ModContent.GetInstance<Configs.Config>().UIYPos || SoulBarsUI.SoulBars[0].Left != ModContent.GetInstance<Configs.Config>().UIXPos)
                ModContent.GetInstance<UISystem>().SoulBarsUI.Init();

            StaticUI.Draw(Main.spriteBatch, new GameTime());
            return true;
        }
        private bool DrawDynamicUI()
        {
            if (Main.gameMenu || !ModContent.GetInstance<Configs.Config>().EnableUI)
                return true;
            if (SoulsUI.Top.Percent != ModContent.GetInstance<Configs.Config>().UIYPos / 1920f || SoulsUI.Left.Percent != ModContent.GetInstance<Configs.Config>().UIXPos / 1920f)
                ModContent.GetInstance<UISystem>().SoulsUI.Init();

            DynamicUI.Draw(Main.spriteBatch, new GameTime());
            return true;
        }
    }
}
