using System;

namespace CustomModT001.Abstract;

public class AssetIdAttribute(string assetId) : Attribute
{
    public readonly string AssetId = assetId;
}