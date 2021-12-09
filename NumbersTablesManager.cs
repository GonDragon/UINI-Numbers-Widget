using RimWorld;
using System.Collections.Generic;
using Verse;

namespace UINotIncluded.Widget
{
    [StaticConstructorOnStartup]
    public static class NumbersTablesManager
    {
        static NumbersTablesManager()
        {
            Widget.WidgetManager.AddButtonGetter("Numbers Button", TablesToConfigs);
        }

        public static IEnumerable<Configs.ButtonConfig> TablesToConfigs()
        {
            foreach (NumbersTable table in AvaibleTables())
            {
                Widget.Configs.ButtonConfig config = new Configs.NumbersConfig(table);
                yield return config;
            }
        }

        public static IEnumerable<NumbersTable> AvaibleTables()
        {
            HarmonyLib.Traverse staticNumbers = HarmonyLib.Traverse.CreateWithType("Numbers.StaticConstructorOnGameStart");

            List<PawnColumnDef> initial = Numbers.NumbersDefOf.Numbers_MainTable.columns;

            Numbers.NumbersDefOf.Numbers_MainTable.columns = staticNumbers.Field("medicalPreset").GetValue<List<PawnColumnDef>>();
            yield return new NumbersTable()
            {
                label = "Numbers_Presets.Medical".Translate(),
                comaDelimitedTable = Numbers.HorribleStringParsersForSaving.TurnPawnTableDefIntoCommaDelimitedString(Numbers.NumbersDefOf.Numbers_MainTable)
            };

            Numbers.NumbersDefOf.Numbers_MainTable.columns = staticNumbers.Field("combatPreset").GetValue<List<PawnColumnDef>>();
            yield return new NumbersTable()
            {
                label = "Numbers_Presets.Combat".Translate(),
                comaDelimitedTable = Numbers.HorribleStringParsersForSaving.TurnPawnTableDefIntoCommaDelimitedString(Numbers.NumbersDefOf.Numbers_MainTable)
            };

            Numbers.NumbersDefOf.Numbers_MainTable.columns = staticNumbers.Field("workTabPlusPreset").GetValue<List<PawnColumnDef>>();
            yield return new NumbersTable()
            {
                label = "Numbers_Presets.WorkTabPlus".Translate(),
                comaDelimitedTable = Numbers.HorribleStringParsersForSaving.TurnPawnTableDefIntoCommaDelimitedString(Numbers.NumbersDefOf.Numbers_MainTable)
            };

            Numbers.NumbersDefOf.Numbers_MainTable.columns = staticNumbers.Field("colonistNeedsPreset").GetValue<List<PawnColumnDef>>();
            yield return new NumbersTable()
            {
                label = "Numbers_Presets.ColonistNeeds".Translate(),
                comaDelimitedTable = Numbers.HorribleStringParsersForSaving.TurnPawnTableDefIntoCommaDelimitedString(Numbers.NumbersDefOf.Numbers_MainTable)
            };

            if (ModLister.RoyaltyInstalled)
            {
                Numbers.NumbersDefOf.Numbers_MainTable.columns = staticNumbers.Field("psycastingPreset").GetValue<List<PawnColumnDef>>();
                yield return new NumbersTable()
                {
                    label = "Numbers_Presets.Psycasting".Translate(),
                    comaDelimitedTable = Numbers.HorribleStringParsersForSaving.TurnPawnTableDefIntoCommaDelimitedString(Numbers.NumbersDefOf.Numbers_MainTable)
                };
            }

            Numbers.NumbersDefOf.Numbers_MainTable.columns = initial;
            foreach (string stringtable in LoadedModManager.GetMod<Numbers.Numbers>().GetSettings<Numbers.Numbers_Settings>().storedPawnTableDefs)
            {
                string[] pawnTableDef = stringtable.Split(',');

                yield return new NumbersTable()
                {
                    label = pawnTableDef[1] == "Default" ? pawnTableDef[0].Split('_')[1] + " (" + pawnTableDef[1] + ")" : pawnTableDef[1],
                    comaDelimitedTable = stringtable
                };
            }
        }

        private static PawnTableDef ColumnToTable(List<PawnColumnDef> list)
        {
            PawnTableDef PawnTable = Numbers.NumbersDefOf.Numbers_MainTable;
            PawnTable.columns = new List<PawnColumnDef>(list);
            return PawnTable;
        }
    }
}