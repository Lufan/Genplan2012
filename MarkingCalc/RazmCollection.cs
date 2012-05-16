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

namespace LufsGenplan
{
    

    public class RazmCollection : System.Collections.Generic.Dictionary<String, RazmData>
    {
        public RazmCollection()
            :base()
        {
        }

        public void Add(RazmData item)
        {
            try
            {
                if (this.ContainsKey(item.RazmType) && this[item.RazmType].RazmDescription == item.RazmDescription)
                {
                    this[item.RazmType].NPP = (int.Parse(this[item.RazmType].NPP) + 1).ToString();
                    this[item.RazmType].RazmArea = AddPaintArea(this[item.RazmType].RazmArea, item.RazmArea);
                    this[item.RazmType].RazmLenght = (double.Parse(this[item.RazmType].RazmLenght) + double.Parse(item.RazmLenght)).ToString();
                }
                else
                {
                    base.Add(item.RazmType, item);
                }
            }
            catch (Exception ex)
            {
                AcadApp.AcaEd.WriteMessage("ERROR: RazmCollection().add " + ex + "\n");
            }
        }

        // "paint:Number;paint:Number;...." paintArea1 and paintArea2 has some number of paints
        private String AddPaintArea(String paintArea1, String paintArea2)
        {
            try
            {
                String area = "";
                Char[] delims = new Char[] { ':', ' ' };
                String[] mas1 = paintArea1.Split(delims);
                String[] mas2 = paintArea2.Split(delims);

                for (var i = 0; i < mas1.Length-1; i += 2)
                {
                    area += mas1[i] + ":" + (Double.Parse(mas1[i + 1]) + Double.Parse(mas2[i + 1])).ToString() + " ";
                }
                return area;
            }
            catch (Exception ex)
            {
                AcadApp.AcaEd.WriteMessage("ERROR: RazmCollection().AddPaintArea " + ex + "\n");
            }
            return "ADD_ERROR:0.0;";
        }
        // "paint:Number paint:Number ...." paintArea1 and paintArea2 has some number of paints
        public static String GetPaintArea(String paintArea, Double mulFactor, Double width)
        {
            try
            {
                String area = "";
                Char[] delims = new Char[] { ':', ' ' };
                String[] mas = paintArea.Split(delims);

                for (var i = 0; i < mas.Length - 1; i += 2)
                {
                    area += mas[i] + ":" + Math.Round((Double.Parse(mas[i + 1]) * mulFactor * width), 2).ToString() + " ";
                }
                return area;
            }
            catch (Exception ex)
            {
                AcadApp.AcaEd.WriteMessage("ERROR: RazmCollection().GetPaintArea " + ex + "\n");
            }
            return "MUL_ERROR:0.0;";
        }
    }
}
