﻿using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables;
using CalamityMod.Projectiles.Rogue;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace CalamityMod.Items.Weapons.Rogue
{
    public class StormfrontRazor : RogueWeapon
    {

        public static readonly SoundStyle LightningStrikeSound = new("CalamityMod/Sounds/Custom/LightningStrike");
        public const float LightningDamageFactor = 1.5f;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Stormfront Razor");
            Tooltip.SetDefault("Throws a throwing knife that summons lightning on hit.\n" +
                               "Stealth strikes cause the knife to be faster and summon multiple lightning bolts on hit\n" +
                               "[c/24c7f0:Quite shocking]");
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(6, 4));
            ItemID.Sets.AnimatesAsSoul[Type] = true;
            SacrificeTotal = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 38;
            Item.height = 38;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.value = CalamityGlobalItem.Rarity5BuyPrice;
            Item.rare = ItemRarityID.Pink;
            Item.useAnimation = 20;
            Item.useTime = 20;
            Item.damage = 43;
            Item.knockBack = 7f;
            Item.shoot = ModContent.ProjectileType<StormfrontRazorProjectile>();
            Item.shootSpeed = 8f;
            Item.DamageType = RogueDamageClass.Instance;
        }

        // Terraria seems to really dislike high crit values in SetDefaults
        public override void ModifyWeaponCrit(Player player, ref float crit) => crit += 8;

		public override float StealthDamageMultiplier => 1f;
        public override float StealthVelocityMultiplier => 1.5f;

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.Calamity().StealthStrikeAvailable())
            {
                int p = Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, 0, 10f);
                if (p.WithinBounds(Main.maxProjectiles))
                    Main.projectile[p].Calamity().stealthStrike = true;
                return false;
            }
            else
            {
                Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, 0, 1f);
                return false;
            }
        }

        public override void AddRecipes()
        {
            CreateRecipe().
                AddIngredient<Cinquedea>().
                AddRecipeGroup("AnyMythrilBar", 6).
                AddIngredient<EssenceofSunlight>(4).
                AddIngredient<SeaPrism>(15).
                AddIngredient<StormlionMandible>(2).
                AddTile(TileID.MythrilAnvil).
                Register();
        }
    }
}
