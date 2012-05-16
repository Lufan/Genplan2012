/*
 * Created by Луферов Александр Николаевич
 * 
 * 
 * 
 * Лицензия GPL v2.0: http://www.gnu.org/licenses/gpl-2.0.html.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LufsGenplan
{
    /// <summary>
    /// Interaction logic for MarkingCalc.xaml
    /// </summary>
    public partial class MarkingCalcOld : UserControl, ILUFSPlug
    {
        public Boolean DataUpdate()
        {
            btRefresh_Click(null, null);
            return true;
        }
        public String GetModuleName()
        {
            return "Разметка";
        }
        public PlugType GetTargetApp()
        {
            return PlugType.Autocad;
        }
        
        //Collection of the data to display at the grid. Changed by method cbCategory_SelectionChanged and 
        private System.Collections.ObjectModel.ObservableCollection<RazmData> calculatedData;
        //Collection of the raw data (without applaying scale factor and width) taken from the drawing. Changed by methods BtSolut_Click
        private RazmCollection rawData;
        
        //Collection of the types of markings to be calculated from configuration file. Initialized into constructor
        private System.Collections.Generic.ICollection<RazmType> listOfTypes;

        //Distance between line into DoubleLine... marking 
        private const Double OFFSET = 0.5d;

        private static String configFileName;
        private static String configName = "razmConfig.xml";
        private String templateName = "MarkingCalc.xlsx";

        private String CurrentCategory {get;set;}
        private String CurrentMasht{get;set;}

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

        private static MarkingDarawOverrule _drawOverrule;

        public MarkingCalcOld()
        {
            InitializeComponent();
            //Set current config file name
            configFileName = SetCurrentConfigFile();

            LoadMarkingType(configFileName);

            btRefresh_Click(null, null);

            SetcbCategoryItems();
            cbCategory.SelectedIndex = 0;
            CurrentCategory = cbCategory.Text;

            SetcbMashtItems();
            cbMasht.SelectedIndex = 6;
            CurrentMasht = cbMasht.Text;

            SetOverrule();
        }
        private void SetcbMashtItems()
        {
            String[] items = { "1:1", "1:10", "1:20", "1:50", "1:100", "1:200", "1:500", "1:1000", "1:2000", "1:5000"};
            foreach (var item in items)
            {
                cbMasht.Items.Add(item);
            }
        }
        private void SetcbCategoryItems()
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

                    _drawOverrule = new MarkingDarawOverrule(lineTypesOfDoubleLines, OFFSET);
                    Autodesk.AutoCAD.Runtime.Overrule.AddOverrule(Autodesk.AutoCAD.Runtime.RXObject.GetClass(typeof(Autodesk.AutoCAD.DatabaseServices.Polyline)), _drawOverrule, false);
                    Autodesk.AutoCAD.Runtime.Overrule.Overruling = true;
                    AcadApp.AcaEd.Regen();
                }
            }
            catch (Exception ex)
            {
                AcadApp.AcaEd.WriteMessage("ERROR: MarkingCalc.SetOverrule() " + ex + "\n");
                this.BtSolut.IsEnabled = false;
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
                this.BtSolut.IsEnabled = false;
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
                this.BtSolut.IsEnabled = false;
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
                this.BtSolut.IsEnabled = false;
            }
            return fileName;
        }
        private void BtSolut_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (RbUser.IsChecked ?? false)
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
                else if (RbAll.IsChecked ?? false)
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
            }
            catch (System.Exception ex)
            {
                AcadApp.AcaEd.WriteMessage("ERROR: MarkingCalc().BtSolut_Click " + ex + "\n");
            }
        }
        private Double GetCurWidth(String razmType, String category)
        {
            Double result = 0.0d;
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
        private void DisplayResult()
        {
            try
            {
                if (calculatedData == null)
                {
                    calculatedData = new System.Collections.ObjectModel.ObservableCollection<RazmData>();
                }
                else
                {
                    calculatedData.Clear();
                }

                var index = 1;
                var outFrmt = "{0:0.00}";
                var scaleFactor = MashtToDouble(CurrentMasht);

                AcadApp.AcaEd.WriteMessage("DEBUG: MarkingCalc.DisplayResult() M = " + CurrentMasht + "\n");
                AcadApp.AcaEd.WriteMessage("DEBUG: MarkingCalc.DisplayResult() Scale = " + scaleFactor + "\n");
                AcadApp.AcaEd.WriteMessage("DEBUG: MarkingCalc.DisplayResult() Category = " + CurrentCategory + "\n");
                
                IEnumerable<KeyValuePair<String, RazmData>> query = this.rawData.OrderBy(a => a.Key, new CompareRazmType());

                foreach (var item in query)
                {
                    Double length = Double.Parse(item.Value.RazmLenght) * scaleFactor;
                    Double area = Double.Parse(item.Value.RazmArea) * Math.Pow(scaleFactor, 2.0d);
                    String description;
                    if (item.Value.EntityType == RazmType.typeOfEntity.Line)
                    {
                        description = item.Value.RazmDescription + " B=" + String.Format(outFrmt, GetCurWidth(item.Key, CurrentCategory)) + "м.";
                        calculatedData.Add(new RazmData(index.ToString(), description, item.Key, String.Format(outFrmt, length) + "м.п.", String.Format(outFrmt, area) + "м.кв.", item.Value.EntityType));
                    }
                    else if ((item.Value.EntityType == RazmType.typeOfEntity.DoubleLineCenter) || (item.Value.EntityType ==  RazmType.typeOfEntity.DoubleLineSide))
                    {
                        description = item.Value.RazmDescription + " B=2x" + String.Format(outFrmt, GetCurWidth(item.Key, CurrentCategory)) + "м.";
                        calculatedData.Add(new RazmData(index.ToString(), description, item.Key, String.Format(outFrmt, length) + "м.п.", String.Format(outFrmt, area) + "м.кв.", item.Value.EntityType));
                    }
                    else if (item.Value.EntityType == RazmType.typeOfEntity.Area)
                    {
                        description = item.Value.RazmDescription;
                        calculatedData.Add(new RazmData(index.ToString(), description, item.Key, item.Value.NPP + "шт.", String.Format(outFrmt, area) + "м.кв.", item.Value.EntityType));
                    }
                    else if (item.Value.EntityType == RazmType.typeOfEntity.Block)
                    {
                        description = item.Value.RazmDescription;
                        calculatedData.Add(new RazmData(index.ToString(), description, item.Key, item.Value.NPP + "шт.", String.Format(outFrmt, area) + "м.кв.", item.Value.EntityType));
                    }
                    index++;
                }
                dataGridResult.ItemsSource = calculatedData;
                dataGridResult.Items.Refresh();
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
                                        //For block references calculated only total amount inside RazmCollection into RazmData.NPP
                                        result.Add(new RazmData("1", lineType.Description, lineType.Type, "0.0", "0.0", lineType.EntityType));
                                        break; // lineType
                                    }
                                }//BlockReference
                                else if ((lineType.EntityType == RazmType.typeOfEntity.Area) && (selOb.GetType().Name != "BlockReference"))
                                {
                                    Autodesk.AutoCAD.DatabaseServices.Curve cur = selOb as Autodesk.AutoCAD.DatabaseServices.Curve;
                                    if (cur.Linetype == lineType.LineTypeName)
                                    {
                                        //For the area type calculated only total area
                                        result.Add(new RazmData("1", lineType.Description, lineType.Type, "0.0", GetArea(selOb).ToString(), lineType.EntityType));
                                    }
                                }//Area
                                else if (((lineType.EntityType == RazmType.typeOfEntity.Line) || 
                                          (lineType.EntityType == RazmType.typeOfEntity.DoubleLineCenter) ||
                                          (lineType.EntityType == RazmType.typeOfEntity.DoubleLineSide)) && (selOb.GetType().Name != "BlockReference"))
                                {
                                    Autodesk.AutoCAD.DatabaseServices.Curve cur = selOb as Autodesk.AutoCAD.DatabaseServices.Curve;
                                    if (cur.Linetype == lineType.LineTypeName)
                                    {
                                        //For the line type calculated only total length
                                        result.Add(new RazmData("1", lineType.Description, lineType.Type, GetLength(selOb).ToString(), "0.0", lineType.EntityType));
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
        private Double GetLength(Autodesk.AutoCAD.DatabaseServices.DBObject dbobj)
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
        private Double GetArea(Autodesk.AutoCAD.DatabaseServices.DBObject dbobj)
        {
            Double result = 0.0d;

            if (dbobj.GetType().Name == "Polyline")
            {
                result = (dbobj as Autodesk.AutoCAD.DatabaseServices.Polyline).Area;
            }
            else if (dbobj.GetType().Name == "Circle")
            {
                result = (dbobj as Autodesk.AutoCAD.DatabaseServices.Circle).Area;
            }
            return result;
        }
        private void CheckbMasUse_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbMasht != null)
                {
                    cbMasht.IsEnabled = true;
                }
            }
            catch (System.Exception ex)
            {
                AcadApp.AcaEd.WriteMessage("ERROR: MarkingCalc().CheckbMasUse_Checked " + ex + "\n");
            }
        }
        private void CheckbMasUse_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbMasht != null)
                {
                    cbMasht.IsEnabled = false;
                }
            }
            catch (System.Exception ex)
            {
                AcadApp.AcaEd.WriteMessage("ERROR: MarkingCalc().CheckbMasUse_Unchecked " + ex + "\n");
            }
        }
        private void btRefresh_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                cbLayerUse.Items.Clear();
                foreach (var item in AcadApp.GetLayersList())
                {
                    cbLayerUse.Items.Add(item as object);
                    cbLayerUse.Text = cbLayerUse.Items[0] as String;
                }
                System.ComponentModel.SortDescription sd = new System.ComponentModel.SortDescription();
                cbLayerUse.Items.SortDescriptions.Add(sd);
            }
            catch (Exception ex)
            {
                AcadApp.AcaEd.WriteMessage("\nERROR: AreaLinearCalc.btRefresh_Click() " + ex + "\n");
            }
        }
        private void btSample_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                String layerName;
                String lineType;
                Autodesk.AutoCAD.Colors.Color color;
                btRefresh_Click(sender, e);

                AcadApp.SelectSample(true, out layerName, out color, out lineType);

                if (layerName == null)
                {
                    return;
                }
                cbLayerUse.Text = layerName;
            }
            catch (Exception ex)
            {
                AcadApp.AcaEd.WriteMessage("\nERROR: MarkingCalc.btSample_Click() " + ex + "\n");
            }
        }
        private void btClear_Click(object sender, RoutedEventArgs e)
        {
            if (calculatedData != null)
            {
                calculatedData.Clear();
            }
            dataGridResult.Items.Refresh();
        }
        private void cbCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (rawData != null)
            {
                CurrentCategory = e.AddedItems[0] as String;
                DisplayResult();
            }
        }

        private void cbMasht_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (rawData != null)
            {
                CurrentMasht = e.AddedItems[0] as String;
                DisplayResult();
            }
        }

        private void btToExcel_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
