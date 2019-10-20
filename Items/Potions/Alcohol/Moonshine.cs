using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using CalamityMod.Buffs.Alcohol;

namespace CalamityMod.Items.Potions.Alcohol
{
    public class Moonshine : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Moonshine");
            Tooltip.SetDefault(@"Increases defense by 10 and damage reduction by 5%
Reduces life regen by 1
This stuff is pretty strong but I'm sure you can handle it");
        }

        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 18;
            item.useTurn = true;
            item.maxStack = 30;
            item.rare = 2;
            item.useAnimation = 17;
            item.useTime = 17;
            item.useStyle = 2;
            item.UseSound = SoundID.Item3;
            item.consumable = true;
            item.buffType = ModContent.BuffType<MoonshineBuff>();
            item.buffTime = 18000; //5 minutes
            item.value = Item.buyPrice(0, 3, 30, 0);
        }
    }
}
