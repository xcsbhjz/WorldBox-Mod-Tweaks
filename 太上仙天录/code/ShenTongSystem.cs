using System;
using System.Collections.Generic;
using UnityEngine;
using VideoCopilot.code.utils;

namespace XianTu.code
{
    public class ShenTongSystem
    {
        // ========== 神通命名相关功能 ==========
        // 各层次神通名称列表 - 法术级别（3字，以术结尾）
        public static List<string> level1ShenTongNames = new List<string> {
            "火球术", "水球术", "金刃术", "木藤术", "土墙术",
            "风刃术", "雷弧术", "冰锥术", "火弹术", "水箭术",
            "金剑术", "木刺术", "土盾术", "风墙术", "雷网术",
            "冰甲术", "火球术", "水幕术", "金光术", "木盾术",
            "土刺术", "风刃术", "雷球术", "冰锥术", "火墙术",
            "水浪术", "金刀术", "木藤术", "土山术", "风卷术",
            "木剑术", "土甲术", "风盾术", "雷链术", "冰盾术"
        };

        // 小神通级别（4字）
        public static List<string> level2ShenTongNames = new List<string> {
            "五雷正法", "三昧真火", "玄冰神决", "风卷残云", "大地之力",
            "金刚不坏", "万物生长", "碧海潮生", "旭日东升", "夜影随行",
            "神魂出窍", "移形换影", "缩地成寸", "天眼通神", "天耳通神",
            "他心通神", "宿命通神", "神足通神", "漏尽通神", "如意变化",
            "九霄神雷", "焚天圣火", "玄冰寒狱", "风雷神速", "大地脉动",
            "金刚不坏", "生机盎然", "沧海横流", "烈日当空", "月影重重",
            "神魂离体", "瞬息万变", "缩地成寸", "天眼洞察", "天耳聆听",
            "他心感知", "宿命通晓", "神足纵地", "漏尽通玄", "如意随心"
        };

        // 大神通级别（5字）
        public static List<string> level3ShenTongNames = new List<string> {
            "翻江倒海诀", "呼风唤雨术", "撒豆成兵法", "点石成金手", "移山填海印",
            "斗转星移功", "时空静止术", "生死大手印", "大日如来掌", "混沌初开印",
            "天地熔炉法", "万剑归宗诀", "一气化三清", "袖里乾坤功", "掌心雷法诀",
            "九字真言咒", "六丁六甲阵", "五方揭谛印", "四值功曹法", "三官大帝咒",
            "九霄神雷诀", "焚天圣火术", "玄冰寒狱印", "风雷神速法", "大地脉动功",
            "金刚不坏身", "生机盎然诀", "沧海横流术", "烈日当空印", "月影重重法",
            "神魂出窍功", "移形换影术", "缩地成寸诀", "天眼通神印", "天耳通神法",
            "他心通神功", "宿命通神术", "神足通神诀", "漏尽通神印", "如意变化法"
        };

        // 天地大神通级别（6字）
        public static List<string> level4ShenTongNames = new List<string> {
            "开天辟地神诀", "混沌灭世神印", "大道无形神功", "虚无缥缈神术", "天地同寿神法",
            "宇宙洪荒神诀", "造化玉牒神印", "太极图录神功", "盘古幡法神术", "混沌钟鸣神法",
            "诛仙四剑神诀", "十二品莲台印", "乾坤鼎镇神功", "山河社稷图术", "河图洛书神法",
            "天书降世神诀", "地书现世神印", "冥书显世神功", "人书问世神术", "生死簿录神法",
            "九霄神雷天罚", "焚天圣火焚世", "玄冰寒狱封天", "风雷神速追日", "大地脉动镇狱",
            "金刚不坏圣体", "生机盎然回春", "沧海横流吞日", "烈日当空焚世", "月影重重遮天",
            "神魂出窍神游", "移形换影幻影", "缩地成寸缩界", "天眼通神观天", "天耳通神听地",
            "他心通神知心", "宿命通神知命", "神足通神纵地", "漏尽通神尽漏", "如意变化随心"
        };

        // 无上妙法级别（7字）
        public static List<string> level5ShenTongNames = new List<string> {
            "大道至简虚无道", "无极生太极妙法", "太一生水创世诀", "道生一混沌初开", "一生二阴阳交感",
            "二生三万物化生", "三生万物造化功", "天人合一混元道", "道法自然无为诀", "无为而治天地法",
            "混元归一太初道", "万法归宗虚空诀", "天地大同合一道", "宇宙本源鸿蒙功", "鸿蒙初辟创世法",
            "太初有道太虚诀", "有无相生自然法", "难易相成变化功", "长短相较平衡道", "高下相倾大势法",
            "开天辟地创世诀", "混沌灭世终极法", "大道形虚无妙法", "虚无缥缈太初法", "天地同寿永生法",
            "宇宙洪荒演化诀", "造化玉牒天机道", "太极图录混元功", "盘古幡法混沌法", "混沌钟鸣镇世法",
            "诛仙四剑破灭诀", "十二品莲台净世", "乾坤鼎镇造化道", "山河社稷图录法", "河图洛书天机法",
            "天书降世开天诀", "地书现世辟地功", "冥书显世渡厄道", "人书问世度人法", "生死簿录轮回法",
            "九霄神雷天罚诀", "焚天圣火焚世功", "玄冰寒狱封天道", "风雷神速追日法", "大地脉动镇狱法",
            "金刚不坏圣体诀", "生机盎然回春功", "沧海横流吞日道", "烈日当空焚世法", "月影重重遮天法",
            "神魂出窍神游诀", "移形换影幻影功", "缩地成寸缩界道", "天眼通神观天法", "天耳通神听地法",
            "他心通神知心诀", "宿命通神知命功", "神足通神纵地道", "漏尽通神尽漏法", "如意变化随心法"
        };

        // 获取随机神通名称
        public static string GetRandomShenTongName(int level = 1)
        {
            List<string> nameList;
            
            switch (level)
            {
                case 1:
                    nameList = level1ShenTongNames;
                    break;
                case 2:
                    nameList = level2ShenTongNames;
                    break;
                case 3:
                    nameList = level3ShenTongNames;
                    break;
                case 4:
                    nameList = level4ShenTongNames;
                    break;
                case 5:
                    nameList = level5ShenTongNames;
                    break;
                default:
                    return "无";
            }
            
            return nameList[UnityEngine.Random.Range(0, nameList.Count)];
        }

        // 获取神通层次名称
        public static string GetShenTongLevelName(int level)
        {
            switch (level)
            {
                case 1:
                    return "法术";
                case 2:
                    return "小神通";
                case 3:
                    return "大神通";
                case 4:
                    return "天地大神通";
                case 5:
                    return "无上妙法神通";
                default:
                    return "无";
            }
        }

        // ========== 神通伤害相关功能 ==========
        // 基于神通层次的真实伤害攻击动作
        public static bool ShenTongTrueDamage_AttackAction(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget == null || !pTarget.isActor() || pSelf == null || !pSelf.isActor())
                return false;

            Actor attacker = pSelf.a;
            Actor targetActor = pTarget.a;
            
            // 获取攻击者的神通层次
            int shenTongLevel = attacker.GetShenTongLevel();
            
            // 获取攻击者的攻击属性值
            float attackValue = attacker.stats["damage"];
            
            // 初始化伤害倍数
            float damageMultiplier = 0f;
            bool isEffective = true;
            
            // 根据神通层次确定伤害倍数和有效目标范围
            switch (shenTongLevel)
            {
                case 1: // 法术层次
                    damageMultiplier = 0.1f;
                    // 检查目标是否为金丹及以上修士或神游及以上层次的武道修士
                    if (targetActor.hasTrait("XianTu3") || targetActor.hasTrait("XianTu4") || 
                        targetActor.hasTrait("XianTu5") || targetActor.hasTrait("XianTu6") || 
                        targetActor.hasTrait("XianTu7") || targetActor.hasTrait("XianTu8") ||
                        targetActor.hasTrait("XianTu9") || targetActor.hasTrait("XianTu91") ||
                        targetActor.hasTrait("TyWudao7") || targetActor.hasTrait("TyWudao8") ||
                        targetActor.hasTrait("TyWudao9") || targetActor.hasTrait("TyWudao91") ||
                        targetActor.hasTrait("TyWudao92") || targetActor.hasTrait("TyWudao93") ||
                        targetActor.hasTrait("TyWudao94"))
                    {
                        isEffective = false;
                    }
                    break;
                case 2: // 小神通层次
                    damageMultiplier = 0.3f;
                    // 检查目标是否为化神及以上修士或明我及以上层次的武道修士
                    if (targetActor.hasTrait("XianTu5") || targetActor.hasTrait("XianTu6") || 
                        targetActor.hasTrait("XianTu7") || targetActor.hasTrait("XianTu8") ||
                        targetActor.hasTrait("XianTu9") || targetActor.hasTrait("XianTu91") ||
                        targetActor.hasTrait("TyWudao8") || targetActor.hasTrait("TyWudao9") ||
                        targetActor.hasTrait("TyWudao91") || targetActor.hasTrait("TyWudao92") ||
                        targetActor.hasTrait("TyWudao93") || targetActor.hasTrait("TyWudao94"))
                    {
                        isEffective = false;
                    }
                    break;
                case 3: // 大神通层次
                    damageMultiplier = 0.5f;
                    // 检查目标是否为大乘及以上修士或圣躯及以上层次的武道修士
                    if (targetActor.hasTrait("XianTu7") || targetActor.hasTrait("XianTu8") ||
                        targetActor.hasTrait("XianTu9") || targetActor.hasTrait("XianTu91") ||
                        targetActor.hasTrait("TyWudao91") || targetActor.hasTrait("TyWudao92") ||
                        targetActor.hasTrait("TyWudao93") || targetActor.hasTrait("TyWudao94"))
                    {
                        isEffective = false;
                    }
                    break;
                case 4: // 天地大神通层次
                    damageMultiplier = 1f;
                    // 检查目标是否为半仙或证帝及以上层次的武道修士
                    if (targetActor.hasTrait("XianTu8") || targetActor.hasTrait("XianTu9") ||
                        targetActor.hasTrait("XianTu91") || targetActor.hasTrait("TyWudao92") ||
                        targetActor.hasTrait("TyWudao93") || targetActor.hasTrait("TyWudao94"))
                    {
                        isEffective = false;
                    }
                    break;
                case 5: // 无上妙法神通层次
                    damageMultiplier = 10f;
                    // 无上妙法神通对所有境界都有效
                    isEffective = true;
                    break;
                default:
                    // 未觉醒神通
                    isEffective = false;
                    break;
            }
            
            // 施加真实伤害
            if (isEffective && damageMultiplier > 0 && targetActor.data.health > 0)
            {
                // 计算真实伤害
                int trueDamage = Mathf.RoundToInt(attackValue * damageMultiplier);
                
                // 确保至少造成1点伤害
                if (trueDamage < 1) trueDamage = 1;
                
                // 施加真实伤害（受位格减伤限制）
                ActorExtensions.ApplyTrueDamageWithRealmReduction(targetActor, trueDamage, attacker);
                
                // 检查目标是否死亡
                if (targetActor.data.health <= 0)
                {
                    targetActor.batch.c_check_deaths.Add(targetActor);
                }
                
                // 添加视觉效果
                AssetManager.terraform.get("lightning_normal").apply_force = false;
                MapBox.spawnLightningMedium(pTile, 0.1f);
            }
            
            return false; // 允许后续攻击动作继续执行
        }
        
        // 基于真元(XianTu)境界的真实伤害攻击动作
        public static bool XianTuTrueDamage_AttackAction(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget == null || !pTarget.isActor() || pSelf == null || !pSelf.isActor())
                return false;

            Actor attacker = pSelf.a;
            Actor targetActor = pTarget.a;
            
            // 初始化伤害倍数和效果标记
            float damageMultiplier = 0f;
            bool isEffective = true;
            bool isWuDaoDamage = false; // 标记是否为武道气血伤害
            
            // 首先检查攻击者是否为武道修士并应用气血伤害
            // 真意和玄罡境界（1倍气血伤害）
            if (attacker.hasTrait("TyWudao3") || attacker.hasTrait("TyWudao4"))
            {
                damageMultiplier = 1f;
                isWuDaoDamage = true;
                // 对筑基及以上仙道修士无效，对天人及以上武道修士无效
                if (targetActor.hasTrait("XianTu2") || targetActor.hasTrait("XianTu3") || 
                    targetActor.hasTrait("XianTu4") || targetActor.hasTrait("XianTu5") || 
                    targetActor.hasTrait("XianTu6") || targetActor.hasTrait("XianTu7") || 
                    targetActor.hasTrait("XianTu8") || targetActor.hasTrait("TyWudao5") || 
                    targetActor.hasTrait("TyWudao6") || targetActor.hasTrait("TyWudao7") || 
                    targetActor.hasTrait("TyWudao8") || targetActor.hasTrait("TyWudao9") || 
                    targetActor.hasTrait("TyWudao91") || targetActor.hasTrait("TyWudao92") ||
                    targetActor.hasTrait("TyWudao93") || targetActor.hasTrait("TyWudao94") ||
                    targetActor.hasTrait("XianTu9") || targetActor.hasTrait("XianTu91"))
                {
                    isEffective = false;
                }
            }
            // 天人境界（2倍气血伤害）
            else if (attacker.hasTrait("TyWudao5"))
            {
                damageMultiplier = 2f;
                isWuDaoDamage = true;
                // 对金丹及以上仙道修士无效，对神游及以上武道修士无效
                if (targetActor.hasTrait("XianTu3") || targetActor.hasTrait("XianTu4") || 
                    targetActor.hasTrait("XianTu5") || targetActor.hasTrait("XianTu6") || 
                    targetActor.hasTrait("XianTu7") || targetActor.hasTrait("XianTu8") || 
                    targetActor.hasTrait("TyWudao6") || targetActor.hasTrait("TyWudao7") || 
                    targetActor.hasTrait("TyWudao8") || targetActor.hasTrait("TyWudao9") || 
                    targetActor.hasTrait("TyWudao91") || targetActor.hasTrait("TyWudao92") ||
                    targetActor.hasTrait("TyWudao93") || targetActor.hasTrait("TyWudao94") ||
                    targetActor.hasTrait("XianTu9") || targetActor.hasTrait("XianTu91"))
                {
                    isEffective = false;
                }
            }
            // 神游境界（3倍气血伤害）
            else if (attacker.hasTrait("TyWudao6"))
            {
                damageMultiplier = 3f;
                isWuDaoDamage = true;
                // 对元婴及以上仙道修士无效，对踏虚及以上武道修士无效
                if (targetActor.hasTrait("XianTu4") || targetActor.hasTrait("XianTu5") || 
                    targetActor.hasTrait("XianTu6") || targetActor.hasTrait("XianTu7") || 
                    targetActor.hasTrait("XianTu8") || targetActor.hasTrait("TyWudao7") || 
                    targetActor.hasTrait("TyWudao8") || targetActor.hasTrait("TyWudao9") || 
                    targetActor.hasTrait("TyWudao91") || targetActor.hasTrait("TyWudao92") ||
                    targetActor.hasTrait("TyWudao93") || targetActor.hasTrait("TyWudao94") ||
                    targetActor.hasTrait("XianTu9") || targetActor.hasTrait("XianTu91"))
                {
                    isEffective = false;
                }
            }
            // 踏虚境界（4倍气血伤害）
            else if (attacker.hasTrait("TyWudao7"))
            {
                damageMultiplier = 4f;
                isWuDaoDamage = true;
                // 对化神及以上仙道修士无效，对明我及以上武道修士无效
                if (targetActor.hasTrait("XianTu5") || targetActor.hasTrait("XianTu6") || 
                    targetActor.hasTrait("XianTu7") || targetActor.hasTrait("XianTu8") || 
                    targetActor.hasTrait("TyWudao8") || targetActor.hasTrait("TyWudao9") || 
                    targetActor.hasTrait("TyWudao91") || targetActor.hasTrait("TyWudao92") ||
                    targetActor.hasTrait("TyWudao93") || targetActor.hasTrait("TyWudao94") ||
                    targetActor.hasTrait("XianTu9") || targetActor.hasTrait("XianTu91"))
                {
                    isEffective = false;
                }
            }
            // 明我境界（5倍气血伤害）
            else if (attacker.hasTrait("TyWudao8"))
            {
                damageMultiplier = 5f;
                isWuDaoDamage = true;
                // 对合体及以上仙道修士无效，对山海及以上武道修士无效
                if (targetActor.hasTrait("XianTu6") || targetActor.hasTrait("XianTu7") || 
                    targetActor.hasTrait("XianTu8") || targetActor.hasTrait("TyWudao9") || 
                    targetActor.hasTrait("TyWudao91") || targetActor.hasTrait("TyWudao92") ||
                    targetActor.hasTrait("TyWudao93") || targetActor.hasTrait("TyWudao94") ||
                    targetActor.hasTrait("XianTu9") || targetActor.hasTrait("XianTu91"))
                {
                    isEffective = false;
                }
            }
            // 山海境界（7倍气血伤害）
            else if (attacker.hasTrait("TyWudao9"))
            {
                damageMultiplier = 7f;
                isWuDaoDamage = true;
                // 对大乘及以上仙道修士无效，对圣躯及以上武道修士无效
                if (targetActor.hasTrait("XianTu7") || targetActor.hasTrait("XianTu8") || 
                    targetActor.hasTrait("TyWudao91") || targetActor.hasTrait("TyWudao92") ||
                    targetActor.hasTrait("TyWudao93") || targetActor.hasTrait("TyWudao94") ||
                    targetActor.hasTrait("XianTu9") || targetActor.hasTrait("XianTu91"))
                {
                    isEffective = false;
                }
            }
            // 圣躯境界（9倍气血伤害）
            else if (attacker.hasTrait("TyWudao91"))
            {
                damageMultiplier = 9f;
                isWuDaoDamage = true;
                // 对半仙及以上仙道修士无效，对证帝及以上武道修士无效
                if (targetActor.hasTrait("XianTu8") || targetActor.hasTrait("TyWudao92") ||
                    targetActor.hasTrait("TyWudao93") || targetActor.hasTrait("TyWudao94") ||
                    targetActor.hasTrait("XianTu9") || targetActor.hasTrait("XianTu91"))
                {
                    isEffective = false;
                }
            }
            // 证帝境界（12倍气血伤害）
            else if (attacker.hasTrait("TyWudao92"))
            {
                damageMultiplier = 12f;
                isWuDaoDamage = true;
                // 对至尊及以上武道修士无效
                if (targetActor.hasTrait("TyWudao93") || targetActor.hasTrait("TyWudao94") ||
                    targetActor.hasTrait("XianTu9") || targetActor.hasTrait("XianTu91"))
                {
                    isEffective = false;
                }
            }
            // 至尊境界（15倍气血伤害）
            else if (attacker.hasTrait("TyWudao93"))
            {
                damageMultiplier = 15f;
                isWuDaoDamage = true;
                // 对道祖及以上武道修士无效
                if (targetActor.hasTrait("TyWudao94") || targetActor.hasTrait("XianTu91"))
                {
                    isEffective = false;
                }
            }
            // 道祖境界（20倍气血伤害）
            else if (attacker.hasTrait("TyWudao94"))
            {
                damageMultiplier = 20f;
                isWuDaoDamage = true;
                // 无额外限制
            }
            // 如果不是武道伤害，再检查修仙境界的真伤
            else if (attacker.hasTrait("XianTu2")) // 筑基期
            {
                damageMultiplier = 2f;
                // 对筑基期以上境界无效，对神游及以上武道修士无效
                    // 对练气期以上境界无效，对天人及以上武道修士无效
                if (targetActor.hasTrait("XianTu2") || targetActor.hasTrait("XianTu3") || 
                    targetActor.hasTrait("XianTu4") || targetActor.hasTrait("XianTu5") || 
                    targetActor.hasTrait("XianTu6") || targetActor.hasTrait("XianTu7") || 
                    targetActor.hasTrait("XianTu8") || targetActor.hasTrait("TyWudao5") || 
                    targetActor.hasTrait("XianTu9") || targetActor.hasTrait("XianTu91") ||
                    targetActor.hasTrait("TyWudao6") || targetActor.hasTrait("TyWudao7") || 
                    targetActor.hasTrait("TyWudao8") || targetActor.hasTrait("TyWudao9") || 
                    targetActor.hasTrait("TyWudao91") || targetActor.hasTrait("TyWudao92") ||
                    targetActor.hasTrait("TyWudao93") || targetActor.hasTrait("TyWudao94"))
                {
                    isEffective = false;
                }
            }
            else if (attacker.hasTrait("XianTu2")) // 筑基期
            {
                damageMultiplier = 2f;
                // 对筑基期以上境界无效，对神游及以上武道修士无效
                if (targetActor.hasTrait("XianTu3") || targetActor.hasTrait("XianTu4") || 
                    targetActor.hasTrait("XianTu5") || targetActor.hasTrait("XianTu6") || 
                    targetActor.hasTrait("XianTu7") || targetActor.hasTrait("XianTu8") || 
                    targetActor.hasTrait("XianTu9") || targetActor.hasTrait("XianTu91") ||
                    targetActor.hasTrait("TyWudao6") || targetActor.hasTrait("TyWudao7") || 
                    targetActor.hasTrait("TyWudao8") || targetActor.hasTrait("TyWudao9") || 
                    targetActor.hasTrait("TyWudao91") || targetActor.hasTrait("TyWudao92") ||
                    targetActor.hasTrait("TyWudao93") || targetActor.hasTrait("TyWudao94"))
                {
                    isEffective = false;
                }
            }
            else if (attacker.hasTrait("XianTu3")) // 金丹期
            {
                damageMultiplier = 3f;
                // 对金丹期以上境界无效，对踏虚及以上武道修士无效
                if (targetActor.hasTrait("XianTu4") || targetActor.hasTrait("XianTu5") || 
                    targetActor.hasTrait("XianTu6") || targetActor.hasTrait("XianTu7") || 
                    targetActor.hasTrait("XianTu8") || targetActor.hasTrait("TyWudao7") ||
                    targetActor.hasTrait("XianTu9") || targetActor.hasTrait("XianTu91") || 
                    targetActor.hasTrait("TyWudao8") || targetActor.hasTrait("TyWudao9") || 
                    targetActor.hasTrait("TyWudao91") || targetActor.hasTrait("TyWudao92") ||
                    targetActor.hasTrait("TyWudao93") || targetActor.hasTrait("TyWudao94"))
                {
                    isEffective = false;
                }
            }
            else if (attacker.hasTrait("XianTu4")) // 元婴期
            {
                damageMultiplier = 4f;
                // 对元婴期以上境界无效，对明我及以上武道修士无效
                if (targetActor.hasTrait("XianTu5") || targetActor.hasTrait("XianTu6") || 
                    targetActor.hasTrait("XianTu7") || targetActor.hasTrait("XianTu8") || 
                    targetActor.hasTrait("XianTu9") || targetActor.hasTrait("XianTu91") || 
                    targetActor.hasTrait("TyWudao8") || targetActor.hasTrait("TyWudao9") || 
                    targetActor.hasTrait("TyWudao91") || targetActor.hasTrait("TyWudao92") ||
                    targetActor.hasTrait("TyWudao93") || targetActor.hasTrait("TyWudao94"))
                {
                    isEffective = false;
                }
            }
            else if (attacker.hasTrait("XianTu5")) // 化神期
            {
                damageMultiplier = 5f;
                // 对化神期以上境界无效，对山海及以上武道修士无效
                if (targetActor.hasTrait("XianTu6") || targetActor.hasTrait("XianTu7") || 
                    targetActor.hasTrait("XianTu8") || targetActor.hasTrait("TyWudao9") || 
                    targetActor.hasTrait("XianTu9") || targetActor.hasTrait("XianTu91") || 
                    targetActor.hasTrait("TyWudao91") || targetActor.hasTrait("TyWudao92") ||
                    targetActor.hasTrait("TyWudao93") || targetActor.hasTrait("TyWudao94"))
                {
                    isEffective = false;
                }
            }
            else if (attacker.hasTrait("XianTu6")) // 合体期
            {
                damageMultiplier = 7f;
                // 对合体期以上境界无效，对圣躯及以上武道修士无效
                if (targetActor.hasTrait("XianTu7") || targetActor.hasTrait("XianTu8") || 
                    targetActor.hasTrait("XianTu9") || targetActor.hasTrait("XianTu91") || 
                    targetActor.hasTrait("TyWudao91") || targetActor.hasTrait("TyWudao92") ||
                    targetActor.hasTrait("TyWudao93") || targetActor.hasTrait("TyWudao94"))
                {
                    isEffective = false;
                }
            }
            else if (attacker.hasTrait("XianTu7")) // 大乘期
            {
                damageMultiplier = 9f;
                // 对大乘期以上境界无效，对证帝及以上武道修士无效
                if (targetActor.hasTrait("XianTu8") || targetActor.hasTrait("TyWudao92") ||
                    targetActor.hasTrait("XianTu9") || targetActor.hasTrait("XianTu91") || 
                    targetActor.hasTrait("TyWudao93") || targetActor.hasTrait("TyWudao94"))
                {
                    isEffective = false;
                }
            }
            else if (attacker.hasTrait("XianTu8")) // 半仙期
            {
                damageMultiplier = 12f;
                // 对至尊及以上武道修士无效
                if (targetActor.hasTrait("TyWudao93") || targetActor.hasTrait("TyWudao94") ||
                    targetActor.hasTrait("XianTu9") || targetActor.hasTrait("XianTu91"))
                {
                    isEffective = false;
                }
            }
            else if (attacker.hasTrait("XianTu9")) // 真仙期
            {
                damageMultiplier = 15f;
                // 对道祖及以上武道修士无效
                if (targetActor.hasTrait("TyWudao94") || targetActor.hasTrait("XianTu91"))
                {
                    isEffective = false;
                }
            }
            else if (attacker.hasTrait("XianTu91")) // 金仙期
            {
                damageMultiplier = 20f;
                // 无额外限制
            }
            else
            {
                // 没有境界特质，不触发真伤
                isEffective = false;
            }
            
            // 施加真实伤害
            if (isEffective && damageMultiplier > 0 && targetActor.data.health > 0)
            {
                int trueDamage = 0;
                
                if (isWuDaoDamage)
                {
                    // 武道气血伤害：基于攻击者的气血值计算伤害
                    float attackerQiXue = attacker.GetQiXue();
                    trueDamage = Mathf.RoundToInt(attackerQiXue * damageMultiplier);
                }
                else
                {
                    // 修仙真实伤害：基于目标的伤害属性计算伤害
                    trueDamage = Mathf.RoundToInt(targetActor.stats["damage"] * damageMultiplier);
                }
                
                // 确保至少造成1点伤害
                if (trueDamage < 1) trueDamage = 1;
                
                // 施加真实伤害（受位格减伤限制）
                ActorExtensions.ApplyTrueDamageWithRealmReduction(targetActor, trueDamage, attacker);
                
                // 检查目标是否死亡
                if (targetActor.data.health <= 0)
                {
                    targetActor.batch.c_check_deaths.Add(targetActor);
                }
                
                // 添加视觉效果
                AssetManager.terraform.get("lightning_normal").apply_force = false;
                MapBox.spawnLightningMedium(pTile, 0.1f);
            }
            
            return false; // 允许后续攻击动作继续执行
        }
    }
}