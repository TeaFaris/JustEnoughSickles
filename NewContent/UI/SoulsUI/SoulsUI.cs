using JustEnoughSickles.NewContent.NPCs.Souls;
using JustEnoughSickles.NewContent.Systems.ReaperSystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;
namespace JustEnoughSickles.NewContent.UI.SoulsUI
{
	public class SoulsUI : UIState
	{
		UIImage SoulCounter;
		public override void OnInitialize()
		{
			Init();
		}
		public void Init()
        {
			if (SoulCounter != null)
				SoulCounter.Remove();
			SoulCounter = new UIImage(ModContent.Request<Texture2D>("JustEnoughSickles/NewContent/UI/SoulsUI/UI_Souls"));
			SoulCounter.Top.Set(0f, ModContent.GetInstance<Configs.Config>().UIYPos / 1920f);
			SoulCounter.Left.Set(0f, ModContent.GetInstance<Configs.Config>().UIXPos / 1920f);
			SoulCounter.Width.Set(30 / Main.PendingResolutionWidth, 0f);
			SoulCounter.Height.Set(139.5f / Main.PendingResolutionHeight, 0f);
			Append(SoulCounter);
		}
    }
	public class SoulBarsUI : UIState
    {
		public SoulBar[] SoulBars;
		public override void OnInitialize()
        {
			Init();
		}
		public override void Draw(SpriteBatch spriteBatch)
		{
			if (!ModContent.GetInstance<Configs.Config>().EnableUI)
				return;
			for (int i = 0; i < SoulBars.Length; i++)
				Append(SoulBars[i].Update(i));
			Recalculate();
			base.Draw(spriteBatch);
		}
		public void Init()
        {
			if (SoulBars != null)
				new List<SoulBar>(SoulBars).ForEach(x => x.Image.Remove());
			SoulBars = new SoulBar[Enum.GetValues<SoulType>().Length];
			Player Player = Main.LocalPlayer;
			for (int i = 0; i < SoulBars.Length; i++)
				SoulBars[i] = new SoulBar(ModContent.GetInstance<Configs.Config>().UIYPos, ModContent.GetInstance<Configs.Config>().UIXPos, Player.whoAmI, (SoulType)i);
		}
	}
	public class SoulBar : UIElement 
	{
		private int Player;
		private SoulType Type;
		private Asset<Texture2D> Texture;
		public float Top, Left;
		public UIImageFramed Image { get; set; }
		private new float? Height => SoulsCount / (float)ReaperPlayer.MaxSoulSize * Texture.Value.Height;
		private float ActualHeight = 0;
		private float Counter;
		private ReaperPlayer ReaperPlayer => Main.player[Player].GetModPlayer<ReaperPlayer>();
		public uint? SoulsCount
        {
			get
            {
				if (ReaperPlayer.Souls == null)
					return null;
				return ReaperPlayer.Souls[(int)Type];
			}
        }
		public SoulBar(float TopPixel, float LeftPixel, int Player, SoulType Type)
        {
			Texture = ModContent.Request<Texture2D>($"JustEnoughSickles/NewContent/UI/SoulsUI/UI_{Type}_Bar");
			this.Type = Type;
			this.Player = Player;
			Top = TopPixel + 38f;
			Left = LeftPixel + 6f;
        }
		public UIImageFramed Update(int i)
        {
			if (Image != null)
				Image.Remove();
			if (SoulsCount == null) 
				return null;

			float Offset = 0;
			for(Offset = 0; -Math.Pow(Offset, 2) + Height.Value >= 0; Offset += 0.01f) {  }

			if (ActualHeight < Height && Height > 0 && ActualHeight >= 0)
				ActualHeight += (float)-Math.Pow(Counter - Offset, 2) + Height.Value + 1f;
			else if (ActualHeight < 0 || Height == 0)
			{
				ActualHeight = 0;
				Counter = 0;
			}
			else
				Counter = 0;
				
			Image = new UIImageFramed(Texture, new Rectangle(0, 0, Texture.Value.Width, (int)ActualHeight));
			Image.Top.Set(0f, Top / 1920f + 1920f / Main.PendingResolutionHeight * 5f / 1920f);
			Image.Left.Set(0f, Left / 1920f + 1920f / Main.PendingResolutionWidth * 33f / 1920f * i);
			Image.Width.Set(0f, (float)Texture.Value.Width / Main.PendingResolutionWidth);
			Image.Height.Set(0f, (float)Texture.Value.Height / Main.PendingResolutionHeight);

			if (Main.mouseX / (float)Main.PendingResolutionWidth > (Left / 1920f + 1920f / Main.PendingResolutionWidth * 37f / 1920f * i) + 122f / 1920f
				&& Main.mouseX / (float)Main.PendingResolutionWidth < (Left / 1920f + 1920f / Main.PendingResolutionWidth * 37f / 1920f * i + Image.Width.Percent) + 122f / 1920f
				&& Main.mouseY / (float)Main.PendingResolutionHeight > Top / 1920f + 1920f / Main.PendingResolutionHeight * 5f / 1920f
				&& Main.mouseY / (float)Main.PendingResolutionHeight < Top / 1920f + 1920f / Main.PendingResolutionHeight * 5f / 1920f + Image.Height.Percent)
				Main.instance.MouseText($"{SoulsCount} / {ReaperPlayer.MaxSoulSize}");

			Counter += 0.01f;
			return Image;
		}
    }
}
