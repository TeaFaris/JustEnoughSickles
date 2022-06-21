using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace JustEnoughSickles.NewContent.Items.Materials
{
    public class Uranium : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Uranium");
            Tooltip.SetDefault("That's uranium bro. Don't touch it.");
        }
        public override void UpdateInventory(Player player)
        {
            base.UpdateInventory(player);

            player.Hurt(Terraria.DataStructures.PlayerDeathReason.ByCustomReason($"{player.name} was so bad at following directions; It's incradeble he wasn't died years ago."), 10, 1);
        }
    }
}
