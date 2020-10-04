using System;
using System.Collections.Generic;

namespace ETModel
{
    /// <summary>
    /// 游戏UI模型
    /// </summary>
    public static partial class LandUIType
    {
        public const string LandMainLogin = "LandMainLogin";
        public const string LoginPanel = "LoginPanel";
        public const string MainLobby = "MainLobby";
        public const string UserInfoPanel = "UserInfoPanel";
        public const string LandRoom = "LandRoom";
        public const string LandInteraction = "LandInteraction";
        public const string TestLobby = "TestLobby";
        public const string ShopPanel = "ShopPanel";
    }
    /// <summary>
    /// UI事件模型
    /// </summary>
    public static partial class UIEventType
    {
        public const string LandInitSceneStart = "LandInitSceneStart";
        public const string LandMainLoginFinish = "LandMainLoginFinish";

        public const string UserLoginStart = "UserLoginStart";
        public const string UserLoginFinish = "UserLoginFinish";

        public const string LoginPanelStart = "LoginPanelStart";
        public const string LoginPanelFinish = "LoginPanelFinish";

        public const string MainLobbyStart = "MainLobbyStart";
        public const string MainLobbyFinish = "MainLobbyFinish";

        public const string UserInfoPanelStart = "UserInfoPanelStart";
        public const string UserInfoPanelFinish = "UserInfoPanelFinish";

        public const string LandRoomStart = "LandRoomStart";
        public const string LandRoomFinish = "LandRoomFinish";

        public const string ShopPanelStart = "ShopPanelStart";
        public const string ShopPanelFinish = "ShopPanelFinish";

        public const string testLobbyStart = "testLobbyStart";
    }

    /// <summary>
    /// 创建登录主界面
    /// </summary>
    [Event(UIEventType.LandInitSceneStart)]
    public class InitSceneStart_CreateLandLogin : AEvent
    {
        public override void Run()
        {
            Game.Scene.GetComponent<UIComponent>().Create(LandUIType.LandMainLogin);
        }
    }

    /// <summary>
    /// 关闭登录主界面
    /// </summary>
    [Event(UIEventType.LandMainLoginFinish)]
    public class LandMainLoginFinish : AEvent
    {
        public override void Run()
        {
            Game.Scene.GetComponent<UIComponent>().Remove(LandUIType.LandMainLogin);
        }
    }



    [Event(UIEventType.MainLobbyStart)]
    public class StartLobbyStart : AEvent
    {
        public override void Run()
        {
            Game.Scene.GetComponent<UIComponent>().Create(LandUIType.MainLobby);
        }
    }

    [Event(UIEventType.MainLobbyFinish)]
    public class StartLobbyFinish : AEvent
    {
        public override void Run()
        {
            Game.Scene.GetComponent<UIComponent>().Remove(LandUIType.MainLobby);
        }
    }

    [Event(UIEventType.LoginPanelStart)]
    public class LoginPanelStart : AEvent
    {
        public override void Run()
        {
            Game.Scene.GetComponent<UIComponent>().Create(LandUIType.LoginPanel);
        }
    }

    [Event(UIEventType.LoginPanelFinish)]
    public class LoginPanelFinish : AEvent
    {
        public override void Run()
        {
            Game.Scene.GetComponent<UIComponent>().Remove(LandUIType.LoginPanel);
        }
    }

    [Event(UIEventType.UserInfoPanelStart)]
    public class UserInfoPanelStart : AEvent
    {
        public override void Run()
        {
            Game.Scene.GetComponent<UIComponent>().Create(LandUIType.UserInfoPanel);
        }
    }

    [Event(UIEventType.UserInfoPanelFinish)]
    public class UserInfoPanelFinish : AEvent
    {
        public override void Run()
        {
            Game.Scene.GetComponent<UIComponent>().Remove(LandUIType.UserInfoPanel);
        }
    }

    [Event(UIEventType.ShopPanelStart)]
    public class ShopPanelStart : AEvent
    {
        public override void Run()
        {
            Game.Scene.GetComponent<UIComponent>().Create(LandUIType.ShopPanel);
        }
    }

    [Event(UIEventType.ShopPanelFinish)]
    public class ShopPanelFinish : AEvent
    {
        public override void Run()
        {
            Game.Scene.GetComponent<UIComponent>().Remove(LandUIType.ShopPanel);
        }
    }

    [Event(UIEventType.LandRoomStart)]
    public class LandRoomStart : AEvent
    {
        public override void Run()
        {
            Game.Scene.GetComponent<UIComponent>().Create(LandUIType.LandRoom);
        }
    }

    [Event(UIEventType.LandRoomFinish)]
    public class LandRoomFinish : AEvent
    {
        public override void Run()
        {
            Game.Scene.GetComponent<UIComponent>().Remove(LandUIType.LandRoom);
        }
    }




}
