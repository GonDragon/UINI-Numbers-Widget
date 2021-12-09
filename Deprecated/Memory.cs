using UINotIncluded.Widget.Configs;
using Verse;

namespace UINotIncluded
{
    internal class NumbersMemory : Memory
    {
        public MainButtonMemory fakeButtonMemory;
        public string tableName;
        public string unparsedTable;

        public override ElementConfig ConvertToConfig()
        {
            NumbersConfig config = new NumbersConfig(new Widget.NumbersTable()
            {
                label = tableName,
                comaDelimitedTable = unparsedTable
            });
            config.Label = fakeButtonMemory.label;
            config.minimized = fakeButtonMemory.minimized;
            config.IconPath = fakeButtonMemory.iconPath;
            config.RefreshIcon();
            config.RefreshCache();
            return config;
        }

        public override void ExposeData()
        {
            Scribe_Deep.Look(ref fakeButtonMemory, "fakeButtonMemory");
            Scribe_Values.Look(ref tableName, "tableName");
            Scribe_Values.Look(ref unparsedTable, "unparsedTable");
        }
    }
}