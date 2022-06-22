using JustEnoughSickles.NewContent.Items.Materials;
using JustEnoughSickles.NewContent.Items.Offerings;
using JustEnoughSickles.NewContent.Systems.ReaperSystem;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;
using System.Collections.Generic;

namespace JustEnoughSickles.NewContent.Items.Tiles
{
    public class OfferingAltar : ModTile
    {
        public override void SetStaticDefaults()
        {
			Main.tileFrameImportant[Type] = true;
			Main.tileNoAttach[Type] = true;
			Main.tileLavaDeath[Type] = true;
			TileID.Sets.HasOutlines[Type] = true;
			TileID.Sets.DisableSmartCursor[Type] = true;

			AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTable);

			// Names
			AddMapEntry(new Color(200, 200, 200), Language.GetText("MapObject.Altar"));

			// Placement
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
			TileObjectData.newTile.CoordinateHeights = new[] { 16, 18 };
			TileObjectData.newTile.CoordinatePaddingFix = new Point16(0, 2);
			// The following 3 lines are needed if you decide to add more styles and stack them vertically
			TileObjectData.newTile.StyleWrapLimit = 2;
			TileObjectData.newTile.StyleHorizontal = true;

			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.Direction = TileObjectDirection.PlaceRight;
			TileObjectData.addTile(Type);
		}
        public override void MouseOver(int i, int j)
        {
			Main.instance.MouseText("Make an offering...");
        }
        public override bool RightClick(int i, int j)
        {
			ReaperPlayer Player = Main.LocalPlayer.GetModPlayer<ReaperPlayer>();
			Dictionary<int, Item> Offerings = new Dictionary<int, Item>();

			for(int a = 0; a < Player.Player.inventory.Length; a++)
				if (Player.Player.inventory[a].ModItem is OfferingBase)
					Offerings.Add(a, Player.Player.inventory[a]);

			if (Offerings.Count <= 0)
				return false;

			foreach (int Offering in Offerings.Keys)
				if (Player.UsedOfferings.Any(x => x.Name == Offerings.GetValueOrDefault(Offering).Name))
					Offerings.Remove(Offering);

			new SoundPlayer().Play(SoundID.Zombie53);
			Player.UsedOfferings.AddRange(Offerings.Values);
			foreach(int Offering in Offerings.Keys)
				Player.Player.inventory[Offering] = Main.item[Player.Player.QuickSpawnItem(new EntitySource_ItemUse(Player.Player, Offerings.GetValueOrDefault(Offering)), ItemID.Coal)];

			return true;
        }
    }
}