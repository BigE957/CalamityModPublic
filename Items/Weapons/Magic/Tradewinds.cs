﻿using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Magic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;

namespace CalamityMod.Items.Weapons.Magic
{
    public class Tradewinds : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tradewinds");
            Tooltip.SetDefault("Casts fast moving sunlight feathers");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 31;
            Item.DamageType = DamageClass.Magic;
            Item.mana = 5;
            Item.width = 28;
            Item.height = 30;
            Item.useTime = 12;
            Item.useAnimation = 12;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 5;
            Item.value = Item.buyPrice(0, 4, 0, 0);
            Item.rare = ItemRarityID.Orange;
            Item.UseSound = SoundID.Item7;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<TradewindsProjectile>();
            Item.shootSpeed = 25f;
        }

        public override void AddRecipes()
        {
            CreateRecipe().
                AddIngredient<AerialiteBar>(6).
                AddIngredient(ItemID.SunplateBlock, 5).
                AddIngredient(ItemID.Feather, 3).
                AddTile(TileID.Bookcases).
                Register();
        }
    }
}
