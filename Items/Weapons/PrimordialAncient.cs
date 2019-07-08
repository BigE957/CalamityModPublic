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
	public class PrimordialAncient : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Primordial Ancient");
			Tooltip.SetDefault("An ancient relic from an ancient land\n" +
				"Casts a gigantic blast of dust");
		}

	    public override void SetDefaults()
	    {
	        item.damage = 385;
	        item.magic = true;
	        item.mana = 20;
	        item.width = 28;
	        item.height = 30;
	        item.useTime = 17;
	        item.useAnimation = 17;
	        item.useStyle = 5;
	        item.noMelee = true;
	        item.knockBack = 5;
            item.value = Item.buyPrice(1, 80, 0, 0);
            item.rare = 10;
            item.UseSound = SoundID.Item20;
	        item.autoReuse = true;
	        item.shoot = mod.ProjectileType("Ancient");
	        item.shootSpeed = 8f;
			item.GetGlobalItem<CalamityGlobalItem>(mod).postMoonLordRarity = 14;
		}
	
	    public override void AddRecipes()
	    {
	        ModRecipe recipe = new ModRecipe(mod);
	        recipe.AddIngredient(null, "PrimordialEarth");
	        recipe.AddIngredient(ItemID.AncientBattleArmorMaterial, 10);
	        recipe.AddIngredient(null, "CosmiliteBar", 10);
	        recipe.AddIngredient(null, "Phantoplasm", 5);
	        recipe.AddTile(null, "DraedonsForge");
	        recipe.SetResult(this);
	        recipe.AddRecipe();
	    }
	}
}