using CalamityMod.Items.Placeables.Walls.DraedonStructures;
using Terraria.ID;
using Terraria.ModLoader;
namespace CalamityMod.Items.Placeables.DraedonStructures
{
    public class LaboratoryPlating : ModItem
    {
        public override void SetStaticDefaults()
        {
            SacrificeTotal = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 12;
            Item.height = 12;
            Item.maxStack = 999;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.createTile = ModContent.TileType<Tiles.DraedonStructures.LaboratoryPlating>();
        }

        public override void AddRecipes()
        {
            CreateRecipe(25).AddIngredient(ItemID.IronBar).AddRecipeGroup("AnyStoneBlock", 3).AddTile(TileID.HeavyWorkBench).Register();
            CreateRecipe(1).AddIngredient(ModContent.ItemType<RustedPlating>()).AddTile(TileID.Anvils).Register();
            CreateRecipe(1).AddIngredient(ModContent.ItemType<LaboratoryShelf>(), 2).Register();
            CreateRecipe(1).AddIngredient(ModContent.ItemType<LaboratoryPlatingWall>(), 4).AddTile(TileID.WorkBenches).Register();
            CreateRecipe(1).AddIngredient(ModContent.ItemType<LaboratoryPlateBeam>(), 4).AddTile(TileID.WorkBenches).Register();
            CreateRecipe(1).AddIngredient(ModContent.ItemType<LaboratoryPlatePillar>(), 4).AddTile(TileID.WorkBenches).Register();
        }
    }
}
