using System.Collections.Concurrent;
using System.Collections.Generic;

namespace CustomModT001;

public static class StatusExtendTools
{
    private static readonly ConcurrentDictionary<StatusAsset, StatusExtend> _extends = new();

    public static StatusExtend GetExtend(this StatusAsset status_asset, bool create = true)
    {
        return _extends.GetOrAdd(status_asset, key => create ? new StatusExtend() : null);
    

/*
        GameObject obj = new();

        float delta_angle = 1f;

        float forward(float euler_angle)
        {
            // 310 -> -50
            // 50 -> 50
            return (euler_angle + 180) % 360 - 180;
        }
        obj.transform.rotation = Quaternion.Euler(0, Mathf.Clamp(forward(obj.transform.eulerAngles.y + delta_angle), -50, 50), 0);*/

    }
}