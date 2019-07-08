﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using CalamityMod.World;

namespace CalamityMod.NPCs.AstralBiomeNPCs
{
	public class Hive : ModNPC
	{
		private static Texture2D glowmask;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Hive");
			if (!Main.dedServ)
				glowmask = mod.GetTexture("NPCs/AstralBiomeNPCs/HiveGlow");
			Main.npcFrameCount[npc.type] = 6;
		}

		public override void SetDefaults()
		{
			npc.width = 38;
			npc.height = 60;
			npc.aiStyle = -1;
			npc.damage = 55;
			npc.defense = 15;
			npc.lifeMax = 470;
			npc.DeathSound = mod.GetLegacySoundSlot(SoundType.NPCKilled, "Sounds/NPCKilled/AstralEnemyDeath");
			npc.knockBackResist = 0f;
			npc.value = Item.buyPrice(0, 0, 15, 0);
			banner = npc.type;
			bannerItem = mod.ItemType("HiveBanner");
			if (CalamityWorld.downedAstrageldon)
			{
				npc.damage = 90;
				npc.defense = 25;
				npc.lifeMax = 700;
			}
		}

		public override void AI()
		{
			npc.ai[0]++;
			if (npc.ai[0] > 180)
			{
				if (Main.rand.Next(100) == 0)
				{
					npc.ai[0] = 0;

					//spawn hiveling, it's ai[0] is the hive npc index.
					int n = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("Hiveling"), 0, npc.whoAmI);
					Main.npc[n].velocity.X = Main.rand.NextFloat(-0.4f, 0.4f);
					Main.npc[n].velocity.Y = Main.rand.NextFloat(-0.5f, -0.05f);
				}
			}
		}

		public override void FindFrame(int frameHeight)
		{
			npc.frameCounter++;
			if (npc.frameCounter > 10)
			{
				npc.frameCounter = 0;
				npc.frame.Y += frameHeight;
				if (npc.frame.Y > frameHeight * 4)
				{
					npc.frame.Y = 0;
				}
			}
		}

		public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			//draw glowmask
			spriteBatch.Draw(glowmask, npc.Center - Main.screenPosition, npc.frame, Color.White * 0.6f, npc.rotation, new Vector2(19, 30), 1f, npc.spriteDirection == 1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (npc.soundDelay == 0)
			{
				npc.soundDelay = 15;
				switch (Main.rand.Next(3))
				{
					case 0:
						Main.PlaySound(mod.GetLegacySoundSlot(SoundType.NPCHit, "Sounds/NPCHit/AstralEnemyHit"), npc.Center);
						break;
					case 1:
						Main.PlaySound(mod.GetLegacySoundSlot(SoundType.NPCHit, "Sounds/NPCHit/AstralEnemyHit2"), npc.Center);
						break;
					case 2:
						Main.PlaySound(mod.GetLegacySoundSlot(SoundType.NPCHit, "Sounds/NPCHit/AstralEnemyHit3"), npc.Center);
						break;
				}
			}

			CalamityGlobalNPC.DoHitDust(npc, hitDirection, (Main.rand.Next(0, Math.Max(0, npc.life)) == 0) ? 5 : mod.DustType("AstralEnemy"), 1f, 3, 20);

			//if dead do gores
			if (npc.life <= 0)
			{
				int type = mod.NPCType("Hiveling");
				for (int i = 0; i < 200; i++)
				{
					if (Main.npc[i].type == type)
					{
						Main.npc[i].ai[0] = -1f;
					}
				}
			}
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			//only 1 hive possible.
			bool anyHives = NPC.CountNPCS(npc.type) > 0;
			if (anyHives) return 0f;

			Tile tile = Main.tile[spawnInfo.spawnTileX, spawnInfo.spawnTileY];
			if (spawnInfo.player.GetModPlayer<CalamityPlayer>().ZoneAstral && (spawnInfo.player.ZoneDirtLayerHeight || spawnInfo.player.ZoneRockLayerHeight))
			{
				return 0.17f;
			}
			return 0f;
		}

		public override void NPCLoot()
		{
			Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Stardust"), Main.rand.Next(2, 4));
			if (Main.expertMode)
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Stardust"));
			}
			if (CalamityWorld.downedAstrageldon && Main.rand.Next(7) == 0)
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("HivePod"));
			}
		}
	}
}
