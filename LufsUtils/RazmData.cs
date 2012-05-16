/*
 * Created by Луферов Александр Николаевич
 * 
 * 
 * 
 * Лицензия GNU Lesser General Public License : http://www.gnu.org/copyleft/lesser.html.
 */
using System;
using System.Collections.Generic;
using System.Text;

namespace LufsGenplan
{
    public class CompareRazmType : IComparer<string>
    {
        // Because the class implements IComparer, it must define a 
        // Compare method. The method returns a signed integer that indicates 
        // whether s1 > s2 (return is greater than 0), s1 < s2 (return is negative),
        // or s1 equals s2 (return value is 0). This Compare method compares strings with numbers splits with '.'
        public int Compare(string s1, string s2)
        {
            string[] words1 = s1.Split(new char[] { '.' });
            string[] words2 = s2.Split(new char[] { '.' });
            

            for (var i = 0; i < (words1.Length < words2.Length ? words1.Length : words2.Length); i++)
            {
                int res = int.Parse(GetNumberString(words1[i])) - int.Parse(GetNumberString(words2[i]));
                if (res != 0)
                {
                    return res;
                }
            }
            return words1.Length - words2.Length;
        }

        private string GetNumberString(String source)
        {
            StringBuilder res = new StringBuilder();
            foreach (var ch in source)
            {
                if (char.IsNumber(ch))
                {
                    res.Append(ch);
                }
            }
            return res.ToString();
        }
    }

    public class RazmData
    {
        private String _npp = String.Empty;
        private String _razmDesc = String.Empty;
        private String _razmType = String.Empty;
        private String _razmLen = String.Empty;
        private String _razmArea = String.Empty;

        private RazmType.typeOfEntity _entType;

        public RazmType.typeOfEntity EntityType
        {
            get
            {
                return this._entType;
            }
            set
            {
                if (value != this._entType)
                {
                    this._entType = value;
                }
            }
        }

        public String NPP
        {
            get
            {
                return this._npp;
            }
            set
            {
                if (value != this._npp)
                {
                    this._npp = value;
                }
            }
        }

        public String RazmDescription
        {
            get
            {
                return this._razmDesc;
            }
            set
            {
                if (value != this._razmDesc)
                {
                    this._razmDesc = value;
                }
            }
        }

        public String RazmType
        {
            get
            {
                return this._razmType;
            }
            set
            {
                if (value != this._razmType)
                {
                    this._razmType = value;
                }
            }
        }

        public String RazmLenght
        {
            get
            {
                return this._razmLen;
            }
            set
            {
                if (value != this._razmLen)
                {
                    this._razmLen = value;
                }
            }
        }

        public String RazmArea
        {
            get
            {
                return this._razmArea;
            }
            set
            {
                if (value != this._razmArea)
                {
                    this._razmArea = value;
                }
            }
        }

        public RazmData(String npp, String razmDesc, String razmType, String razmLen, String razmArea, RazmType.typeOfEntity entType)
        {
            NPP = npp;
            RazmDescription = razmDesc;
            RazmType = razmType;
            RazmLenght = razmLen;
            RazmArea = razmArea;
            EntityType = entType;
        }

    }
}
