using CalamityMod.Projectiles.Ranged;
using Terraria.ID;
using Terraria.ModLoader;
namespace CalamityMod.Items.Ammo
{
    public class AccelerationBullet : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Acceleration Round");
            Tooltip.SetDefault("Gains speed over time");
        }

        public override void SetDefaults()
        {
            item.damage = 11;
            item.ranged = true;
            item.width = 8;
            item.height = 8;
            item.maxStack = 999;
            item.consumable = true;
            item.knockBack = 1.25f;
            item.value = 250;
            item.rare = 1;
            item.shoot = ModContent.ProjectileType<AccelerationBulletProj>();
            item.shootSpeed = 1f;
            item.ammo = 97;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.MusketBall, 150);
            recipe.AddIngredient(null, "VictoryShard");
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this, 150);
            recipe.AddRecipe();
        }
    }
}
