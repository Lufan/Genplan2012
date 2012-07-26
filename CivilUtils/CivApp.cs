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
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.Civil.DatabaseServices;
using Autodesk.Civil;
using Autodesk.AutoCAD.DatabaseServices;

namespace LufsGenplan
{
    /// <summary>
    /// Implement interractions with Autodesk Civil3d application
    /// </summary>
    public static class CivApp
    {
        public static Autodesk.Civil.ApplicationServices.CivilDocument CivDoc
        {
            get
            {
                return Autodesk.Civil.ApplicationServices.CivilApplication.ActiveDocument;
            }
        }

        /// <summary>
        /// Reference to existing alignment to improve performance into calculation methods
        /// </summary>
        private static Alignment curAlignment;
        /// <summary>
        ///Reference to existing surface to improve performance into calculation methods
        /// </summary>
        private static TinSurface curSurfaceEx;
        /// <summary>
        ///Reference to proecting surface to improve performance into calculation methods
        /// </summary>
        private static TinSurface curSurfacePr;


        public static String PromptSelectAlignment(String alignmentStatus)
        {
            String algnName = "";
            try
            {
                using (var tr = AcadApp.StartTransaction())
                {
                    var opt = new PromptEntityOptions("\nВыберите " + alignmentStatus + " трассу: ");
                    opt.SetRejectMessage("\nОбъект должен быть трассой.\n");
                    opt.AddAllowedClass(typeof(Alignment), false);
                    var ent = AcadApp.AcaEd.GetEntity(opt);
                    if (ent.Status == PromptStatus.OK)
                    {
                        var alignId = ent.ObjectId;
                        if (alignId == ObjectId.Null)
                        {
                            return "";
                        }
                        var curAlgn = tr.GetObject(alignId, OpenMode.ForRead) as Alignment;
                        algnName = curAlgn.Name;
                        return algnName; // All OK. Return alignment name
                    } // If PromptStatus.OK
                }
            }
            catch (Autodesk.AutoCAD.Runtime.Exception ex)
            {
                AcadApp.AcaEd.WriteMessage("ERROR: CivApp.PromptSelectAlignment()\n" + ex + "\n");
            }
            return ""; //There have been some errors
        }

        public static String PromptSelectTINSurface(String sufraceStatus)
        {
            String surfName = "";
            try
            {
                using (var tr = AcadApp.StartTransaction())
                {
                    var opt = new PromptEntityOptions("\nВыберите " + sufraceStatus + " поверхность TIN: ");
                    opt.SetRejectMessage("\nОбъект должен быть поверхностью TIN.\n");
                    opt.AddAllowedClass(typeof(TinSurface), false);
                    var ent = AcadApp.AcaEd.GetEntity(opt);
                    if (ent.Status == PromptStatus.OK)
                    {
                        var tinsurfId = ent.ObjectId;
                        if (tinsurfId == ObjectId.Null)
                        {
                            return "";
                        }
                        var tinsurf = tinsurfId.GetObject(OpenMode.ForRead) as TinSurface;
                        surfName = tinsurf.Name;
                        return surfName; // All OK. Return surface name
                    } // If PromptStatus.OK
                } // using
            }
            catch (Autodesk.AutoCAD.Runtime.Exception ex)
            {
                AcadApp.AcaEd.WriteMessage("ERROR: CivApp.PromptSelectTINSurface()\n" + ex + "\n");
            }
            return ""; //There have been some errors
        }

        public static Double PromptEnterDouble(String message, Double defaultValue = 0.0d)
        {
            Double result = -1.0d;
            try
            {
                using (var tr = AcadApp.StartTransaction())
                {
                    var optDist = new PromptDoubleOptions(message);
                    optDist.UseDefaultValue = true;
                    optDist.DefaultValue = defaultValue;
                    optDist.AllowNone = false;
                    optDist.AllowNegative = false;
                    var valDist = AcadApp.AcaEd.GetDouble(optDist);
                    if (valDist.Status == PromptStatus.OK)
                    {
                        result = valDist.Value;
                        return result;
                    }
                } // using
            }
            catch (Autodesk.AutoCAD.Runtime.Exception ex)
            {
                AcadApp.AcaEd.WriteMessage("ERROR: CivApp.PromptEnterDouble()\n" + ex + "\n");
            }
            return -1.0d; //There have been some errors
        }

        

        /// <summary>
        /// Get list of alignments names from drawing
        /// </summary>
        /// <returns>Collection of the alignments names</returns>
        public static List<AcadUtils.CbAutocadItem> GetAlignmentList()
        {
            var algnList = new List<AcadUtils.CbAutocadItem>();
            using (var tr = AcadApp.StartTransaction())
            {
                try
                {
                    var alignments = CivDoc.GetAlignmentIds();
                    foreach (ObjectId aid in alignments)
                    {
                        var curAlgn = tr.GetObject(aid, OpenMode.ForRead) as Alignment;
                        algnList.Add(new AcadUtils.CbAutocadItem(curAlgn.Name, curAlgn.Id));
                    }
                }
                catch (System.Exception ex)
                {
                    AcadApp.AcaEd.WriteMessage("ERROR: CivApp.GetAlignmentList()\n" + ex + "\n");
                }
            }
            return algnList;
        }

        /// <summary>
        /// Get list of surfaces (only TIN) names from drawing
        /// </summary>
        /// <returns>Collection of the surfaces names</returns>
        public static List<AcadUtils.CbAutocadItem> GetSurfaceList()
        {
            var surfList = new List<AcadUtils.CbAutocadItem>();
            using (var tr = AcadApp.StartTransaction())
            {
                try
                {
                    var surfaces = CivDoc.GetSurfaceIds();
                    foreach (ObjectId sid in surfaces)
                    {
                        var curSurf = tr.GetObject(sid, OpenMode.ForRead) as Autodesk.Civil.DatabaseServices.Surface;
                        if (curSurf.GetType() == typeof(TinSurface))
                        {
                            surfList.Add(new AcadUtils.CbAutocadItem(curSurf.Name, curSurf.Id));
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    AcadApp.AcaEd.WriteMessage("ERROR: ACivilApplication.GetSurfaceList()\n" + ex + "\n");
                }
            }// using
            return surfList;
        }

        /// <summary>
        /// Get alignment data 
        /// </summary>
        /// <param name="alignment">Name of the alignment</param>
        /// <param name="startPK">out parametr - starting station</param>
        /// <param name="endPK">out parametr - ending station</param>
        /// <param name="length">out parametr - length of the alignment</param>
        /// <returns>True if no errors, otherwise false</returns>
        public static Boolean GetAlignmentData(String alignment, out Double startPK, out Double endPK,
                                        out Double length)
        {
            startPK = -1.0d;
            endPK = -1.0d;
            length = -1.0d;
            try
            {
                Alignment algn = GetAlignmentByName(alignment);
                startPK = algn.StartingStation;
                endPK = algn.EndingStation;
                length = algn.Length;
            }
            catch (System.Exception ex)
            {
                AcadApp.AcaEd.WriteMessage("ERROR: ACivilApplication.GetAlignmentData()\n" + ex + "\n");
                return false;
            }
            return true;
        }

        /// <summary>
        /// Get elevation from %surfaceName% along %alignmentName% at pk and offset 
        /// </summary>
        /// <param name="alignmentName">Name of the alignment</param>
        /// <param name="surfaceName">Name of the surface</param>
        /// <param name="pk">PK position along alignment</param>
        /// <param name="offset">Offset distance from alignment</param>
        /// <param name="elevation">out parametr - elevation at this point</param>
        /// <returns>True if point onto surface, otherwise false</returns>
        public static Boolean GetElevationAtPK(Alignment align, TinSurface surf, Double pk,
                                        Double offset, out Double elevation)
        {
            elevation = 0.0d;
            try
            {
                var east = 0.0d;
                var north = 0.0d;
                align.PointLocation(pk, offset, ref east, ref north);
                // Get elevation from surface at point
                elevation = surf.FindElevationAtXY(east, north);
            }
            catch (Autodesk.Civil.PointNotOnEntityException)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Method used to resolve from the Alignment Name and store the current alignment object
        /// </summary>
        /// <param name="name">Alignment name</param>
        public static void SetCurAlignment(String name)
        {
            curAlignment = GetAlignmentByName(name);
        }

        /// <summary>
        /// Method used to resolve the existing TinSurface Name and store the current surface object
        /// </summary>
        /// <param name="name">TinSurface name</param>
        public static void SetCurSurfaceEx(String name)
        {
            curSurfaceEx = GetTinSurfaceByName(name);
        }

        /// <summary>
        /// Method used to resolve the proecting TinSurface Name and store the current surface object
        /// </summary>
        /// <param name="name">TinSurface name</param>
        public static void SetCurSurfacePr(String name)
        {
            curSurfacePr = GetTinSurfaceByName(name);
        }

        /// <summary>
        /// Return the current alignment object
        /// </summary>
        /// <returns>Alignment object</returns>
        public static Alignment CurAlignment
        {
            get
            {
                return curAlignment;
            }
        }

        /// <summary>
        /// Return the current existing TinSurface object
        /// </summary>
        /// <returns>TinSurface object</returns>
        public static TinSurface CurSurfaceEx
        {
            get
            {
                return curSurfaceEx;
            }
        }

        /// <summary>
        /// Return the current proecting TinSurface object
        /// </summary>
        /// <returns>TinSurface object</returns>
        public static TinSurface CurSurfacePr
        {
            get
            {
                return curSurfacePr;
            }
        }



        /// <summary>
        /// Get alignment from drawing by the given name
        /// </summary>
        /// <param name="name">Name of the alignment</param>
        /// <returns>Alignment</returns>
        private static Alignment GetAlignmentByName(String name)
        {
            Alignment curAlgn;
            using (var tr = AcadApp.StartTransaction())
            {
                try
                {
                    var alignments = CivDoc.GetAlignmentIds();
                    foreach (ObjectId aid in alignments)
                    {
                        curAlgn = tr.GetObject(aid, OpenMode.ForRead) as Alignment;
                        if (curAlgn.Name == name)
                        {
                            return curAlgn;
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    AcadApp.AcaEd.WriteMessage("ERROR: ACivilApplication.GetAlignmentByName()\n" + ex + "\n");
                }
            }// using
            return null;
        }
        /// <summary>
        /// Get TinSurface from drawing by the given name
        /// </summary>
        /// <param name="name">Name of the surface</param>
        /// <returns>TinSurface</returns>
        private static TinSurface GetTinSurfaceByName(String name)
        {
            Autodesk.Civil.DatabaseServices.Surface curSurf;
            using (var tr = AcadApp.StartTransaction())
            {
                try
                {
                    var surfaces = CivDoc.GetSurfaceIds();
                    foreach (ObjectId sid in surfaces)
                    {
                        curSurf = tr.GetObject(sid, OpenMode.ForRead) as Autodesk.Civil.DatabaseServices.Surface;
                        if (curSurf.GetType() == typeof(TinSurface) && curSurf.Name == name)
                        {
                            return curSurf as TinSurface;
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    AcadApp.AcaEd.WriteMessage("ERROR: ACivilApplication.GetTinSurfaceByName()\n" + ex + "\n");
                }
            }// using
            return null;
        }
    }
}
