/*
 * Created by Луферов Александр Николаевич
 * 
 * 
 * 
 * Лицензия GNU Lesser General Public License : http://www.gnu.org/copyleft/lesser.html.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AcadUtils
{
    public class CbAutocadItem
    {
        private Autodesk.AutoCAD.DatabaseServices.ObjectId id;
        private String name;

        public CbAutocadItem(String name, Autodesk.AutoCAD.DatabaseServices.ObjectId id)
        {
            Name = name;
            ID = id;
        }

        public String Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        public Autodesk.AutoCAD.DatabaseServices.ObjectId ID
        {
            get
            {
                return id;
            }
            private set
            {
                id = value;
            }
        }

        public override String ToString()
        {
            return Name;
        }
    }
}
