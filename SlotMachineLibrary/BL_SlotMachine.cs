using System;
using System.Collections.Generic;
using System.Text;

namespace SlotMachineLibrary
{
    public static class BL_SlotMachine
    {
        public static Column GetColumn1()
        {
            Column column1 = new Column
                (
                    new string[]
                    {
                        "Cherry",
                        "Clock",
                        "Berry",
                        "Berry",
                        "Melon",
                        "Cherry",
                        "Clock",
                        "Cherry",
                        "Berry",
                        "Clock",
                        "7",
                        "Clock",
                        "BAR",
                        "Cherry",
                        "Cherry",
                        "Berry",
                        "Melon",
                        "Melon",
                        "Berry",
                        "7",
                        "Berry",
                        "Clock",
                        "Cherry",
                        "BAR",
                        "BAR",
                        "Cherry",
                        "Melon"
                    }
                );
            return column1;
        }

        public static Column GetColumn2()
        {
            Column column2 = new Column
                (
                    new string[]
                    {
                        "7",
                        "Melon",
                        "7",
                        "Cherry",
                        "Clock",
                        "BAR",
                        "Cherry",
                        "Berry",
                        "Clock",
                        "Berry",
                        "BAR",
                        "Clock",
                        "Melon",
                        "BAR",
                        "Berry",
                        "Cherry",
                        "Clock",
                        "Cherry",
                        "Berry",
                        "Clock",
                        "Melon",
                        "Cherry",
                        "Berry",
                        "Berry",
                        "Cherry",
                        "Melon"
                    }
                );
            return column2;
        }

        public static string SetLabel(string lblName)
        {
            return ":  x " + GetRewardMultiplier(lblName.Replace("lbl", "")).ToString();
        }

        public static Column GetColumn3()
        {
            Column column3 = new Column
                (
                    new string[]
                    {
                        "Berry",
                        "Cherry",
                        "BAR",
                        "Clock",
                        "Clock",
                        "Berry",
                        "Cherry",
                        "7",
                        "Melon",
                        "Cherry",
                        "BAR",
                        "Melon",
                        "Berry",
                        "Melon",
                        "Cherry",
                        "Berry",
                        "Clock",
                        "Cherry",
                        "Cherry",
                        "Berry",
                        "7",
                        "Clock",
                        "BAR",
                        "Clock",
                        "Berry",
                        "Melon",
                        "Cherry"
                    }
                );
            return column3;
        }

        public static int GetRewardMultiplier(string slot)
        {
            int multiplier = 0;

            switch (slot)
            {
                case "Cherry":
                    multiplier = 5;
                    break;

                case "Berry":
                    multiplier = 9;
                    break;

                case "Clock":
                    multiplier = 14;
                    break;

                case "Melon":
                    multiplier = 20;
                    break;

                case "BAR":
                    multiplier = 35;
                    break;

                case "7":
                    multiplier = 200;
                    break;

                default:
                    multiplier = 0;
                    break;
            }

            return multiplier;
        }
    }
    // 7Cherry 6Berry 5Clock 4Melon 3BAR 2 7
}
