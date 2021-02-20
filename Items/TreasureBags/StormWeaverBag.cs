using CalamityMod.Items.Accessories;
using CalamityMod.Items.Armor.Vanity;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Weapons.Magic;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.NPCs.StormWeaver;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.TreasureBags
{
    public class StormWeaverBag : ModItem
    {
        public override int BossBagNPC => ModContent.NPCType<StormWeaverHeadNaked>();

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Treasure Bag");
            Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}");
        }

        public override void SetDefaults()
        {
            item.maxStack = 999;
            item.consumable = true;
            item.width = 24;
            item.height = 24;
            item.rare = ItemRarityID.Cyan;
            item.expert = true;
        }

        public override bool CanRightClick() => true;

        public override void OpenBossBag(Player player)
        {
            // Materials
            DropHelper.DropItem(player, ModContent.ItemType<ArmoredShell>(), 7, 10);

            // Weapons
			DropHelper.DropItemChance(player, ModContent.ItemType<TheStorm>(), 3);
			DropHelper.DropItemChance(player, ModContent.ItemType<StormDragoon>(), 3);
			DropHelper.DropItemChance(player, ModContent.ItemType<Thunderstorm>(), DropHelper.RareVariantDropRateFloat);

            // Equipment (None yet boohoo)

            // Vanity
			DropHelper.DropItemChance(player, ModContent.ItemType<StormWeaverMask>(), 7);
			if (Main.rand.NextBool(20))
			{
				DropHelper.DropItem(player, ModContent.ItemType<AncientGodSlayerHelm>());
				DropHelper.DropItem(player, ModContent.ItemType<AncientGodSlayerChestplate>());
				DropHelper.DropItem(player, ModContent.ItemType<AncientGodSlayerLeggings>());
			}
        }
    }
}
