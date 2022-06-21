using JustEnoughSickles.NewContent.NPCs.Souls;
using JustEnoughSickles.NewContent.Systems.ReaperSystem;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace JustEnoughSickles.NewContent.Items.Weapons.Sickles
{
    public abstract class SickleBase : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Le Sickle");
            Tooltip.SetDefault("This is a new content sickle.");
        }
        public override void SetDefaults()
        {
            Item.useStyle = ItemUseStyleID.Swing;
			Item.autoReuse = false;
			Item.DamageType = ModContent.GetInstance<Reaper>();
			Item.UseSound = SoundID.Item71;

            Item.crit = 8;
            Item.width = 40;
            Item.height = 40;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.knockBack = 7;
            Item.ArmorPenetration = (int)(Item.damage / 2f);
        }
        
        public override void OnHitNPC (Player player, NPC target, int damage, float knockBack, bool crit)
        {
            if (target.aiStyle == NPCAIStyleID.Passive ||
                target.aiStyle == NPCAIStyleID.Firefly ||
                target.aiStyle == NPCAIStyleID.Butterfly ||
                target.aiStyle == NPCAIStyleID.Dragonfly ||
                target.aiStyle == NPCAIStyleID.Slime ||
                target.aiStyle == NPCAIStyleID.CritterWorm ||
                target.aiStyle == NPCAIStyleID.Bird ||
                target.aiStyle == NPCAIStyleID.Balloon ||
                target.aiStyle == NPCAIStyleID.Snail ||
                target.aiStyle == NPCAIStyleID.TeslaTurret ||
                target.aiStyle == NPCAIStyleID.Ladybug ||
                target.aiStyle == NPCAIStyleID.WaterStrider ||
                target.aiStyle == NPCAIStyleID.Piranha ||
                target.aiStyle == NPCAIStyleID.Spell ||
                target.aiStyle == 0)
                return;
            if ( (target.life > 0 && !target.boss) || (target.boss && !crit) || (!crit && new Random().Next(0, 3) > 0) )
                return;
            
            WeightedRandom<int> Rand = new WeightedRandom<int>();
            Rand.Add(ModContent.NPCType<Frost>());
            Rand.Add(ModContent.NPCType<Inferno>());
            Rand.Add(ModContent.NPCType<Light>());
            Rand.Add(ModContent.NPCType<Shadow>());
            // It's govnocode time
            ((SoulMob)Main.npc[NPC.NewNPC(NPC.GetSource_NaturalSpawn(), (int)target.Top.X, (int)target.Top.Y, Rand.Get())].ModNPC).SetTarget(player);
        }
    }
}
