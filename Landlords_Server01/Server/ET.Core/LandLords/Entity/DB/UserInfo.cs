using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace ETModel
{
    [ObjectSystem]
    public class UserInfoAwakeSystem : AwakeSystem<UserInfo, string>
    {
        public override void Awake(UserInfo self, string name)
        {
            self.Awake(name);
        }
    }

    /// <summary>
    /// 用户信息
    /// </summary>
    public class UserInfo : Entity
    {
        //昵称(默认为Id后6位+玩家)
        public string UserName { get; set; }

        //等级
        public int Level { get; set; }

        //欢乐豆
        public int Douzi { get; set; }
        
        //砖石
        public int Jwel { get; set; }

        //手机
        public string Phone { get; set; }

        /// <summary>
        /// /显示给玩家的ID（Id的后5位数）
        /// </summary>
        public string UserInfoId { get; set; }



        //上次游戏角色序列 1/2/3
        public int LastPlay { get; set; }


        //public List<Ca>
        public void Awake(string name)
        {
            string user = Id.ToString();
            //Substring(6,8);从字符串的第7位开始取8个字符串（即第7到第15个字符，0是第一位字符）
            UserName = user.Substring(6,6)+"玩家";
            UserInfoId= 1+user.Substring(7,5);
            Level = 1;
            Douzi = 1000;
            Jwel = 20;
            LastPlay = 0;

        }

    }
}