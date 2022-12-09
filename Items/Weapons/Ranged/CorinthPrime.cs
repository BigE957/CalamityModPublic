﻿using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Ranged;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Ranged
{
    public class CorinthPrime : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Corinth Prime");
            Tooltip.SetDefault(@"Shoots a tight burst of 6 high velocity bullets
Converts musket balls into high velocity explosive bullets
Right click to fire a grenade
The grenade emits a massive explosion if it doesn't touch anything for 2 seconds
Otherwise, the grenade emits an extremely weak explosion");
            SacrificeTotal = 1;
            ItemID.Sets.ItemsThatAllowRepeatedRightClick[Item.type] = true;
        }

        public override void SetDefaults()
        {
            Item.damage = 100;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 106;
            Item.height = 42;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 8f;
            Item.value = CalamityGlobalItem.Rarity12BuyPrice;
            Item.rare = ModContent.RarityType<Turquoise>();
            Item.UseSound = SoundID.Item38;
            Item.autoReuse = true;
            Item.shoot = ProjectileID.PurificationPowder;
            Item.shootSpeed = 12f;
            Item.useAmmo = AmmoID.Bullet;
            Item.shoot = ModContent.ProjectileType<RealmRavagerBullet>();
            Item.Calamity().canFirePointBlankShots = true;
        }

        public override Vector2? HoldoutOffset() => new Vector2(-20, 5);

        public override bool AltFunctionUse(Player player) => true;

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                Item.useTime = 90;
                Item.useAnimation = 90;
                Item.UseSound = SoundID.Item61;
                Item.shootSpeed = 8f;
            }
            else
            {
                Item.useTime = 30;
                Item.useAnimation = 30;
                Item.UseSound = SoundID.Item38;
                Item.shootSpeed = 12f;
            }
            return base.CanUseItem(player);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.altFunctionUse == 2)
            {
                Projectile.NewProjectile(source, position + Vector2.Normalize(velocity) * 60f, velocity, ModContent.ProjectileType<CorinthPrimeAirburstGrenade>(), damage, knockback, player.whoAmI);
            }
            else
            {
                int numBullets = 6;
                for (int index = 0; index < numBullets; index++)
                {
                    float SpeedX = velocity.X + Main.rand.Next(-35, 36) * 0.05f;
                    float SpeedY = velocity.Y + Main.rand.Next(-35, 36) * 0.05f;
                    int proj = Projectile.NewProjectile(source, position.X, position.Y, SpeedX, SpeedY, type == ProjectileID.Bullet ? ModContent.ProjectileType<RealmRavagerBullet>() : type, damage, knockback, player.whoAmI);
                    Main.projectile[proj].extraUpdates += 1;
                }
            }

            return false;
        }

        public override void AddRecipes()
        {
            CreateRecipe().
                AddIngredient<RealmRavager>().
                AddIngredient(ItemID.VortexBeater).
                AddIngredient(ItemID.IllegalGunParts).
                AddIngredient<ArmoredShell>(2).
                AddTile(TileID.Anvils).
                Register();
        }
    }
}