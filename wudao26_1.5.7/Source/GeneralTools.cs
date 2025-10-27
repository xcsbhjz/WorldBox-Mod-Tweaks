using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace CustomModT001;

public static class GeneralTools
{
    private static readonly JsonSerializerSettings private_members_visit_settings = new()
    {
        ContractResolver = new DefaultContractResolver
        {
            // 反正不改版本, 就用这个吧
#pragma warning disable 618
            DefaultMembersSearchFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance
#pragma warning restore 618
        }
    };

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string to_json(object obj, bool private_members_included = false)
    {
        if (private_members_included) return JsonConvert.SerializeObject(obj, private_members_visit_settings);

        return JsonConvert.SerializeObject(obj);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T from_json<T>(string json, bool private_members_included = false)
    {
        //return System.Text.Json.JsonSerializer.Deserialize<T>(json);
        if (private_members_included) return JsonConvert.DeserializeObject<T>(json, private_members_visit_settings);

        return JsonConvert.DeserializeObject<T>(json);
    }

    /// <summary>
    ///     以key为key, 将pObject JSON序列化后写入data
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void WriteObj<T>(this BaseSystemData pData, string pKey, T pObject,
                                   bool                pPrivateMembersIncluded = false)
    {
        if (pObject == null) pData.removeString(pKey);
        pData.set(pKey, to_json(pObject, pPrivateMembersIncluded));
    }

    /// <summary>
    ///     以key为key, 从data中读取JSON, 并反序列化为T, 若不存在则会返回default(T)
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T ReadObj<T>(this BaseSystemData pData, string pKey, bool pPrivateMembersIncluded = false)
    {
        pData.get(pKey, out string obj_str);

        if (string.IsNullOrEmpty(obj_str)) return default;

        return from_json<T>(obj_str, pPrivateMembersIncluded);
    }

    public static void AfterDeserialize(this BaseStats pBaseStats)
    {
        pBaseStats._stats_list = new List<BaseStatsContainer>(pBaseStats._stats_list);
    }

    public static void ApplyMods(this BaseStats stats, BaseStats mod_source)
    {
        if (mod_source?._multipliers_list == null) return;

        foreach (BaseStatsContainer mod_data in mod_source._multipliers_list)
        {
            BaseStatAsset mod_asset = mod_data.asset;
            BaseStatsContainer target_container = stats.getContainer(mod_asset.main_stat_to_multiply);
            if (target_container == null) continue;
            target_container.value += target_container.value * mod_data.value;
        }
    }
}