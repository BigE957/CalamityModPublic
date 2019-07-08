﻿using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityMod.Projectiles;
using CalamityMod.World;

namespace CalamityMod.NPCs.TheDevourerofGods
{
	[AutoloadBossHead]
	public class DevourerofGodsBodyS : ModNPC
	{
		private int invinceTime = 360;
		private bool setAlpha = false;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("The Devourer of Gods");
		}

		public override void SetDefaults()
		{
			npc.damage = 220; //70
			npc.npcSlots = 5f;
			npc.width = 54; //34
			npc.height = 54; //34
			npc.defense = 0;
			npc.lifeMax = CalamityWorld.revenge ? 1875000 : 1650000; //720000 672000
			if (CalamityWorld.death)
			{
				npc.lifeMax = 3060000;
			}
			if (CalamityWorld.bossRushActive)
			{
				npc.lifeMax = CalamityWorld.death ? 10000000 : 9200000;
			}
			double HPBoost = (double)Config.BossHealthPercentageBoost * 0.01;
			npc.lifeMax += (int)((double)npc.lifeMax * HPBoost);
			npc.aiStyle = -1; //new
			aiType = -1; //new
			animationType = 10; //new
			npc.knockBackResist = 0f;
			npc.alpha = 255;
			npc.behindTiles = true;
			npc.noGravity = true;
			npc.noTileCollide = true;
			npc.chaseable = false;
			npc.canGhostHeal = false;
			npc.HitSound = SoundID.NPCHit4;
			npc.DeathSound = SoundID.NPCDeath14;
			npc.netAlways = true;
			npc.boss = true;
			for (int k = 0; k < npc.buffImmune.Length; k++)
			{
				npc.buffImmune[k] = true;
			}
			Mod calamityModMusic = ModLoader.GetMod("CalamityModMusic");
			if (calamityModMusic != null)
				music = calamityModMusic.GetSoundSlot(SoundType.Music, "Sounds/Music/UniversalCollapse");
			else
				music = MusicID.LunarBoss;
			npc.dontCountMe = true;
		}

		public override void SendExtraAI(BinaryWriter writer)
		{
			writer.Write(invinceTime);
			writer.Write(setAlpha);
			writer.Write(npc.dontTakeDamage);
		}

		public override void ReceiveExtraAI(BinaryReader reader)
		{
			invinceTime = reader.ReadInt32();
			setAlpha = reader.ReadBoolean();
			npc.dontTakeDamage = reader.ReadBoolean();
		}

		public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
		{
			return false;
		}

		public override void BossHeadRotation(ref float rotation)
		{
			rotation = npc.rotation;
		}

		public override void AI()
		{
			bool expertMode = Main.expertMode;
			bool speedBoost1 = (double)npc.life <= (double)npc.lifeMax * 0.8; //speed increase
			bool speedBoost2 = (double)npc.life <= (double)npc.lifeMax * 0.6; //speed increase
			bool speedBoost3 = (double)npc.life <= (double)npc.lifeMax * 0.4; //speed increase
			bool speedBoost4 = (double)npc.life <= (double)npc.lifeMax * 0.2; //speed increase
			Lighting.AddLight((int)((npc.position.X + (float)(npc.width / 2)) / 16f), (int)((npc.position.Y + (float)(npc.height / 2)) / 16f), 0.2f, 0.05f, 0.2f);
			if (invinceTime > 0)
			{
				invinceTime--;
				npc.dontTakeDamage = true;
			}
			else
			{
				npc.dontTakeDamage = false;
			}
			if (npc.ai[3] > 0f)
			{
				npc.realLife = (int)npc.ai[3];
			}
			if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead)
			{
				npc.TargetClosest(true);
			}
			npc.velocity.Length();
			if (npc.velocity.X < 0f)
			{
				npc.spriteDirection = -1;
			}
			else if (npc.velocity.X > 0f)
			{
				npc.spriteDirection = 1;
			}
			bool flag = false;
			if (npc.ai[1] <= 0f)
			{
				flag = true;
			}
			else if (Main.npc[(int)npc.ai[1]].life <= 0)
			{
				flag = true;
			}
			if (flag)
			{
				npc.life = 0;
				npc.HitEffect(0, 10.0);
				npc.checkDead();
			}
			if (CalamityGlobalNPC.DoGHead < 0 || !Main.npc[CalamityGlobalNPC.DoGHead].active)
			{
				npc.active = false;
			}
			if (Main.npc[(int)npc.ai[1]].alpha < 128 && !setAlpha)
			{
				npc.alpha -= 42;
				if (npc.alpha <= 0 && invinceTime <= 0)
				{
					setAlpha = true;
					npc.alpha = 0;
				}
			}
			else
			{
				npc.alpha = Main.npc[(int)npc.ai[2]].alpha;
			}
			float fallSpeed = 16f;
			float turnSpeed = 0.3f;
			float speed = fallSpeed;
			if (Main.npc[(int)npc.ai[2]].ai[2] == 0f)
			{
				if (speedBoost4 || CalamityWorld.death)
				{
					speed = 26f;
					turnSpeed = 0.53f;
				}
				else if (speedBoost3)
				{
					speed = 21.5f;
					turnSpeed = 0.43f;
				}
				else if (speedBoost2)
				{
					speed = 20.5f;
					turnSpeed = 0.39f;
				}
				else if (speedBoost1)
				{
					speed = 19f;
					turnSpeed = 0.36f;
				}
			}
			else
			{
				turnSpeed = 0.2f;
				if (speedBoost4 || CalamityWorld.death)
				{
					turnSpeed = 0.4f;
					speed = 28f;
				}
				else if (speedBoost3)
				{
					turnSpeed = 0.29f;
					speed = 22f;
				}
				else if (speedBoost2)
				{
					turnSpeed = 0.25f;
					speed = 20f;
				}
				else if (speedBoost1)
				{
					turnSpeed = 0.22f;
					speed = 18f;
				}
			}
			int num180 = (int)(npc.position.X / 16f) - 1;
			int num181 = (int)((npc.position.X + (float)npc.width) / 16f) + 2;
			int num182 = (int)(npc.position.Y / 16f) - 1;
			int num183 = (int)((npc.position.Y + (float)npc.height) / 16f) + 2;
			if (num180 < 0)
			{
				num180 = 0;
			}
			if (num181 > Main.maxTilesX)
			{
				num181 = Main.maxTilesX;
			}
			if (num182 < 0)
			{
				num182 = 0;
			}
			if (num183 > Main.maxTilesY)
			{
				num183 = Main.maxTilesY;
			}
			if (Main.player[npc.target].dead)
			{
				npc.TargetClosest(false);
			}
			float num188 = speed;
			float num189 = turnSpeed;
			Vector2 vector18 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
			float num191 = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2);
			float num192 = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2);
			num191 = (float)((int)(num191 / 16f) * 16);
			num192 = (float)((int)(num192 / 16f) * 16);
			vector18.X = (float)((int)(vector18.X / 16f) * 16);
			vector18.Y = (float)((int)(vector18.Y / 16f) * 16);
			num191 -= vector18.X;
			num192 -= vector18.Y;
			float num193 = (float)System.Math.Sqrt((double)(num191 * num191 + num192 * num192));
			if (npc.ai[1] > 0f && npc.ai[1] < (float)Main.npc.Length)
			{
				try
				{
					vector18 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
					num191 = Main.npc[(int)npc.ai[1]].position.X + (float)(Main.npc[(int)npc.ai[1]].width / 2) - vector18.X;
					num192 = Main.npc[(int)npc.ai[1]].position.Y + (float)(Main.npc[(int)npc.ai[1]].height / 2) - vector18.Y;
				}
				catch
				{
				}
				npc.rotation = (float)System.Math.Atan2((double)num192, (double)num191) + 1.57f;
				num193 = (float)System.Math.Sqrt((double)(num191 * num191 + num192 * num192));
				int num194 = npc.width;
				num193 = (num193 - (float)num194) / num193;
				num191 *= num193;
				num192 *= num193;
				npc.velocity = Vector2.Zero;
				npc.position.X = npc.position.X + num191;
				npc.position.Y = npc.position.Y + num192;
				if (num191 < 0f)
				{
					npc.spriteDirection = -1;
				}
				else if (num191 > 0f)
				{
					npc.spriteDirection = 1;
				}
			}
			else
			{
				num193 = (float)System.Math.Sqrt((double)(num191 * num191 + num192 * num192));
				float num196 = System.Math.Abs(num191);
				float num197 = System.Math.Abs(num192);
				float num198 = num188 / num193;
				num191 *= num198;
				num192 *= num198;
				if ((npc.velocity.X > 0f && num191 > 0f) || (npc.velocity.X < 0f && num191 < 0f) || (npc.velocity.Y > 0f && num192 > 0f) || (npc.velocity.Y < 0f && num192 < 0f))
				{
					if (npc.velocity.X < num191)
					{
						npc.velocity.X = npc.velocity.X + num189;
					}
					else
					{
						if (npc.velocity.X > num191)
						{
							npc.velocity.X = npc.velocity.X - num189;
						}
					}
					if (npc.velocity.Y < num192)
					{
						npc.velocity.Y = npc.velocity.Y + num189;
					}
					else
					{
						if (npc.velocity.Y > num192)
						{
							npc.velocity.Y = npc.velocity.Y - num189;
						}
					}
					if ((double)System.Math.Abs(num192) < (double)num188 * 0.2 && ((npc.velocity.X > 0f && num191 < 0f) || (npc.velocity.X < 0f && num191 > 0f)))
					{
						if (npc.velocity.Y > 0f)
						{
							npc.velocity.Y = npc.velocity.Y + num189 * 2f;
						}
						else
						{
							npc.velocity.Y = npc.velocity.Y - num189 * 2f;
						}
					}
					if ((double)System.Math.Abs(num191) < (double)num188 * 0.2 && ((npc.velocity.Y > 0f && num192 < 0f) || (npc.velocity.Y < 0f && num192 > 0f)))
					{
						if (npc.velocity.X > 0f)
						{
							npc.velocity.X = npc.velocity.X + num189 * 2f; //changed from 2
						}
						else
						{
							npc.velocity.X = npc.velocity.X - num189 * 2f; //changed from 2
						}
					}
				}
				else
				{
					if (num196 > num197)
					{
						if (npc.velocity.X < num191)
						{
							npc.velocity.X = npc.velocity.X + num189 * 1.1f; //changed from 1.1
						}
						else if (npc.velocity.X > num191)
						{
							npc.velocity.X = npc.velocity.X - num189 * 1.1f; //changed from 1.1
						}
						if ((double)(System.Math.Abs(npc.velocity.X) + System.Math.Abs(npc.velocity.Y)) < (double)num188 * 0.5)
						{
							if (npc.velocity.Y > 0f)
							{
								npc.velocity.Y = npc.velocity.Y + num189;
							}
							else
							{
								npc.velocity.Y = npc.velocity.Y - num189;
							}
						}
					}
					else
					{
						if (npc.velocity.Y < num192)
						{
							npc.velocity.Y = npc.velocity.Y + num189 * 1.1f;
						}
						else if (npc.velocity.Y > num192)
						{
							npc.velocity.Y = npc.velocity.Y - num189 * 1.1f;
						}
						if ((double)(System.Math.Abs(npc.velocity.X) + System.Math.Abs(npc.velocity.Y)) < (double)num188 * 0.5)
						{
							if (npc.velocity.X > 0f)
							{
								npc.velocity.X = npc.velocity.X + num189;
							}
							else
							{
								npc.velocity.X = npc.velocity.X - num189;
							}
						}
					}
				}
			}
		}

		public override bool CanHitPlayer(Player target, ref int cooldownSlot)
		{
			cooldownSlot = 0;
			return (npc.alpha == 0 && invinceTime <= 0);
		}

		public override bool StrikeNPC(ref double damage, int defense, ref float knockback, int hitDirection, ref bool crit)
		{
			if (damage > npc.lifeMax / 2)
			{
				damage = 0;
				return false;
			}
			double protection = CalamityWorld.death ? 0.03 : 0.045;
			damage = (int)((double)damage * protection);
			return true;
		}

		public override bool CheckActive()
		{
			return false;
		}

		public override bool PreNPCLoot()
		{
			return false;
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (npc.life <= 0)
			{
				float randomSpread = (float)(Main.rand.Next(-100, 100) / 100);
				Gore.NewGore(npc.position, npc.velocity * randomSpread * Main.rand.NextFloat(), mod.GetGoreSlot("Gores/DoGS3"), 1f);
				Gore.NewGore(npc.position, npc.velocity * randomSpread * Main.rand.NextFloat(), mod.GetGoreSlot("Gores/DoGS4"), 1f);
				Gore.NewGore(npc.position, npc.velocity * randomSpread * Main.rand.NextFloat(), mod.GetGoreSlot("Gores/DoGS5"), 1f);
				npc.position.X = npc.position.X + (float)(npc.width / 2);
				npc.position.Y = npc.position.Y + (float)(npc.height / 2);
				npc.width = 50;
				npc.height = 50;
				npc.position.X = npc.position.X - (float)(npc.width / 2);
				npc.position.Y = npc.position.Y - (float)(npc.height / 2);
				for (int num621 = 0; num621 < 10; num621++)
				{
					int num622 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, 173, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num622].velocity *= 3f;
					if (Main.rand.Next(2) == 0)
					{
						Main.dust[num622].scale = 0.5f;
						Main.dust[num622].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
					}
				}
				for (int num623 = 0; num623 < 20; num623++)
				{
					int num624 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, 173, 0f, 0f, 100, default(Color), 3f);
					Main.dust[num624].noGravity = true;
					Main.dust[num624].velocity *= 5f;
					num624 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, 173, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num624].velocity *= 2f;
				}
			}
		}

		public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
		{
			npc.lifeMax = (int)(npc.lifeMax * 0.8f * bossLifeScale);
			npc.damage = (int)(npc.damage * 0.8f);
		}

		public override void OnHitPlayer(Player player, int damage, bool crit)
		{
			player.AddBuff(mod.BuffType("GodSlayerInferno"), 240, true);
			player.AddBuff(mod.BuffType("WhisperingDeath"), 300, true);
			player.AddBuff(BuffID.Frostburn, 240, true);
			player.AddBuff(BuffID.Darkness, 240, true);
		}
	}
}