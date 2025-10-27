using System;
using System.Threading;
using NCMS;
using UnityEngine;
using ReflectionUtility;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using HarmonyLib;

namespace PeerlessThedayofGodswrath.code
{
    internal class SwordImmortalFlyingSword
    {
        public static void Init()
        {
            AssetManager.projectiles.add(new ProjectileAsset
            {
                id = "MagicArrow", // 新ID
                speed = 20f,
                speed_random = 5f, // 速度随机值
                texture = "arrow", // 你的贴图名（需放在effects/projectiles/thesword.png）
                texture_shadow = "shadows/projectiles/shadow_arrow", // 原版箭影子
                sound_launch = "event:/SFX/WEAPONS/WeaponStartArrow", // 原版箭发射音效
                sound_impact = "event:/SFX/HIT/HitGeneric", // 原版箭命中音效
                can_be_left_on_ground = false, // 命中后可留在地面
                can_be_blocked = false, // 可被格挡
                look_at_target = true, // 朝向目标
                can_be_collided = false, // 可被碰撞
            });

            AssetManager.projectiles.add(new ProjectileAsset
            {
                id = "fx_wind_trail_t", // 新ID
                speed = 20f, // 原版arrow速度
                speed_random = 3f, // 速度随机值
                texture = "fx_wind_trail_t", // 你的贴图名（需放在effects/projectiles/thesword.png）
                texture_shadow = "shadows/projectiles/shadow_arrow", // 原版箭影子
                sound_launch = "event:/SFX/WEAPONS/WeaponStartArrow", // 原版箭发射音效
                sound_impact = "event:/SFX/HIT/HitGeneric", // 原版箭命中音效
                scale_start = 0.1f, // 初始缩放
                scale_target = 0.1f, // 目标缩放
                can_be_left_on_ground = false, // 命中后可留在地面
                can_be_blocked = false, // 可被格挡
                animated = true, // 启用动画
                animation_speed = 8f, // 动画速度
                look_at_target = true, // 朝向目标
                can_be_collided = false, // 可被碰撞
            });

            AssetManager.projectiles.add(new ProjectileAsset
            {
                id = "Thesunsets", // 新ID
                speed = 20f, // 原版arrow速度
                speed_random = 3f, // 速度随机值
                texture = "Thesunsets", // 你的贴图名（需放在effects/projectiles/thesword.png）
                texture_shadow = "shadows/projectiles/shadow_arrow", // 原版箭影子
                sound_launch = "event:/SFX/WEAPONS/WeaponStartArrow", // 原版箭发射音效
                sound_impact = "event:/SFX/HIT/HitGeneric", // 原版箭命中音效
                terraform_option = "NuclearFusion_to",
				terraform_range = 30,										//地形範圍
				trigger_on_collision = true,
                scale_start = 0.1f, // 初始缩放
                scale_target = 0.1f, // 目标缩放
                can_be_left_on_ground = false, // 命中后可留在地面
                can_be_blocked = false, // 可被格挡
                animated = true, // 启用动画
                animation_speed = 8f, // 动画速度
                look_at_target = true, // 朝向目标
                can_be_collided = false, // 可被碰撞
            });

            AssetManager.terraform.add(new TerraformOptions{// true / false
					id = "NuclearFusion_to",					//ID
					flash = true,						//閃光?動畫?
					explode_tile = true,				//爆炸
					apply_force = true,					//震飛
					remove_top_tile = true,				//移除頂部圖塊
					remove_lava = false,				//移除岩漿
					remove_fire = false,				//移除火
					remove_frozen = true,				//移除冰
					remove_water = true,				//移除水
					remove_tornado = true,				//移除旋風
					set_fire = true,					//設置火焰
					add_heat = 404,						//添加熱量
					add_burned = true,					//添加燒毀
					transform_to_wasteland = false,		//廢土化
					applies_to_high_flyers = true,		//適用於高飛者
					destroy_buildings = true,			//摧毀房屋
					damage_buildings = true,			//傷害房屋
					make_ruins = true,					//製造廢墟
					force_power = 4.50f,				//震飛強度
					explode_strength = 1,				//爆炸強度
					explode_and_set_random_fire = true,	//爆炸火焰
					lightning_effect = true,			//雷電效果
					shake = true,						//震動
					shake_intensity = 10.0f,			//震動強度
			});
        }
    }
}