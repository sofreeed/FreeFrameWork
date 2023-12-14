using System.Collections;
using System.Collections.Generic;
using cfg;
using UnityEngine;
using YooAsset;

public class UIWindow
{
    public UIBaseView UIView { set; get; }
    public UIConfig UIConfig { set; get; }
    public UILayerContainer UILayerContainer { set; get; }
    public AssetHandle UIAssetHandle { set; get; }
}