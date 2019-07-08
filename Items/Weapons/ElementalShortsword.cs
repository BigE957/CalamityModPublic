using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons 
{
	public class ElementalShortsword : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Elemental Shiv");
			Tooltip.SetDefault("Don't underestimate the power of shivs\n" +
				"Shoots a rainbow orb that spawns shivs at multiple locations");
		}

		public override void SetDefaults()
		{
			item.useStyle = 3;
			item.useTurn = false;
			item.useAnimation = 10;
			item.useTime = 10;
			item.width = 44;
			item.height = 44;
			item.damage = 140;
			item.melee = true;
			item.knockBack = 8.5f;
			item.UseSound = SoundID.Item1;
			item.useTurn = true;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("ElementBallShiv");
			item.shootSpeed = 14f;
            item.value = Item.buyPrice(1, 20, 0, 0);
            item.rare = 10;
			item.GetGlobalItem<CalamityGlobalItem>(mod).postMoonLordRarity = 12;
		}

		public override bool Shoot(Player player, ref Microsoft.Xna.Framework.Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Projectile.NewProjectile(position.X, position.Y, item.shootSpeed * player.direction, 0f, type, damage, knockBack, player.whoAmI, 0f, 0f);
			return false;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "TerraShiv");
			recipe.AddIngredient(null, "GalacticaSingularity", 5);
			recipe.AddIngredient(ItemID.LunarBar, 5);
	        recipe.AddTile(TileID.LunarCraftingStation);	
	        recipe.SetResult(this);
	        recipe.AddRecipe();
		}
	
	    public override void MeleeEffects(Player player, Rectangle hitbox)
	    {
	        if (Main.rand.Next(5) == 0)
			{
				int num250 = Dust.NewDust(new Vector2((float)hitbox.X, (float)hitbox.Y), hitbox.Width, hitbox.Height, 66, (float)(player.direction * 2), 0f, 150, new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB), 1.3f);
				Main.dust[num250].velocity *= 0.2f;
				Main.dust[num250].noGravity = true;
			}
	    }
	    
	    public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{
	    	target.AddBuff(mod.BuffType("HolyLight"), 120);
	    	target.AddBuff(mod.BuffType("GlacialState"), 120);
	    	target.AddBuff(mod.BuffType("BrimstoneFlames"), 120);
	    	target.AddBuff(mod.BuffType("Plague"), 120);
		}
	}
}
