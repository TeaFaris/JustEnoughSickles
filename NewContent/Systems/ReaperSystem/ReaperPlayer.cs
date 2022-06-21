using JustEnoughSickles.NewContent.NPCs.Souls;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace JustEnoughSickles.NewContent.Systems.ReaperSystem
{
    public class ReaperPlayer : ModPlayer
    {
        public uint MaxSoulSize = 5;
        public float PickupRange = 5;
        public int[] Souls = new int[Enum.GetValues(typeof(SoulType)).Length];
        public virtual void ModifySoul (int Count, SoulType Type)
        {
            SoundPlayer Sound = new SoundPlayer();

            if ( (Souls[(int)Type] >= MaxSoulSize) || (Souls[(int)Type] + Count < 0) )
            {
                Sound.Play(SoundID.MaxMana);
                return;
            }
            
            Sound.Play(SoundID.Zombie53);
            Souls[(int)Type] += Count;
        }
        public override void OnRespawn(Player player)
        {
            Souls = new int[Enum.GetValues(typeof(SoulType)).Length];
        }
    }
}
