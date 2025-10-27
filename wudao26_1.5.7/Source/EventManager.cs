using System;
using System.Collections.Generic;
using System.Linq;

namespace CustomModT001;

public static class EventManager
{
    // 伤害事件结构体：存储单次伤害的核心信息
    private struct DamageEvent
    {
        public BaseSimObject attacker;  // 攻击者
        public BaseSimObject target;    // 目标
        public float damage;            // 伤害值
        public AttackType type;         // 攻击类型
    }

    // 伤害事件队列：采用Queue保证FIFO处理顺序
    private static readonly Queue<DamageEvent> damageEvents = new Queue<DamageEvent>();
    
    // 统计信息：用于动态计算过滤阈值
    private static float _totalDamageInQueue = 0;  // 队列中所有伤害的总和
    private static int _eventCountInQueue = 0;     // 队列中事件数量
    private static readonly float[] _damageValues;  // 环形缓冲区存储伤害值
    private static int _damageValuesIndex = 0;     // 当前索引位置
    private static bool _bufferFilled = false;     // 缓冲区是否已填满
    
    // 配置参数：可根据游戏需求调整
    private const int MAX_QUEUE_SIZE = 3000;       // 最大队列容量（防内存溢出）
    private const float MIN_DAMAGE_THRESHOLD = 0.5f; // 最小伤害阈值（绝对阈值）
    private const float RELATIVE_THRESHOLD_RATIO = 0.1f; // 相对阈值比例（相对于中位数）
    private const int MIN_EVENTS_FOR_RELATIVE_FILTER = 5; // 启用相对过滤的最小事件数量
    private const float MIN_APPLIED_DAMAGE = 1f;   // 最小应用伤害
    private const int MEDIAN_SAMPLE_LIMIT = 100;   // 中位数计算的最大样本量（平衡性能）
    
    // 静态构造函数初始化数组
    static EventManager()
    {
        _damageValues = new float[MEDIAN_SAMPLE_LIMIT];
    }
    
    /// <summary>
    /// 处理队列中所有伤害事件（批量应用伤害）
    /// </summary>
    public static void ProcessDamageEvents()
    {
        // 重置统计信息
        _totalDamageInQueue = 0;
        _eventCountInQueue = 0;
        _damageValuesIndex = 0;
        _bufferFilled = false;
        // 不需要清空数组，后续会被覆盖

        int eventCount = damageEvents.Count;
        while (eventCount-- > 0)
        {
            DamageEvent ev = damageEvents.Dequeue();
            if (ev.target == null || !ev.target.isAlive()) continue;
            
            float damageToApply = Math.Max(MIN_APPLIED_DAMAGE, ev.damage);
            ev.target.getHit(damageToApply, true, ev.type, ev.attacker, false);
        }
    }

    /// <summary>
    /// 将伤害事件加入队列（带过滤和容量控制）
    /// </summary>
    public static void EnqueueDamageEvent(BaseSimObject attacker, BaseSimObject target, float damage, AttackType type)
    {
        if (target == null || !target.isAlive()) return;
        
        if (damageEvents.Count >= MAX_QUEUE_SIZE)
        {
            ProcessDamageEvents();
        }
        
        if (!ShouldEnqueueDamage(damage))
        {
            return;
        }
        
        damageEvents.Enqueue(new DamageEvent
        {
            attacker = attacker,
            target = target,
            damage = damage,
            type = type
        });
        
        // 更新统计信息
        _totalDamageInQueue += damage;
        _eventCountInQueue++;
        
        // 使用环形缓冲区存储伤害值，避免列表操作开销
        _damageValues[_damageValuesIndex] = damage;
        _damageValuesIndex = (_damageValuesIndex + 1) % MEDIAN_SAMPLE_LIMIT;
        if (!_bufferFilled && _damageValuesIndex == 0)
        {
            _bufferFilled = true;
        }
    }

    /// <summary>
    /// 判断伤害是否应该入队（核心过滤逻辑）
    /// </summary>
    private static bool ShouldEnqueueDamage(float damage)
    {
        // 低于绝对阈值直接过滤
        if (damage < MIN_DAMAGE_THRESHOLD)
        {
            return false;
        }

        // 事件不足时不启用相对过滤
        if (_eventCountInQueue < MIN_EVENTS_FOR_RELATIVE_FILTER)
        {
            return true;
        }

        // 计算中位数（对极端值不敏感）
        float medianDamage = CalculateMedian();
        float relativeThreshold = medianDamage * RELATIVE_THRESHOLD_RATIO;
        
        // 伤害需同时高于绝对阈值和相对阈值
        return damage >= relativeThreshold;
    }

    /// <summary>
    /// 计算伤害值的中位数（优化版）
    /// </summary>
    private static float CalculateMedian()
    {
        int count = _bufferFilled ? MEDIAN_SAMPLE_LIMIT : _damageValuesIndex;
        if (count == 0) return 0;
        
        // 只复制有效数据
        float[] sorted = new float[count];
        if (_bufferFilled)
        {
            // 环形缓冲区已满，需要分两段复制
            int firstPartLength = MEDIAN_SAMPLE_LIMIT - _damageValuesIndex;
            System.Array.Copy(_damageValues, _damageValuesIndex, sorted, 0, firstPartLength);
            System.Array.Copy(_damageValues, 0, sorted, firstPartLength, _damageValuesIndex);
        }
        else
        {
            // 缓冲区未满，直接复制
            System.Array.Copy(_damageValues, 0, sorted, 0, count);
        }
        
        // 排序
        System.Array.Sort(sorted);
        
        if (count % 2 == 1)
        {
            // 奇数个元素：取中间值
            return sorted[count / 2];
        }
        else
        {
            // 偶数个元素：取中间两个值的平均值
            return (sorted[count / 2 - 1] + sorted[count / 2]) / 2;
        }
    }
}
