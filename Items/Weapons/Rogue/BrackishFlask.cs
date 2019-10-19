using Terraria;
using Terraria.ModLoader; using CalamityMod.Items.Materials;
using Terraria.ID;

namespace CalamityMod.Items.Weapons.Rogue
{
    public class BrackishFlask : RogueWeapon
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Brackish Flask");
            Tooltip.SetDefault("Explodes into poisonous seawater blasts");
        }

        public override void SafeSetDefaults()
        {
            item.width = 28;
            item.damage = 40;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.useAnimation = 35;
            item.useStyle = 1;
            item.useTime = 35;
            item.knockBack = 6.5f;
            item.UseSound = SoundID.Item106;
            item.autoReuse = true;
            item.height = 30;
            item.value = Item.buyPrice(0, 60, 0, 0);
            item.rare = 7;
            item.shoot = ModContent.ProjectileType<Projectiles.BrackishFlask>();
            item.shootSpeed = 12f;
            item.Calamity().rogue = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<IOU>());
            recipe.AddIngredient(ModContent.ItemType<LivingShard>());
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
