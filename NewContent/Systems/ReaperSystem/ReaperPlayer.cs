using JustEnoughSickles.NewContent.Items.Offerings;
using JustEnoughSickles.NewContent.NPCs.Souls;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace JustEnoughSickles.NewContent.Systems.ReaperSystem
{
    public class ReaperPlayer : ModPlayer
    {
        public uint MaxSoulSize { get
            {
                uint MaxSize = 5;
                UsedOfferings ??= new List<OfferingBase>();
                    
                UsedOfferings.ForEach(x => MaxSize += x.SoulsToAdd);
                return MaxSize;
            } 
        }
        public float PickupRange = 5;
        public uint[] Souls = new uint[Enum.GetValues(typeof(SoulType)).Length];
        public List<OfferingBase> UsedOfferings;
        public virtual void ModifySoul (uint Count, SoulType Type)
        {
            SoundPlayer SP = new SoundPlayer();
            if ( (Souls[(int)Type] >= MaxSoulSize) || (Souls[(int)Type] + Count < 0) )
            {
                SP.Play(SoundID.MaxMana);
                return;
            }

            SP.Play(SoundID.Zombie53);
            Souls[(int)Type] += Count;
        }
        public override void OnRespawn(Player player)
        {
            Souls = new uint[Enum.GetValues(typeof(SoulType)).Length];
        }
        public override void SaveData(TagCompound tag)
        {
            tag["UsedOfferings"] = UsedOfferings;
        }
        public override void LoadData(TagCompound tag)
        {
            UsedOfferings = tag.Get<List<OfferingBase>>("UsedOfferings") ?? new List<OfferingBase>();
        }
    }
}
