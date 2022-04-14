using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
namespace CalamityMod.Items.Placeables.FurnitureOccult
{
    public class OccultBed : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.SetNameOverride("Otherworldly Bed");
            Item.width = 28;
            Item.height = 20;
            Item.maxStack = 999;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.createTile = ModContent.TileType<Tiles.FurnitureOccult.OccultBed>();
        }

        public override void AddRecipes()
        {
            CreateRecipe(1).AddIngredient(ModContent.ItemType<OccultStone>(), 15).AddIngredient(ItemID.Silk, 5).AddTile(TileID.LunarCraftingStation).Register();
        }
    }
}
