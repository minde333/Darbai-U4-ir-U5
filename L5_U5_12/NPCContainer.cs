using System.Linq;

namespace L5_U5_12
{
    class NPCContainer
    {
        private const int MaxNPC = 100;
        NPC[] NPCs;
        public int Count { get; set; }

        public  NPCContainer()
        {
            NPCs = new NPC[MaxNPC];
            Count = 0;
        }

        public void AddNPC(int index, NPC nPC)
        {
            NPCs[index] = nPC;
        }
        public void AddNPC(NPC nPC)
        {
            NPCs[Count++] = nPC;
        }

        public NPC GetNPC(int index)
        {
            return NPCs[index];
        }

        public bool Contains(NPC nPC)
        {
            return NPCs.Contains(nPC);
        }

        public void SortNPCs()
        {
            for (int i = 0; i < Count - 1; i++)
            {
                var minValueNPC = NPCs[i];
                int minValueIndex = i;
                for (int j = i + 1; j < Count; j++)
                {
                    if (NPCs[j] <= minValueNPC)
                    {
                        minValueNPC = NPCs[j];
                        minValueIndex = j;
                    }
                }
                NPCs[minValueIndex] = NPCs[i];
                NPCs[i] = minValueNPC;
            }
        }

        public void SortNPCsByDamage()
        {
            for (int i = 0; i < Count; i++)
            {
                for (int j = 0; j < Count; j++)
                {
                    if (NPCs[i].Damage >= NPCs[j].Damage)
                    {
                        var temp = NPCs[i];
                        NPCs[i] = NPCs[j];
                        NPCs[j] = temp;
                    }
                }
            }
        }

        //public void SortByGenreAndName()
        //{
        //    for (int i = 0; i < Count; i++)
        //    {
        //        for (int j = 0; j < Count; j++)
        //        {
        //            var cmp = Videos[i].Genre.CompareTo(Videos[j].Genre);
        //            if (cmp == -1)
        //            {
        //                var temp = Videos[i];
        //                Videos[i] = Videos[j];
        //                Videos[j] = temp;
        //            }
        //            else if (cmp == 0)
        //            {
        //                if (Videos[i].Name.CompareTo(Videos[j].Name) == -1)
        //                {
        //                    var temp = Videos[i];
        //                    Videos[i] = Videos[j];
        //                    Videos[j] = temp;
        //                }
        //            }
        //        }
        //    }
        //}
    }
}
