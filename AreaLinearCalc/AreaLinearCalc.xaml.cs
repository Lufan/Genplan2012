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
    /// Interaction logic for AreaLinearCalc.xaml
    /// </summary>
    public partial class AreaLinearCalcOld : UserControl, ILUFSPlug
    {
        public Boolean DataUpdate()
        {
            btRefresh_Click(null, null);
            return true;
        }

        public String GetModuleName()
        {
            return "Площадь/длина";
        }

        public PlugType GetTargetApp()
        {
            return PlugType.Autocad;
        }

        private FlowDocument curFlow;

        public static Autodesk.AutoCAD.Colors.Color selected_color = null;

        public AreaLinearCalcOld()
        {
            InitializeComponent();
            curFlow = new FlowDocument();
            tbResult.Document = curFlow;
            btRefresh_Click(new object(), new RoutedEventArgs());
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

                cbLinetUse.Items.Clear();
                foreach (var item in AcadApp.GetLinetypeList())
                {
                    cbLinetUse.Items.Add(item as object);
                    cbLinetUse.Text = cbLinetUse.Items[0] as String;
                }
                cbLinetUse.Items.SortDescriptions.Add(sd);
            }
            catch (Exception ex)
            {
                AcadApp.AcaEd.WriteMessage("\nERROR: AreaLinearCalc.btRefresh_Click() " + ex + "\n");
            }
        }
        private void CheckbLayerUse_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                cbLayerUse.IsEnabled = true;
            }
            catch (Exception ex)
            {
                AcadApp.AcaEd.WriteMessage("\nERROR: AreaLinearCalc.CheckbLayerUse_Checked() " + ex + "\n");
            }
        }
        private void CheckbLayerUse_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                cbLayerUse.IsEnabled = false;
            }
            catch (Exception ex)
            {
                AcadApp.AcaEd.WriteMessage("\nERROR: AreaLinearCalc.CheckbLayerUse_Unchecked() " + ex + "\n");
            }
        }
        private void CheckbColorUse_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                BtSelectColor.IsEnabled = true;
                if (selected_color == null)
                {
                    BtSelectColor_Click(sender, e);
                }
            }
            catch (Exception ex)
            {
                AcadApp.AcaEd.WriteMessage("\nERROR: AreaLinearCalc.CheckbColorUse_Checked() " + ex + "\n");
            }
        }
        private void CheckbColorUse_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                BtSelectColor.IsEnabled = false;
            }
            catch (Exception ex)
            {
                AcadApp.AcaEd.WriteMessage("\nERROR: AreaLinearCalc.CheckbColorUse_Unchecked() " + ex + "\n");
            }
        }
        private void BtSelectColor_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                selected_color = AcadApp.SelectColor();
                if (selected_color == null)
                {
                    CheckbColorUse_Unchecked(sender, e);
                    return;
                }

                if (selected_color.ColorNameForDisplay == "BYLAYER")
                {
                    Autodesk.AutoCAD.Colors.Color c = AcadApp.GetColorByLayer(cbLayerUse.Text);
                    pColor.Background = new SolidColorBrush(System.Windows.Media.Color.FromArgb(c.ColorValue.A, c.ColorValue.R,
                                                                                                c.ColorValue.G, c.ColorValue.B));
                }
                else
                {
                    pColor.Background = new SolidColorBrush(System.Windows.Media.Color.FromArgb(selected_color.ColorValue.A, selected_color.ColorValue.R,
                                                                                            selected_color.ColorValue.G, selected_color.ColorValue.B));
                }

                pColor.UpdateLayout();

                LbColorName.Content = "Цвет: " + selected_color.ColorNameForDisplay;
            }
            catch (Exception ex)
            {
                AcadApp.AcaEd.WriteMessage("\nERROR: AreaLinearCalc.BtSelectColor_Click() " + ex + "\n");
            }
        }
        private void CheckbLinetUse_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                cbLinetUse.IsEnabled = true;
            }
            catch (Exception ex)
            {
                AcadApp.AcaEd.WriteMessage("\nERROR: AreaLinearCalc.CheckbLinetUse_Checked() " + ex + "\n");
            }
        }
        private void CheckbLinetUse_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                cbLinetUse.IsEnabled = false;
            }
            catch (Exception ex)
            {
                AcadApp.AcaEd.WriteMessage("\nERROR: AreaLinearCalc.CheckbLinetUse_Unchecked() " + ex + "\n");
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

                AcadApp.SelectSample(RbAllEnt.IsChecked ?? false, out layerName, out color, out lineType);
                
                if (layerName == null)
                {
                    return;
                }
                CheckbLayerUse.IsChecked = true;
                cbLayerUse.Text = layerName;

                selected_color = color;
                if (selected_color.ColorNameForDisplay == "BYLAYER")
                {
                    Autodesk.AutoCAD.Colors.Color c = AcadApp.GetColorByLayer(cbLayerUse.Text);
                    pColor.Background = new SolidColorBrush(System.Windows.Media.Color.FromArgb(c.ColorValue.A, c.ColorValue.R,
                                                                                                c.ColorValue.G, c.ColorValue.B));
                }
                else
                {
                    pColor.Background = new SolidColorBrush(System.Windows.Media.Color.FromArgb(selected_color.ColorValue.A, selected_color.ColorValue.R,
                                                                                            selected_color.ColorValue.G, selected_color.ColorValue.B));
                }
                CheckbColorUse.IsChecked = true;
                
                pColor.UpdateLayout();
                LbColorName.Content = "Цвет: " + selected_color.ColorNameForDisplay;

                CheckbLinetUse.IsChecked = true;
                cbLinetUse.Text = lineType;
            }
            catch (Exception ex)
            {
                AcadApp.AcaEd.WriteMessage("\nERROR: AreaLinearCalc.btSample_Click() " + ex + "\n");
            }
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
            catch (Exception ex)
            {
                AcadApp.AcaEd.WriteMessage("\nERROR: AreaLinearCalc.CheckbMasUse_Checked() " + ex + "\n");
            }
        }
        private void CheckbMasUse_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                cbMasht.IsEnabled = false;
            }
            catch (Exception ex)
            {
                AcadApp.AcaEd.WriteMessage("\nERROR: AreaLinearCalc.CheckbMasUse_Unchecked() " + ex + "\n");
            }
        }
        private Autodesk.AutoCAD.DatabaseServices.TypedValue[] GetFilteredParam()
        {
            List<Autodesk.AutoCAD.DatabaseServices.TypedValue> selval_list = new List<Autodesk.AutoCAD.DatabaseServices.TypedValue>();
            try
            {
                //Filter by type PolyLine
                if (RbPlineEnt.IsChecked ?? false)
                {
                    selval_list.Add(new Autodesk.AutoCAD.DatabaseServices.TypedValue((int)(Autodesk.AutoCAD.DatabaseServices.DxfCode.Start), "LWPOLYLINE"));
                }
                else
                {
                    //Select all entitys
                }
                if ((CheckbLayerUse.IsChecked ?? false) && cbLayerUse.Text != "")
                {
                    //Filter by layer
                    selval_list.Add(new Autodesk.AutoCAD.DatabaseServices.TypedValue((int)(Autodesk.AutoCAD.DatabaseServices.DxfCode.LayerName), cbLayerUse.Text));
                }
                if (CheckbColorUse.IsChecked ?? false)
                {
                    //Filter by color - ОГРАНИЧЕНИЕ: применимы цвета из индексной палитры
                    selval_list.Add(new Autodesk.AutoCAD.DatabaseServices.TypedValue((int)(Autodesk.AutoCAD.DatabaseServices.DxfCode.Color), selected_color.ColorIndex));
                }
                if (CheckbLinetUse.IsChecked ?? false)
                {
                    //Filter by linetype
                    selval_list.Add(new Autodesk.AutoCAD.DatabaseServices.TypedValue((int)(Autodesk.AutoCAD.DatabaseServices.DxfCode.LinetypeName), cbLinetUse.Text));
                }
            }
            catch (Exception ex)
            {
                AcadApp.AcaEd.WriteMessage("\nERROR: AreaLinearCalc.GetFilteredParam() " + ex + "\n");
            }
            return selval_list.ToArray();
        }
        private void DisplayResult(double[] res)
        {
            try
            {
                var outFrmt = "{0:0.00}";
                Paragraph par = new Paragraph();
                par.KeepWithNext = true;
                par.KeepTogether = true;
                par.Margin = new Thickness(0.0d);

                par.Inlines.Add(new Run(String.Format("{0:0}", res[2]) + " шт. "));

                if (CheckbLayerUse.IsChecked ?? false)
                {
                    par.Inlines.Add(new Run("Слой "));
                    par.Inlines.Add(new Bold(new Run(cbLayerUse.Text)) { Foreground = Brushes.Red });
                    par.Inlines.Add(new Run("; "));
                }
                if (CheckbColorUse.IsChecked ?? false)
                {
                    par.Inlines.Add(new Run("Цвет "));
                    par.Inlines.Add(new Bold(new Run(("****"))) { Foreground = pColor.Background, Background = pColor.Background });
                    par.Inlines.Add(new Run("; "));
                }
                if (CheckbLinetUse.IsChecked ?? false)
                {
                    par.Inlines.Add(new Run("Тип линии "));
                    par.Inlines.Add(new Bold(new Run(cbLinetUse.Text)) { Foreground = Brushes.Red });
                    par.Inlines.Add(new Run("; "));
                }

                if (!(CheckbMasUse.IsChecked ?? false))
                {
                    par.Inlines.Add(new Run("L = "));
                    par.Inlines.Add(new Bold(new Run(String.Format(outFrmt, res[0]))) { Foreground = Brushes.Blue });
                    par.Inlines.Add(new Run(" ед черт. А = "));
                    par.Inlines.Add(new Bold(new Run(String.Format(outFrmt, res[1]))) { Foreground = Brushes.Blue });
                    par.Inlines.Add(new Run(" ед.кв. черт."));
                }
                else 
                {
                    par.Inlines.Add(new Run("L = "));
                    par.Inlines.Add(new Bold(new Run(String.Format(outFrmt, (MashtToDouble(cbMasht.Text) * res[0])))) { Foreground = Brushes.Blue });
                    par.Inlines.Add(new Run(" м. А = "));
                    par.Inlines.Add(new Bold(new Run(String.Format(outFrmt, (MashtToDouble(cbMasht.Text) * MashtToDouble(cbMasht.Text) * res[1])))) { Foreground = Brushes.Blue });
                    par.Inlines.Add(new Run(" м.кв. М: " + cbMasht.Text));
                }
                curFlow.Blocks.Add(par);
            }
            catch (Exception ex)
            {
                AcadApp.AcaEd.WriteMessage("\nERROR: AreaLinearCalc.DisplayResult() " + ex + "\n");
            }
        }
        private static double MashtToDouble(string mashtab)
        {
            string temp = mashtab.Substring(mashtab.IndexOf(':') + 1);
            return (System.Convert.ToDouble(temp) / 1000.0d);
        }
        private double[] CalcRes(Autodesk.AutoCAD.EditorInput.SelectionSet sset, Boolean isOnlyModelSpace, Boolean isOnlyPlineAllowed)
        {
            using (var trans = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Database.TransactionManager.StartTransaction())
            {
                try
                {
                    double length = 0;
                    double area = 0;
                    int count = 0;
                    foreach (Autodesk.AutoCAD.DatabaseServices.ObjectId id in sset.GetObjectIds())
                    {

                        Autodesk.AutoCAD.DatabaseServices.BlockTableRecord BTR = (Autodesk.AutoCAD.DatabaseServices.BlockTableRecord)trans.GetObject(
                                                  trans.GetObject(id, Autodesk.AutoCAD.DatabaseServices.OpenMode.ForRead).OwnerId,
                                                  Autodesk.AutoCAD.DatabaseServices.OpenMode.ForRead);
                        string BTR_name = (BTR.Name).ToUpper();
                        if (isOnlyModelSpace && BTR_name != "*MODEL_SPACE")
                        {
                            continue;
                        }

                        if (isOnlyPlineAllowed)
                        {
                            Autodesk.AutoCAD.DatabaseServices.Polyline pline;
                            pline = (Autodesk.AutoCAD.DatabaseServices.Polyline)trans.GetObject(id, Autodesk.AutoCAD.DatabaseServices.OpenMode.ForRead);
                            length += pline.Length;
                            area += pline.Area;
                            count++;
                        }
                        else
                        {
                            Autodesk.AutoCAD.DatabaseServices.DBObject selOb;
                            selOb = trans.GetObject(id, Autodesk.AutoCAD.DatabaseServices.OpenMode.ForRead);
                            if (selOb.GetType().Name == "Polyline")
                            {
                                length += ((Autodesk.AutoCAD.DatabaseServices.Polyline)selOb).Length;
                                area += ((Autodesk.AutoCAD.DatabaseServices.Polyline)selOb).Area;
                                count++;
                            }
                            else if (selOb.GetType().Name == "Line")
                            {
                                length += ((Autodesk.AutoCAD.DatabaseServices.Line)selOb).Length;
                                count++;
                            }
                            else if (selOb.GetType().Name == "Arc")
                            {
                                length += ((Autodesk.AutoCAD.DatabaseServices.Arc)selOb).Length;
                                area += ((Autodesk.AutoCAD.DatabaseServices.Arc)selOb).Area;
                                count++;
                            }
                            else if (selOb.GetType().Name == "Circle")
                            {
                                length += ((Autodesk.AutoCAD.DatabaseServices.Circle)selOb).Circumference;
                                area += ((Autodesk.AutoCAD.DatabaseServices.Circle)selOb).Area;
                                count++;
                            }
                        }
                    }
                    double[] result = { length, area, count };
                    return result;
                }
                catch (System.Exception ex)
                {
                    AcadApp.AcaEd.WriteMessage("\nERROR: AcadApp.CalcRes() " + ex + "\n");
                }
            }// using transaction
            double[] result_error = { -1, -1, -1 };
            return result_error;
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
                    selval = GetFilteredParam();
                    Autodesk.AutoCAD.EditorInput.SelectionFilter ssfilter = new Autodesk.AutoCAD.EditorInput.SelectionFilter(selval);
                    edRes = AcadApp.AcaEd.GetSelection(edOpt, ssfilter);

                    Autodesk.AutoCAD.EditorInput.SelectionSet sset = edRes.Value;
                    if (edRes.Status == Autodesk.AutoCAD.EditorInput.PromptStatus.OK)
                    {
                        double[] res = CalcRes(sset, CheckbOnlyModel.IsChecked ?? false, RbPlineEnt.IsChecked ?? false);
                        DisplayResult(res);
                    }
                }
                else if (RbAll.IsChecked ?? false)
                {
                    Autodesk.AutoCAD.EditorInput.PromptSelectionResult edRes;
                    Autodesk.AutoCAD.DatabaseServices.TypedValue[] selval;

                    selval = GetFilteredParam();
                    Autodesk.AutoCAD.EditorInput.SelectionFilter ssfilter = new Autodesk.AutoCAD.EditorInput.SelectionFilter(selval);

                    edRes = AcadApp.AcaEd.SelectAll(ssfilter);
                    Autodesk.AutoCAD.EditorInput.SelectionSet sset = edRes.Value;
                    if (edRes.Status == Autodesk.AutoCAD.EditorInput.PromptStatus.OK)
                    {
                        double[] res = CalcRes(sset, CheckbOnlyModel.IsChecked ?? false, RbPlineEnt.IsChecked ?? false);
                        DisplayResult(res);
                    }
                }
            }
            catch (Exception ex)
            {
                AcadApp.AcaEd.WriteMessage("\nERROR: AreaLinearCalc.BtSolut_Click() " + ex + "\n");
            }
        }

        private void btErase_Click(object sender, RoutedEventArgs e)
        {
            curFlow = new FlowDocument();
            tbResult.Document = curFlow;
        }
    }
}
