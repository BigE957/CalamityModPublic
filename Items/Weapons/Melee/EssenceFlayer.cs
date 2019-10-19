using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader; using CalamityMod.Items.Materials;
using Terraria.ID;
using CalamityMod.Projectiles.Melee;
using CalamityMod.Buffs.DamageOverTime;

namespace CalamityMod.Items.Weapons.Melee
{
    public class EssenceFlayer : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Essence Flayer");
            Tooltip.SetDefault("Shoots an essence scythe that generates healing spirits on enemy kills");
        }

        public override void SetDefaults()
        {
            item.width = 100;
            item.damage = 450;
            item.melee = true;
            item.useAnimation = 19;
            item.useStyle = 1;
            item.useTime = 19;
            item.useTurn = true;
            item.knockBack = 8f;
            item.UseSound = SoundID.Item71;
            item.autoReuse = true;
            item.height = 78;
            item.value = Item.buyPrice(1, 80, 0, 0);
            item.rare = 10;
            item.shoot = ModContent.ProjectileType<EssenceScythe>();
            item.shootSpeed = 21f;
            item.Calamity().postMoonLordRarity = 14;
        }

        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            Vector2 origin = new Vector2(50f, 37f);
            spriteBatch.Draw(ModContent.GetTexture("CalamityMod/Items/Weapons/Melee/EssenceFlayerGlow"), item.Center - Main.screenPosition, null, Color.White, rotation, origin, 1f, SpriteEffects.None, 0f);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<CosmiliteBar>(), 11);
            recipe.AddIngredient(ModContent.ItemType<NightmareFuel>(), 5);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<CosmiliteBar>(), 11);
            recipe.AddIngredient(ModContent.ItemType<EndothermicEnergy>(), 5);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.NextBool(3))
            {
                int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 173);
            }
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(ModContent.BuffType<GodSlayerInferno>(), 300);
        }
    }
}
