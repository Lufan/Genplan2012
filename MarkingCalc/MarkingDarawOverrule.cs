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

using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.GraphicsInterface;

namespace LufsGenplan
{
    class MarkingDarawOverrule : DrawableOverrule
    {
        static private MarkingDarawOverrule theOverrule = new MarkingDarawOverrule();

        private System.Collections.Generic.ICollection<RazmType> LineTypesOfDoubleLines { get; set; }
        private Double Offset { get; set; }

        private MarkingDarawOverrule()
        {
            SetCustomFilter();
        }

        public override bool IsApplicable(RXObject overruledSubject)
        {
            try
            {
                if (LineTypesOfDoubleLines == null || LineTypesOfDoubleLines.Count == 0)
                {
                    return false;
                }
                Autodesk.AutoCAD.DatabaseServices.Polyline pln = overruledSubject as Autodesk.AutoCAD.DatabaseServices.Polyline;
                foreach (var lineType in LineTypesOfDoubleLines)
                {
                    if (pln.Linetype == lineType.LineTypeName)
                    {
                        return true;
                    }
                }
            }
            catch (System.Exception ex)
            {
                AcadApp.AcaEd.WriteMessage("\nERROR: MarkingDarawOverrule.IsApplicable " + ex + "\n");
            }
            return false;
        }

        static public MarkingDarawOverrule GetMarkingDarawOverrule(System.Collections.Generic.ICollection<RazmType> layersOfDoubleLines, Double offset)
        {
            try
            {
                theOverrule.LineTypesOfDoubleLines = layersOfDoubleLines;
                theOverrule.Offset = offset;
                return theOverrule;
            }
            catch (System.Exception ex)
            {
                AcadApp.AcaEd.WriteMessage("\nERROR: MarkingDarawOverrule.GetMarkingDarawOverrule " + ex + "\n");
            }
            return null;
        }



        public override bool WorldDraw(Drawable drawable, WorldDraw wd)
        {

            if (LineTypesOfDoubleLines == null || LineTypesOfDoubleLines.Count == 0)
            {
                AcadApp.AcaEd.WriteMessage("\nERROR: MarkingDarawOverrule Filter do not work!\n");
                return base.WorldDraw(drawable, wd);
            }

            try
            {
                // Cast Drawable to Polyline so we can access its methods and
                // properties
                Autodesk.AutoCAD.DatabaseServices.Polyline pln = drawable as Autodesk.AutoCAD.DatabaseServices.Polyline;

                Double scaleFactor = pln.LinetypeScale;

                foreach (var lineType in LineTypesOfDoubleLines)
                {
                    if (pln.Linetype == lineType.LineTypeName)
                    {
                        if (lineType.EntityType == RazmType.typeOfEntity.DoubleLineCenter)
                        {
                            GetPlineOffset(-Offset/2.0d * scaleFactor, pln, wd);
                            GetPlineOffset(Offset / 2.0d * scaleFactor, pln, wd);
                        }
                        else if (lineType.EntityType == RazmType.typeOfEntity.DoubleLineSide)
                        {
                            GetPlineOffset(-Offset * scaleFactor, pln, wd);
                            ObjectId curLT = wd.SubEntityTraits.LineType;
                            using (var trans = AcadApp.StartTransaction())
                            {
                                LinetypeTable lt = trans.GetObject(AcadApp.AcaDb.LinetypeTableId, OpenMode.ForRead) as LinetypeTable;
                                wd.SubEntityTraits.LineType = lt["Continuous"];
                            }
                            GetPlineOffset(0.0d * scaleFactor, pln, wd, true);
                        }
                        return true;
                    }
                }
                //If collection don't have item with current item's layer name
                //Draw as is, without overrule
                return base.WorldDraw(drawable, wd);                
            }
            catch (System.Exception ex)
            {
                AcadApp.AcaEd.WriteMessage("\nERROR: MarkingDarawOverrule.WorldDraw " + ex + "\n");
            }
                return true;
        }

        private Boolean GetPlineOffset(double offset, Autodesk.AutoCAD.DatabaseServices.Polyline source, WorldDraw wd, Boolean IsOverrideLineType = false)
        {
            try
            {
                DBObjectCollection dbcol = source.GetOffsetCurves(offset);
                for (int i = 0; i < dbcol.Count; i++)
                {
                    Autodesk.AutoCAD.DatabaseServices.Polyline pln = dbcol[i] as Autodesk.AutoCAD.DatabaseServices.Polyline;

                    for (int j = 0; j < pln.NumberOfVertices; j++)
                    {
                        if (pln.GetSegmentType(j) == SegmentType.Line)
                        {
                            Autodesk.AutoCAD.Geometry.LineSegment3d line = pln.GetLineSegmentAt(j);
                            wd.Geometry.Polyline(new Autodesk.AutoCAD.Geometry.Point3dCollection() { line.StartPoint, line.EndPoint }, pln.Normal, new System.IntPtr(0));
                        }
                        else if (pln.GetSegmentType(j) == SegmentType.Arc)
                        {
                            Autodesk.AutoCAD.Geometry.CircularArc3d arc = pln.GetArcSegmentAt(j);
                            //Get center point
                            Double startPt = pln.GetDistAtPoint(pln.GetPoint3dAt(j));
                            Double endPt = pln.GetDistAtPoint(pln.GetPoint3dAt(j + 1));
                            Double centrPt = startPt + (endPt - startPt) / 2.0d;
                            Autodesk.AutoCAD.Geometry.Point3d pt = pln.GetPointAtDist(centrPt);
                            //Draw arc
                            wd.Geometry.CircularArc(arc.StartPoint, pt, arc.EndPoint, ArcType.ArcSimple);
                        }
                        else
                        {
                            //If other SegmentType - nothing to do
                        }
                    }
                    dbcol[i].Dispose();
                }
                
                return true;
            }
            catch (System.Exception ex)
            {
                AcadApp.AcaEd.WriteMessage("\nERROR: MarkingDarawOverrule.GetPlineOffset " + ex + "\n");
            }
            return false;
        }
    }
}
