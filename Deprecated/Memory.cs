using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UINotIncluded.Widget.Configs;
using Verse;

namespace UINotIncluded
{
    class NumbersMemory : Memory
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
            config.Def.label = tableName;
            ((ButtonConfig)config).Label = fakeButtonMemory.label;
            ((ButtonConfig)config).minimized = fakeButtonMemory.minimized;
            ((ButtonConfig)config).IconPath = fakeButtonMemory.iconPath;
            ((ButtonConfig)config).RefreshIcon();
            ((ButtonConfig)config).RefreshCache();
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
