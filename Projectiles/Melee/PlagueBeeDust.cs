﻿using CalamityMod.Buffs.DamageOverTime;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;

namespace CalamityMod.Projectiles.Melee
{
    public class PlagueBeeDust : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dust");
            Main.projFrames[projectile.type] = 4;
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 4;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }

        public override void SetDefaults()
        {
            projectile.width = 70;
            projectile.height = 70;
            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
            projectile.melee = true;
            projectile.penetrate = -1;
            projectile.extraUpdates = 3;
            projectile.timeLeft = 150;
        }

        public override void AI()
        {
            projectile.ai[1]++;
            Lighting.AddLight(projectile.Center, 0.05f, 0.4f, 0f);
            if (projectile.ai[1] < 60f)
            {
                if (projectile.ai[0] > 7f)
                {
                    float num296 = 1f;
                    if (projectile.ai[0] == 8f)
                    {
                        num296 = 0.25f;
                    }
                    else if (projectile.ai[0] == 9f)
                    {
                        num296 = 0.5f;
                    }
                    else if (projectile.ai[0] == 10f)
                    {
                        num296 = 0.75f;
                    }
                    projectile.ai[0] += 1f;
                    if (projectile.ai[0] % 2f == 0f)
                    {
                        int spawnX = (int)(projectile.width / 2);
                        int spawnY = (int)(projectile.width / 2);
                        int bee = Projectile.NewProjectile(projectile.Center.X + (float)Main.rand.Next(-spawnX, spawnX), projectile.Center.Y + (float)Main.rand.Next(-spawnY, spawnY),
                            projectile.velocity.X, projectile.velocity.Y, Main.player[projectile.owner].beeType(),
                            Main.player[projectile.owner].beeDamage(projectile.damage / 3), Main.player[projectile.owner].beeKB(0f), projectile.owner, 0f, 0f);
                        Main.projectile[bee].penetrate = 1;
						Main.projectile[bee].Calamity().forceMelee = true;
                    }
                    int num297 = 89;
                    //Dust
                    int num299 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, num297, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 100, default, 1f);
                    Dust dust = Main.dust[num299];
                    if (Main.rand.NextBool(3))
                    {
                        dust.noGravity = true;
                        dust.scale *= 1.8f;
                        dust.velocity.X *= 2f;
                        dust.velocity.Y *= 2f;
                    }
                    else
                    {
                        dust.scale *= 1.3f;
                    }
                    dust.velocity.X *= 1.2f;
                    dust.velocity.Y *= 1.2f;
                    dust.scale *= num296;

                }
                else
                {
                    projectile.ai[0] += 1f;
                }
            }
            else
            {
                projectile.damage = 0;
                projectile.velocity *= 0.99f;
                //Fade out
                if (projectile.alpha < 255)
                    projectile.alpha += 5;
                if (projectile.alpha > 255)
                    projectile.alpha = 255;
                if (projectile.ai[1] >= 150)
                    projectile.Kill();
            }

            //Rotation
            projectile.spriteDirection = projectile.direction = (projectile.velocity.X > 0).ToDirectionInt();
            projectile.rotation = projectile.velocity.ToRotation() + (projectile.spriteDirection == 1 ? 0f : MathHelper.Pi);

            //Animation
            projectile.frameCounter++;
            if (projectile.frameCounter > 8)
            {
                projectile.frame++;
                projectile.frameCounter = 0;
            }
            if (projectile.frame > 3)
            {
                projectile.frame = 0;
            }
        }  

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.immune[projectile.owner] = 5;
            target.AddBuff(ModContent.BuffType<Plague>(), 180);
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            CalamityGlobalProjectile.DrawCenteredAndAfterimage(projectile, lightColor, ProjectileID.Sets.TrailingMode[projectile.type], 1);
            return false;
        }
    }
}
