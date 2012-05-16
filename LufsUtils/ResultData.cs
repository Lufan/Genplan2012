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

namespace LufsGenplan
{
    /// <summary>
    /// Class for store result data to the method C
    /// alculateSectionElevation and CalculateSectionWorkElev
    /// </summary>
    public class ResultData
    {
        public ResultData(Double pk, Double centerElevPr, Double centerElevEx, Double leftOffset, Double rightOffset, Double leftElevPr, Double leftElevEx,
                          Double rightElevPr, Double rightElevEx, Double areaCut, Double areaFill)
        {
            _PK = pk;
            // Set points by existing surface
            _centrPtEx = new KeyValuePair<Double, Double>(0.0d, centerElevEx);
            _leftPtEx = new KeyValuePair<Double, Double>(leftOffset, leftElevEx);
            _rigthPtEx = new KeyValuePair<Double, Double>(rightOffset, rightElevEx);
            // Set points by proecting surface
            _centrPtPr = new KeyValuePair<Double, Double>(0.0d, centerElevPr);
            _leftPtPr = new KeyValuePair<Double, Double>(leftOffset, leftElevPr);
            _rigthPtPr = new KeyValuePair<Double, Double>(rightOffset, rightElevPr);

            _areaCut = areaCut;
            _areaFill = areaFill;
        }
        
        public Double PK
        {
            get
            {
                return _PK;
            }
        }

        //For the display into datagrid
        public String PKPl
        {
            get
            {
                String pk = ((int)PK / 100).ToString();

                String pl = (PK - ((int)PK / 100)).ToString();

                return pk + "+" + pl;
            }
        }

        public String LeftB
        {
            get
            {
                return String.Format(outFrmt, LeftPtEx.Key);
            }
        }

        public String LeftDelta
        {
            get
            {
                Double delta = LeftPtPr.Value - LeftPtEx.Value;
                return String.Format(outFrmt, delta);
            }
        }

        public String CentrDelta
        {
            get
            {
                Double delta = CentrPtPr.Value - CentrPtEx.Value;
                return String.Format(outFrmt, delta);
            }
        }

        public String RightDelta
        {
            get
            {
                Double delta = RightPtPr.Value - RightPtEx.Value;
                return String.Format(outFrmt, delta);
            }
        }

        public String RightB
        {
            get
            {
                if (RightPtEx.Key >= RightPtPr.Key)
                {
                    return String.Format(outFrmt, RightPtPr.Key);
                }
                return String.Format(outFrmt, RightPtEx.Key);
            }
        }

        public String AreaF
        {
            get
            {
                return String.Format(outFrmt, AreaFill);
            }
        }

        public String AreaC
        {
            get
            {
                return String.Format(outFrmt, AreaCut);
            }
        }
        //

        public KeyValuePair<Double, Double> LeftPtEx
        {
            get
            {
                return _leftPtEx;
            }
        }

        public KeyValuePair<Double, Double> RightPtEx
        {
            get
            {
                return _rigthPtEx;
            }
        }

        public KeyValuePair<Double, Double> CentrPtEx
        {
            get
            {
                return _centrPtEx;
            }
        }

        public KeyValuePair<Double, Double> LeftPtPr
        {
            get
            {
                return _leftPtPr;
            }
        }

        public KeyValuePair<Double, Double> RightPtPr
        {
            get
            {
                return _rigthPtPr;
            }
        }

        public KeyValuePair<Double, Double> CentrPtPr
        {
            get
            {
                return _centrPtPr;
            }
        }

        public Double AreaCut
        {
            get
            {
                return _areaCut;
            }
        }

        public Double AreaFill
        {
            get
            {
                return _areaFill;
            }
        }



        private Double _areaCut;
        private Double _areaFill;
        private Double _PK;
        private KeyValuePair<Double, Double> _leftPtEx;
        private KeyValuePair<Double, Double> _rigthPtEx;
        private KeyValuePair<Double, Double> _centrPtEx;
        private KeyValuePair<Double, Double> _leftPtPr;
        private KeyValuePair<Double, Double> _rigthPtPr;
        private KeyValuePair<Double, Double> _centrPtPr;
        private string outFrmt = "{0:0.00}";
    }
}
