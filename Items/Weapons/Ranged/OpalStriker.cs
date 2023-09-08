﻿using Terraria.DataStructures;
using CalamityMod.Projectiles.Ranged;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace CalamityMod.Items.Weapons.Ranged
{
    public class OpalStriker : ModItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Weapons.Ranged";

        public static readonly SoundStyle Charge = new("CalamityMod/Sounds/Item/OpalCharge") { Volume = 0.2f};
        public static readonly SoundStyle ChargeLoop = new("CalamityMod/Sounds/Item/OpalChargeLoop") { Volume = 0.2f};
        public static readonly SoundStyle Fire = new("CalamityMod/Sounds/Item/OpalFire") { PitchVariance = 0.3f, Volume = 0.3f };

        public override void SetDefaults()
        {
            Item.damage = 9;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 46;
            Item.height = 30;
            Item.useTime = 14;
            Item.useAnimation = 14;
            Item.reuseDelay = 18;
            Item.useLimitPerAnimation = 18;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 2f;
            Item.value = CalamityGlobalItem.Rarity2BuyPrice;
            Item.rare = ItemRarityID.Green;
            Item.UseSound = null;
            Item.autoReuse = true;
            Item.channel = true;
            Item.shoot = ModContent.ProjectileType<OpalStrike>();
            Item.shootSpeed = 12f;
            Item.Calamity().canFirePointBlankShots = true;
        }

        public override Vector2? HoldoutOffset() => new Vector2(-15, 0);
        public override void HoldItem(Player player)
        {
            Item.noUseGraphic = true;
        }
        public override bool CanUseItem(Player player) => player.ownedProjectileCounts[ModContent.ProjectileType<OpalStrikerHoldout>()] <= 0;
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 shootVelocity = velocity;
            Vector2 shootDirection = shootVelocity.SafeNormalize(Vector2.UnitX * player.direction);
            Projectile.NewProjectile(source, position, shootDirection, ModContent.ProjectileType<OpalStrikerHoldout>(), damage, knockback, player.whoAmI);
            return false;
        }

        public override bool CanConsumeAmmo(Item ammo, Player player) => Main.rand.NextBool();

        public override void AddRecipes()
        {
            CreateRecipe().
                AddIngredient(ItemID.Marble, 20).
                AddIngredient(ItemID.Amber, 5).
                AddIngredient(ItemID.Diamond, 3).
                AddTile(TileID.Anvils).
                Register();
        }
    }
}
