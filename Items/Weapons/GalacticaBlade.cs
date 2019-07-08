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
	public class GalacticaBlade : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Galactus Blade");
			Tooltip.SetDefault("Forged with the fury of nuclear chaos\n" +
				"Launches a barrage of comets from the sky");
		}

		public override void SetDefaults()
		{
			item.width = 76;
			item.damage = 95;
			item.melee = true;
			item.useAnimation = 17;
			item.useStyle = 1;
			item.useTime = 17;
			item.useTurn = true;
			item.knockBack = 6f;
			item.UseSound = SoundID.Item105;
			item.autoReuse = true;
			item.height = 68;
            item.value = Item.buyPrice(1, 20, 0, 0);
            item.rare = 10;
            item.shoot = mod.ProjectileType("GalacticaComet");
			item.shootSpeed = 23f;
			item.GetGlobalItem<CalamityGlobalItem>(mod).postMoonLordRarity = 12;
		}
	
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "GalacticaSingularity", 5);
			recipe.AddIngredient(ItemID.StarWrath);
			recipe.AddIngredient(ItemID.Ectoplasm, 10);
			recipe.AddIngredient(ItemID.SoulofMight, 20);
			recipe.AddIngredient(ItemID.LunarBar, 5);
			recipe.AddIngredient(ItemID.DarkShard);
			recipe.AddIngredient(ItemID.LightShard);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
		
		public override bool Shoot(Player player, ref Microsoft.Xna.Framework.Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
	    {
			float num72 = item.shootSpeed;
	    	Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
	    	float num78 = (float)Main.mouseX + Main.screenPosition.X - vector2.X;
			float num79 = (float)Main.mouseY + Main.screenPosition.Y - vector2.Y;
			if (player.gravDir == -1f)
			{
				num79 = Main.screenPosition.Y + (float)Main.screenHeight - (float)Main.mouseY - vector2.Y;
			}
			float num80 = (float)Math.Sqrt((double)(num78 * num78 + num79 * num79));
			float num81 = num80;
			if ((float.IsNaN(num78) && float.IsNaN(num79)) || (num78 == 0f && num79 == 0f))
			{
				num78 = (float)player.direction;
				num79 = 0f;
				num80 = num72;
			}
			else
			{
				num80 = num72 / num80;
			}
	    	num78 *= num80;
			num79 *= num80;
			int num107 = 5;
			for (int num108 = 0; num108 < num107; num108++)
			{
				vector2 = new Vector2(player.position.X + (float)player.width * 0.5f + (float)(Main.rand.Next(201) * -(float)player.direction) + ((float)Main.mouseX + Main.screenPosition.X - player.position.X), player.MountedCenter.Y - 600f);
				vector2.X = (vector2.X + player.Center.X) / 2f + (float)Main.rand.Next(-200, 201);
				vector2.Y -= (float)(100 * num108);
				num78 = (float)Main.mouseX + Main.screenPosition.X - vector2.X;
				num79 = (float)Main.mouseY + Main.screenPosition.Y - vector2.Y;
				if (num79 < 0f)
				{
					num79 *= -1f;
				}
				if (num79 < 20f)
				{
					num79 = 20f;
				}
				num80 = (float)Math.Sqrt((double)(num78 * num78 + num79 * num79));
				num80 = num72 / num80;
				num78 *= num80;
				num79 *= num80;
				float speedX4 = num78 + (float)Main.rand.Next(-100, 101) * 0.02f;
				float speedY5 = num79 + (float)Main.rand.Next(-100, 101) * 0.02f;
				int projectile = Projectile.NewProjectile(vector2.X, vector2.Y, speedX4, speedY5, mod.ProjectileType("GalacticaComet"), damage, knockBack, player.whoAmI, 0f, (float)Main.rand.Next(10));
			}
			return false;
		}
	
	    public override void MeleeEffects(Player player, Rectangle hitbox)
	    {
	        if (Main.rand.Next(4) == 0)
	        {
	        	int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, (Main.rand.Next(2) == 0 ? 164 : 229));
	        }
	    }
	    
	    public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{
			target.AddBuff(BuffID.OnFire, 600);
			target.AddBuff(BuffID.Frostburn, 600);
		}
	}
}
