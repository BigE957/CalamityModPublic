using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityMod.Items;

namespace CalamityMod.Items.Weapons
{
    public class TriactisTruePaladinianMageHammerofMightMelee : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Triactis' True Paladinian Mage-Hammer of Might");
			Tooltip.SetDefault("Explodes on enemy hits");
		}

        public override void SetDefaults()
        {
            item.width = 160;
            item.damage = 10000;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.autoReuse = true;
            item.useAnimation = 10;
            item.useStyle = 1;
            item.useTime = 10;
            item.knockBack = 50f;
            item.UseSound = SoundID.Item1;
            item.melee = true;
            item.height = 160;
            item.value = Item.buyPrice(5, 0, 0, 0);
            item.rare = 10;
            item.shoot = mod.ProjectileType("TriactisOPHammerMelee");
            item.shootSpeed = 25f;
			item.GetGlobalItem<CalamityGlobalItem>(mod).postMoonLordRarity = 16;
		}

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "TruePaladinsHammerMelee");
            recipe.AddIngredient(ItemID.SoulofMight, 30);
            recipe.AddIngredient(null, "ShadowspecBar", 5);
            recipe.AddTile(null, "DraedonsForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
