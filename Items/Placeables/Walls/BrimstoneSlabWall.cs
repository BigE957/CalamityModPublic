﻿using Terraria.ModLoader;
using WallTiles = CalamityMod.Walls;
using Terraria.ID;

namespace CalamityMod.Items.Placeables.Walls
{
    public class BrimstoneSlabWall : ModItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Placeables";
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 400;
        }

        public override void SetDefaults()
        {
            Item.width = 12;
            Item.height = 12;
            Item.maxStack = 9999;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 7;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.createWall = ModContent.WallType<WallTiles.BrimstoneSlabWall>();
        }

        public override void AddRecipes() 
        {
            CreateRecipe(4).AddIngredient(ModContent.ItemType<BrimstoneSlab>()).AddTile(TileID.WorkBenches).Register();
        }
    }
}
