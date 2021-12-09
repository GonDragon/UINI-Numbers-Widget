using RimWorld;
using System.Collections.Generic;

namespace UINotIncluded.Windows
{
    internal class CustomNumberWindows_Numbers : Numbers.MainTabWindow_Numbers
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