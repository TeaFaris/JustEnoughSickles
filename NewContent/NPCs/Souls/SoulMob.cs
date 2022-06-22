using JustEnoughSickles.NewContent.Systems.ReaperSystem;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ModLoader;

namespace JustEnoughSickles.NewContent.NPCs.Souls
{
    public abstract class SoulMob : HandledNPC
    {
        public SoulType Type;
        public Color MainColor;
        public float MaxVelocity;
        public override void SetDefaults()
        {
            NPC.width = 22;
            NPC.height = 22;
            NPC.damage = 1;
            NPC.lifeMax = 1;
            NPC.immortal = true;
            NPC.aiStyle = -1;
            NPC.knockBackResist = float.MinValue;
            MaxVelocity = 4f;
            Target = null;
        }
        public override void AI()
        {
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            if (Target == null)
                return;
            NPC.target = Target.whoAmI;
            Vector2 TargetPos = Target.position;

            // Dust
            Dust Dust = Main.dust[Dust.NewDust(NPC.position, NPC.width, NPC.height, 267, NPC.velocity.X, NPC.velocity.Y, newColor: MainColor)];
            Dust.noGravity = true;
            Dust.velocity *= 0.1f;

            if (Math.Sqrt(Math.Pow(TargetPos.X - NPC.position.X, 2) + Math.Pow(TargetPos.Y - NPC.position.Y, 2)) > Target.GetModPlayer<ReaperPlayer>().PickupRange * 16)
            {
                NPC.velocity = Vector2.Zero;
                return;
            }
               

            if (TargetPos.X < NPC.position.X && NPC.velocity.X > -MaxVelocity)
                NPC.velocity.X -= MaxVelocity / 10f;
            if (TargetPos.X > NPC.position.X && NPC.velocity.X < MaxVelocity)
                NPC.velocity.X += MaxVelocity / 10f;
            if (TargetPos.Y < NPC.position.Y && NPC.velocity.Y > -MaxVelocity)
                NPC.velocity.Y -= MaxVelocity / 10f;
            if (TargetPos.Y > NPC.position.Y && NPC.velocity.Y < MaxVelocity)
                NPC.velocity.Y += MaxVelocity / 10f;
        }
        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter += 0.05000000596046448;
            NPC.frameCounter %= Main.npcFrameCount[NPC.type];
            NPC.frame.Y = (int)NPC.frameCounter * frameHeight;
        }
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 4;
        }
        public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
        {
            target.immune = true;
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.GetModPlayer<ReaperPlayer>().ModifySoul(1, Type);
            NPC.immortal = false;
            NPC.life = -1;
            target.immune = false;
        }
        public override bool? CanHitNPC(NPC target)
        {
            return false;
        }
    }
    public enum SoulType
    {
        Inferno,
        Frost,
        Shadow,
        Light
    }
}
