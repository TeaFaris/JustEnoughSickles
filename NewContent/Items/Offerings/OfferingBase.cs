using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace JustEnoughSickles.NewContent.Items.Offerings
{
    public abstract class OfferingBase : ModItem
    {
        public uint SoulsToAdd;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Some Offering");
            Tooltip.SetDefault("The... offering?");
        }
        public override void SetDefaults()
        {
            SoulsToAdd = 5;
        }
        public override bool CanUseItem(Player player)
        {
            return false;
        }
    }
}
