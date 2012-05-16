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
    /// Interaction logic for RoadPavenment.xaml
    /// </summary>
    public partial class RoadPavenmentOld : UserControl, ILUFSPlug
    {
        public Boolean DataUpdate()
        {
            btRefresh_Click(null, null);
            return true;
        }
        public String GetPluginName()
        {
            return "Выравнивание/фрезерование";
        }
        public PlugType GetTargetApp()
        {
            return PlugType.Civil3d;
        }
        private String templateName = "RoadPavenment.xlsx";
        /// <summary>
        /// Precision is the step to calculate elevations from the sections
        /// </summary>
        private const Double PRECISION = 0.01d;
        public RoadPavenmentOld()
        {
            InitializeComponent();
        }
        private void BtSolut_Click(object sender, RoutedEventArgs e)
        {
            //Check is it civil drawing? If no, exit
            if (!CivApp.isCivilDatabase(AcadApp.AcaDb))
            {
                AcadApp.AcaEd.WriteMessage("\nЭта команда доступна только в Civil 3d приложении.\n");
                return;
            }
            try
            {
                //Ask user to select alignment from drawing
                String algnName;
                algnName = CivApp.PromptSelectAlignment("проектную");
                if (algnName == "")
                {
                    return; //If nothing selected - exit without prompt
                }
                var stPK = 0.0d;
                var edPK = 0.0d;
                var leng = 0.0d;
                CivApp.GetAlignmentData(algnName, out stPK, out edPK, out leng);
                AcadApp.AcaEd.WriteMessage("Имя выбранной трассы: " + algnName + "\n");
                AcadApp.AcaEd.WriteMessage("Пикет начала трассы: " + stPK + " м\n");
                AcadApp.AcaEd.WriteMessage("Пикет  конца трассы: " + edPK + " м\n");
                AcadApp.AcaEd.WriteMessage("Общая  длина трассы: " + leng + " м\n");

                //Ask user to select existing TINSurface from drawing
                String surfNameEx;
                surfNameEx = CivApp.PromptSelectTINSurface("существующую");
                if (surfNameEx == "")
                {
                    return; //If nothing selected - exit without prompt
                }
                AcadApp.AcaEd.WriteMessage("Имя выбранной существующей поверхности: " + surfNameEx + "\n");

                //Ask user to select proecting TINSurface from drawing
                String surfNamePr;
                surfNamePr = CivApp.PromptSelectTINSurface("проектную");
                if (surfNamePr == "")
                {
                    return; //If nothing selected - exit without prompt
                }
                if (surfNamePr == surfNameEx)
                {
                    AcadApp.AcaEd.WriteMessage("\nПоверхности должны быть различными.\n");
                    return;
                }
                AcadApp.AcaEd.WriteMessage("Имя выбранной проектной поверхности: " + surfNamePr + "\n");

                // Ask user to input a start station
                var startPK = CivApp.PromptEnterDouble("\nВведите начальный пикет [м]: ", stPK);
                if (startPK == -1.0d)
                {
                    return; //If nothing entered - exit without prompt
                }
                if (startPK < stPK)
                {
                    AcadApp.AcaEd.WriteMessage("\nНачальный пикет должен быть более либо равен пикету начала трассы.\n");
                    return;
                }

                // Ask user to input a end station 
                var endPK = CivApp.PromptEnterDouble("\nВведите конечный пикет [м]: ", edPK);
                if (endPK == -1.0d)
                {
                    return; //If nothing entered - exit without prompt
                }
                if (endPK <= startPK)
                {
                    AcadApp.AcaEd.WriteMessage("\nКонечный пикет должен быть более начального пикета.\n");
                    return;
                }

                // Ask user to input step of the computation. Default is 10.0 
                var stepPK = CivApp.PromptEnterDouble("\nВведите шаг вычислений [м]: ", 10.0d);
                if (stepPK == -1.0d)
                {
                    return; //If nothing entered - exit without prompt
                }
                if (stepPK * 2.0d > leng)
                {
                    AcadApp.AcaEd.WriteMessage("\nШаг должен быть менее либо равен половине длины трассы.\n");
                    return;
                }

                // Ask user to input minimum width of the surface at sections. Default 0.0
                var minW = CivApp.PromptEnterDouble("\nВведите минимальную ширину поперечника [м]: ", 0.0d);
                if (minW == -1.0d)
                {
                    return; //If nothing entered - exit without prompt
                }
                if (minW < 0.0)
                {
                    AcadApp.AcaEd.WriteMessage("\nМинимальная ширина должна быть более либо равна нулю.\n");
                    return;
                }

                // Ask user to input maximum width of the surface at sections. Default 10.0 
                var maxW = CivApp.PromptEnterDouble("\nВведите максимальную ширину поперечника [м]: ", 10.0d);
                if (maxW == -1.0d)
                {
                    return; //If nothing entered - exit without prompt
                }
                if (maxW <= minW)
                {
                    AcadApp.AcaEd.WriteMessage("\nМаксимальная ширина должна быть более минимальной ширины.\n");
                    return;
                }

                //Start calculiation. Store result onto variable result
                List<ResultData> result;
                result = CalculateSectionElevation(algnName, surfNamePr, surfNameEx, startPK, endPK, stepPK, minW, maxW);

                AcadApp.AcaEd.WriteMessage("\nОбработано поперечников: " + result.Count + "\n");
                AcadApp.AcaEd.WriteMessage("\nПередача данных в Excel:\n");

                //Create filename for the Creating excel file in same folder where placed current drawing file
                var fileName = AcadApp.GetFileName() + ".xls";

                //Write data to the excel application
                var resFileName = ExcApp.WriteDataToExcelRoadPavenment(ExcApp.ResultToExcelRoadPavenment(result), fileName, templateName);
                AcadApp.AcaEd.WriteMessage("\nСоздан файл: " + resFileName + "\n");
            }
            catch (Autodesk.AutoCAD.Runtime.Exception ex)
            {
                AcadApp.AcaEd.WriteMessage("ERROR: [CommandMethod(\"LUFSSURF2EXCELCMD\")].Surf2ExcelCMD()\n" + ex + "\n");
            }       
        }
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
        private static List<ResultData> CalculateSectionElevation(String algnName, String surfNamePr, String surfNameEx,
                                                           Double startPK, Double endPK, Double stepPK,
                                                           Double minW, Double maxW)
        {
            var result = new List<ResultData>((int)((endPK - startPK) / stepPK) + 1);
            // Resolve alignment and surface name
            CivApp.SetCurAlignment(algnName);
            CivApp.SetCurSurfaceEx(surfNameEx);
            CivApp.SetCurSurfacePr(surfNamePr);
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

            for (Double curPK = startPK; curPK <= endPK; curPK += stepPK)
            {
                // Get elevation at alignment -> offset = 0.0;
                // If point outside from the existing surface -> skip section and set elevation to 0.0d;
                if (!CivApp.GetElevationAtPK(CivApp.CurAlignment, CivApp.CurSurfaceEx, curPK, 0.0d /*at the alignment*/, out elevatCentrEx))
                {
                    AcadApp.AcaEd.WriteMessage("\nERROR. ПК: " + curPK + ". По оси не найдена существующая поверхность. Пропуск поперечника.\n");
                    elevatCentrEx = 0.0d;
                    isSkipSection = true;
                }

                // Get elevation at alignment -> offset = 0.0;
                // If point outside from the proecting surface -> skip section and set elevation to 0.0d;
                if (!CivApp.GetElevationAtPK(CivApp.CurAlignment, CivApp.CurSurfacePr, curPK, 0.0d /*at the alignment*/, out elevatCentrPr))
                {
                    AcadApp.AcaEd.WriteMessage("\nERROR. ПК: " + curPK + ". По оси не найдена проектная поверхность. Пропуск поперечника.\n");
                    elevatCentrEx = 0.0d;
                    isSkipSection = true;
                }

                if (!isSkipSection)
                { // As a result set offset and elevation to 0.0d
                    // Get elevation at left offset on the proect surface  - remember that left offset must be negative value;
                    // If the surface not found then elevation set to 0.0d;
                    GetElevationAndOffset(CivApp.CurAlignment, CivApp.CurSurfacePr, (-1.0d) * minW, (-1.0d) * maxW, curPK, out offsetLeftPr, out elevatLeftPr);

                    // Get elevation at left offset on the existing surface - remember that left offset must be negative value;
                    // If the surface not found then elevation set to 0.0d;
                    GetElevationAndOffset(CivApp.CurAlignment, CivApp.CurSurfaceEx, (-1.0d) * minW, (-1.0d) * maxW, curPK, out offsetLeftEx, out elevatLeftEx);

                    // Get elevation at right offset on the proect surface;
                    // If point outside from the surface elevation set to 0.0d;
                    GetElevationAndOffset(CivApp.CurAlignment, CivApp.CurSurfacePr, minW, maxW, curPK, out offsetRightPr, out elevatRightPr);

                    // Get elevation at right offset on the existing surface;
                    // If point outside from the surface elevation set to 0.0d;
                    GetElevationAndOffset(CivApp.CurAlignment, CivApp.CurSurfaceEx, minW, maxW, curPK, out offsetRightEx, out elevatRightEx);

                    // Set borders of the proect section to the smallest width (from proecting or existing surfaces)
                    if (offsetLeftPr < offsetLeftEx)
                    {
                        leftBorder = offsetLeftPr;
                        // Set elevatLeftEx onto the offset leftBorder - remember that left offset must be negative value
                        Boolean res = CivApp.GetElevationAtPK(CivApp.CurAlignment, CivApp.CurSurfaceEx, curPK, (-1.0d) * leftBorder, out elevatLeftEx);
                        if (!res) { AcadApp.AcaEd.WriteMessage("\n" + "ПК " + curPK + " лево " + leftBorder + " сущ.пов-ть.\n"); }
                    }
                    else
                    {
                        leftBorder = offsetLeftEx;
                        // Set elevatLeftPr onto the offset leftBorder - remember that left offset must be negative value
                        Boolean res = CivApp.GetElevationAtPK(CivApp.CurAlignment, CivApp.CurSurfacePr, curPK, (-1.0d) * leftBorder, out elevatLeftPr);
                        if (!res) { AcadApp.AcaEd.WriteMessage("\n" + "ПК " + curPK + " лево " + leftBorder + " проект.пов-ть.\n"); }
                    }
                    if (offsetRightPr < offsetRightEx)
                    {
                        rightBorder = offsetRightPr;
                        // Set elevatrightEx onto the offset rightBorder
                        Boolean res = CivApp.GetElevationAtPK(CivApp.CurAlignment, CivApp.CurSurfaceEx, curPK, rightBorder, out elevatRightEx);
                        if (!res) { AcadApp.AcaEd.WriteMessage("\n" + "ПК " + curPK + " право " + leftBorder + " сущ.пов-ть.\n"); }
                    }
                    else
                    {
                        rightBorder = offsetRightEx;
                        // Set elevatrightPr onto the offset rightBorder
                        Boolean res = CivApp.GetElevationAtPK(CivApp.CurAlignment, CivApp.CurSurfacePr, curPK, rightBorder, out elevatRightPr);
                        if (!res) { AcadApp.AcaEd.WriteMessage("\n" + "ПК " + curPK + " право " + leftBorder + " проект.пов-ть.\n"); }
                    }
                }// !isSkipSection
                // Get delta elevation data at proect section
                var deltaData = GetDeltaElevationData(CivApp.CurAlignment, CivApp.CurSurfacePr, CivApp.CurSurfaceEx, curPK, leftBorder, rightBorder);

                Double areaCut, areaFill;
                GetAreaFillAndCutFromSection(deltaData, out areaFill, out areaCut);

                result.Add(new ResultData(curPK, elevatCentrPr, elevatCentrEx, leftBorder, rightBorder, elevatLeftPr, elevatLeftEx,
                              elevatRightPr, elevatRightEx, areaCut, areaFill));
            }// End for through the alignment
            return result;
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
        private static List<float> GetDeltaElevationData(Autodesk.Civil.Land.DatabaseServices.Alignment align, Autodesk.Civil.Land.DatabaseServices.TinSurface surfPr,
            Autodesk.Civil.Land.DatabaseServices.TinSurface surfEx, Double curPK, Double leftBorder, Double rightBorder)
        {
            if (leftBorder == 0.0d && rightBorder == 0.0d)
            {
                AcadApp.AcaEd.WriteMessage("\nПК: " + curPK + ". Границы лежат на оси. \n");
                return new List<float>(); // Nothing to be calculated
            }
            var deltaData = new List<float>((int)((leftBorder + rightBorder) / 0.1d) + 1);

            var elevationEx = 0.0d;
            var elevationPr = 0.0d;

            for (var offset = -leftBorder; offset < rightBorder; offset += PRECISION)
            {
                CivApp.GetElevationAtPK(align, surfEx, curPK, offset, out elevationEx);
                CivApp.GetElevationAtPK(align, surfPr, curPK, offset, out elevationPr);
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
        private static void GetElevationAndOffset(Autodesk.Civil.Land.DatabaseServices.Alignment align, Autodesk.Civil.Land.DatabaseServices.TinSurface surf,
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
                if (CivApp.GetElevationAtPK(align, surf, curPK, koef * offset, out elevation))
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

        private void btRefresh_Click(object sender, RoutedEventArgs e)
        {
            System.ComponentModel.SortDescription sd = new System.ComponentModel.SortDescription();
            cbAlignment.Items.SortDescriptions.Add(sd);
            cbSurfEx.Items.SortDescriptions.Add(sd);
            cbSurfPr.Items.SortDescriptions.Add(sd);
        }
    }
}
