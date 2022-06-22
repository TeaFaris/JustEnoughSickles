using JustEnoughSickles.NewContent.Items.Offerings;
using JustEnoughSickles.NewContent.NPCs.Souls;
using System;
using System.Collections.Generic;
using System.Linq;
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
                UsedOfferings ??= new List<Item>();
                    
                UsedOfferings.ForEach(x => MaxSize += ((OfferingBase)x.ModItem).SoulsToAdd);
                return MaxSize;
            } 
        }
        public float PickupRange = 5;
        public uint[] Souls = new uint[Enum.GetValues(typeof(SoulType)).Length];
        public List<Item> UsedOfferings;
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
            if(UsedOfferings != null)
                tag.Add("UsedOfferings", UsedOfferings.Select(ItemIO.Save).ToList());
        }
        public override void LoadData(TagCompound tag)
        {
            UsedOfferings = tag.GetList<TagCompound>("UsedOfferings").Select(ItemIO.Load).ToList();
        }
    }
}
