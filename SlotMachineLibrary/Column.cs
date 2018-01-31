using System;
using System.Collections.Generic;
using System.Text;

namespace SlotMachineLibrary
{
    public class Column
    {
        public string[] ColumnArray { get; set; }

        public Column(string[] columnArray)
        {
            //if (ColumnArray.Length < 3)
            //{
            //    throw new CustomArrayTooShort("Column Arrays cannot contain less than 3 elements.");
            //}

            ColumnArray = columnArray;
        }

        public string[] GetPictures(int position1)
        {
            string[] returnArray = new string[3];

            if (position1 == ColumnArray.Length - 2)
            {
                returnArray[0] = ColumnArray[ColumnArray.Length - 2];
                returnArray[1] = ColumnArray[ColumnArray.Length - 1];
                returnArray[2] = ColumnArray[0];
            }
            else if (position1 == ColumnArray.Length - 1)
            {
                returnArray[0] = ColumnArray[ColumnArray.Length - 1];
                returnArray[1] = ColumnArray[0];
                returnArray[2] = ColumnArray[1];
            }
            else
            {
                returnArray[0] = ColumnArray[position1];
                returnArray[1] = ColumnArray[position1 + 1];
                returnArray[2] = ColumnArray[position1 + 2];
            }

            return returnArray;
        }

    }

    public class CustomArrayTooShort : Exception
    {
        public CustomArrayTooShort() : base() { }

        public CustomArrayTooShort(string mijnBericht) : base(mijnBericht)
        {

        }
    }
}
