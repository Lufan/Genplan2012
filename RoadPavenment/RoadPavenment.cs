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

using Autodesk.Civil.DatabaseServices;
using Autodesk.AutoCAD.DatabaseServices;

namespace RoadPavenment
{
    public partial class RoadPavenment : UserControl, LufsGenplan.ILUFSPlug
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
                LufsGenplan.AcadApp.AcaEd.WriteMessage("\nERROR: RoadPavenment.DocumentCreated() " + ex + "\n");
            }
            return true;
        }

        public Boolean DocumentActivated()
        {
            try
            {
                cbAlignment.Items.Clear();
                foreach (var item in LufsGenplan.CivApp.GetAlignmentList())
                {
                    cbAlignment.Items.Add(item as object);
                    cbAlignment.SelectedItem = cbAlignment.Items[0];
                }

                cbSurfEx.Items.Clear();
                foreach (var item in LufsGenplan.CivApp.GetSurfaceList())
                {
                    cbSurfEx.Items.Add(item as object);
                    cbSurfEx.SelectedItem = cbSurfEx.Items[0];
                }

                cbSurfPr.Items.Clear();
                foreach (var item in LufsGenplan.CivApp.GetSurfaceList())
                {
                    cbSurfPr.Items.Add(item as object);
                    cbSurfPr.SelectedItem = cbSurfPr.Items[0];
                }

                if (isInputValid())
                {
                    BtSolut.Enabled = true;
                }
                else
                {
                    BtSolut.Enabled = false;
                }
                return true;
            }
            catch (Exception ex)
            {
                LufsGenplan.AcadApp.AcaEd.WriteMessage("\nERROR: RoadPavenment.DocumentActivated() " + ex + "\n");
            }
            return false;
        }

        public String GetPluginName()
        {
            return "Выравнивание/фрезерование";
        }
        public LufsGenplan.PlugType GetTargetApp()
        {
            return LufsGenplan.PlugType.Civil3d;
        }
        #endregion ILUFSPlug

        public RoadPavenment()
        {
            InitializeComponent();
            //Check is it civil drawing? If no, hide control
            if (!LufsGenplan.AcadApp.isCivilDatabase(LufsGenplan.AcadApp.AcaDb))
            {
                this.Visible = false;
            }
        }

        private void RegisterDatabaseEvents()
        {
            try
            {
                LufsGenplan.AcadApp.RegisterDatabaseAppendEvent(DbEvent_ObjectAppened_Handler_RoadPavenment);
                LufsGenplan.AcadApp.RegisterDatabaseEraseEvent(DbEvent_ObjectErased_Handler_RoadPavenment);
                LufsGenplan.AcadApp.RegisterDatabaseModifiEvent(DbEvent_ObjectModified_Handler_RoadPavenment);
            }
            catch (Exception ex)
            {
                LufsGenplan.AcadApp.AcaEd.WriteMessage("\nERROR: RoadPavenment.RegisterDatabaseEvents() " + ex + "\n");
            }
        }
        
        private void BtSolut_Click(object sender, EventArgs e)
        {
            //Check is it civil drawing? If no, exit. 
            //if (!LufsGenplan.AcadApp.isCivilDatabase(LufsGenplan.AcadApp.AcaDb))
            //{
            //    LufsGenplan.AcadApp.AcaEd.WriteMessage("\nЭта команда доступна только в Civil 3d приложении.\n");
            //    return;
            //}
            try
            {
                BtSolut.Enabled = false;
                //Ask user to select alignment from drawing
                String algnName;
                algnName = cbAlignment.Text;
                if (algnName == "")
                {
                    return; //If nothing selected - exit without prompt
                }
                var stPK = 0.0d;
                var edPK = 0.0d;
                var leng = 0.0d;
                LufsGenplan.CivApp.GetAlignmentData(algnName, out stPK, out edPK, out leng);

                String surfNameEx;
                surfNameEx = cbSurfEx.Text;
                if (surfNameEx == "")
                {
                    return; //If nothing selected - exit without prompt
                }

                String surfNamePr;
                surfNamePr = cbSurfPr.Text;
                if (surfNamePr == "")
                {
                    return; //If nothing selected - exit without prompt
                }

                var startPK = Double.Parse(tbStartStation.Text);
                
                if (startPK < stPK)
                {
                    LufsGenplan.AcadApp.AcaEd.WriteMessage("\nНачальный пикет должен быть более либо равен пикету начала трассы.\n");
                    return;
                }

                var endPK = Double.Parse(tbEndStation.Text);

                if (endPK <= startPK)
                {
                    LufsGenplan.AcadApp.AcaEd.WriteMessage("\nКонечный пикет должен быть более начального пикета.\n");
                    return;
                }

                var stepPK = Double.Parse(tbStep.Text);

                if (stepPK * 2.0d > leng)
                {
                    LufsGenplan.AcadApp.AcaEd.WriteMessage("\nШаг должен быть менее либо равен половине длины трассы.\n");
                    return;
                }

                var minW = Double.Parse(tbMinWidth.Text);

                if (minW < 0.0)
                {
                    minW = 0.0;
                    tbMinWidth.Text = "0.0";
                }

                var maxW = Double.Parse(tbMaxWidth.Text);

                if (maxW <= minW)
                {
                    LufsGenplan.AcadApp.AcaEd.WriteMessage("\nМаксимальная ширина должна быть более минимальной ширины.\n");
                    return;
                }

                //Start calculiation. Store result onto variable RoadPavenment.rawResult
                rawResult = CalculateSectionElevation(algnName, surfNamePr, surfNameEx, startPK, endPK, stepPK, minW, maxW);

                LufsGenplan.AcadApp.AcaEd.WriteMessage("\nОбработано поперечников: " + rawResult.Count + "\n");

                dataGridResult.DataSource = rawResult;
                dataGridResult.Refresh();
                btToExcel.Enabled = true;
                btClear.Enabled = true;
            }
            catch (Autodesk.AutoCAD.Runtime.Exception ex)
            {
                LufsGenplan.AcadApp.AcaEd.WriteMessage("ERROR: RoadPavenment.BtSolut_Click()\n" + ex + "\n");
            }
            finally
            {
                BtSolut.Enabled = true;
            }
        }

        #region Calculation methods

        /// <summary>
        /// Calculate points onto the given surface along given alignment
        /// </summary>
        /// <param name="algnName">Alignment name along which will process calculation</param>
        /// <param name="surfNamePr">Proect Surface name</param>
        /// <param name="surfNameEx">Exist Surface name</param>
        /// <param name="startPK">A start station of the calculation along the alignment</param>
        /// <param name="endPK">An end station of the calculation along the alignment</param>
        /// <param name="stepPK">A step which used  to calculate data along alignment </param>
        /// <param name="minW">A distance less than distance from the central line of section to the border of surface</param>
        /// <param name="maxW">A distance more than distance from the central line of section to the border of surface</param>
        /// <returns> Return collection of the calculated data </returns>
        private static System.Collections.ObjectModel.Collection<LufsGenplan.ResultData> CalculateSectionElevation(String algnName, String surfNamePr, String surfNameEx,
                                                           Double startPK, Double endPK, Double stepPK,
                                                           Double minW, Double maxW)
        {
            var result = new List<LufsGenplan.ResultData>((int)((endPK - startPK) / stepPK) + 1);
            // Resolve alignment and surface name
            LufsGenplan.CivApp.SetCurAlignment(algnName);
            LufsGenplan.CivApp.SetCurSurfaceEx(surfNameEx);
            LufsGenplan.CivApp.SetCurSurfacePr(surfNamePr);
            // Variables to store elevation data
            var elevatCentrPr = 0.0d;
            var elevatCentrEx = 0.0d;
            var elevatLeftPr = 0.0d;
            var elevatLeftEx = 0.0d;
            var elevatRightPr = 0.0d;
            var elevatRightEx = 0.0d;
            // Variables to store offset from alignment distance data
            var offsetLeftPr = 0.0d;
            var offsetLeftEx = 0.0d;
            var offsetRightPr = 0.0d;
            var offsetRightEx = 0.0d;
            // Minimal from offset...Pr and offset...Ex distances
            var leftBorder = 0.0d;
            var rightBorder = 0.0d;

            Boolean isSkipSection = false;

            for (Double curPK = startPK; curPK < endPK + stepPK; curPK += stepPK)
            {
                //For using last station into calculation
                if (curPK > endPK)
                {
                    curPK = endPK;
                }
                // Get elevation at alignment -> offset = 0.0;
                // If point outside from the existing surface -> skip section and set elevation to 0.0d;
                if (!LufsGenplan.CivApp.GetElevationAtPK(LufsGenplan.CivApp.CurAlignment, LufsGenplan.CivApp.CurSurfaceEx, curPK, 0.0d /*at the alignment*/, out elevatCentrEx))
                {
                    LufsGenplan.AcadApp.AcaEd.WriteMessage("\nERROR. ПК: " + curPK + ". По оси не найдена существующая поверхность. Пропуск поперечника.\n");
                    elevatCentrEx = 0.0d;
                    isSkipSection = true;
                }

                // Get elevation at alignment -> offset = 0.0;
                // If point outside from the proecting surface -> skip section and set elevation to 0.0d;
                if (!LufsGenplan.CivApp.GetElevationAtPK(LufsGenplan.CivApp.CurAlignment, LufsGenplan.CivApp.CurSurfacePr, curPK, 0.0d /*at the alignment*/, out elevatCentrPr))
                {
                    LufsGenplan.AcadApp.AcaEd.WriteMessage("\nERROR. ПК: " + curPK + ". По оси не найдена проектная поверхность. Пропуск поперечника.\n");
                    elevatCentrEx = 0.0d;
                    isSkipSection = true;
                }

                if (!isSkipSection)
                { // As a result set offset and elevation to 0.0d
                    // Get elevation at left offset on the proect surface  - remember that left offset must be negative value;
                    // If the surface not found then elevation set to 0.0d;
                    GetElevationAndOffset(LufsGenplan.CivApp.CurAlignment, LufsGenplan.CivApp.CurSurfacePr, (-1.0d) * minW, (-1.0d) * maxW, curPK, out offsetLeftPr, out elevatLeftPr);

                    // Get elevation at left offset on the existing surface - remember that left offset must be negative value;
                    // If the surface not found then elevation set to 0.0d;
                    GetElevationAndOffset(LufsGenplan.CivApp.CurAlignment, LufsGenplan.CivApp.CurSurfaceEx, (-1.0d) * minW, (-1.0d) * maxW, curPK, out offsetLeftEx, out elevatLeftEx);

                    // Get elevation at right offset on the proect surface;
                    // If point outside from the surface elevation set to 0.0d;
                    GetElevationAndOffset(LufsGenplan.CivApp.CurAlignment, LufsGenplan.CivApp.CurSurfacePr, minW, maxW, curPK, out offsetRightPr, out elevatRightPr);

                    // Get elevation at right offset on the existing surface;
                    // If point outside from the surface elevation set to 0.0d;
                    GetElevationAndOffset(LufsGenplan.CivApp.CurAlignment, LufsGenplan.CivApp.CurSurfaceEx, minW, maxW, curPK, out offsetRightEx, out elevatRightEx);

                    // Set borders of the proect section to the smallest width (from proecting or existing surfaces)
                    if (offsetLeftPr < offsetLeftEx)
                    {
                        leftBorder = offsetLeftPr;
                        // Set elevatLeftEx onto the offset leftBorder - remember that left offset must be negative value
                        Boolean res = LufsGenplan.CivApp.GetElevationAtPK(LufsGenplan.CivApp.CurAlignment, LufsGenplan.CivApp.CurSurfaceEx, curPK, (-1.0d) * leftBorder, out elevatLeftEx);
                        if (!res) { LufsGenplan.AcadApp.AcaEd.WriteMessage("\n" + "ПК " + curPK + " лево " + leftBorder + " сущ.пов-ть.\n"); }
                    }
                    else
                    {
                        leftBorder = offsetLeftEx;
                        // Set elevatLeftPr onto the offset leftBorder - remember that left offset must be negative value
                        Boolean res = LufsGenplan.CivApp.GetElevationAtPK(LufsGenplan.CivApp.CurAlignment, LufsGenplan.CivApp.CurSurfacePr, curPK, (-1.0d) * leftBorder, out elevatLeftPr);
                        if (!res) { LufsGenplan.AcadApp.AcaEd.WriteMessage("\n" + "ПК " + curPK + " лево " + leftBorder + " проект.пов-ть.\n"); }
                    }
                    if (offsetRightPr < offsetRightEx)
                    {
                        rightBorder = offsetRightPr;
                        // Set elevatrightEx onto the offset rightBorder
                        Boolean res = LufsGenplan.CivApp.GetElevationAtPK(LufsGenplan.CivApp.CurAlignment, LufsGenplan.CivApp.CurSurfaceEx, curPK, rightBorder, out elevatRightEx);
                        if (!res) { LufsGenplan.AcadApp.AcaEd.WriteMessage("\n" + "ПК " + curPK + " право " + leftBorder + " сущ.пов-ть.\n"); }
                    }
                    else
                    {
                        rightBorder = offsetRightEx;
                        // Set elevatrightPr onto the offset rightBorder
                        Boolean res = LufsGenplan.CivApp.GetElevationAtPK(LufsGenplan.CivApp.CurAlignment, LufsGenplan.CivApp.CurSurfacePr, curPK, rightBorder, out elevatRightPr);
                        if (!res) { LufsGenplan.AcadApp.AcaEd.WriteMessage("\n" + "ПК " + curPK + " право " + leftBorder + " проект.пов-ть.\n"); }
                    }
                }// !isSkipSection
                // Get delta elevation data at proect section
                var deltaData = GetDeltaElevationData(LufsGenplan.CivApp.CurAlignment, LufsGenplan.CivApp.CurSurfacePr, LufsGenplan.CivApp.CurSurfaceEx, curPK, leftBorder, rightBorder);

                Double areaCut, areaFill;
                GetAreaFillAndCutFromSection(deltaData, out areaFill, out areaCut);

                result.Add(new LufsGenplan.ResultData(curPK, elevatCentrPr, elevatCentrEx, leftBorder, rightBorder, elevatLeftPr, elevatLeftEx,
                              elevatRightPr, elevatRightEx, areaCut, areaFill));
            }// End for through the alignment
            return new System.Collections.ObjectModel.Collection<LufsGenplan.ResultData>(result);
        }

        /// <summary>
        /// Calculate delta from existing and proecting surfaces
        /// </summary>
        /// <param name="align">Alignment</param>
        /// <param name="surfPr">Proecting surface</param>
        /// <param name="surfEx">Existing surface</param>
        /// <param name="curPK">PK position along alignment</param>
        /// <param name="leftBorder">left section border</param>
        /// <param name="rightBorder">right section border</param>
        /// <returns></returns>
        private static List<float> GetDeltaElevationData(Alignment align, TinSurface surfPr,
            Autodesk.Civil.DatabaseServices.TinSurface surfEx, Double curPK, Double leftBorder, Double rightBorder)
        {
            if (leftBorder == 0.0d && rightBorder == 0.0d)
            {
                LufsGenplan.AcadApp.AcaEd.WriteMessage("\nПК: " + curPK + ". Границы лежат на оси. \n");
                return new List<float>(); // Nothing to be calculated
            }
            var deltaData = new List<float>((int)((leftBorder + rightBorder) / 0.1d) + 1);

            var elevationEx = 0.0d;
            var elevationPr = 0.0d;

            for (var offset = -leftBorder; offset < rightBorder; offset += PRECISION)
            {
                LufsGenplan.CivApp.GetElevationAtPK(align, surfEx, curPK, offset, out elevationEx);
                LufsGenplan.CivApp.GetElevationAtPK(align, surfPr, curPK, offset, out elevationPr);
                deltaData.Add((float)(elevationPr - elevationEx)); // Convert double data to the float result
            }// for
            return deltaData;
        }

        /// <summary>
        /// Calculate area of fill and cut into section
        /// </summary>
        /// <param name="deltaData"> List of delta elevation of proecting and existing surface</param>
        /// <param name="areaFill">out parametr - fill area</param>
        /// <param name="areaCut">out parametr - cut area</param>
        private static void GetAreaFillAndCutFromSection(List<float> deltaData, out double areaFill, out double areaCut)
        {
            areaFill = 0.0d;
            areaCut = 0.0d;
            foreach (var val in deltaData)
            {
                if (val > 0.0d)
                {
                    areaFill += val * PRECISION;
                }
                else if (val < 0.0d)
                {
                    areaCut += (-1.0d) * val * PRECISION;
                }
            }
        }

        ///<summary> Get elevation and offset on the given surface;</summary>
        ///<summary> If the surface not found then elevation set to 0.0d;</summary>
        ///<param name="align">Alignment name along which will process calculation</param>
        ///<param name="surf">Surface name</param>
        ///<param name="minW">A distance less than distance from the central line of section to the border of surface</param>
        ///<param name="maxW">A distance more than distance from the central line of section to the border of surface</param>
        ///<param name="curPK">Current PK along alignment</param>
        ///<param name="offset">out parameter - Offset from alignment</param>
        ///<param name="elevation">out parameter - elevation from surface</param>
        private static void GetElevationAndOffset(Alignment align, TinSurface surf,
                                            Double minW, Double maxW, Double curPK, out Double offset, out Double elevation)
        {
            var elevat_prev = 0.0d; // The temporary variable to store previous elevation into each step
            var offset_prev = 0.0d; // The temporary variable to store previous offset into each step
            elevation = 0.0d;

            // If elevation at the left side, koef * offset must be negative
            var koef = 1.0d;
            if (maxW < 0.0d)
            {
                koef = -1.0d;
            }
            // If elevation at the left side, koef * (minW and maxW) must be positive
            var minWT = koef * minW;
            var maxWT = koef * maxW;
            for (offset = minWT; offset < maxWT; offset += PRECISION)
            {
                if (LufsGenplan.CivApp.GetElevationAtPK(align, surf, curPK, koef * offset, out elevation))
                {
                    elevat_prev = elevation;
                    offset_prev = offset;
                }
                else
                {
                    // Return to previous step
                    // When point is out of surface
                    offset = offset_prev;
                    elevation = elevat_prev;
                    return;
                }
            }// for
            return;
        }

        #endregion Calculation methods

        private void btToExcel_Click(object sender, EventArgs e)
        {
            if (rawResult != null && rawResult.Count > 0)
            {
                LufsGenplan.AcadApp.AcaEd.WriteMessage("\nПередача данных в Excel:\n");
                //Create filename for the Creating excel file in same folder where placed current drawing file
                var fileName = LufsGenplan.AcadApp.GetFileName() + ".xlsx";

                //Write data to the excel application
                var resFileName = WriteDataToExcelRoadPavenment(ResultToExcelRoadPavenment(rawResult.ToList()), fileName, templateName);
                LufsGenplan.AcadApp.AcaEd.WriteMessage("\nСоздан файл: " + resFileName + "\n");
            }
        }

        #region WriteToExcel
        private static int HEADERROWSRoadPavenment = 5; // 5 rows have table's header
        private static int HEADERCOLUMNSRoadPavenment = 18; // 18 columns have table's header
        /// <summary>
        /// Write array to the worksheet Excel
        /// </summary>
        /// <param name="rows">The required amount of row into the worksheet </param>
        /// <param name="columns">The required amount of columns into the worksheet </param>
        /// <param name="worksheet">The worksheet in which data will be written </param>
        /// <param name="data"> The data to be written on the worksheet </param>
        private void WriteArrayRoadPavenment(int rows, int columns, Microsoft.Office.Interop.Excel.Worksheet worksheet, object[,] data)
        {
            var startRow = HEADERROWSRoadPavenment;
            var startCell = worksheet.Cells[startRow + 1, 1] as Microsoft.Office.Interop.Excel.Range; // At excel range start at the 1,1 not the 0,0
            var endCell = worksheet.Cells[rows + startRow, columns] as Microsoft.Office.Interop.Excel.Range;
            var writeRange = worksheet.get_Range(startCell, endCell);
            writeRange.Value2 = data;
        }

        /// <summary>
        /// Create rectangular array of objects from ResultData collection
        /// </summary>
        /// <param name="res">Collection of the ResultData to convert </param>
        /// <returns> Return rectangular array of objects </returns>
        public object[,] ResultToExcelRoadPavenment(List<LufsGenplan.ResultData> res)
        {
            var data = new object[(res.Count + 1) * 2, HEADERCOLUMNSRoadPavenment];
            var count = 0;
            foreach (var r in res)
            {
                data[count, 0] = r.PK; // PK position along the alignment

                data[count, 1] = r.LeftPtEx.Key; // Left offset from alignment
                data[count, 2] = r.LeftPtEx.Value; // Existing elevation into the left side point at the offset distance from alignment
                data[count, 3] = r.LeftPtPr.Value - r.LeftPtEx.Value; // Delta elevation
                data[count, 4] = r.LeftPtPr.Value; // Proecting elevation into the left side point at the offset distance from alignment

                data[count, 5] = r.CentrPtEx.Value; // Existing elevation into the point at the alignment
                data[count, 6] = r.CentrPtPr.Value - r.CentrPtEx.Value; // Delta elevation
                data[count, 7] = r.CentrPtPr.Value; // Proecting elevation into the point at the alignment

                data[count, 8] = r.RightPtEx.Value; // Existing elevation into the Right side point at the offset distance from alignment
                data[count, 9] = r.RightPtPr.Value - r.RightPtEx.Value; // Delta elevation
                data[count, 10] = r.RightPtPr.Value; // Proecting elevation into the Right side point at the offset distance from alignment
                data[count, 11] = r.RightPtEx.Key; // Right offset from alignment

                data[count, 12] = r.AreaFill;
                data[count, 16] = r.AreaCut;

                

                var rowPos = count + HEADERROWSRoadPavenment + 1; // At excel range start at the 1,1 not the 0,0
                if ((count / 2) + 1 < res.Count)
                { // If has next element 'r' in collection 'res'
                    data[count+1, 15] = "=" + tbFillM.Text + "*O" + (rowPos + 1);//Weigth

                    data[count + 1, 13] = "=A" + (rowPos + 2) + "-A" + (rowPos); // Distance between sections
                    data[count + 1, 14] = "=(M" + (rowPos + 2) + "+M" + (rowPos) + ")/2*N" + (rowPos + 1); // Fill volume between sections
                    data[count + 1, 17] = "=(Q" + (rowPos + 2) + "+Q" + (rowPos) + ")/2*N" + (rowPos + 1); // Cut volume between sections
                }
                else
                {
                    data[count + 1, 0] = "ИТОГО:";
                    data[count + 1, 14] = "=sum(O" + (HEADERROWSRoadPavenment + 1) + ":O" + (rowPos) + ")";
                    data[count + 1, 15] = "=sum(P" + (HEADERROWSRoadPavenment + 1) + ":P" + (rowPos) + ")";
                    data[count + 1, 17] = "=sum(R" + (HEADERROWSRoadPavenment + 1) + ":R" + (rowPos) + ")";
                }

                count += 2;
            }
            return data;
        }

        /// <summary>
        /// Create instance of the Excel application and into write data 
        /// </summary>
        /// <param name="data">Collection of the ResultData to write </param>
        /// <returns> Return filename of the created file or the string with error description</returns>
        public String WriteDataToExcelRoadPavenment(object[,] obData, String fileName, String templateName)
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
                WriteArrayRoadPavenment(obData.GetLength(0), obData.GetLength(1), wsTemp, obData);

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

        private void btGetAlign_Click(object sender, EventArgs e)
        {
            try
            {
                btGetAlign.Enabled = false;
                //Ask user to select alignment from drawing
                String algnName;
                algnName = LufsGenplan.CivApp.PromptSelectAlignment("проектную");
                if (algnName == "")
                {
                    return; //If nothing selected - exit without prompt
                }
                //cbAlignment.Text = algnName;
            }
            catch (Autodesk.AutoCAD.Runtime.Exception ex)
            {
                LufsGenplan.AcadApp.AcaEd.WriteMessage("ERROR: RoadPavenment.btGetAlign_Click()\n" + ex + "\n");
            }
            finally
            {
                btGetAlign.Enabled = true;
            }
        }

        private void btGetSurfEx_Click(object sender, EventArgs e)
        {
            try
            {
                btGetSurfEx.Enabled = false;
                //Ask user to select alignment from drawing
                String surfExName;
                surfExName = LufsGenplan.CivApp.PromptSelectTINSurface("существующую");
                if (surfExName == "")
                {
                    return; //If nothing selected - exit without prompt
                }
                //cbSurfEx.Text = surfExName;
            }
            catch (Autodesk.AutoCAD.Runtime.Exception ex)
            {
                LufsGenplan.AcadApp.AcaEd.WriteMessage("ERROR: RoadPavenment.btGetSurfEx_Click()\n" + ex + "\n");
            }
            finally
            {
                btGetSurfEx.Enabled = true;
            }
        }

        private void btGetSurfPr_Click(object sender, EventArgs e)
        {
            try
            {
                btGetSurfPr.Enabled = false;
                //Ask user to select alignment from drawing
                String surfPrName;
                surfPrName = LufsGenplan.CivApp.PromptSelectTINSurface("проектную");
                if (surfPrName == "")
                {
                    return; //If nothing selected - exit without prompt
                }
                //cbSurfEx = surfPrName;
            }
            catch (Autodesk.AutoCAD.Runtime.Exception ex)
            {
                LufsGenplan.AcadApp.AcaEd.WriteMessage("ERROR: RoadPavenment.btGetSurfPr_Click()\n" + ex + "\n");
            }
            finally
            {
                btGetSurfPr.Enabled = true;
            }
        }

        private void btClear_Click(object sender, EventArgs e)
        {
            if (rawResult != null)
            {
                dataGridResult.DataSource = null;
                rawResult.Clear();
            }
            dataGridResult.Refresh();
            btToExcel.Enabled = false;
            btClear.Enabled = false;
        }

        private void cbAlignment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbAlignment.Text != "")
            {
                var algnName = (cbAlignment.SelectedItem as AcadUtils.CbAutocadItem).Name;
                var stPK = 0.0d;
                var edPK = 0.0d;
                var leng = 0.0d;
                LufsGenplan.CivApp.GetAlignmentData(algnName, out stPK, out edPK, out leng);

                var outFrmt = "{0:0.00}";
                tbStartStation.Text = String.Format(outFrmt, stPK);
                tbEndStation.Text = String.Format(outFrmt, edPK);
            }
        }

        #region KeyPress and TextChanged behavior
        private Boolean isNumeric(KeyPressEventArgs e, String text)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                return false;
            }
            if (e.KeyChar == '.')
            {
                return  text.Contains(".");
            }
            return e.KeyChar != (char)Keys.Back;
        }

        private Boolean isInputValid()
        {
            Double tmp;
            if (!Double.TryParse(tbFillM.Text, out tmp) ||
                !Double.TryParse(tbCutM.Text, out tmp) ||
                !Double.TryParse(tbMinWidth.Text, out tmp) ||
                !Double.TryParse(tbMaxWidth.Text, out tmp) ||
                !Double.TryParse(tbStartStation.Text, out tmp) ||
                !Double.TryParse(tbEndStation.Text, out tmp) ||
                !Double.TryParse(tbStep.Text, out tmp) ||
                (String.IsNullOrEmpty(cbAlignment.Text)) ||
                (String.IsNullOrEmpty(cbSurfEx.Text)) ||
                (String.IsNullOrEmpty(cbSurfPr.Text)) ||
                (cbSurfEx.Text == cbSurfPr.Text)
                )
            {
                return false;
            }
            return true;
        }

        private void BtSolutSetState()
        {
            if (isInputValid())
            {
                BtSolut.Enabled = true;
            }
            else
            {
                BtSolut.Enabled = false;
            }
        }

        private void tbFillM_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = isNumeric(e, tbFillM.Text);
        }

        private void tbCutM_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = isNumeric(e, tbCutM.Text);
        }

        private void tbMinWidth_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = isNumeric(e, tbMinWidth.Text);
        }

        private void tbMaxWidth_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = isNumeric(e, tbMaxWidth.Text);
        }

        private void tbStartStation_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = isNumeric(e, tbMinWidth.Text);
        }

        private void tbEndStation_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = isNumeric(e, tbMinWidth.Text);
        }

        private void tbStep_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = isNumeric(e, tbMinWidth.Text);
        }

        private void tbFillM_TextChanged(object sender, EventArgs e)
        {
            BtSolutSetState();
        }

        private void tbCutM_TextChanged(object sender, EventArgs e)
        {
            BtSolutSetState();
        }

        private void tbMinWidth_TextChanged(object sender, EventArgs e)
        {
            BtSolutSetState();
        }

        private void tbMaxWidth_TextChanged(object sender, EventArgs e)
        {
            BtSolutSetState();
        }

        private void cbAlignment_TextChanged(object sender, EventArgs e)
        {
            BtSolutSetState();
        }

        private void cbSurfEx_TextChanged(object sender, EventArgs e)
        {
            BtSolutSetState();
        }

        private void cbSurfPr_TextChanged(object sender, EventArgs e)
        {
            BtSolutSetState();
        }

        private void tbStartStation_TextChanged(object sender, EventArgs e)
        {
            BtSolutSetState();
        }

        private void tbEndStation_TextChanged(object sender, EventArgs e)
        {
            BtSolutSetState();
        }

        private void tbStep_TextChanged(object sender, EventArgs e)
        {
            BtSolutSetState();
        }

        #endregion KeyPress and TextChanged behavior

        private String templateName = "RoadPavenment.xlsx";
        /// <summary>
        /// Precision is the step to calculate elevations from the sections
        /// </summary>
        private const Double PRECISION = 0.01d;

        //Collection of the data to display at the grid. Changed by method BtSolut_click, BtErase_Click
        System.Collections.ObjectModel.Collection<LufsGenplan.ResultData> rawResult;
        
        #region Database event implementation

        public void DbEvent_ObjectAppened_Handler_RoadPavenment(object sender, ObjectEventArgs e)
        {
            try
            {
                if (e.DBObject is Autodesk.Civil.DatabaseServices.Alignment)
                {
                    cbAlignment.Items.Add(new AcadUtils.CbAutocadItem((e.DBObject as Alignment).Name,
                                                    (e.DBObject as Alignment).ObjectId));
                }
                else if (e.DBObject is Autodesk.Civil.DatabaseServices.TinSurface)
                {
                    cbSurfEx.Items.Add(new AcadUtils.CbAutocadItem((e.DBObject as TinSurface).Name,
                                                    (e.DBObject as TinSurface).ObjectId));
                    cbSurfPr.Items.Add(new AcadUtils.CbAutocadItem((e.DBObject as TinSurface).Name,
                                                    (e.DBObject as TinSurface).ObjectId));
                }
            }
            catch (Exception ex)
            {
                LufsGenplan.AcadApp.AcaEd.WriteMessage("\nERROR: RoadPavenment.DbEvent_ObjectAppened_Handler() " + ex + "\n");
            }
        }

        public void DbEvent_ObjectErased_Handler_RoadPavenment(object sender, ObjectErasedEventArgs e)
        {
            try
            {
                if (e.DBObject is Alignment)
                {
                    AcadUtils.CbAutocadItem obj = new AcadUtils.CbAutocadItem((e.DBObject as Alignment).Name,
                                                    (e.DBObject as Alignment).ObjectId);
                    if (obj == null)
                    {
                        return;
                    }
                    for (int i = 0; i < cbAlignment.Items.Count; i++)
                    {
                        if ((cbAlignment.Items[i] as AcadUtils.CbAutocadItem).ID == obj.ID)
                        {
                            if (cbAlignment.Text == obj.Name)
                            {
                                cbAlignment.Items.RemoveAt(i);
                                if (cbAlignment.Items.Count != 0)
                                {
                                    cbAlignment.SelectedIndex = 0;
                                    cbAlignment.SelectedItem = cbAlignment.Items[0];
                                }
                            }
                            else
                            {
                                cbAlignment.Items.RemoveAt(i);
                            }
                            break; //for i
                        }
                    }
                }
                else if (e.DBObject is TinSurface)
                {
                    AcadUtils.CbAutocadItem obj = new AcadUtils.CbAutocadItem((e.DBObject as TinSurface).Name,
                                                    (e.DBObject as TinSurface).ObjectId);
                    if (obj == null)
                    {
                        return;
                    }
                    for (int i = 0; i < cbSurfEx.Items.Count; i++)
                    {
                        if ((cbSurfEx.Items[i] as AcadUtils.CbAutocadItem).ID == obj.ID)
                        {
                            if (cbSurfEx.Text == obj.Name)
                            {
                                cbSurfEx.Items.RemoveAt(i);

                                if (cbSurfEx.Items.Count != 0)
                                {
                                    cbSurfEx.SelectedIndex = 0;
                                    cbSurfEx.SelectedItem = cbSurfPr.Items[0];
                                }
                            }
                            else
                            {
                                cbSurfEx.Items.RemoveAt(i);
                            }
                            break; //for
                        }
                    }

                    for (int i = 0; i < cbSurfPr.Items.Count; i++)
                    {
                        if ((cbSurfPr.Items[i] as AcadUtils.CbAutocadItem).ID == obj.ID)
                        {
                            if (cbSurfPr.Text == obj.Name)
                            {
                                cbSurfPr.Items.RemoveAt(i);

                                if (cbSurfPr.Items.Count != 0)
                                {
                                    cbSurfPr.SelectedIndex = 0;
                                    cbSurfPr.SelectedItem = cbSurfPr.Items[0];
                                }
                            }
                            else
                            {
                                cbSurfPr.Items.RemoveAt(i);
                            }
                            break; //for
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LufsGenplan.AcadApp.AcaEd.WriteMessage("\nERROR: RoadPavenment.DbEvent_ObjectErased_Handler() " + ex + "\n");
            }
        }

        public void DbEvent_ObjectModified_Handler_RoadPavenment(object sender, ObjectEventArgs e)
        {
            try
            {
                if (e.DBObject is Alignment)
                {
                    AcadUtils.CbAutocadItem obj = new AcadUtils.CbAutocadItem((e.DBObject as Alignment).Name,
                                                    (e.DBObject as Alignment).ObjectId);
                    if (obj == null)
                    {
                        return;
                    }
                    for (int i = 0; i < cbAlignment.Items.Count; i++)
                    {
                        if ((cbAlignment.Items[i] as AcadUtils.CbAutocadItem).ID == obj.ID)
                        {
                            if ((cbAlignment.Items[i] as AcadUtils.CbAutocadItem).Name == cbAlignment.Text)
                            {
                                cbAlignment.Items.RemoveAt(i);
                                cbAlignment.Items.Insert(i, obj);
                                cbAlignment.SelectedItem = obj;
                                //TO DO check alignment data
                            }
                            else
                            {
                                cbAlignment.Items.RemoveAt(i);
                                cbAlignment.Items.Insert(i, obj);
                            }
                            break;
                        }
                    }
                }
                else if (e.DBObject is TinSurface)
                {
                    AcadUtils.CbAutocadItem obj = new AcadUtils.CbAutocadItem((e.DBObject as TinSurface).Name,
                                                    (e.DBObject as TinSurface).ObjectId);
                    if (obj == null)
                    {
                        return;
                    }
                    for (int i = 0; i < cbSurfEx.Items.Count; i++)
                    {
                        if ((cbSurfEx.Items[i] as AcadUtils.CbAutocadItem).ID == obj.ID)
                        {
                            if ((cbSurfEx.Items[i] as AcadUtils.CbAutocadItem).Name == cbSurfEx.Text)
                            {
                                cbSurfEx.Items.RemoveAt(i);
                                cbSurfEx.Items.Insert(i, obj);
                                cbSurfEx.SelectedItem = obj;
                            }
                            else
                            {
                                cbSurfEx.Items.RemoveAt(i);
                                cbSurfEx.Items.Insert(i, obj);
                            }
                            break;
                        }
                    }

                    for (int i = 0; i < cbSurfPr.Items.Count; i++)
                    {
                        if ((cbSurfPr.Items[i] as AcadUtils.CbAutocadItem).ID == obj.ID)
                        {
                            if ((cbSurfPr.Items[i] as AcadUtils.CbAutocadItem).Name == cbSurfPr.Text)
                            {
                                cbSurfPr.Items.RemoveAt(i);
                                cbSurfPr.Items.Insert(i, obj);
                                cbSurfPr.SelectedItem = obj;
                            }
                            else
                            {
                                cbSurfPr.Items.RemoveAt(i);
                                cbSurfPr.Items.Insert(i, obj);
                            }
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LufsGenplan.AcadApp.AcaEd.WriteMessage("\nERROR: RoadPavenment.DbEvent_ObjectModified_Handler() " + ex + "\n");
            }
        }

        #endregion Database event implementation

        
    }
}
