﻿using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityMod;
using CalamityMod.NPCs;

namespace CalamityMod.Buffs
{
	public class TranquilityCandle : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Tranquility Candle");
			Description.SetDefault("Spawn rates around the candle are reduced!");
			Main.buffNoTimeDisplay[Type] = true;
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = false;
			longerExpertDebuff = false;
		}
		
		public override void Update(Player player, ref int buffIndex)
		{
			player.GetModPlayer<CalamityPlayer>(mod).tranquilityCandle = true;
		}
	}
}