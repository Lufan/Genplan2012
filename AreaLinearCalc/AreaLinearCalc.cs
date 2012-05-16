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
using System.Text;
using System.Windows.Forms;

namespace AreaLinearCalc
{
    public partial class AreaLinearCalc : UserControl, LufsGenplan.ILUFSPlug
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
                LufsGenplan.AcadApp.AcaEd.WriteMessage("\nERROR: AreaLinearCalc.DocumentCreated() " + ex + "\n");
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
                cbLayerUse.SelectedItem = cbLayerUse.Items[0];
                currentLayer = cbLayerUse.Text;
                //Linetype setup
                cbLinetUse.Items.Clear();
                foreach (var item in LufsGenplan.AcadApp.GetLinetypeList())
                {
                    cbLinetUse.Items.Add(item as object);
                }
                cbLinetUse.SelectedItem = cbLinetUse.Items[0];
                currentLinetType = cbLinetUse.Text;
                //Color setup
                CheckbColorUse.Checked = false;
                selected_color = null;
                LbColorName.Text = "Цвет: ";
                pColor.BackColor = this.BackColor;
                return true;
            }
            catch (Exception ex)
            {
                LufsGenplan.AcadApp.AcaEd.WriteMessage("\nERROR: AreaLinearCalc.DocumentActivated() " + ex + "\n");
            }
            return false;
        }

        public String GetPluginName()
        {
            return "Площадь/длина";
        }

        public LufsGenplan.PlugType GetTargetApp()
        {
            return LufsGenplan.PlugType.Autocad;
        }

        #endregion ILUFSPlug

        public AreaLinearCalc()
        {
            InitializeComponent();
            cbMasht.Text = "1:500";
        }

        #region private

        private String currentLayer;
        private String currentLinetType;
        private Autodesk.AutoCAD.Colors.Color selected_color;

        private void RegisterDatabaseEvents()
        {
            try
            {
                LufsGenplan.AcadApp.RegisterDatabaseAppendEvent(DbEvent_ObjectAppened_Handler_AreaLinearCalc);
                LufsGenplan.AcadApp.RegisterDatabaseEraseEvent(DbEvent_ObjectErased_Handler_AreaLinearCalc);
                LufsGenplan.AcadApp.RegisterDatabaseModifiEvent(DbEvent_ObjectModified_Handler_AreaLinearCalc);
            }
            catch (Exception ex)
            {
                LufsGenplan.AcadApp.AcaEd.WriteMessage("\nERROR: AreaLinearCalc.RegisterDatabaseEvents() " + ex + "\n");
            }
        }

        private void CheckbLayerUse_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (CheckbLayerUse.Checked)
                {
                    cbLayerUse.Enabled = true;
                }
                else
                {
                    cbLayerUse.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                LufsGenplan.AcadApp.AcaEd.WriteMessage("\nERROR: AreaLinearCalc.CheckbLayerUse_CheckedChanged() " + ex + "\n");
            }
        }

        private void CheckbColorUse_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (CheckbColorUse.Checked)
                {
                    BtSelectColor.Enabled = true;
                    pColor.Enabled = true;
                    if (selected_color == null)
                    {
                        BtSelectColor_Click(sender, e);
                    }
                }
                else
                {
                    BtSelectColor.Enabled = false;
                    pColor.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                LufsGenplan.AcadApp.AcaEd.WriteMessage("\nERROR: AreaLinearCalc.CheckbColorUse_Checked() " + ex + "\n");
            }
        }

        private void BtSelectColor_Click(object sender, EventArgs e)
        {
            try
            {
                selected_color = LufsGenplan.AcadApp.SelectColor();

                if (selected_color == null)
                {
                    CheckbColorUse.CheckState = CheckState.Unchecked;
                    return;
                }

                if (selected_color.ColorNameForDisplay == "BYLAYER")
                {
                    Autodesk.AutoCAD.Colors.Color cl = LufsGenplan.AcadApp.GetColorByLayer((cbLayerUse.SelectedItem as AcadUtils.CbAutocadItem).ID);
                    pColor.BackColor = cl.ColorValue; //!!!!!!!!!!!
                }
                else
                {
                    pColor.BackColor = selected_color.ColorValue;
                }
                pColor.Update();

                LbColorName.Text = "Цвет: " + selected_color.ColorNameForDisplay;
            }
            catch (Exception ex)
            {
                LufsGenplan.AcadApp.AcaEd.WriteMessage("\nERROR: AreaLinearCalc.BtSelectColor_Click() " + ex + "\n");
            }
        }

        private void CheckbLinetUse_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (CheckbLinetUse.Checked)
                {
                    cbLinetUse.Enabled = true;
                }
                else
                {
                    cbLinetUse.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                LufsGenplan.AcadApp.AcaEd.WriteMessage("\nERROR: AreaLinearCalc.CheckbLinetUse_CheckedChanged() " + ex + "\n");
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

                LufsGenplan.AcadApp.SelectSample(RbAllEnt.Checked, out layerName, out color, out lineType);

                if (layerName == null)
                {
                    return;
                }
                CheckbLayerUse.Checked = true;

                for (int j = 0; j < cbLayerUse.Items.Count; j++)
                {
                    if ((cbLayerUse.Items[j] as AcadUtils.CbAutocadItem).Name == layerName)
                    {
                        cbLayerUse.SelectedIndex = j;
                        break; //for
                    }
                }
                currentLayer = cbLayerUse.Text;

                selected_color = color;
                if (selected_color.ColorNameForDisplay == "BYLAYER")
                {
                    Autodesk.AutoCAD.Colors.Color cl = LufsGenplan.AcadApp.GetColorByLayer((cbLayerUse.SelectedItem as AcadUtils.CbAutocadItem).ID);
                    pColor.BackColor = cl.ColorValue;
                }
                else
                {
                    pColor.BackColor = selected_color.ColorValue;
                }
                pColor.Update();
                LbColorName.Text = "Цвет: " + selected_color.ColorNameForDisplay;
                CheckbColorUse.Checked = true;

                CheckbLinetUse.Checked = true;

                for (int j = 0; j < cbLinetUse.Items.Count; j++)
                {
                    if ((cbLinetUse.Items[j] as AcadUtils.CbAutocadItem).Name == lineType)
                    {
                        cbLinetUse.SelectedIndex = j;
                        break; //for
                    }
                }
                currentLinetType = cbLinetUse.Text;
            }
            catch (Exception ex)
            {
                LufsGenplan.AcadApp.AcaEd.WriteMessage("\nERROR: AreaLinearCalc.btSample_Click() " + ex + "\n");
            }
            finally
            {
                btSample.Enabled = true;
            }
        }

        private void CheckbMasUse_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (CheckbMasUse.Checked)
                {
                    cbMasht.Enabled = true;
                }
                else
                {
                    cbMasht.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                LufsGenplan.AcadApp.AcaEd.WriteMessage("\nERROR: AreaLinearCalc.CheckbMasUse_CheckedChanged() " + ex + "\n");
            }
        }

        private Autodesk.AutoCAD.DatabaseServices.TypedValue[] GetFilteredParam()
        {
            List<Autodesk.AutoCAD.DatabaseServices.TypedValue> selval_list = new List<Autodesk.AutoCAD.DatabaseServices.TypedValue>();
            try
            {
                //Filter by type PolyLine
                if (RbPlineEnt.Checked)
                {
                    selval_list.Add(new Autodesk.AutoCAD.DatabaseServices.TypedValue((int)(Autodesk.AutoCAD.DatabaseServices.DxfCode.Start), "LWPOLYLINE"));
                }
                else
                {
                    //Select all entitys
                }
                if ((CheckbLayerUse.Checked) && cbLayerUse.Text != "")
                {
                    //Filter by layer
                    selval_list.Add(new Autodesk.AutoCAD.DatabaseServices.TypedValue((int)(Autodesk.AutoCAD.DatabaseServices.DxfCode.LayerName), cbLayerUse.Text));
                }
                if (CheckbColorUse.Checked)
                {
                    //Filter by color - ОГРАНИЧЕНИЕ: применимы цвета из индексной палитры
                    selval_list.Add(new Autodesk.AutoCAD.DatabaseServices.TypedValue((int)(Autodesk.AutoCAD.DatabaseServices.DxfCode.Color), selected_color.ColorIndex));
                }
                if (CheckbLinetUse.Checked)
                {
                    //Filter by linetype
                    selval_list.Add(new Autodesk.AutoCAD.DatabaseServices.TypedValue((int)(Autodesk.AutoCAD.DatabaseServices.DxfCode.LinetypeName), cbLinetUse.Text));
                }
            }
            catch (Exception ex)
            {
                LufsGenplan.AcadApp.AcaEd.WriteMessage("\nERROR: AreaLinearCalc.GetFilteredParam() " + ex + "\n");
            }
            return selval_list.ToArray();
        }

        private void DisplayResult(double[] res)
        {
            try
            {
                if (res[2] == 0.0d)
                {
                    return;
                }
                var outFrmt = "{0:0.00}";
                tbResult.AppendText(String.Format("{0:0}", res[2]) + " шт. ");

                if (CheckbLayerUse.Checked)
                {
                    tbResult.Select(0, 0);
                    tbResult.AppendText("Слой ");
                    tbResult.AppendText(cbLayerUse.Text);
                    tbResult.Select(tbResult.Text.Length - cbLayerUse.Text.Length, cbLayerUse.Text.Length);
                    Color prevColor = tbResult.SelectionColor;
                    Font prevFont = tbResult.SelectionFont;
                    tbResult.SelectionColor = Color.Red;
                    tbResult.SelectionFont = new Font(tbResult.SelectionFont, FontStyle.Bold);
                    tbResult.Select(tbResult.Text.Length, 0);
                    tbResult.SelectionColor = prevColor;
                    tbResult.SelectionFont = prevFont;
                    tbResult.AppendText("; ");
                }
                if (CheckbColorUse.Checked)
                {
                    tbResult.Select(0, 0);
                    tbResult.AppendText("Цвет ");
                    tbResult.AppendText("ЦВЕТ");
                    Color prevColor = tbResult.SelectionColor;
                    Color prevBackColor = tbResult.SelectionBackColor;
                    Font prevFont = tbResult.SelectionFont;
                    tbResult.Select(tbResult.Text.Length - "ЦВЕТ".Length, "ЦВЕТ".Length);
                    tbResult.SelectionBackColor = pColor.BackColor;
                    tbResult.SelectionColor = pColor.BackColor;
                    tbResult.Select(tbResult.Text.Length, 0);
                    tbResult.SelectionColor = prevColor;
                    tbResult.SelectionFont = prevFont;
                    tbResult.SelectionBackColor = prevBackColor;
                    tbResult.AppendText("; ");
                }
                if (CheckbLinetUse.Checked)
                {
                    tbResult.Select(0, 0);
                    tbResult.AppendText("Тип линии ");
                    tbResult.AppendText(cbLinetUse.Text);
                    Color prevColor = tbResult.SelectionColor;
                    Font prevFont = tbResult.SelectionFont;
                    tbResult.Select(tbResult.Text.Length - cbLinetUse.Text.Length, cbLinetUse.Text.Length);
                    tbResult.SelectionFont = new Font(tbResult.SelectionFont, FontStyle.Bold);
                    tbResult.SelectionColor = Color.Red;
                    tbResult.Select(tbResult.Text.Length, 0);
                    tbResult.SelectionColor = prevColor;
                    tbResult.SelectionFont = prevFont;
                    tbResult.AppendText("; ");
                }

                if (!(CheckbMasUse.Checked))
                {
                    tbResult.Select(0, 0);
                    tbResult.AppendText("L = ");
                    tbResult.AppendText(String.Format(outFrmt, res[0]));
                    Color prevColor = tbResult.SelectionColor;
                    Font prevFont = tbResult.SelectionFont;
                    tbResult.Select(tbResult.Text.Length - String.Format(outFrmt, res[0]).Length, String.Format(outFrmt, res[0]).Length);
                    tbResult.SelectionFont = new Font(tbResult.SelectionFont, FontStyle.Bold);
                    tbResult.SelectionColor = Color.Blue;
                    tbResult.Select(tbResult.Text.Length, 0);
                    tbResult.SelectionColor = prevColor;
                    tbResult.SelectionFont = prevFont;
                    tbResult.AppendText(" ед черт. А = ");
                    tbResult.AppendText(String.Format(outFrmt, res[1]));
                    prevColor = tbResult.SelectionColor;
                    prevFont = tbResult.SelectionFont;
                    tbResult.Select(tbResult.Text.Length - String.Format(outFrmt, res[1]).Length, String.Format(outFrmt, res[1]).Length);
                    tbResult.SelectionFont = new Font(tbResult.SelectionFont, FontStyle.Bold);
                    tbResult.SelectionColor = Color.Blue;
                    tbResult.Select(tbResult.Text.Length, 0);
                    tbResult.SelectionColor = prevColor;
                    tbResult.SelectionFont = prevFont;
                    tbResult.AppendText(" ед.кв. черт.\n");
                }
                else
                {
                    tbResult.Select(0, 0);
                    tbResult.AppendText("L = ");
                    tbResult.AppendText(String.Format(outFrmt, (MashtToDouble(cbMasht.Text) * res[0])));
                    Color prevColor = tbResult.SelectionColor;
                    Font prevFont = tbResult.SelectionFont;
                    tbResult.Select(tbResult.Text.Length - String.Format(outFrmt, (MashtToDouble(cbMasht.Text) * res[0])).Length, String.Format(outFrmt, (MashtToDouble(cbMasht.Text) * res[0])).Length);
                    tbResult.SelectionFont = new Font(tbResult.SelectionFont, FontStyle.Bold);
                    tbResult.SelectionColor = Color.Blue;
                    tbResult.Select(tbResult.Text.Length, 0);
                    tbResult.SelectionColor = prevColor;
                    tbResult.SelectionFont = prevFont;
                    tbResult.AppendText(" м. А = ");
                    tbResult.AppendText(String.Format(outFrmt, (MashtToDouble(cbMasht.Text) * MashtToDouble(cbMasht.Text) * res[1])));
                    prevColor = tbResult.SelectionColor;
                    prevFont = tbResult.SelectionFont;
                    tbResult.Select(tbResult.Text.Length - String.Format(outFrmt, (MashtToDouble(cbMasht.Text) * MashtToDouble(cbMasht.Text) * res[1])).Length, String.Format(outFrmt, (MashtToDouble(cbMasht.Text) * MashtToDouble(cbMasht.Text) * res[1])).Length);
                    tbResult.SelectionFont = new Font(tbResult.SelectionFont, FontStyle.Bold);
                    tbResult.SelectionColor = Color.Blue;
                    tbResult.Select(tbResult.Text.Length, 0);
                    tbResult.SelectionColor = prevColor;
                    tbResult.SelectionFont = prevFont;
                    tbResult.AppendText(" м.кв. М: " + cbMasht.Text + "\n");
                }
                
            }
            catch (Exception ex)
            {
                LufsGenplan.AcadApp.AcaEd.WriteMessage("\nERROR: AreaLinearCalc.DisplayResult() " + ex + "\n");
            }
        }

        private static double MashtToDouble(string mashtab)
        {
            try
            {
                string temp = mashtab.Substring(mashtab.IndexOf(':') + 1);
                return (System.Convert.ToDouble(temp) / 1000.0d);
            }
            catch (Exception ex)
            {
                LufsGenplan.AcadApp.AcaEd.WriteMessage("\nERROR: AreaLinearCalc.MashtToDouble() " + ex + "\n");
            }
            return 0.0d;
        }

        private double[] CalcRes(Autodesk.AutoCAD.EditorInput.SelectionSet sset, Boolean isOnlyModelSpace, Boolean isOnlyPlineAllowed)
        {
            try
            {
                using (var trans = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Database.TransactionManager.StartTransaction())
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
                }// using transaction
            }
            catch (System.Exception ex)
            {
                LufsGenplan.AcadApp.AcaEd.WriteMessage("\nERROR: AcadApp.CalcRes() " + ex + "\n");
            }
            double[] result_error = { -1, -1, -1 };
            return result_error;
        }

        private void BtSolut_Click(object sender, EventArgs e)
        {
            try
            {
                BtSolut.Enabled = false;
                if (RbUser.Checked)
                {
                    Autodesk.AutoCAD.EditorInput.PromptSelectionOptions edOpt = new Autodesk.AutoCAD.EditorInput.PromptSelectionOptions();
                    Autodesk.AutoCAD.EditorInput.PromptSelectionResult edRes;
                    edOpt.MessageForAdding = "Выберите объекты";
                    edOpt.AllowDuplicates = false;
                    Autodesk.AutoCAD.DatabaseServices.TypedValue[] selval;
                    selval = GetFilteredParam();
                    Autodesk.AutoCAD.EditorInput.SelectionFilter ssfilter = new Autodesk.AutoCAD.EditorInput.SelectionFilter(selval);
                    edRes = LufsGenplan.AcadApp.AcaEd.GetSelection(edOpt, ssfilter);

                    Autodesk.AutoCAD.EditorInput.SelectionSet sset = edRes.Value;
                    if (edRes.Status == Autodesk.AutoCAD.EditorInput.PromptStatus.OK)
                    {
                        double[] res = CalcRes(sset, CheckbOnlyModel.Checked, RbPlineEnt.Checked);
                        DisplayResult(res);
                    }
                }
                else if (RbAll.Checked)
                {
                    Autodesk.AutoCAD.EditorInput.PromptSelectionResult edRes;
                    Autodesk.AutoCAD.DatabaseServices.TypedValue[] selval;

                    selval = GetFilteredParam();
                    Autodesk.AutoCAD.EditorInput.SelectionFilter ssfilter = new Autodesk.AutoCAD.EditorInput.SelectionFilter(selval);

                    edRes = LufsGenplan.AcadApp.AcaEd.SelectAll(ssfilter);
                    Autodesk.AutoCAD.EditorInput.SelectionSet sset = edRes.Value;
                    if (edRes.Status == Autodesk.AutoCAD.EditorInput.PromptStatus.OK)
                    {
                        double[] res = CalcRes(sset, CheckbOnlyModel.Checked, RbPlineEnt.Checked);
                        DisplayResult(res);
                    }
                }
            }
            catch (Exception ex)
            {
                LufsGenplan.AcadApp.AcaEd.WriteMessage("\nERROR: AreaLinearCalc.BtSolut_Click() " + ex + "\n");
            }
            finally
            {
                BtSolut.Enabled = true;
            }
        }

        private void btErase_Click(object sender, EventArgs e)
        {
            tbResult.Clear();
        }

        private void cbLayerUse_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                currentLayer = cbLayerUse.Text;
                if (CheckbColorUse.Enabled && selected_color != null && selected_color.ColorNameForDisplay == "BYLAYER")
                {
                    SetCurrenColorByLayer(LufsGenplan.AcadApp.GetColorByLayer((cbLayerUse.SelectedItem as AcadUtils.CbAutocadItem).ID));
                }
            }
            catch (Exception ex)
            {
                LufsGenplan.AcadApp.AcaEd.WriteMessage("\nERROR: AreaLinearCalc.SelectedIndexChanged() layer = " + currentLayer + "\n" + ex + "\n");
            }
        }

        private void SetCurrenColorByLayer(Autodesk.AutoCAD.Colors.Color cl)
        {
            try
            {
                pColor.BackColor = cl.ColorValue; //!!!!!!!!!!!
                pColor.Update();
                LbColorName.Text = "Цвет: " + selected_color.ColorNameForDisplay;
            }
            catch (Exception ex)
            {
                LufsGenplan.AcadApp.AcaEd.WriteMessage("\nERROR: AreaLinearCalc.SetCurrenColorByLayer() " + ex + "\n");
            }
        }

        #endregion private

        #region Database event implementation

        public void DbEvent_ObjectAppened_Handler_AreaLinearCalc(object sender, Autodesk.AutoCAD.DatabaseServices.ObjectEventArgs e)
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
                else if (e.DBObject is Autodesk.AutoCAD.DatabaseServices.LinetypeTableRecord)
                {
                    //LufsGenplan.AcadApp.AcaEd.WriteMessage("DEBUG: ObjectAppened sender = " + (e.DBObject as Autodesk.AutoCAD.DatabaseServices.LinetypeTableRecord).Name +
                    //    " ID = " + (e.DBObject as Autodesk.AutoCAD.DatabaseServices.LinetypeTableRecord).ObjectId + "\n");

                    cbLinetUse.Items.Add(new AcadUtils.CbAutocadItem((e.DBObject as Autodesk.AutoCAD.DatabaseServices.LinetypeTableRecord).Name,
                                                    (e.DBObject as Autodesk.AutoCAD.DatabaseServices.LinetypeTableRecord).ObjectId));
                }
            }
            catch (Exception ex)
            {
                LufsGenplan.AcadApp.AcaEd.WriteMessage("\nERROR: AreaLinearCalc.DbEvent_ObjectAppened_Handler() " + ex + "\n");
            }
        }

        public void DbEvent_ObjectErased_Handler_AreaLinearCalc(object sender, Autodesk.AutoCAD.DatabaseServices.ObjectErasedEventArgs e)
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
                                        cbLayerUse.SelectedIndex = j;
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
                else if (e.DBObject is Autodesk.AutoCAD.DatabaseServices.LinetypeTableRecord)
                {
                    //LufsGenplan.AcadApp.AcaEd.WriteMessage("DEBUG: ObjectErased sender = " + (e.DBObject as Autodesk.AutoCAD.DatabaseServices.LinetypeTableRecord).Name +
                    //    " ID = " + (e.DBObject as Autodesk.AutoCAD.DatabaseServices.LinetypeTableRecord).ObjectId + "\n");

                    AcadUtils.CbAutocadItem obj = new AcadUtils.CbAutocadItem((e.DBObject as Autodesk.AutoCAD.DatabaseServices.LinetypeTableRecord).Name,
                                                    (e.DBObject as Autodesk.AutoCAD.DatabaseServices.LinetypeTableRecord).ObjectId);
                    if (obj == null)
                    {
                        return;
                    }
                    for (int i = 0; i < cbLinetUse.Items.Count; i++)
                    {
                        if ((cbLinetUse.Items[i] as AcadUtils.CbAutocadItem).ID == obj.ID)
                        {
                            if (currentLinetType == obj.Name)
                            {
                                cbLinetUse.Items.RemoveAt(i);
                                currentLinetType = "ByBlock";
                                for (int j = 0; j < cbLinetUse.Items.Count; j++)
                                {
                                    if ((cbLinetUse.Items[j] as AcadUtils.CbAutocadItem).Name == "ByBlock")
                                    {
                                        cbLinetUse.SelectedIndex = j;
                                        break; //for j
                                    }
                                }
                            }
                            else
                            {
                                cbLinetUse.Items.RemoveAt(i);
                            }
                            break; //for
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LufsGenplan.AcadApp.AcaEd.WriteMessage("\nERROR: AreaLinearCalc.DbEvent_ObjectErased_Handler() " + ex + "\n");
            }
        }

        public void DbEvent_ObjectModified_Handler_AreaLinearCalc(object sender, Autodesk.AutoCAD.DatabaseServices.ObjectEventArgs e)
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

                                if (CheckbColorUse.Checked && selected_color.ColorNameForDisplay == "BYLAYER")
                                {
                                    SetCurrenColorByLayer((e.DBObject as Autodesk.AutoCAD.DatabaseServices.LayerTableRecord).Color);
                                }
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
                else if (e.DBObject is Autodesk.AutoCAD.DatabaseServices.LinetypeTableRecord)
                {
                    //LufsGenplan.AcadApp.AcaEd.WriteMessage("DEBUG: ObjectModified sender = " + (e.DBObject as Autodesk.AutoCAD.DatabaseServices.LinetypeTableRecord).Name +
                    //    " ID = " + (e.DBObject as Autodesk.AutoCAD.DatabaseServices.LinetypeTableRecord).ObjectId + "\n");

                    AcadUtils.CbAutocadItem obj = new AcadUtils.CbAutocadItem((e.DBObject as Autodesk.AutoCAD.DatabaseServices.LinetypeTableRecord).Name,
                                                    (e.DBObject as Autodesk.AutoCAD.DatabaseServices.LinetypeTableRecord).ObjectId);
                    if (obj == null)
                    {
                        return;
                    }
                    for (int i = 0; i < cbLinetUse.Items.Count; i++)
                    {
                        if ((cbLinetUse.Items[i] as AcadUtils.CbAutocadItem).ID == obj.ID)
                        {
                            if ((cbLinetUse.Items[i] as AcadUtils.CbAutocadItem).Name == currentLinetType)
                            {
                                cbLinetUse.Items.RemoveAt(i);
                                cbLinetUse.Items.Insert(i, obj);
                                currentLinetType = cbLinetUse.Items[i].ToString();
                                cbLinetUse.SelectedItem = obj;
                            }
                            else
                            {
                                cbLinetUse.Items.RemoveAt(i);
                                cbLinetUse.Items.Insert(i, obj);
                            }
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LufsGenplan.AcadApp.AcaEd.WriteMessage("\nERROR: AreaLinearCalc.DbEvent_ObjectModified_Handler() " + ex + "\n");
            }
        }

        #endregion Database event implementation
    }
}
