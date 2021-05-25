using CalamityMod.CalPlayer;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace CalamityMod.Items.Weapons.Rogue
{
	public abstract class RogueWeapon : ModItem
	{
		public virtual void SafeSetDefaults()
		{
		}

		public float StealthStrikePrefixBonus;

		public RogueWeapon()
		{
			StealthStrikePrefixBonus = 1f;
		}

		public override ModItem Clone(Item itemClone)
		{
			RogueWeapon myClone = (RogueWeapon)base.Clone(itemClone);
			myClone.StealthStrikePrefixBonus = StealthStrikePrefixBonus;
			return myClone;
		}

		public override int ChoosePrefix(UnifiedRandom rand)
		{
			WeightedRandom<string> newPrefix = new WeightedRandom<string>();
			newPrefix.Add("Pointy", 1);
			newPrefix.Add("Sharp", 1);
			newPrefix.Add("Feathered", 1);
			newPrefix.Add("Sleek", 1);
			newPrefix.Add("Hefty", 1);
			newPrefix.Add("Mighty", 1);
			newPrefix.Add("Glorious", 1);
			newPrefix.Add("Serrated", 1);
			newPrefix.Add("Vicious", 1);
			newPrefix.Add("Lethal", 1);
			newPrefix.Add("Flawless", 1);
			newPrefix.Add("Radical", 1);
			newPrefix.Add("Blunt", 1);
			newPrefix.Add("Flimsy", 1);
			newPrefix.Add("Unbalanced", 1);
			newPrefix.Add("Atrocious", 1);
			return mod.GetPrefix(newPrefix).Type;
		}

		public override bool NewPreReforge()
		{
			StealthStrikePrefixBonus = 1f;
			return true;
		}

		public override bool? PrefixChance(int pre, UnifiedRandom rand)
		{
			if (item.maxStack > 1)
			{
				return false;
			}
			return null;
		}

		public sealed override void SetDefaults()
		{
			SafeSetDefaults();
			item.melee = false;
			item.ranged = false;
			item.magic = false;
			item.thrown = true;
			item.summon = false;
		}

		// Add both the player's dedicated rogue damage and stealth strike damage as applicable.
		// Rogue weapons are internally throwing so they already benefit from throwing damage boosts.
		// 5E-06 to prevent downrounding is not needed anymore, added by TML itself
		public override void ModifyWeaponDamage(Player player, ref float add, ref float mult, ref float flat)
		{
			CalamityPlayer mp = player.Calamity();

			// Both regular rogue damage stat and stealth damage are added to the weapon simultaneously.
			add += mp.throwingDamage + mp.stealthDamage - 1f;

			// Boost (or lower) the weapon's damage if it has a stealth strike available and an associated prefix
			if (mp.StealthStrikeAvailable() && item.prefix > 0)
				mult += StealthStrikePrefixBonus - 1f;
		}

		// Simply add the player's dedicated rogue crit chance.
		// Rogue crit isn't boosted by Calamity universal crit boosts, so this won't double-add universal crit.
		public override void GetWeaponCrit(Player player, ref int crit)
		{
			crit += player.Calamity().throwingCrit;
		}

		public override float UseTimeMultiplier(Player player)
		{
			float rogueAS = 1f;
			if (player.Calamity().gloveOfPrecision)
				rogueAS -= 0.2f;
			if (player.Calamity().gloveOfRecklessness)
				rogueAS += 0.12f;
			if (player.Calamity().titanHeartMantle)
				rogueAS -= 0.15f;
			return rogueAS;
		}

		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			TooltipLine damageTooltip = tooltips.FirstOrDefault(x => x.Name == "Damage" && x.mod == "Terraria");
			if (damageTooltip != null)
			{
				// Replace the word "throwing" with "rogue" in the item's damage line.
				string text = damageTooltip.text.Replace(" throwing ", " rogue ");

				// Split visible damage into stealth and non-stealth damage values if the player has the stealth mechanic available to them.
				Player p = Main.LocalPlayer;
				CalamityPlayer mp = p.Calamity();
				if (mp.rogueStealthMax > 0f)
				{
					int damageNumberSubstringIndex = text.IndexOf(' ');
					string restOfTooltip = text.Substring(damageNumberSubstringIndex);
					int damageWithStealth = int.Parse(text.Substring(0, damageNumberSubstringIndex));

					int damageWithoutStealth = (int)(item.damage * (p.allDamage + p.thrownDamage + mp.throwingDamage - 2f));
					text = damageWithoutStealth + restOfTooltip + " : " + damageWithStealth + " stealth strike damage";
				}

				damageTooltip.text = text;
			}

			// Add a tooltip line for the stealth strike damage bonus of the item's prefix, if applicable.
			if (item.prefix > 0)
			{
				float ssDmgBoost = StealthStrikePrefixBonus - 1f;
				if (ssDmgBoost != 0f)
				{
					bool badModifier = ssDmgBoost < 0f;
					string txt = (badModifier ? "-" : "+") + Math.Round(Math.Abs(ssDmgBoost) * 100f) + "% stealth strike damage";
					TooltipLine stealthTooltip = new TooltipLine(mod, "PrefixSSDmg", txt)
					{
						isModifier = true,
						isModifierBad = badModifier
					};
					tooltips.Add(stealthTooltip);
				}
			}
		}

		public override bool ConsumeItem(Player player) => Main.rand.NextFloat() <= player.Calamity().throwingAmmoCost;
	}
}
