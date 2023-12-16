using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using cfg;
using UnityEngine;
using YooAsset;
using Object = UnityEngine.Object;

/*
 * 还没有做的事：
 * 1.把[隐藏下面范围]从Normal层扩展到其他层
 * 2.把View拆分开，让MonoBehaviour只存组件引用，防止View生命周期的方法不好调用
 * 3.子界面
 * 4.国际化
 */

public class UIMgr : Singleton<UIMgr>
{
    private const string UIRootPrefab = "UIRoot";

    public GameObject UIRoot { set; get; }
    public Camera UICamera { set; get; }

    private Dictionary<UINameEnum, UIWindow> _openedWindows = new Dictionary<UINameEnum, UIWindow>();
    private List<UIWindow> _reversedWindows = new List<UIWindow>();
    private Dictionary<UILayerEnum, UILayerContainer> _uiLayers = new Dictionary<UILayerEnum, UILayerContainer>();

    private static Vector3 _HiddenPosition = new Vector3(0, 10000, 0);
    private static Vector3 _ShowPosition = Vector3.zero;

    private ResourcePackage _defaultPackage;
    private AssetHandle _rootAssetHandle;

    private UIMaskMgr _uiMaskMgr;

    private UIMgr()
    {
    }

    public override void Init()
    {
        _defaultPackage = YooAssetManager.Instance.DefaultPackage;

        _rootAssetHandle = _defaultPackage.LoadAssetSync<GameObject>(UIRootPrefab);
        UIRoot = _rootAssetHandle.InstantiateSync();
        UIRoot.name = UIRootPrefab;
        UICamera = UIRoot.transform.Find("UICamera")?.GetComponent<Camera>();
        Object.DontDestroyOnLoad(UIRoot);

        _uiMaskMgr = new UIMaskMgr();
        _uiMaskMgr.Init(UIRoot.transform);

        InitLayers();
    }

    private void InitLayers()
    {
        _uiLayers.Add(UILayerEnum.Main, new UILayerContainer(UILayerEnum.Main, UIRoot.transform));
        _uiLayers.Add(UILayerEnum.Fixed, new UILayerContainer(UILayerEnum.Fixed, UIRoot.transform));
        _uiLayers.Add(UILayerEnum.Normal, new UILayerContainer(UILayerEnum.Normal, UIRoot.transform));
        _uiLayers.Add(UILayerEnum.Pop, new UILayerContainer(UILayerEnum.Pop, UIRoot.transform));
        _uiLayers.Add(UILayerEnum.Guide, new UILayerContainer(UILayerEnum.Guide, UIRoot.transform));
        _uiLayers.Add(UILayerEnum.Loading, new UILayerContainer(UILayerEnum.Loading, UIRoot.transform));
        _uiLayers.Add(UILayerEnum.Tips, new UILayerContainer(UILayerEnum.Tips, UIRoot.transform));
        _uiLayers.Add(UILayerEnum.NetError, new UILayerContainer(UILayerEnum.NetError, UIRoot.transform));
    }

    public override void Dispose()
    {
        Object.Destroy(UIRoot);
        UIRoot = null;
        UICamera = null;

        _rootAssetHandle.Release();
        _rootAssetHandle = null;

        _defaultPackage = null;

        _uiMaskMgr.Dispose();
        _uiMaskMgr = null;
    }

    public void OpenWindow(UINameEnum uiName)
    {
        var uiConfig = DataTableMgr.Tables.TbUIConfig[(int)uiName];
        if (!_uiLayers.TryGetValue((UILayerEnum)uiConfig.Layer, out var uiLayerContainer))
        {
            Logger.Error($"UIMgr，要加载的UI Layer不存在...{uiConfig.Layer}");
            return;
        }

        var window = GetWindow(uiName);
        if (window != null)
        {
            PutWindowOnTop(window);
            return;
        }

        window = new UIWindow
        {
            UIConfig = uiConfig,
            UILayerContainer = uiLayerContainer
        };
        InnerOpenWindow(window, uiName);
    }

    private async void InnerOpenWindow(UIWindow window, UINameEnum uiName)
    {
        AssetHandle handle = _defaultPackage.LoadAssetAsync<GameObject>(window.UIConfig.Path);
        window.UIAssetHandle = handle;
        await handle.Task;

        if (_openedWindows.ContainsKey(uiName))
        {
            //说明在界面加载的期间，再次点击打开界面，直接返回
            return;
        }

        var gameObj = handle.InstantiateSync(window.UILayerContainer.Transform);
        window.UIView = gameObj.GetComponent<UIBaseView>();
        window.UIView.UIName = uiName;
        window.UIView.gameObject.name = window.UIConfig.Name;
        window.UIView.Create();
        _openedWindows.Add(uiName, window);

        PutWindowOnTop(window);
    }


    public void CloseWindow(UINameEnum uiName)
    {
        var window = GetWindow(uiName);
        if (window == null)
        {
            Logger.Error($"UIMgr，要关闭的UI不存在，ID...{uiName}");
            return;
        }

        RemoveWindowInStack(window);

        InnerCloseWindow(window);
    }

    private void InnerCloseWindow(UIWindow window)
    {
        window.UIView.Hide();
        window.UIView.Close();
        Object.Destroy(window.UIView.gameObject);
        window.UIAssetHandle.Release();

        if (_openedWindows.ContainsKey(window.UIView.UIName))
        {
            _openedWindows.Remove(window.UIView.UIName);
        }
    }

    private void PutWindowOnTop(UIWindow window)
    {
        window.UIView.transform.SetAsLastSibling();

        InnerShowWindow(window);

        PutWindowInStack(window);
    }


    //只有Normal层入栈
    private void PutWindowInStack(UIWindow window)
    {
        if (window.UILayerContainer.UILayer == UILayerEnum.Normal)
        {
            //如果是主面板，清栈
            if (window.UIConfig.IsMain)
            {
                foreach (var reversedWindow in _reversedWindows)
                {
                    if (window == reversedWindow)
                        continue;
                    InnerCloseWindow(reversedWindow);
                }

                _reversedWindows.Clear();
            }

            //如果之前就在列表里，先删除再添加
            if (_reversedWindows.Contains(window))
                _reversedWindows.Remove(window);

            _reversedWindows.Add(window);
            ProcessHiddenOther();
        }
    }

    private void RemoveWindowInStack(UIWindow window)
    {
        //如果在栈里，需要把下面的显示出来
        if (_reversedWindows.Contains(window))
        {
            _reversedWindows.Remove(window);
            ProcessHiddenOther();
        }
    }

    //只有normal层才有遮挡隐藏的功能
    private void ProcessHiddenOther()
    {
        if (_reversedWindows.Count <= 0)
            return;

        bool hiddenFlag = false; //如果上面有隐藏下面的设置
        for (int i = _reversedWindows.Count - 1; i >= 0; i--)
        {
            if (hiddenFlag)
            {
                InnerHideWindow(_reversedWindows[i]);
                continue;
            }
            else
            {
                InnerShowWindow(_reversedWindows[i]);
            }

            //如果有全屏或者隐藏其他的设置，隐藏下面所有面板
            var config = DataTableMgr.Tables.TbUIConfig[(int)_reversedWindows[i].UIView.UIName];
            if (config.IsFull)
            {
                hiddenFlag = true;
            }
            else
            {
                if (config.HidenOther)
                {
                    hiddenFlag = true;
                }
            }
        }
    }

    private void InnerShowWindow(UIWindow window)
    {
        if (window.UIView.IsShow == false)
        {
            //先显示再调用代码，否则调不到
            //window.UIView.gameObject.SetActive(true);
            window.UIView.Show();

            window.UIView.transform.localPosition = _ShowPosition;

            _uiMaskMgr.Show(window.UIView, window.UIConfig.BgAlpha, window.UIConfig.ClickCross,
                window.UIConfig.ClickClose);
        }
    }

    private void InnerHideWindow(UIWindow window)
    {
        if (window.UIView.IsShow == true)
        {
            //先调用代码再隐藏，否则调不到
            window.UIView.Hide();
            //window.UIView.gameObject.SetActive(false);
            window.UIView.transform.localPosition = _HiddenPosition;
        }
    }

    private UIWindow GetWindow(UINameEnum uiNameEnum)
    {
        _openedWindows.TryGetValue(uiNameEnum, out var window);
        return window;
    }

    #region Event

    //private readonly TypeEventSystem _uiEventSystem = new TypeEventSystem();

    //public IUnRegister AddListener<T>(Action<T> action)
    //{
    //    return _uiEventSystem.Register<T>(action);
    //}

    //public void RemoveListener<T>(Action<T> action)
    //{
    //    _uiEventSystem.UnRegister<T>(action);
    //}

    //public void Broadcast<T>(T e)
    //{
    //    _uiEventSystem.Send<T>(e);
    //}

    #endregion
}