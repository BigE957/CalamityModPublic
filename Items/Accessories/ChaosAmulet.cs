﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;

namespace CalamityMod.Items.Accessories
{
    public class ChaosAmulet : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            DisplayName.SetDefault("Spelunker's Amulet");
            Tooltip.SetDefault("Spelunker effect and 15% increased mining speed");
        }

        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 32;
            Item.value = CalamityGlobalItem.Rarity4BuyPrice;
            Item.rare = ItemRarityID.LightRed;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.pickSpeed -= 0.15f;
            player.findTreasure = true;
        }

        public override void AddRecipes()
        {
            CreateRecipe().
                AddIngredient(ItemID.GoldDust, 7).
                AddIngredient(ItemID.SpelunkerPotion, 7).
                AddTile(TileID.Anvils).
                Register();
        }
    }
}
