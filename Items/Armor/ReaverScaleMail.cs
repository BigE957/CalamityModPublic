﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityMod.Items.Armor;
using CalamityMod.Items.CalamityCustomThrowingDamage;

namespace CalamityMod.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    public class ReaverScaleMail : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Reaver Scale Mail");
            Tooltip.SetDefault("9% increased damage and 4% increased critical strike chance\n" +
                "+20 max life");
        }

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;
			item.value = Item.buyPrice(0, 24, 0, 0);
			item.rare = 7;
            item.defense = 19;
        }

        public override void UpdateEquip(Player player)
        {
            player.statLifeMax2 += 20;
            player.allDamage += 0.09f;
            player.GetModPlayer<CalamityPlayer>().AllCritBoost(4);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "DraedonBar", 15);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
