﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityMod.Items.CalamityCustomThrowingDamage;

namespace CalamityMod.Items.Accessories
{
    public class DarkSunRing : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dark Sun Ring");
            Tooltip.SetDefault("10% increase to damage, minion knockback, and melee speed\n" +
				"+1 life regen, 30% increased pick speed, and +2 max minions\n" +
                "During the day the player has +2 life regen\n" +
                "During the night the player has +20 defense\n" +
                "Contains the power of the dark sun");
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(6, 6));
        }

        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 26;
            item.value = Item.buyPrice(0, 90, 0, 0);
			item.defense = 10;
			item.lifeRegen = 1;
            item.accessory = true;
			item.GetGlobalItem<CalamityGlobalItem>(mod).postMoonLordRarity = 15;
		}

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            CalamityPlayer modPlayer = player.GetModPlayer<CalamityPlayer>(mod);
            modPlayer.darkSunRing = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "UeliaceBar", 10);
            recipe.AddIngredient(null, "DarksunFragment", 100);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}