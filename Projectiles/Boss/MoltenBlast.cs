﻿using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Boss
{
	public class MoltenBlast : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Molten Blast");
			Main.projFrames[projectile.type] = 4;
		}

		public override void SetDefaults()
		{
			projectile.width = 40;
			projectile.height = 40;
			projectile.hostile = true;
			projectile.penetrate = 1;
			projectile.timeLeft = 90;
			cooldownSlot = 1;
		}

		public override void SendExtraAI(BinaryWriter writer)
		{
			writer.Write(projectile.localAI[0]);
		}

		public override void ReceiveExtraAI(BinaryReader reader)
		{
			projectile.localAI[0] = reader.ReadSingle();
		}

		public override void AI()
		{
			projectile.frameCounter++;
			if (projectile.frameCounter > 6)
			{
				projectile.frame++;
				projectile.frameCounter = 0;
			}
			if (projectile.frame > 3)
			{
				projectile.frame = 0;
			}
			if (projectile.wet || projectile.lavaWet)
			{
				projectile.Kill();
			}
			if (projectile.ai[1] == 0f)
			{
				for (int num621 = 0; num621 < 10; num621++)
				{
					int num622 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 244, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num622].velocity *= 3f;
					if (Main.rand.Next(2) == 0)
					{
						Main.dust[num622].scale = 0.5f;
						Main.dust[num622].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
					}
				}
				projectile.ai[1] = 1f;
				Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 20);
			}
			projectile.localAI[0] += 1f;
			if (projectile.localAI[0] == 30f)
			{
				projectile.localAI[0] = 0f;
				for (int l = 0; l < 12; l++)
				{
					Vector2 vector3 = Vector2.UnitX * (float)(-(float)projectile.width) / 2f;
					vector3 += -Vector2.UnitY.RotatedBy((double)((float)l * 3.14159274f / 6f), default(Vector2)) * new Vector2(8f, 16f);
					vector3 = vector3.RotatedBy((double)(projectile.rotation - 1.57079637f), default(Vector2));
					int num9 = Dust.NewDust(projectile.Center, 0, 0, 244, 0f, 0f, 160, default(Color), 1f);
					Main.dust[num9].scale = 1.1f;
					Main.dust[num9].noGravity = true;
					Main.dust[num9].position = projectile.Center + vector3;
					Main.dust[num9].velocity = projectile.velocity * 0.1f;
					Main.dust[num9].velocity = Vector2.Normalize(projectile.Center - projectile.velocity * 3f - Main.dust[num9].position) * 1.25f;
				}
			}
			projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
		}

		public override Color? GetAlpha(Color lightColor)
		{
			return new Color(250, 150, 0, projectile.alpha);
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Texture2D texture2D13 = Main.projectileTexture[projectile.type];
			int num214 = Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type];
			int y6 = num214 * projectile.frame;
			Main.spriteBatch.Draw(texture2D13, projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(0, y6, texture2D13.Width, num214)), projectile.GetAlpha(lightColor), projectile.rotation, new Vector2((float)texture2D13.Width / 2f, (float)num214 / 2f), projectile.scale, SpriteEffects.None, 0f);
			return false;
		}

		public override void Kill(int timeLeft)
		{
			int num251 = Main.rand.Next(5, 10);
			if (projectile.owner == Main.myPlayer)
			{
				for (int num252 = 0; num252 < num251; num252++)
				{
					Vector2 value15 = new Vector2((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101));
					while (value15.X == 0f && value15.Y == 0f)
					{
						value15 = new Vector2((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101));
					}
					value15.Normalize();
					value15 *= (float)Main.rand.Next(70, 121) * 0.1f; //70 101
					Projectile.NewProjectile(projectile.oldPosition.X + (float)(projectile.width / 2), projectile.oldPosition.Y + (float)(projectile.height / 2), value15.X, value15.Y, mod.ProjectileType("MoltenBlob"), projectile.damage, 0f, projectile.owner, 0f, 0f);
				}
			}
			Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 20);
			for (int num193 = 0; num193 < 3; num193++)
			{
				Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 244, 0f, 0f, 50, default(Color), 1.5f);
			}
			for (int num194 = 0; num194 < 30; num194++)
			{
				int num195 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 244, 0f, 0f, 0, default(Color), 2.5f);
				Main.dust[num195].noGravity = true;
				Main.dust[num195].velocity *= 3f;
				num195 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 244, 0f, 0f, 50, default(Color), 1.5f);
				Main.dust[num195].velocity *= 2f;
				Main.dust[num195].noGravity = true;
			}
		}

		public override void OnHitPlayer(Player target, int damage, bool crit)
		{
			target.AddBuff(mod.BuffType("HolyLight"), 180);
		}
	}
}