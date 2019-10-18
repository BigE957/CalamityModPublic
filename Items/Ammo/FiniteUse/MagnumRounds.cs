using CalamityMod.Projectiles.Typeless.FiniteUse;
using Terraria.ModLoader;

namespace CalamityMod.Items.Ammo.FiniteUse
{
    public class MagnumRounds : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Magnum Round");
        }

        public override void SetDefaults()
        {
            item.damage = 80;
            item.crit += 4;
            item.width = 18;
            item.height = 18;
            item.maxStack = 12;
            item.consumable = true;
            item.knockBack = 8f;
            item.value = 10000;
            item.rare = 8;
            item.shoot = ModContent.ProjectileType<MagnumRound>();
            item.shootSpeed = 12f;
            item.ammo = ModContent.ItemType<MagnumRounds>(); // TODO -- would item.type work here
        }
    }
}
