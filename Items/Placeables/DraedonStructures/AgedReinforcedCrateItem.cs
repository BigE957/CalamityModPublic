using CalamityMod.Items.Materials;
using CalamityMod.Tiles.DraedonStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
namespace CalamityMod.Items.Placeables.DraedonStructures
{
    public class AgedReinforcedCrateItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            DisplayName.SetDefault("Aged Reinforced Crate");
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
            Item.createTile = ModContent.TileType<AgedReinforcedCrate>();
        }

        public override void AddRecipes()
        {
            CreateRecipe(1).AddIngredient(ModContent.ItemType<Items.Placeables.DraedonStructures.RustedPlating>(), 7).AddIngredient(ModContent.ItemType<DubiousPlating>()).AddTile(TileID.Anvils).Register();
        }
    }
}
