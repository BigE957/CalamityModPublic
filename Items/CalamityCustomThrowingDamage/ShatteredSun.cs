using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityMod.Items;

namespace CalamityMod.Items.CalamityCustomThrowingDamage
{
	public class ShatteredSun : CalamityDamageItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Shattered Sun");
			Tooltip.SetDefault("Throws daggers that split twice and explode upon contact");
		}

		public override void SafeSetDefaults()
		{
			item.width = 56;
			item.height = 56;
			item.damage = 40;
			item.crit += 10;
			item.noMelee = true;
			item.noUseGraphic = true;
			item.useAnimation = 11;
			item.useStyle = 1;
			item.useTime = 11;
			item.knockBack = 6f;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.value = Item.buyPrice(1, 20, 0, 0);
			item.rare = 10;
			item.shoot = mod.ProjectileType("ShatteredSun");
			item.shootSpeed = 25f;
			item.GetGlobalItem<CalamityGlobalItem>(mod).rogue = true;
			item.GetGlobalItem<CalamityGlobalItem>(mod).postMoonLordRarity = 12;
		}
		
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
	        recipe.AddIngredient(null, "RadiantStar");
	        recipe.AddIngredient(null, "DivineGeode", 6);
	        recipe.AddTile(TileID.LunarCraftingStation);
	        recipe.SetResult(this);
	        recipe.AddRecipe();
		}
	}
}
