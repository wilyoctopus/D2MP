namespace D2MP.Models
{
    public class SlotItem
    {
        public SlotItem()
        {

        }

        public SlotItem(int slot, int itemId)
        {
            ItemId = itemId;
            Slot = slot;
        }

        public int ItemId { get; set; }
        public int Slot { get; set; }
        public bool IsSlotEmpty
        {
            get { return ItemId == 0; }
        }
    }
}
