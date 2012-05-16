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
using Autodesk.AutoCAD.DatabaseServices;

namespace LufsGenplan
{
    public static class AcadApp
    {
        public static Document AcaDoc
        {
            get
            {
                return Application.DocumentManager.MdiActiveDocument;
            }
        }
        public static Autodesk.AutoCAD.DatabaseServices.Database AcaDb
        {
            get
            {
                return HostApplicationServices.WorkingDatabase;
            }
        }
        public static Autodesk.AutoCAD.EditorInput.Editor AcaEd
        {
            get
            {
                return AcaDoc.Editor;
            }
        }
        public static Autodesk.AutoCAD.DatabaseServices.Transaction StartTransaction()
        {
            return AcaDb.TransactionManager.StartTransaction();
        }

        /// <summary>
        /// Get name of the current drawing
        /// </summary>
        /// <returns>Current drawing's name</returns>
        public static String GetFileName()
        {
            var hs = HostApplicationServices.Current;
            return hs.FindFile(AcaDoc.Name, AcaDoc.Database, FindFileHint.Default);
        }

        public static Autodesk.AutoCAD.Colors.Color GetColorByLayer(ObjectId layer)
        {
            Autodesk.AutoCAD.Colors.Color cl = new Autodesk.AutoCAD.Colors.Color();
            try
            {
                using (var loc = AcadApp.AcaDoc.LockDocument())
                {
                    using (var trans = StartTransaction())
                    {
                        LayerTableRecord ltr = trans.GetObject(layer, OpenMode.ForRead) as LayerTableRecord;
                        if (ltr == null)
                        {
                            LufsGenplan.AcadApp.AcaEd.WriteMessage("\nDEBUG: GetColorByLayer ltr == null\n");
                        }
                        cl = ltr.Color;

                        trans.Commit();
                    }
                }
                return cl;
             }
            catch (System.Exception ex)
            {
                AcadApp.AcaEd.WriteMessage("\nERROR: AcadApp.GetColorByLayer() " + ex.Message + "\n");
            }
            return cl;
        }

        public static List<AcadUtils.CbAutocadItem> GetLayersList()
        {
            List<AcadUtils.CbAutocadItem> layers = new List<AcadUtils.CbAutocadItem>();
            try
            {
                using (var trans = StartTransaction())
                {
                    LayerTable lt = (LayerTable)trans.GetObject(AcadApp.AcaDb.LayerTableId, OpenMode.ForRead);
                    foreach (ObjectId lid in lt)
                    {
                        LayerTableRecord ltr = trans.GetObject(lid, OpenMode.ForRead) as LayerTableRecord;
                        layers.Add(new AcadUtils.CbAutocadItem(ltr.Name, lid));
                    }
                }
            }
            catch (System.Exception ex)
            {
                AcadApp.AcaEd.WriteMessage("\nERROR: AcadApp.GetLayersList() " + ex + "\n");
            }

            return layers;
        }

        public static List<AcadUtils.CbAutocadItem> GetLinetypeList()
        {
            List<AcadUtils.CbAutocadItem> linetypes = new List<AcadUtils.CbAutocadItem>();
            try
            {
                using (var trans = StartTransaction())
                {

                    LinetypeTable lt = (LinetypeTable)trans.GetObject(AcadApp.AcaDb.LinetypeTableId, OpenMode.ForRead);
                    foreach (ObjectId lid in lt)
                    {
                        LinetypeTableRecord ltr = trans.GetObject(lid, OpenMode.ForRead) as LinetypeTableRecord;
                        linetypes.Add(new AcadUtils.CbAutocadItem(ltr.Name, lid));
                    }
                }
            }
            catch (System.Exception ex)
            {
                AcadApp.AcaEd.WriteMessage("\nERROR: AcadApp.GetLinetypeList() " + ex + "\n");
            }
            
            return linetypes;
        }

        public static Autodesk.AutoCAD.Colors.Color SelectColor()
        {
            try
            {
                Autodesk.AutoCAD.Windows.ColorDialog cd = new Autodesk.AutoCAD.Windows.ColorDialog();
                cd.IncludeByBlockByLayer = true;
                cd.SetDialogTabs(Autodesk.AutoCAD.Windows.ColorDialog.ColorTabs.ACITab);
                System.Windows.Forms.DialogResult resd = cd.ShowDialog();
                if (resd == System.Windows.Forms.DialogResult.Cancel)
                {
                    return null;
                }
                return cd.Color;
            }
            catch (System.Exception ex)
            {
                AcadApp.AcaEd.WriteMessage("\nERROR: AcadApp.SelectColor() " + ex + "\n");
            }
            return null;
        }

        public static void SelectSample(Boolean isAllEnt, out String layerName, out Autodesk.AutoCAD.Colors.Color color, out String lineType)
        {
            layerName = null;
            color = new Autodesk.AutoCAD.Colors.Color();
            lineType = null;
            try
            {
                Autodesk.AutoCAD.EditorInput.PromptEntityOptions edOpt = new Autodesk.AutoCAD.EditorInput.PromptEntityOptions("\nВыберите объект аналог: ");
                if (isAllEnt)
                {
                    edOpt.SetRejectMessage("Объект не Arc,Circle,Line,Polyline.\n");
                    edOpt.AddAllowedClass(typeof(Autodesk.AutoCAD.DatabaseServices.Circle), false);
                    edOpt.AddAllowedClass(typeof(Autodesk.AutoCAD.DatabaseServices.Line), false);
                    edOpt.AddAllowedClass(typeof(Autodesk.AutoCAD.DatabaseServices.Polyline), false);
                    edOpt.AddAllowedClass(typeof(Autodesk.AutoCAD.DatabaseServices.Arc), false);
                }
                else
                {
                    edOpt.SetRejectMessage("Объект не полилиния.\n");
                    edOpt.AddAllowedClass(typeof(Autodesk.AutoCAD.DatabaseServices.Polyline), false);
                }
                edOpt.AllowNone = false;
                edOpt.AllowObjectOnLockedLayer = false;

                Autodesk.AutoCAD.EditorInput.PromptEntityResult edRes = AcadApp.AcaEd.GetEntity(edOpt);
                if (edRes.Status == Autodesk.AutoCAD.EditorInput.PromptStatus.OK)
                {
                    using (var trans = AcadApp.StartTransaction())
                    {
                        try
                        {
                            Curve selCurve;
                            selCurve = (Curve)trans.GetObject(edRes.ObjectId, OpenMode.ForRead);
                            layerName = selCurve.Layer;
                            color = selCurve.Color;
                            lineType = selCurve.Linetype;
                            AcadApp.AcaEd.WriteMessage("OK.\n");
                        }
                        catch (System.Exception ex)
                        {
                            AcadApp.AcaEd.WriteMessage("\nERROR: AcadApp.SelectSample()::PROCESS " + ex + "\n");
                        }
                    } //using trans
                }
            }
            catch (System.Exception ex)
            {
                AcadApp.AcaEd.WriteMessage("\nERROR: AcadApp.SelectSample()::EXTERNAL " + ex + "\n");
            }
            return;
        }

        public static bool isCivilDatabase(Database db)
        {
            try
            {
                using (var tr = AcadApp.StartTransaction())
                {
                    DBDictionary namedObjectDict = db.NamedObjectsDictionaryId.GetObject(OpenMode.ForRead) as DBDictionary;
                    return (namedObjectDict.Contains("Root") && Autodesk.AutoCAD.ApplicationServices.Application.MainWindow.Text.Contains("AutoCAD Civil 3D"));
                }
            }
            catch (Autodesk.AutoCAD.Runtime.Exception ex)
            {
                AcadApp.AcaEd.WriteMessage("ERROR: AcadApp.isCivilDatabase()\n" + ex + "\n");
            }
            return false;
        }

        #region Register database events

        public static void RegisterDatabaseAppendEvent(Autodesk.AutoCAD.DatabaseServices.ObjectEventHandler DbEvent_ObjectAppened_Handler)
        {
            try
            {
                AcadApp.AcaDb.ObjectAppended += new Autodesk.AutoCAD.DatabaseServices.ObjectEventHandler(DbEvent_ObjectAppened_Handler);
            }
            catch (Autodesk.AutoCAD.Runtime.Exception ex)
            {
                AcadApp.AcaEd.WriteMessage("ERROR: AcadApp.RegisterDatabaseAppendEvent()\n" + ex + "\n");
            }
        }

        public static void RegisterDatabaseEraseEvent(Autodesk.AutoCAD.DatabaseServices.ObjectErasedEventHandler DbEvent_ObjectErased_Handler)
        {
            try
            {
                AcadApp.AcaDb.ObjectErased += new Autodesk.AutoCAD.DatabaseServices.ObjectErasedEventHandler(DbEvent_ObjectErased_Handler);
            }
            catch (Autodesk.AutoCAD.Runtime.Exception ex)
            {
                AcadApp.AcaEd.WriteMessage("ERROR: AcadApp.RegisterDatabaseEraseEvent()\n" + ex + "\n");
            }
        }

        public static void RegisterDatabaseModifiEvent(Autodesk.AutoCAD.DatabaseServices.ObjectEventHandler DbEvent_ObjectModified_Handler)
        {
            try
            {
                AcadApp.AcaDb.ObjectModified += new Autodesk.AutoCAD.DatabaseServices.ObjectEventHandler(DbEvent_ObjectModified_Handler);
            }
            catch (Autodesk.AutoCAD.Runtime.Exception ex)
            {
                AcadApp.AcaEd.WriteMessage("ERROR: AcadApp.RegisterDatabaseModifiEvent()\n" + ex + "\n");
            }
        }
        #endregion Registr dstabase events

    }
}
