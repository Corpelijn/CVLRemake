using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Grid
{
    class Direction
    {
        #region "Fields"

        public static readonly Direction North = new Direction(1);
        public static readonly Direction East = new Direction(2);
        public static readonly Direction South = new Direction(-1);
        public static readonly Direction West = new Direction(-2);
        public static readonly Direction NorthEast = new Direction(11);
        public static readonly Direction SouthEast = new Direction(12);
        public static readonly Direction SouthWest = new Direction(-11);
        public static readonly Direction NorthWest = new Direction(-12);

        private int value;

        #endregion

        #region "Constructors"

        private Direction(int value)
        {
            this.value = value;
        }

        public Direction(int x, int y)
        {
            if (x == -1 && y == 0)
                value = -2;
            else if (x == 1 && y == 0)
                value = 2;
            else if (x == 0 && y == -1)
                value = -1;
            else if (x == 0 & y == 1)
                value = 1;
            else if (x == 1 && y == 1)
                value = 11;
            else if (x == 1 && y == -1)
                value = 12;
            else if (x == -1 && y == -1)
                value = -11;
            else if (x == -1 && y == 1)
                value = -12;
        }

        #endregion

        #region "Properties"



        #endregion

        #region "Methods"



        #endregion

        #region "Abstract/Virtual Methods"



        #endregion

        #region "Inherited Methods"

        public override int GetHashCode()
        {
            return value;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Direction)) return false;
            return value == (obj as Direction).value;
        }

        public override string ToString()
        {
            switch (value)
            {
                case 1:
                    return "North";
                case -1:
                    return "South";
                case 2:
                    return "East";
                case -2:
                    return "West";
                case 11:
                    return "NorthEast";
                case 12:
                    return "SouthEast";
                case -11:
                    return "SouthWest";
                case -12:
                    return "NorthWest";
                default:
                    return "";
            }
        }

        #endregion

        #region "Static Methods"

        public static Direction GetDirectionFromValue(int direction)
        {
            switch(direction)
            {
                case 0:
                    return North;
                case 1:
                    return East;
                case 2:
                    return South;
                case 3:
                    return West;
                default:
                    return null;
            }
        }

        #endregion

        #region "Operators"

        public static implicit operator Direction(string dir)
        {
            if (dir == "north") return new Direction(1);
            else if (dir == "east") return new Direction(2);
            else if (dir == "south") return new Direction(-1);
            else if (dir == "west") return new Direction(-2);
            else if (dir == "northeast") return new Direction(11);
            else if (dir == "southeast") return new Direction(12);
            else if (dir == "southwest") return new Direction(-11);
            else if (dir == "northwest") return new Direction(-12);
            return new Direction(0);
        }

        public static implicit operator int(Direction dir)
        {
            return dir.value;
        }

        public static bool operator ==(Direction right, Direction left)
        {
            return right.value == left.value;
        }

        public static bool operator !=(Direction right, Direction left)
        {
            return right.value != left.value;
        }

        #endregion
    }
}
