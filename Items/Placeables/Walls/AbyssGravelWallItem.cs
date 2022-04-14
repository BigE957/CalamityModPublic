using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using WallTiles = CalamityMod.Walls;

namespace CalamityMod.Items.Placeables.Walls
{
    public class AbyssGravelWallItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 400;
            DisplayName.SetDefault("Abyss Gravel Wall");
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.maxStack = 999;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 7;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.createWall = ModContent.WallType<WallTiles.AbyssGravelWallSafe>();
        }

        public override void AddRecipes()
        {
            CreateRecipe(4).AddIngredient(ModContent.ItemType<AbyssGravel>()).AddTile(TileID.WorkBenches).Register();
            CreateRecipe(1).AddIngredient(this, 4).AddTile(TileID.WorkBenches).ReplaceResult(ModContent.ItemType<AbyssGravel>());
        }
    }
}
