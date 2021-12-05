using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RimWorld;
using Verse;

namespace UINotIncluded.Windows
{
    class CustomNumberWindows_Numbers : Numbers.MainTabWindow_Numbers
    {
        public bool shouldUpdate = false;
        public PawnTableDef UpdatedPawnTable;
        public List<PawnColumnDef> UpdatedColumns;

        public override void PostOpen()
        {
            if (shouldUpdate)
            {
                shouldUpdate = false;
                pawnTableDef = UpdatedPawnTable;
                pawnTableDef.columns = UpdatedColumns;
                UpdateFilter();
                Notify_ResolutionChanged();
            }
            base.PostOpen();
        }
    }
}
