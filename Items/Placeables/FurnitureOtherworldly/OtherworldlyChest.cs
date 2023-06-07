﻿using CalamityMod.Items.Placeables.FurnitureCosmilite;
using Terraria.ModLoader;
using Terraria.ID;
namespace CalamityMod.Items.Placeables.FurnitureOtherworldly
{
    [LegacyName("OccultChest")]
    public class OtherworldlyChest : ModItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Placeables";
        public override void SetDefaults()
        {
            Item.SetNameOverride("Otherworldly Chest");
            Item.width = 28;
            Item.height = 20;
            Item.maxStack = 9999;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.createTile = ModContent.TileType<Tiles.FurnitureOtherworldly.OtherworldlyChest>();
        }

        public override void AddRecipes()
        {
            CreateRecipe(1).AddIngredient(ModContent.ItemType<OtherworldlyStone>(), 8).AddIngredient(ModContent.ItemType<CosmiliteBrick>(), 2).AddTile(TileID.LunarCraftingStation).Register();
        }
    }
}
