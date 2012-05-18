/*
 * Created by Луферов Александр Николаевич
 * 
 * 
 * 
 * Лицензия GNU Lesser General Public License : http://www.gnu.org/copyleft/lesser.html.
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LufsGenplan
{
    public partial class MarkingCalc : UserControl, LufsGenplan.ILUFSPlug
    {
        #region ILUFSPlug interface implementation
        public Boolean DocumentCreated()
        {
            try
            {
                DocumentActivated();
                RegisterDatabaseEvents();
            }
            catch (Exception ex)
            {
                LufsGenplan.AcadApp.AcaEd.WriteMessage("\nERROR: MarkingCalc.DocumentCreated() " + ex + "\n");
            }
            return true;
        }

        public Boolean DocumentActivated()
        {
            try
            {
                //Layer setup
                cbLayerUse.Items.Clear();
                foreach (var item in LufsGenplan.AcadApp.GetLayersList())
                {
                    cbLayerUse.Items.Add(item as object);
                }
                cbLayerUse.Sorted = true;
                cbLayerUse.SelectedItem = cbLayerUse.Items[0];
                currentLayer = cbLayerUse.Text;
                return true;
            }
            catch (Exception ex)
            {
                LufsGenplan.AcadApp.AcaEd.WriteMessage("\nERROR: MarkingCalc.DocumentActivated() " + ex + "\n");
            }
            return false;
        }
        public String GetPluginName()
        {
            return "Разметка";
        }
        public LufsGenplan.PlugType GetTargetApp()
        {
            return LufsGenplan.PlugType.Autocad;
        }
        #endregion ILUFSPlug

        public MarkingCalc()
        {
            try
            {
                InitializeComponent();
                //Set current config file name
                configFileName = SetCurrentConfigFile();

                LoadMarkingType(configFileName);

                SetcbCategoryItems();
                cbCategory.SelectedIndex = 0;
                CurrentCategory = cbCategory.Text;

                SetcbMashtItems();
                cbMasht.SelectedIndex = 6;
                CurrentMasht = cbMasht.Text;

                SetOverrule();
            }
            catch (Exception ex)
            {
                LufsGenplan.AcadApp.AcaEd.WriteMessage("\nERROR: MarkingCalc.MarkingCalc() " + ex + "\n");
            }
        }

        public static String ConfigName
        {
            get
            {
                return configName;
            }
            private set
            {
                configName = value;
            }
        }

        private void RegisterDatabaseEvents()
        {
            try
            {
                LufsGenplan.AcadApp.RegisterDatabaseAppendEvent(DbEvent_ObjectAppened_Handler_MarkingCalc);
                LufsGenplan.AcadApp.RegisterDatabaseEraseEvent(DbEvent_ObjectErased_Handler_MarkingCalc);
                LufsGenplan.AcadApp.RegisterDatabaseModifiEvent(DbEvent_ObjectModified_Handler_MarkingCalc);
            }
            catch (Exception ex)
            {
                LufsGenplan.AcadApp.AcaEd.WriteMessage("\nERROR: MarkingCalc.RegisterDatabaseEvents() " + ex + "\n");
            }
        }

        private void SetcbMashtItems()
        {
            try
            {
                String[] items = { "1:1", "1:10", "1:20", "1:50", "1:100", "1:200", "1:500", "1:1000", "1:2000", "1:5000" };
                foreach (var item in items)
                {
                    cbMasht.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                LufsGenplan.AcadApp.AcaEd.WriteMessage("\nERROR: MarkingCalc.SetcbMashtItems() " + ex + "\n");
            }
        }

        private void SetcbCategoryItems()
        {
            try
            {
                foreach (var item in listOfTypes)
                {
                    if (item.EntityType == RazmType.typeOfEntity.Line || item.EntityType == RazmType.typeOfEntity.DoubleLineCenter || item.EntityType == RazmType.typeOfEntity.DoubleLineSide)
                    {
                        foreach (var category in item.CategoryData)
                        {
                            cbCategory.Items.Add(category.Category);
                        }
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                LufsGenplan.AcadApp.AcaEd.WriteMessage("\nERROR: MarkingCalc.SetcbCategoryItems() " + ex + "\n");
            }
        }

        private void SetOverrule()
        {
            try
            {
                if (_drawOverrule == null)
                {
                    System.Collections.Generic.ICollection<RazmType> lineTypesOfDoubleLines = new System.Collections.Generic.List<RazmType>();
                    foreach (var type in listOfTypes)
                    {
                        if (type.EntityType == RazmType.typeOfEntity.DoubleLineCenter || type.EntityType == RazmType.typeOfEntity.DoubleLineSide)
                        {
                            lineTypesOfDoubleLines.Add(type);
                        }
                    }

                    _drawOverrule = MarkingDarawOverrule.GetMarkingDarawOverrule(lineTypesOfDoubleLines, OFFSET);
                    
                    if (_drawOverrule != null)
                    {
                        Autodesk.AutoCAD.GraphicsInterface.DrawableOverrule.Overruling = true;
                        Autodesk.AutoCAD.DatabaseServices.ObjectOverrule.AddOverrule(Autodesk.AutoCAD.Runtime.RXObject.GetClass(typeof(Autodesk.AutoCAD.DatabaseServices.Polyline)), _drawOverrule, false);
                        Autodesk.AutoCAD.Runtime.Overrule.Overruling = true;
                        AcadApp.AcaEd.Regen();
                    }
                }
            }
            catch (Exception ex)
            {
                AcadApp.AcaEd.WriteMessage("ERROR: MarkingCalc.SetOverrule() " + ex + "\n");
                BtSolut.Enabled = false;
            }
        }

        private void LoadMarkingType(string configFileName)
        {
            StringBuilder sb = new StringBuilder();
            try
            {
                using (System.IO.StreamReader reader = System.IO.File.OpenText(configFileName))
                {
                    using (System.IO.StringWriter sw = new System.IO.StringWriter(sb))
                    {
                        sw.Write(reader.ReadToEnd());
                    }
                }
            }
            catch (Exception ex)
            {
                AcadApp.AcaEd.WriteMessage("ERROR: MarkingCalc.LoadMarkingType(): ReadFile " + ex + "\n");
                this.BtSolut.Enabled = false;
            }
            try
            {
                using (System.IO.StringReader sr = new System.IO.StringReader(sb.ToString()))
                {
                    System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(System.Collections.Generic.List<RazmType>));
                    listOfTypes = serializer.Deserialize(sr) as System.Collections.Generic.List<RazmType>;
                }
            }
            catch (Exception ex)
            {
                AcadApp.AcaEd.WriteMessage("ERROR: MarkingCalc.LoadMarkingType(): DeserializeFile " + ex + "\n");
                this.BtSolut.Enabled = false;
            }
        }

        private String SetCurrentConfigFile()
        {
            String fileName = "";
            try
            {
                String codeBase = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;
                System.UriBuilder uri = new System.UriBuilder(codeBase);
                String path = System.Uri.UnescapeDataString(uri.Path);
                String dir = System.IO.Path.GetDirectoryName(path);
                fileName = dir + @"\" + configName;
                return fileName;
            }
            catch (Exception ex)
            {
                AcadApp.AcaEd.WriteMessage("ERROR: MarkingCalc.SetCurrentConfigFile() " + ex + "\n");
                this.BtSolut.Enabled = false;
            }
            return fileName;
        }

        private Double GetCurWidth(String razmType, String category)
        {
            try
            {
                Double result = 1.0d; // For the Area types catData is empty and function return 1.0d;
                foreach (var item in listOfTypes)
                {
                    if (razmType == item.Type)
                    {
                        foreach (var catData in item.CategoryData)
                        {
                            if (category == catData.Category)
                            {
                                result = catData.Width;
                                return result;
                            }
                        }
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                AcadApp.AcaEd.WriteMessage("ERROR: MarkingCalc.GetCurWidth() " + ex + "\n");
                this.BtSolut.Enabled = false;
            }
            return 0.0d;
        }

        private void DisplayResult()
        {
            try
            {
                if (calculatedData == null)
                {
                    calculatedData = new System.Collections.ObjectModel.Collection<RazmData>();
                }
                else
                {
                    calculatedData.Clear();
                }

                var index = 1;
                var outFrmt = "{0:0.00}";
                var scaleFactor = MashtToDouble(CurrentMasht);

                IEnumerable<KeyValuePair<String, RazmData>> query = this.rawData.OrderBy(a => a.Key, new CompareRazmType());

                foreach (var item in query)
                {
                    Double length = Double.Parse(item.Value.RazmLenght) * scaleFactor;
                    String area_A = RazmCollection.GetPaintArea(item.Value.RazmArea, Math.Pow(scaleFactor, 2.0d), GetCurWidth(item.Key, CurrentCategory));
                    String area_L = RazmCollection.GetPaintArea(item.Value.RazmArea, scaleFactor, GetCurWidth(item.Key, CurrentCategory));

                    String description;
                    if (item.Value.EntityType == RazmType.typeOfEntity.Line)
                    {
                        description = item.Value.RazmDescription + " B=" + String.Format(outFrmt, GetCurWidth(item.Key, CurrentCategory)) + "м.";
                        calculatedData.Add(new RazmData(index.ToString(), description, item.Key, String.Format(outFrmt, length) + "м.п.", area_L + "м.кв.", item.Value.EntityType));
                    }
                    else if ((item.Value.EntityType == RazmType.typeOfEntity.DoubleLineCenter) || (item.Value.EntityType == RazmType.typeOfEntity.DoubleLineSide))
                    {
                        description = item.Value.RazmDescription + " B=2x" + String.Format(outFrmt, GetCurWidth(item.Key, CurrentCategory)) + "м.";
                        calculatedData.Add(new RazmData(index.ToString(), description, item.Key, String.Format(outFrmt, length) + "м.п.", area_L + "м.кв.", item.Value.EntityType));
                    }
                    else if (item.Value.EntityType == RazmType.typeOfEntity.Area)
                    {
                        description = item.Value.RazmDescription;
                        calculatedData.Add(new RazmData(index.ToString(), description, item.Key, item.Value.NPP + "шт.", area_A + "м.кв.", item.Value.EntityType));
                    }
                    else if (item.Value.EntityType == RazmType.typeOfEntity.Block)
                    {
                        description = item.Value.RazmDescription;
                        calculatedData.Add(new RazmData(index.ToString(), description, item.Key, item.Value.NPP + "шт.", area_A + "м.кв.", item.Value.EntityType));
                    }
                    index++;
                }
                dataGridResult.DataSource = calculatedData;
                dataGridResult.Refresh();
            }
            catch (Exception ex)
            {
                AcadApp.AcaEd.WriteMessage("ERROR: MarkingCalc.DisplayResult() " + ex + "\n");
            }
        }

        private double MashtToDouble(string mashtab)
        {
            try
            {
                string temp = mashtab.Substring(mashtab.IndexOf(':') + 1);
                
                return (System.Convert.ToDouble(temp) / 1000.0d);
            }
            catch (Exception ex)
            {
                AcadApp.AcaEd.WriteMessage("\nНе верно заполнено поле масштаб: " + ex + "\n");
            }
            return 0.0d;
        }

        private RazmCollection CalcRazm(Autodesk.AutoCAD.EditorInput.SelectionSet sset)
        {
            using (var trans = AcadApp.StartTransaction())
            {
                try
                {
                    //result ******************************************** 
                    RazmCollection result = new RazmCollection();
                    //***************************************************
                    foreach (Autodesk.AutoCAD.DatabaseServices.ObjectId id in sset.GetObjectIds())
                    {
                        //подсчет длин и площадей для Polyline Line Arc Circle и штук для BlockReference
                        Autodesk.AutoCAD.DatabaseServices.DBObject selOb;

                        selOb = trans.GetObject(id, Autodesk.AutoCAD.DatabaseServices.OpenMode.ForRead);

                        if (selOb.GetType().Name == "Polyline" || selOb.GetType().Name == "Line" || selOb.GetType().Name == "Arc" ||
                            selOb.GetType().Name == "Circle" || selOb.GetType().Name == "BlockReference")
                        {
                            foreach (var lineType in listOfTypes)
                            {
                                if ((lineType.EntityType == RazmType.typeOfEntity.Block) && (selOb.GetType().Name == "BlockReference"))
                                {
                                    Autodesk.AutoCAD.DatabaseServices.BlockReference blr = selOb as Autodesk.AutoCAD.DatabaseServices.BlockReference;
                                    if (blr.Name == lineType.Type)
                                    {
                                        string area = "";
                                        foreach (var paint in lineType.Paints) 
                                        {
                                            area += paint.ShortTitle + ":" + paint.Amount + " ";
                                        }
                                        //For block references calculated only total amount and area inside RazmCollection into RazmData.NPP
                                        result.Add(new RazmData("1", lineType.Description, lineType.Type, "0.0", area, lineType.EntityType));
                                        break; // lineType
                                    }
                                }//BlockReference
                                else if ((lineType.EntityType == RazmType.typeOfEntity.Area) && (selOb.GetType().Name != "BlockReference"))
                                {
                                    Autodesk.AutoCAD.DatabaseServices.Curve cur = selOb as Autodesk.AutoCAD.DatabaseServices.Curve;
                                    if (cur.Linetype == lineType.LineTypeName)
                                    {
                                        string area = "";
                                        foreach (var paint in lineType.Paints)
                                        {
                                            area += paint.ShortTitle + ":" + (paint.Amount * GetArea(selOb)).ToString() + " ";
                                        }
                                        //For the area type calculated only area
                                        result.Add(new RazmData("1", lineType.Description, lineType.Type, "0.0", area, lineType.EntityType));
                                    }
                                }//Area
                                else if (((lineType.EntityType == RazmType.typeOfEntity.Line) ||
                                          (lineType.EntityType == RazmType.typeOfEntity.DoubleLineCenter) ||
                                          (lineType.EntityType == RazmType.typeOfEntity.DoubleLineSide)) && (selOb.GetType().Name != "BlockReference"))
                                {
                                    Autodesk.AutoCAD.DatabaseServices.Curve cur = selOb as Autodesk.AutoCAD.DatabaseServices.Curve;
                                    if (cur.Linetype == lineType.LineTypeName)
                                    {
                                        string area = "";
                                        foreach (var paint in lineType.Paints)
                                        {
                                            //This area without width. Width applayed in DisplayResult
                                            area += paint.ShortTitle + ":" + (paint.Amount * GetPaintedLength(GetLength(selOb), lineType)).ToString() + " ";
                                        }
                                        //For the line type calculated length and area
                                        result.Add(new RazmData("1", lineType.Description, lineType.Type, GetLength(selOb).ToString(), area, lineType.EntityType));
                                    }
                                }//Line
                            }// Foreach in listOfTypes
                        }//If allowed entity
                    }// Foreach in ssget
                    return result;
                }
                catch (System.Exception ex)
                {
                    AcadApp.AcaEd.WriteMessage("ERROR: MarkingCalc().CalcRazm " + ex + "\n");
                }
            }// using transaction
            return null;
        }

        private Double GetPaintedLength(Double length, RazmType type)
        {
            try
            {
                Double result = 0.0d;
                foreach (var len in type.DashSpaceLen)
                {
                    var total = len.Dash + len.Space;
                    result += len.Dash * length / total;
                }

                return result;
            }
            catch (System.Exception ex)
            {
                AcadApp.AcaEd.WriteMessage("ERROR: MarkingCalc().GetPaintedLength " + ex + "\n");
            }
            return -1.0d;
        }

        private Double GetLength(Autodesk.AutoCAD.DatabaseServices.DBObject dbobj)
        {
            try
            {
                Double result = 0.0d;

                if (dbobj.GetType().Name == "Polyline")
                {
                    result = (dbobj as Autodesk.AutoCAD.DatabaseServices.Polyline).Length;
                }
                else if (dbobj.GetType().Name == "Line")
                {
                    result = (dbobj as Autodesk.AutoCAD.DatabaseServices.Line).Length;
                }
                else if (dbobj.GetType().Name == "Arc")
                {
                    result = (dbobj as Autodesk.AutoCAD.DatabaseServices.Arc).Length;
                }
                else if (dbobj.GetType().Name == "Circle")
                {
                    result = (dbobj as Autodesk.AutoCAD.DatabaseServices.Circle).Circumference;
                }
                return result;
            }
            catch (System.Exception ex)
            {
                AcadApp.AcaEd.WriteMessage("ERROR: MarkingCalc().GetLength " + ex + "\n");
            }
            return -1.0d;
        }

        private Double GetArea(Autodesk.AutoCAD.DatabaseServices.DBObject dbobj)
        {
            Double result = 0.0d;
            try
            {
                if (dbobj.GetType().Name == "Polyline")
                {
                    result = (dbobj as Autodesk.AutoCAD.DatabaseServices.Polyline).Area;
                }
                else if (dbobj.GetType().Name == "Circle")
                {
                    result = (dbobj as Autodesk.AutoCAD.DatabaseServices.Circle).Area;
                }
            }
            catch (System.Exception ex)
            {
                AcadApp.AcaEd.WriteMessage("ERROR: MarkingCalc().GetArea " + ex + "\n");
            }
            return result;
        }
        
        private void BtSolut_Click(object sender, EventArgs e)
        {
            try
            {
                BtSolut.Enabled = false;
                btClear_Click(sender, e);//For clearing current data if exist
                if (RbUser.Checked)
                {
                    Autodesk.AutoCAD.EditorInput.PromptSelectionOptions edOpt = new Autodesk.AutoCAD.EditorInput.PromptSelectionOptions();
                    Autodesk.AutoCAD.EditorInput.PromptSelectionResult edRes;
                    edOpt.MessageForAdding = "Выберите объекты";
                    edOpt.AllowDuplicates = false;
                    Autodesk.AutoCAD.DatabaseServices.TypedValue[] selval;

                    List<Autodesk.AutoCAD.DatabaseServices.TypedValue> selval_list = new List<Autodesk.AutoCAD.DatabaseServices.TypedValue>();
                    selval_list.Add(new Autodesk.AutoCAD.DatabaseServices.TypedValue((int)(Autodesk.AutoCAD.DatabaseServices.DxfCode.LayerName), cbLayerUse.Text));

                    selval = selval_list.ToArray();
                    edOpt.RejectPaperspaceViewport = true;
                    edOpt.RejectObjectsFromNonCurrentSpace = true;

                    Autodesk.AutoCAD.EditorInput.SelectionFilter ssfilter = new Autodesk.AutoCAD.EditorInput.SelectionFilter(selval);
                    edRes = AcadApp.AcaEd.GetSelection(edOpt, ssfilter);

                    Autodesk.AutoCAD.EditorInput.SelectionSet sset = edRes.Value;
                    if (edRes.Status == Autodesk.AutoCAD.EditorInput.PromptStatus.OK)
                    {
                        this.rawData = CalcRazm(sset);
                        DisplayResult();
                    }
                }
                else if (RbAll.Checked)
                {
                    Autodesk.AutoCAD.EditorInput.PromptSelectionResult edRes;
                    Autodesk.AutoCAD.DatabaseServices.TypedValue[] selval;

                    List<Autodesk.AutoCAD.DatabaseServices.TypedValue> selval_list = new List<Autodesk.AutoCAD.DatabaseServices.TypedValue>();
                    selval_list.Add(new Autodesk.AutoCAD.DatabaseServices.TypedValue((int)(Autodesk.AutoCAD.DatabaseServices.DxfCode.LayerName), cbLayerUse.Text));
                    selval = selval_list.ToArray();

                    Autodesk.AutoCAD.EditorInput.SelectionFilter ssfilter = new Autodesk.AutoCAD.EditorInput.SelectionFilter(selval);

                    edRes = AcadApp.AcaEd.SelectAll(ssfilter);
                    Autodesk.AutoCAD.EditorInput.SelectionSet sset = edRes.Value;
                    if (edRes.Status == Autodesk.AutoCAD.EditorInput.PromptStatus.OK)
                    {
                        this.rawData = CalcRazm(sset);
                        DisplayResult();
                    }
                }
                btToExcel.Enabled = true;
                IsTableEmpty = false;
            }
            catch (System.Exception ex)
            {
                AcadApp.AcaEd.WriteMessage("ERROR: MarkingCalc().BtSolut_Click " + ex + "\n");
            }
            finally
            {
                BtSolut.Enabled = true;
            }
        }

        private void CheckbMasUse_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbMasht != null && CheckbMasUse.Checked)
                {
                    cbMasht.Enabled = true;
                }
                else
                {
                    cbMasht.Enabled = false;
                }
            }
            catch (System.Exception ex)
            {
                AcadApp.AcaEd.WriteMessage("ERROR: MarkingCalc().CheckbMasUse_CheckedChanged " + ex + "\n");
            }
        }

        private void btSample_Click(object sender, EventArgs e)
        {
            try
            {
                btSample.Enabled = false;
                String layerName;
                String lineType;
                Autodesk.AutoCAD.Colors.Color color;

                AcadApp.SelectSample(true, out layerName, out color, out lineType);

                if (layerName == null)
                {
                    return;
                }

                for (int j = 0; j < cbLayerUse.Items.Count; j++)
                {
                    if ((cbLayerUse.Items[j] as AcadUtils.CbAutocadItem).Name == layerName)
                    {
                        cbLayerUse.SelectedIndex = j;
                        break; //for
                    }
                }
                currentLayer = cbLayerUse.Text;
            }
            catch (Exception ex)
            {
                AcadApp.AcaEd.WriteMessage("\nERROR: MarkingCalc.btSample_Click() " + ex + "\n");
            }
            finally
            {
                btSample.Enabled = true;
            }
        }

        private void btClear_Click(object sender, EventArgs e)
        {
            try
            {
                if (calculatedData != null)
                {
                    dataGridResult.DataSource = null;
                    calculatedData.Clear();
                }
                dataGridResult.Refresh();
                btToExcel.Enabled = false;
                IsTableEmpty = true;
            }
            catch (System.Exception ex)
            {
                AcadApp.AcaEd.WriteMessage("ERROR: MarkingCalc().btClear_Click " + ex + "\n");
            }
        }

        private void cbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CurrentCategory = cbCategory.Text;
                if (rawData != null)
                {
                    if (!IsTableEmpty)
                    {
                        DisplayResult();
                    }
                }
            }
            catch (System.Exception ex)
            {
                AcadApp.AcaEd.WriteMessage("ERROR: MarkingCalc().cbCategory_SelectedIndexChanged " + ex + "\n");
            }
        }

        private void cbMasht_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CurrentMasht = cbMasht.Text;
                if (rawData != null)
                {
                    if (!IsTableEmpty)
                    {
                        DisplayResult();
                    }
                }
            }
            catch (System.Exception ex)
            {
                AcadApp.AcaEd.WriteMessage("ERROR: MarkingCalc().cbMasht_SelectedIndexChanged " + ex + "\n");
            }
        }

        private void btToExcel_Click(object sender, EventArgs e)
        {
            try
            {
                LufsGenplan.AcadApp.AcaEd.WriteMessage("\nПередача данных в Excel:\n");

                //Create filename for the Creating excel file in same folder where placed current drawing file
                var fileName = LufsGenplan.AcadApp.GetFileName() + ".xlsx";

                //Write data to the excel application
                var resFileName = WriteDataToExcelRoadPavenment(ResultToExcelMarkingCalc(calculatedData), fileName, templateName);
                LufsGenplan.AcadApp.AcaEd.WriteMessage("\nСоздан файл: " + resFileName + "\n");
            }
            catch (System.Exception ex)
            {
                AcadApp.AcaEd.WriteMessage("ERROR: MarkingCalc().btToExcel_Click " + ex + "\n");
            }
        }

        private void cbLayerUse_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                currentLayer = cbLayerUse.Text;
            }
            catch (System.Exception ex)
            {
                AcadApp.AcaEd.WriteMessage("ERROR: MarkingCalc().cbLayerUse_SelectedIndexChanged " + ex + "\n");
            }
        }

        #region WriteToExcel
        private static int HEADERROWSMarkingCalc = 5; // 5 rows have table's header
        private static int HEADERCOLUMNSMarkingCalc = 5; // 5 columns have table's header

        private static void WriteArrayMarkingCalc(int rows, int columns, Microsoft.Office.Interop.Excel.Worksheet worksheet, object[,] data)
        {
            try
            {
                var startRow = HEADERROWSMarkingCalc;
                var startCell = worksheet.Cells[startRow + 1, 1] as Microsoft.Office.Interop.Excel.Range; // At excel range start at the 1,1 not the 0,0
                var endCell = worksheet.Cells[rows + startRow, columns] as Microsoft.Office.Interop.Excel.Range;
                var writeRange = worksheet.get_Range(startCell, endCell);
                writeRange.Value2 = data;
            }
            catch (System.Exception ex)
            {
                AcadApp.AcaEd.WriteMessage("ERROR: MarkingCalc().WriteArrayMarkingCalc " + ex + "\n");
            }
        }

        public static object[,] ResultToExcelMarkingCalc(System.Collections.ObjectModel.Collection<RazmData> res)
        {
            try
            {
                var data = new object[(res.Count + 1), HEADERCOLUMNSMarkingCalc];
                var count = 0;
                foreach (var r in res)
                {
                    data[count, 0] = r.NPP;
                    data[count, 1] = r.RazmDescription;
                    data[count, 2] = r.RazmType;
                    data[count, 3] = r.RazmLenght;
                    data[count, 4] = r.RazmArea;

                    count += 1;
                }
                return data;
            }
            catch (System.Exception ex)
            {
                AcadApp.AcaEd.WriteMessage("ERROR: MarkingCalc().ResultToExcelMarkingCalc " + ex + "\n");
            }
            return null;
        }

        public static String WriteDataToExcelRoadPavenment(object[,] obData, String fileName, String templateName)
        {
            Microsoft.Office.Interop.Excel.Application xlApp = null;
            try
            {
                String codeBase = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;
                System.UriBuilder uri = new System.UriBuilder(codeBase);
                string path = System.Uri.UnescapeDataString(uri.Path);
                string dir = System.IO.Path.GetDirectoryName(path);
                String template = dir + @"\" + templateName;

                xlApp = new Microsoft.Office.Interop.Excel.Application();
                xlApp.DisplayAlerts = false;
                var wbTemp = xlApp.Workbooks.Open(template, Type.Missing, true, Type.Missing, Type.Missing,
                                              Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                                              Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                                              Type.Missing, Type.Missing);

                var wsTemp = wbTemp.ActiveSheet as Microsoft.Office.Interop.Excel.Worksheet;
                wsTemp.Name = "Genplan";
                WriteArrayMarkingCalc(obData.GetLength(0), obData.GetLength(1), wsTemp, obData);

                xlApp.Visible = true;

                wbTemp.SaveAs(fileName, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookDefault,
                              System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value,
                              System.Reflection.Missing.Value, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange,
                              System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value,
                              System.Reflection.Missing.Value, System.Reflection.Missing.Value);

                return fileName;
            }
            catch (System.Exception ex)
            {
                if (xlApp != null)
                {
                    xlApp.Quit();
                }
                return ("ERROR: WriteResultToExcel -> " + ex.ToString());
            }
        }
        #endregion WriteToExcel
        
        #region Database event implementation

        public void DbEvent_ObjectAppened_Handler_MarkingCalc(object sender, Autodesk.AutoCAD.DatabaseServices.ObjectEventArgs e)
        {
            try
            {
                if (e.DBObject is Autodesk.AutoCAD.DatabaseServices.LayerTableRecord)
                {
                    //LufsGenplan.AcadApp.AcaEd.WriteMessage("DEBUG: ObjectAppened sender = " + (e.DBObject as Autodesk.AutoCAD.DatabaseServices.LayerTableRecord).Name +
                    //    " ID = " + (e.DBObject as Autodesk.AutoCAD.DatabaseServices.LayerTableRecord).ObjectId + "\n");

                    cbLayerUse.Items.Add(new AcadUtils.CbAutocadItem((e.DBObject as Autodesk.AutoCAD.DatabaseServices.LayerTableRecord).Name,
                                                    (e.DBObject as Autodesk.AutoCAD.DatabaseServices.LayerTableRecord).ObjectId));
                }
            }
            catch (Exception ex)
            {
                LufsGenplan.AcadApp.AcaEd.WriteMessage("\nERROR: MarkingCalc.DbEvent_ObjectAppened_Handler() " + ex + "\n");
            }
        }

        public void DbEvent_ObjectErased_Handler_MarkingCalc(object sender, Autodesk.AutoCAD.DatabaseServices.ObjectErasedEventArgs e)
        {
            try
            {
                if (e.DBObject is Autodesk.AutoCAD.DatabaseServices.LayerTableRecord)
                {
                    //LufsGenplan.AcadApp.AcaEd.WriteMessage("DEBUG: ObjectErased sender = " + (e.DBObject as Autodesk.AutoCAD.DatabaseServices.LayerTableRecord).Name +
                    //    " ID = " + (e.DBObject as Autodesk.AutoCAD.DatabaseServices.LayerTableRecord).ObjectId + "\n");

                    AcadUtils.CbAutocadItem obj = new AcadUtils.CbAutocadItem((e.DBObject as Autodesk.AutoCAD.DatabaseServices.LayerTableRecord).Name,
                                                    (e.DBObject as Autodesk.AutoCAD.DatabaseServices.LayerTableRecord).ObjectId);
                    if (obj == null)
                    {
                        return;
                    }
                    for (int i = 0; i < cbLayerUse.Items.Count; i++)
                    {
                        if ((cbLayerUse.Items[i] as AcadUtils.CbAutocadItem).ID == obj.ID)
                        {
                            if (currentLayer == obj.Name)
                            {
                                cbLayerUse.Items.RemoveAt(i);
                                currentLayer = "0";
                                for (int j = 0; j < cbLayerUse.Items.Count; j++)
                                {
                                    if ((cbLayerUse.Items[j] as AcadUtils.CbAutocadItem).Name == "0")
                                    {
                                        cbLayerUse.SelectedItem = cbLayerUse.Items[j];
                                        break; //for j
                                    }
                                }
                            }
                            else
                            {
                                cbLayerUse.Items.RemoveAt(i);
                            }
                            break; //for i
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LufsGenplan.AcadApp.AcaEd.WriteMessage("\nERROR: MarkingCalc.DbEvent_ObjectErased_Handler() " + ex + "\n");
            }
        }

        public void DbEvent_ObjectModified_Handler_MarkingCalc(object sender, Autodesk.AutoCAD.DatabaseServices.ObjectEventArgs e)
        {
            try
            {
                if (e.DBObject is Autodesk.AutoCAD.DatabaseServices.LayerTableRecord)
                {
                    //LufsGenplan.AcadApp.AcaEd.WriteMessage("DEBUG: ObjectModified sender = " + (e.DBObject as Autodesk.AutoCAD.DatabaseServices.LayerTableRecord).Name +
                    //    " ID = " + (e.DBObject as Autodesk.AutoCAD.DatabaseServices.LayerTableRecord).ObjectId + "\n");

                    AcadUtils.CbAutocadItem obj = new AcadUtils.CbAutocadItem((e.DBObject as Autodesk.AutoCAD.DatabaseServices.LayerTableRecord).Name,
                                                    (e.DBObject as Autodesk.AutoCAD.DatabaseServices.LayerTableRecord).ObjectId);
                    if (obj == null)
                    {
                        return;
                    }
                    for (int i = 0; i < cbLayerUse.Items.Count; i++)
                    {
                        if ((cbLayerUse.Items[i] as AcadUtils.CbAutocadItem).ID == obj.ID)
                        {
                            if ((cbLayerUse.Items[i] as AcadUtils.CbAutocadItem).Name == currentLayer)
                            {
                                cbLayerUse.Items.RemoveAt(i);
                                cbLayerUse.Items.Insert(i, obj);
                                currentLayer = cbLayerUse.Items[i].ToString();
                                cbLayerUse.SelectedItem = obj;
                            }
                            else
                            {
                                cbLayerUse.Items.RemoveAt(i);
                                cbLayerUse.Items.Insert(i, obj);
                            }
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LufsGenplan.AcadApp.AcaEd.WriteMessage("\nERROR: MarkingCalc.DbEvent_ObjectModified_Handler() " + ex + "\n");
            }
        }

        #endregion Database event implementation

        private static MarkingDarawOverrule _drawOverrule;
        //Collection of the data to display at the grid. Changed by method cbCategory_SelectedIndexChanged and cbMasht_SelectedIndexChanged
        private System.Collections.ObjectModel.Collection<RazmData> calculatedData;
        //Collection of the raw data (without applaying scale factor and width) taken from the drawing. Changed by methods BtSolut_Click
        private RazmCollection rawData;
        //Collection of the types of markings to be calculated from configuration file. Initialized into constructor
        private System.Collections.Generic.ICollection<RazmType> listOfTypes;
        //Distance between line into DoubleLine... marking 
        private const Double OFFSET = 0.5d;
        private static String configFileName;
        private static String configName = "razmConfig.xml";
        private String templateName = "MarkingCalc.xlsx";
        private String CurrentCategory { get; set; }
        private String CurrentMasht { get; set; }
        private Boolean IsTableEmpty { get; set; }
        private String currentLayer;
    }
}
