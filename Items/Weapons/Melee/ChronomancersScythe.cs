﻿using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Melee;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee
{
    public class ChronomancersScythe : ModItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Weapons.Melee";
        public override void SetStaticDefaults()
        {
            ItemID.Sets.ItemsThatAllowRepeatedRightClick[Item.type] = true;
        }

        public override void SetDefaults()
        {
            Item.width = 45;
            Item.height = 45;
            Item.DamageType = TrueMeleeNoSpeedDamageClass.Instance; // ironic how a weapon that increases use speed cant benefit from it
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.channel = true;
            Item.damage = 100;
            Item.knockBack = 4f;
            Item.useAnimation = 25;
            Item.useTime = 5;
            Item.autoReuse = false;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item71;

            Item.value = CalamityGlobalItem.Rarity11BuyPrice;
            Item.rare = ItemRarityID.Purple;
            Item.Calamity().donorItem = true;

            Item.shoot = ModContent.ProjectileType<ChronomancersScytheSwing>();
            Item.shootSpeed = 24f;
        }
        public override bool AltFunctionUse(Player player) => true;

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.altFunctionUse == 2)
            {
                Projectile.NewProjectile(source, position, Vector2.Zero, ModContent.ProjectileType<ChronomancersScytheHoldout>(), damage, knockback, Main.myPlayer);
                return false;
            }
            else
            {
                return true;
            }
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2f)
            {
                Item.reuseDelay = 20;
                Item.channel = false;
            }
            else
            {
                Item.reuseDelay = 0;
                Item.channel = true;
            }
            return base.CanUseItem(player);
        }

        public override void AddRecipes()
        {
            CreateRecipe().
                AddIngredient(ItemID.IceSickle).
                AddIngredient(ItemID.FastClock).
                AddIngredient(ItemID.LunarBar, 10).
                AddIngredient<CoreofEleum>(6).
                AddTile(TileID.LunarCraftingStation).
                Register();
        }
    }
}
