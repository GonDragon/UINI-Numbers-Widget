using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RimWorld;
using Verse;

namespace UINotIncluded.Widget.Configs
{
    public class NumbersConfig : ButtonConfig
    {

        private MainButtonDef def;
        public string tableName;
        public string comaDelimitedTable;
        private Workers.Numbers_worker _worker;

        public NumbersConfig(NumbersTable numbersTable)
        {
            tableName = numbersTable.label;
            comaDelimitedTable = numbersTable.comaDelimitedTable;
        }
        public NumbersConfig() { } //Empty constructor to load from

        public override string SettingLabel => tableName;

        public override Def Def
        {
            get
            {
                if (def == null)
                {
                    def = new MainButtonDef() {
                        defName = "UINI_NMB" + tableName,
                        label = tableName,
                        description = "Custom window made using the mod Numbers.",
                        tabWindowClass = typeof(Windows.CustomNumberWindows_Numbers),
                        closesWorldView = true
                    };
                    Reset();
                }
                return def;
            }
        }


        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref tableName, "tableName");
            Scribe_Values.Look(ref comaDelimitedTable, "unparsedTable");
        }

        public override WidgetWorker Worker
        {
            get
            {
                if (_worker == null) _worker = new Workers.Numbers_worker(this);
                return _worker;
            }
        }

        public void Update()
        {
            string[] pawnTableDef = comaDelimitedTable.Split(',');
            PawnTableDef reconstructedPCD = DefDatabase<PawnTableDef>.GetNamedSilentFail(pawnTableDef[0]);

            List<PawnColumnDef> newColumns = new List<PawnColumnDef>();
            if (reconstructedPCD != null)
            {
                for (int i = 2; i < pawnTableDef.Length; i++)
                {
                    PawnColumnDef pcd = DefDatabase<PawnColumnDef>.GetNamedSilentFail(pawnTableDef[i]);
                    if (pcd != null) newColumns.Add(pcd);
                }
            }
            else
            {
                reconstructedPCD = Numbers.WorldComponent_Numbers.PrimaryFilter.First().Key;
                newColumns = reconstructedPCD.columns;
            };

            ((Windows.CustomNumberWindows_Numbers)((MainButtonDef)Def).TabWindow).shouldUpdate = true;
            ((Windows.CustomNumberWindows_Numbers)((MainButtonDef)Def).TabWindow).UpdatedPawnTable = reconstructedPCD;
            ((Windows.CustomNumberWindows_Numbers)((MainButtonDef)Def).TabWindow).UpdatedColumns = newColumns;
        }

        public override bool Equals(object obj)
        {
            return obj is NumbersConfig config &&
                   base.Equals(obj) &&
                   tableName == config.tableName &&
                   comaDelimitedTable == config.comaDelimitedTable;
        }

        public override int GetHashCode()
        {
            int hashCode = 1572851286;
            hashCode = hashCode * -1521134295 + base.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(tableName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(comaDelimitedTable);
            return hashCode;
        }
    }
}
