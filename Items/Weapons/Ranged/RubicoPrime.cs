﻿using Terraria.DataStructures;
using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Ranged;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;

namespace CalamityMod.Items.Weapons.Ranged
{
    public class RubicoPrime : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Rubico Prime");
            Tooltip.SetDefault("Semi-automatic sniper that fires in 5 second bursts\n" +
                "Fires impact rounds that have an increased crit multiplier");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 1178;
            Item.DamageType = DamageClass.Ranged;
            Item.knockBack = 10f;
            Item.useTime = 30;
            Item.useAnimation = 300;
            Item.autoReuse = false;

            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.width = 82;
            Item.height = 28;

            Item.shoot = ProjectileID.PurificationPowder;
            Item.shootSpeed = 12f;
            Item.useAmmo = AmmoID.Bullet;

            Item.value = CalamityGlobalItem.Rarity14BuyPrice;
            Item.Calamity().customRarity = CalamityRarity.DarkBlue;
            Item.Calamity().donorItem = true;
            Item.Calamity().canFirePointBlankShots = true;
        }

        // Terraria seems to really dislike high crit values in SetDefaults
        public override void ModifyWeaponCrit(Player player, ref float crit) => crit += 40;

        public override Vector2? HoldoutOffset() => new Vector2(-10, 0);

        public override void AddRecipes()
        {
            CreateRecipe(1).AddIngredient(ModContent.ItemType<PestilentDefiler>()).AddIngredient(ModContent.ItemType<CosmiliteBar>(), 8).AddIngredient(ModContent.ItemType<NightmareFuel>(), 20).AddTile(ModContent.TileType<CosmicAnvil>()).Register();
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, ModContent.ProjectileType<ImpactRound>(), damage, knockback, player.whoAmI, 0f, 0f);
            return false;
        }
    }
}
