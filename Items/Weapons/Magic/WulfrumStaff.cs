using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Magic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace CalamityMod.Items.Weapons.Magic
{
    public class WulfrumStaff : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Wulfrum Staff");
            Tooltip.SetDefault("Casts a wulfrum bolt");
            Item.staff[item.type] = true;
        }

        public override void SetDefaults()
        {
            item.damage = 10;
            item.magic = true;
            item.mana = 4;
            item.width = 44;
            item.height = 46;
            item.useTime = 20;
            item.useAnimation = 20;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 3;
            item.value = Item.buyPrice(0, 1, 0, 0);
            item.rare = 1;
            item.UseSound = SoundID.Item43;
            item.autoReuse = true;
            item.shoot = ModContent.ProjectileType<WulfrumBolt>();
            item.shootSpeed = 9f;
        }

        public override Vector2? HoldoutOrigin()
        {
            return new Vector2(10, 25);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<WulfrumShard>(), 12);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
