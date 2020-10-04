using System;
using ETModel;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace ETModel
{
    [ObjectSystem]
    public class ShopPanelComponentAwakeSystem : AwakeSystem<ShopPanelComponent>
    {
        public override void Awake(ShopPanelComponent self)
        {
            self.Awake();
        }
    }

    /// <summary>
    /// 大厅界面组件
    /// </summary>
    public class ShopPanelComponent : Component
    {
        public readonly GameObject[] BesansGroupGo = new GameObject[7];
        public readonly GameObject[] JewlGroupGo = new GameObject[7];
        public Text mJewelQuantityText;
        public Text mBeansQuantityText;
        public Text mNameText;
        public Text mIdText;
       
        public GameObject mBesansBtn;
        public GameObject mBesansON;
        public GameObject mBesansOFF;
        public GameObject mJewelBtn;
        public GameObject mJewelON;
        public GameObject mJewelOFF;

        public GameObject BesansGroup;
        public GameObject JewlGroup;

        public async void Awake()
        {
            ReferenceCollector rc = this.GetParent<UI>().GameObject.GetComponent<ReferenceCollector>();

            GameObject quitButton = rc.Get<GameObject>("Quit");
            GameObject readyButton = rc.Get<GameObject>("Ready");
            mJewelQuantityText = rc.Get<GameObject>("JewelQuantityText").GetComponent<Text>();
            mBeansQuantityText = rc.Get<GameObject>("BeansQuantityText").GetComponent<Text>();
            mNameText = rc.Get<GameObject>("NameText").GetComponent<Text>();
            mIdText = rc.Get<GameObject>("IdText").GetComponent<Text>();
            mBesansON= rc.Get<GameObject>("BesansON");
            mBesansOFF = rc.Get<GameObject>("BesansOFF");
            mJewelON = rc.Get<GameObject>("JewelON");
            mJewelOFF = rc.Get<GameObject>("JewelOFF");

            mJewelBtn = rc.Get<GameObject>("JewelBtn");
            mBesansBtn = rc.Get<GameObject>("BesansBtn");
            mJewelBtn.GetComponent<Button>().onClick.Add(() => JewelShop());
            mBesansBtn.GetComponent<Button>().onClick.Add(() => BeansShop());


            rc.Get<GameObject>("BeansGetBtn").GetComponent<Button>().onClick.Add(() => BeansShop());
            rc.Get<GameObject>("JewelGetBtn").GetComponent<Button>().onClick.Add(() => JewelShop());
            rc.Get<GameObject>("ExitBtn").GetComponent<Button>().onClick.Add(() => ExitBtnOnClick());

            //添加商品版面
            BesansGroup = rc.Get<GameObject>("BesansGroupGo");
            this.BesansGroupGo[0] = BesansGroup.Get<GameObject>("CommodityItemGo1");
            this.BesansGroupGo[1] = BesansGroup.Get<GameObject>("CommodityItemGo2");
            this.BesansGroupGo[2] = BesansGroup.Get<GameObject>("CommodityItemGo3");
            this.BesansGroupGo[3] = BesansGroup.Get<GameObject>("CommodityItemGo4");
            this.BesansGroupGo[4] = BesansGroup.Get<GameObject>("CommodityItemGo5");
            this.BesansGroupGo[5] = BesansGroup.Get<GameObject>("CommodityItemGo6");
            this.BesansGroupGo[6] = BesansGroup.Get<GameObject>("CommodityItemGo7");
            this.BesansGroupGo[7] = BesansGroup.Get<GameObject>("CommodityItemGo8");

            JewlGroup = rc.Get<GameObject>("JewlGroupGo");
            this.JewlGroupGo[0] = JewlGroup.Get<GameObject>("CommodityItemGo1");
            this.JewlGroupGo[1] = JewlGroup.Get<GameObject>("CommodityItemGo2");
            this.JewlGroupGo[2] = JewlGroup.Get<GameObject>("CommodityItemGo3");
            this.JewlGroupGo[3] = JewlGroup.Get<GameObject>("CommodityItemGo4");
            this.JewlGroupGo[4] = JewlGroup.Get<GameObject>("CommodityItemGo5");
            this.JewlGroupGo[5] = JewlGroup.Get<GameObject>("CommodityItemGo6");
            this.JewlGroupGo[6] = JewlGroup.Get<GameObject>("CommodityItemGo7");
            this.JewlGroupGo[7] = JewlGroup.Get<GameObject>("CommodityItemGo8");

            //获取玩家数据
            A1001_GetUserInfo_C2G GetUserInfo_Req = new A1001_GetUserInfo_C2G();
            A1001_GetUserInfo_G2C GetUserInfo_Ack = (A1001_GetUserInfo_G2C)await SessionComponent.Instance.Session.Call(GetUserInfo_Req);

            //显示用户名和用户等级
            mJewelQuantityText.text = GetUserInfo_Ack.Jwel.ToString();
            mBeansQuantityText.text = GetUserInfo_Ack.Douzi.ToString();
            mNameText.text = GetUserInfo_Ack.UserName;
            mIdText.text = GetUserInfo_Ack.UserInfoId;
            Debug.Log("显示信息成功");

    
        }

        public void ShopHint() 
        {
            for (int i = 0; i <= BesansGroupGo.Length; i++)
            {
                Debug.Log("=================>"+ BesansGroupGo[i].name);
                //landRoomComponent.AddComponent<LandlordsGamerPanelComponent>().SetPanel(GamersPanel[index]);


            }
        }

        private void ExitBtnOnClick()
        {
            //返回到到大厅界面
            Game.Scene.GetComponent<UIComponent>().Create(LandUIType.MainLobby);
            Game.Scene.GetComponent<UIComponent>().Remove(LandUIType.ShopPanel);
        }

        private void BeansShop()
        {
            mBesansON.SetActive(true);
            mBesansOFF.SetActive(false);
            mJewelON.SetActive(false);
            mJewelOFF.SetActive(true);
            JewlGroup.SetActive(false);
            BesansGroup.SetActive(true);
        }

        private void JewelShop()
        {
            mBesansON.SetActive(false);
            mBesansOFF.SetActive(true);
            mJewelON.SetActive(true);
            mJewelOFF.SetActive(false);
            JewlGroup.SetActive(true);
            BesansGroup.SetActive(false);
        }



    }
}