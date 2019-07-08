﻿using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Enums;
using Terraria.GameContent.Shaders;
using Terraria.Graphics.Effects;

namespace CalamityMod.Projectiles.Patreon
{
	public class DarkSparkBeam : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Beam");
		}

		public override void SetDefaults()
		{
			projectile.width = 18;
			projectile.height = 18;
			projectile.friendly = true;
			projectile.magic = true;
			projectile.penetrate = -1;
			projectile.alpha = 255;
			projectile.tileCollide = false;
			projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = 10;
		}

		public override void SendExtraAI(BinaryWriter writer)
		{
			writer.Write(projectile.localAI[1]);
		}

		public override void ReceiveExtraAI(BinaryReader reader)
		{
			projectile.localAI[1] = reader.ReadSingle();
		}

		public override void AI()
		{
			Vector2? vector71 = null;
			if (projectile.velocity.HasNaNs() || projectile.velocity == Vector2.Zero)
			{
				projectile.velocity = -Vector2.UnitY;
			}
			if (projectile.type != mod.ProjectileType("DarkSparkBeam") || !Main.projectile[(int)projectile.ai[1]].active || Main.projectile[(int)projectile.ai[1]].type != mod.ProjectileType("DarkSpark"))
			{
				projectile.Kill();
				return;
			}
			float num810 = (float)((int)projectile.ai[0]) - 2.5f;
			Vector2 value36 = Vector2.Normalize(Main.projectile[(int)projectile.ai[1]].velocity);
			Projectile projectile2 = Main.projectile[(int)projectile.ai[1]];
			float num811 = num810 * 0.5235988f;
			Vector2 value37 = Vector2.Zero;
			float num812; //0 to 1
			float y; //6 to 20
			float num813; //1.75 to 20
			float scaleFactor6; //-22 to -2
			Color color = new Color(1, 1, 1, 127);
			projectile.Opacity = 1f;
			if (projectile2.ai[0] < 720f)
			{
				num812 = 0f + projectile2.ai[0] / 1440f; //0 to 0.5
				y = 6f + projectile2.ai[0] / 720f * 7f;
				if (projectile2.ai[0] > 360f)
				{
					int colorValue = (int)((0.01f + (((projectile2.ai[0] - 360f) / 360f) * 2.55f)) * 100f);
					color = new Color(colorValue, colorValue, colorValue, 127);
				}
				if (projectile2.ai[0] < 480f)
				{
					num813 = 1.75f/* + 4f * (projectile2.ai[0] / 120f)*/; //20 to 16
				}
				else
				{
					num813 = 3f + 5f * ((projectile2.ai[0] - 480f) / 240f); //7.99 to 3.01
				}
				scaleFactor6 = -2f - projectile2.ai[0] / 720f * 5f;
			}
			else
			{
				float colorLimit = projectile2.ai[0] - 720f;
				if (colorLimit > 255f)
				{
					colorLimit = 255f;
				}
				switch ((int)projectile.ai[0]) //R O Y G B I V
				{
					case 0:
						color = new Color(255, 255 - (int)colorLimit, 255 - (int)colorLimit, 127); //red
						break;
					case 1:
						color = new Color(255, 255 - (int)(colorLimit * 0.3529412f), 255 - (int)colorLimit, 127); //orange
						break;
					case 2:
						color = new Color(255, 255, 255 - (int)colorLimit, 127); //yellow
						break;
					case 3:
						color = new Color(255 - (int)colorLimit, 255 - (int)(colorLimit * 0.5f), 255 - (int)colorLimit, 127); //green
						break;
					case 4:
						color = new Color(255 - (int)colorLimit, 255 - (int)colorLimit, 255, 127); //blue
						break;
					case 5:
						color = new Color(255 - (int)(colorLimit * 0.7058824f), 255 - (int)colorLimit, 255 - (int)(colorLimit * 0.4901961), 127); //indigo
						break;
					case 6:
						color = new Color(255 - (int)(colorLimit * 0.0666667f), 255 - (int)(colorLimit * 0.4901961), 255 - (int)(colorLimit * 0.0666667f), 127); //violet
						break;
				}
				num812 = 0.5f; //0f, inverted
				num813 = 10.875f; //1.75f, inverted
				y = 13f; //6f, inverted
				scaleFactor6 = -7f; //-2, inverted
			}
			float num814 = (projectile2.ai[0] + num810 * num813) / (num813 * 6f) * 6.28318548f;
			num811 = Vector2.UnitY.RotatedBy((double)num814, default(Vector2)).Y * 0.5235988f * num812;
			value37 = (Vector2.UnitY.RotatedBy((double)num814, default(Vector2)) * new Vector2(4f, y)).RotatedBy((double)projectile2.velocity.ToRotation(), default(Vector2));
			projectile.position = projectile2.Center + value36 * 16f - projectile.Size / 2f + new Vector2(0f, -Main.projectile[(int)projectile.ai[1]].gfxOffY);
			projectile.position += projectile2.velocity.ToRotation().ToRotationVector2() * scaleFactor6;
			projectile.position += value37;
			projectile.velocity = Vector2.Normalize(projectile2.velocity).RotatedBy((double)num811, default(Vector2));
			projectile.scale = 1.5f * (1.5f - num812);
			projectile.damage = projectile2.damage;
			double damageMult = 1.0 + (double)(projectile2.ai[0] * 0.0015f); //1 to 2.0 (1400 * x)
			if (damageMult > 2.0)
			{
				damageMult = 2.0;
			}
			if (projectile2.ai[0] >= 720f)
			{
				projectile.damage = (int)((double)projectile.damage * damageMult);
				vector71 = new Vector2?(projectile2.Center);
			}
			if (!Collision.CanHitLine(Main.player[projectile.owner].Center, 0, 0, projectile2.Center, 0, 0))
			{
				vector71 = new Vector2?(Main.player[projectile.owner].Center);
			}
			projectile.friendly = (projectile2.ai[0] > 120f);
			if (projectile.velocity.HasNaNs() || projectile.velocity == Vector2.Zero)
			{
				projectile.velocity = -Vector2.UnitY;
			}
			float num818 = projectile.velocity.ToRotation();
			projectile.rotation = num818 - 1.57079637f;
			projectile.velocity = num818.ToRotationVector2();
			float num819 = 2f;
			float num820 = 0f;
			Vector2 samplingPoint = projectile.Center;
			if (vector71.HasValue)
			{
				samplingPoint = vector71.Value;
			}
			float[] array3 = new float[(int)num819];
			Collision.LaserScan(samplingPoint, projectile.velocity, num820 * projectile.scale, 2400f, array3);
			float num821 = 0f;
			for (int num822 = 0; num822 < array3.Length; num822++)
			{
				num821 += array3[num822];
			}
			num821 /= num819;
			float amount = 0.75f;
			projectile.localAI[1] = MathHelper.Lerp(projectile.localAI[1], num821, amount);
			if (Math.Abs(projectile.localAI[1] - num821) < 100f && projectile.scale > 0.15f)
			{
				//color.A = 0;
				Vector2 vector80 = projectile.Center + projectile.velocity * (projectile.localAI[1] - 14.5f * projectile.scale);
				for (int num843 = 0; num843 < 2; num843++)
				{
					float num844 = projectile.velocity.ToRotation() + ((Main.rand.Next(2) == 1) ? -1f : 1f) * 1.57079637f;
					float num845 = (float)Main.rand.NextDouble() * 0.8f + 1f;
					Vector2 vector81 = new Vector2((float)Math.Cos((double)num844) * num845, (float)Math.Sin((double)num844) * num845);
					int num846 = Dust.NewDust(vector80, 0, 0, 267, vector81.X, vector81.Y, 0, color, 3.3f); //267
					Main.dust[num846].color = color;
					Main.dust[num846].scale = 1.2f;
					if (projectile.scale > 1f)
					{
						Main.dust[num846].velocity *= projectile.scale;
						Main.dust[num846].scale *= projectile.scale;
					}
					Main.dust[num846].noGravity = true;
					if (projectile.scale != 1.4f)
					{
						Dust dust9 = Dust.CloneDust(num846);
						dust9.color = color;
						dust9.scale /= 2f;
						dust9.noGravity = true;
					}
					Main.dust[num846].color = color;
				}
				if (Main.rand.Next(5) == 0)
				{
					Vector2 value42 = projectile.velocity.RotatedBy(1.5707963705062866, default(Vector2)) * ((float)Main.rand.NextDouble() - 0.5f) * (float)projectile.width;
					int num847 = Dust.NewDust(vector80 + value42 - Vector2.One * 4f, 8, 8, 267, 0f, 0f, 100, color, 5f);
					Main.dust[num847].velocity *= 0.5f;
					Main.dust[num847].noGravity = true;
					Main.dust[num847].velocity.Y = -Math.Abs(Main.dust[num847].velocity.Y);
				}
				DelegateMethods.v3_1 = color.ToVector3() * 0.3f;
				float value43 = 0.1f * (float)Math.Sin((double)(Main.GlobalTime * 20f));
				Vector2 size = new Vector2(projectile.velocity.Length() * projectile.localAI[1], (float)projectile.width * projectile.scale);
				float num848 = projectile.velocity.ToRotation();
				if (Main.netMode != 2)
				{
					((WaterShaderData)Filters.Scene["WaterDistortion"].GetShader()).QueueRipple(projectile.position + new Vector2(size.X * 0.5f, 0f).RotatedBy((double)num848, default(Vector2)), color, size, RippleShape.Square, num848);
				}
				Utils.PlotTileLine(projectile.Center, projectile.Center + projectile.velocity * projectile.localAI[1], (float)projectile.width * projectile.scale, new Utils.PerLinePoint(DelegateMethods.CastLight));
				return;
			}
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			if (projectile.velocity == Vector2.Zero)
			{
				return false;
			}
			Texture2D tex = Main.projectileTexture[projectile.type];
			float num228 = projectile.localAI[1];
			Projectile projectile2 = Main.projectile[(int)projectile.ai[1]];
			Color color = new Color(1, 1, 1, 127);
			if (projectile2.ai[0] < 720f)
			{
				if (projectile2.ai[0] > 360f)
				{
					int colorValue = (int)((0.01f + (((projectile2.ai[0] - 360f) / 360f) * 2.55f)) * 100f);
					color = new Color(colorValue, colorValue, colorValue, 127);
				}
			}
			else
			{
				float colorLimit = projectile2.ai[0] - 720f;
				if (colorLimit > 255f)
				{
					colorLimit = 255f;
				}
				switch ((int)projectile.ai[0]) //R O Y G B I V
				{
					case 0:
						color = new Color(255, 255 - (int)colorLimit, 255 - (int)colorLimit, 127); //red
						break;
					case 1:
						color = new Color(255, 255 - (int)(colorLimit * 0.3529412f), 255 - (int)colorLimit, 127); //orange
						break;
					case 2:
						color = new Color(255, 255, 255 - (int)colorLimit, 127); //yellow
						break;
					case 3:
						color = new Color(255 - (int)colorLimit, 255 - (int)(colorLimit * 0.5f), 255 - (int)colorLimit, 127); //green
						break;
					case 4:
						color = new Color(255 - (int)colorLimit, 255 - (int)colorLimit, 255, 127); //blue
						break;
					case 5:
						color = new Color(255 - (int)(colorLimit * 0.7058824f), 255 - (int)colorLimit, 255 - (int)(colorLimit * 0.4901961), 127); //indigo
						break;
					case 6:
						color = new Color(255 - (int)(colorLimit * 0.0666667f), 255 - (int)(colorLimit * 0.4901961), 255 - (int)(colorLimit * 0.0666667f), 127); //violet
						break;
				}
			}
			Microsoft.Xna.Framework.Color value25 = color;
			//value25.A = 0;
			Vector2 value26 = projectile.Center.Floor();
			value26 += projectile.velocity * projectile.scale * 10.5f;
			num228 -= projectile.scale * 14.5f * projectile.scale;
			Vector2 vector29 = new Vector2(projectile.scale);
			DelegateMethods.f_1 = 1f;
			DelegateMethods.c_1 = value25 * 0.75f * projectile.Opacity;
			Vector2 projPos = projectile.oldPos[0];
			projPos = new Vector2((float)projectile.width, (float)projectile.height) / 2f + Vector2.UnitY * projectile.gfxOffY - Main.screenPosition;
			Utils.DrawLaser(Main.spriteBatch, tex, value26 - Main.screenPosition, value26 + projectile.velocity * num228 - Main.screenPosition, vector29, new Utils.LaserLineFraming(DelegateMethods.RainbowLaserDraw));
			DelegateMethods.c_1 = color * 0.75f * projectile.Opacity;
			Utils.DrawLaser(Main.spriteBatch, tex, value26 - Main.screenPosition, value26 + projectile.velocity * num228 - Main.screenPosition, vector29 / 2f, new Utils.LaserLineFraming(DelegateMethods.RainbowLaserDraw));
			return false;
		}

		public override void CutTiles()
		{
			DelegateMethods.tilecut_0 = TileCuttingContext.AttackProjectile;
			Vector2 unit = projectile.velocity;
			Utils.PlotTileLine(projectile.Center, projectile.Center + unit * projectile.localAI[1], (float)projectile.width * projectile.scale, new Utils.PerLinePoint(DelegateMethods.CutTiles));
		}

		public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
		{
			if (projHitbox.Intersects(targetHitbox))
			{
				return true;
			}
			float num6 = 0f;
			if (Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), projectile.Center, projectile.Center + projectile.velocity * projectile.localAI[1], 22f * projectile.scale, ref num6))
			{
				return true;
			}
			return false;
		}
	}
}