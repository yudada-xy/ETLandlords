using System;
using ETModel;
using UnityEngine;
using UnityEngine.UI;

namespace ETModel
{
    [ObjectSystem]
    public class StartLobbyComponentAwakeSystem : AwakeSystem<StartLobbyComponent>
    {
        public override void Awake(StartLobbyComponent self)
        {
            self.Awake();
        }
    }

    /// <summary>
    /// 大厅界面组件
    /// </summary>
    public class StartLobbyComponent : Component
    {
        //砖石数量
        public Text mJwelNumText;
        //玩家名称
        private Text mNameText;
        //玩家ID
        private Text mIdText;

        public bool isMatching;

        public async void Awake()
        {
            Debug.Log("大厅组件获取成功");
            ReferenceCollector rc = this.GetParent<UI>().GameObject.GetComponent<ReferenceCollector>();

            mJwelNumText = rc.Get<GameObject>("JwelNumText").GetComponent<Text>();
            mNameText = rc.Get<GameObject>("NameText").GetComponent<Text>();
            mIdText = rc.Get<GameObject>("IdText").GetComponent<Text>();

            rc.Get<GameObject>("JwelAddBtn").GetComponent<Button>().onClick.Add(() => JwelAddBtnOnClick());
            rc.Get<GameObject>("ShopBtn").GetComponent<Button>().onClick.Add(() => JwelAddBtnOnClick());
            rc.Get<GameObject>("HeadMaskBtn").GetComponent<Button>().onClick.Add(() => HeadMaskBtnOnClick());
            rc.Get<GameObject>("ServiceBtn").GetComponent<Button>().onClick.Add(()=> ServiceBtnOnClick());
            rc.Get<GameObject>("AnnouncementBtn").GetComponent<Button>().onClick.Add(() => AnnouncementBtnOnClick());
            rc.Get<GameObject>("SetBtn").GetComponent<Button>().onClick.Add(() => SetBtnOnClick());
            rc.Get<GameObject>("RuleBtn").GetComponent<Button>().onClick.Add(() => RuleBtnOnClick());
            rc.Get<GameObject>("CreateRoomBtn").GetComponent<Button>().onClick.Add(() => CreateRoomBtnOnClick());
            rc.Get<GameObject>("JoinRoomBtn").GetComponent<Button>().onClick.Add(() => JoinRoomBtnOnClick());
            rc.Get<GameObject>("MatchBtn").GetComponent<Button>().onClick.Add(() => MatchBtnOnClick());
            Debug.Log("大厅组件获取成功");

            //获取玩家数据
            A1001_GetUserInfo_C2G GetUserInfo_Req = new A1001_GetUserInfo_C2G();
            A1001_GetUserInfo_G2C GetUserInfo_Ack = (A1001_GetUserInfo_G2C)await SessionComponent.Instance.Session.Call(GetUserInfo_Req);
       
            //显示用户名和用户等级
            mJwelNumText.text = GetUserInfo_Ack.Jwel.ToString();
            mNameText.text = GetUserInfo_Ack.UserName;
            mIdText.text = GetUserInfo_Ack.UserInfoId;
            Debug.Log("显示信息成功");

            
       
        }

        /// <summary>
        /// 匹配房间(boat)
        /// </summary>
        public async void MatchBtnOnClick()
        {
            try
            {
                ////发送开始匹配消息
                C2G_StartMatch_Req c2G_StartMatch_Req = new C2G_StartMatch_Req();
                G2C_StartMatch_Back g2C_StartMatch_Ack = (G2C_StartMatch_Back)await SessionComponent.Instance.Session.Call(c2G_StartMatch_Req);

                //if (g2C_StartMatch_Ack.Error == ErrorCode.ERR_UserMoneyLessError)
                //{
                //    Log.Error("余额不足");
                //    return;
                //}

                //切换到房间界面
                Game.EventSystem.Run(UIEventType.LandRoomStart);
                Game.EventSystem.Run(UIEventType.MainLobbyFinish);
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }

        /// <summary>
        /// 加入房间（boat）
        /// </summary>
        private void JoinRoomBtnOnClick()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 创建房间（boat）
        /// </summary>
        private void CreateRoomBtnOnClick()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 规则按钮（right）
        /// </summary>
        private void RuleBtnOnClick()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 设置按钮（right）
        /// </summary>
        private void SetBtnOnClick()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 公告按钮（right）
        /// </summary>
        private void AnnouncementBtnOnClick()
        {
            throw new NotImplementedException();
        }
       
        /// <summary>
        /// 客服按钮（right）
        /// </summary>
        private void ServiceBtnOnClick()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 头像按钮(userinfo)
        /// </summary>
        private void HeadMaskBtnOnClick()
        {
            Debug.Log("点击了头像设置");
           Game.EventSystem.Run(UIEventType.UserInfoPanelStart);
        }

        /// <summary>
        /// 砖石+号按钮和商城按钮事件（userinfo）
        /// </summary>
        private void JwelAddBtnOnClick()
        {
            Game.EventSystem.Run(UIEventType.MainLobbyFinish);
            Game.EventSystem.Run(UIEventType.ShopPanelStart);
        }
    }
}