﻿using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Furniture.Trophies;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.LoreItems
{
    public class KnowledgeHiveMind : LoreItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Hive Mind");
            Tooltip.SetDefault("A hive of clustered microbial-infected flesh.\n" +
                "I do not believe killing it will lessen the corruption here.");
            SacrificeTotal = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.rare = ItemRarityID.Orange;
            Item.consumable = false;
        }

        public override bool CanUseItem(Player player)
        {
            return false;
        }

        public override void AddRecipes()
        {
            CreateRecipe(1).AddTile(TileID.Bookcases).AddIngredient(ModContent.ItemType<HiveMindTrophy>()).AddIngredient(ModContent.ItemType<VictoryShard>(), 10).Register();
        }
    }
}
