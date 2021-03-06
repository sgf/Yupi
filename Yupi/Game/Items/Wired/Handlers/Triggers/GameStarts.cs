using System.Collections.Generic;
using System.Linq;
using Yupi.Game.Items.Interactions.Enums;
using Yupi.Game.Items.Interfaces;
using Yupi.Game.Items.Wired.Interfaces;
using Yupi.Game.Rooms;

namespace Yupi.Game.Items.Wired.Handlers.Triggers
{
    public class GameStarts : IWiredItem
    {
        public GameStarts(RoomItem item, Room room)
        {
            Item = item;
            Room = room;
        }

        public Interaction Type => Interaction.TriggerGameStart;

        public RoomItem Item { get; set; }

        public Room Room { get; set; }

        public List<RoomItem> Items
        {
            get { return new List<RoomItem>(); }
            set { }
        }

        public int Delay
        {
            get { return 0; }
            set { }
        }

        public string OtherString
        {
            get { return ""; }
            set { }
        }

        public string OtherExtraString
        {
            get { return ""; }
            set { }
        }

        public string OtherExtraString2
        {
            get { return ""; }
            set { }
        }

        public bool OtherBool
        {
            get { return true; }
            set { }
        }

        public bool Execute(params object[] stuff)
        {
            List<IWiredItem> conditions = Room.GetWiredHandler().GetConditions(this);
            List<IWiredItem> effects = Room.GetWiredHandler().GetEffects(this);

            if (conditions.Any())
            {
                foreach (IWiredItem current in conditions)
                {
                    if (!current.Execute(null, Type))
                        return false;

                    WiredHandler.OnEvent(current);
                }
            }

            if (effects.Any())
            {
                foreach (IWiredItem current2 in effects)
                {
                    if (current2.Execute(null, Type))
                        WiredHandler.OnEvent(current2);
                }
            }

            WiredHandler.OnEvent(this);
            return true;
        }
    }
}