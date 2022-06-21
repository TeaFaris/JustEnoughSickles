using JustEnoughSickles.NewContent.Items.Materials;
using JustEnoughSickles.NewContent.Items.Offerings;
using JustEnoughSickles.NewContent.Systems.ReaperSystem;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

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
			OfferingBase Offering = null;
			int? Index = null;

			for(int a = 0; a < Player.Player.inventory.Length; a++)
            {
				if (Player.Player.inventory[a].ModItem is OfferingBase)
				{
					Offering = (OfferingBase)Player.Player.inventory[a].ModItem;
					Index = a;
					break;
				}
			}	

			if (Offering == null || Index == null)
				return false;

			if (Player.UsedOfferings.Contains(Offering))
				return false;

			new SoundPlayer().Play(SoundID.Zombie53);
			Player.UsedOfferings.Add(Offering);
			Player.Player.inventory[Index.Value] = Main.item[Player.Player.QuickSpawnItem(new EntitySource_ItemUse(Player.Player, Offering.Item), ModContent.ItemType<Uranium>())];

			return true;
        }
    }
}