using CalamityMod.Items.Placeables.FurnitureOccult;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using WallTiles = CalamityMod.Walls;
using Terraria.ID;
namespace CalamityMod.Items.Placeables.Walls
{
    public class OccultStoneWall : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 400;
        }

        public override void SetDefaults()
        {
            Item.SetNameOverride("Otherworldly Stone Wall");
            Item.width = 12;
            Item.height = 12;
            Item.maxStack = 999;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 7;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.createWall = ModContent.WallType<WallTiles.OccultStoneWall>();
        }

        public override void AddRecipes()
        {
            CreateRecipe(4).AddIngredient(ModContent.ItemType<OccultStone>()).AddTile(TileID.WorkBenches).Register();
        }
    }
}
