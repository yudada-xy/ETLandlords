namespace ETModel
{
    [ObjectSystem]
    public class CommodityAwakeSystem : AwakeSystem<Commodity, long>
    {
        public override void Awake(Commodity self, long id)
        {
            self.Awake(id);
        }
    }

    /// <summary>
    /// 玩家对象
    /// </summary>
    public sealed class Commodity : Entity
    {
        //商品ID
        public long CommodityId { get; private set; }

        public void Awake(long id)
        {
            this.CommodityId = id;
        }

        public override void Dispose()
        {
            if (this.IsDisposed)
            {
                return;
            }

            base.Dispose();

            this.CommodityId = 0;
        }
    }
}