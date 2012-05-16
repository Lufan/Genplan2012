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
    public class DashSpace
    {
        public Double Dash { get; set; }
        public Double Space { get; set; }

        public DashSpace()
        {
        }
        public DashSpace(Double dash, Double space)
        {
            Dash = dash;
            Space = space;
        }
    }

    public class CategoryWidth
    {
        public String Category { get; set; }
        public Double Width { get; set; }

        public CategoryWidth()
        {
        }

        public CategoryWidth(String category, Double width)
        {
            Category = category;
            Width = width;
        }
    }

    public class PaintData
    {
        public String Title { get; set; }
        public String ShortTitle { get; set; }
        public Double Amount { get; set; }

        public PaintData()
        {
        }

        public PaintData(String title, String shortTitle, Double amount)
        {
            Title = title;
            ShortTitle = shortTitle;
            Amount = amount;
        }
    }

    public class RazmType
    {
        public enum typeOfEntity {Line, DoubleLineCenter, DoubleLineSide, Area, Block};

        private String _lineTypeName = String.Empty;
        private String _razmDescription = String.Empty;
        private String _razmType = String.Empty;
        private List<DashSpace> _dashSpaceLen = null;

        private List<PaintData> _paints = null;

        private typeOfEntity _entType = typeOfEntity.Line;

        private List<CategoryWidth> _categoryData;

        public String LineTypeName
        {
            get
            {
                return this._lineTypeName;
            }
            set
            {
                if (value != this._lineTypeName)
                {
                    this._lineTypeName = value;
                }
            }
        }
        public String Description
        {
            get
            {
                return this._razmDescription;
            }
            set
            {
                if (value != this._razmDescription)
                {
                    this._razmDescription = value;
                }
            }
        }
        public String Type
        {
            get
            {
                return this._razmType;
            }
            set
            {
                if (value != this._razmType)
                {
                    this._razmType = value;
                }
            }
        }
        public typeOfEntity EntityType
        {
            get
            {
                return this._entType;
            }
            set
            {
                if (value != this._entType)
                {
                    this._entType = value;
                }
            }
        }

        public List<DashSpace> DashSpaceLen
        {
            get
            {
                return this._dashSpaceLen;
            }
            set
            {
                if (value != this._dashSpaceLen)
                {
                    this._dashSpaceLen = value;
                }
            }
        }

        public List<PaintData> Paints
        {
            get
            {
                return this._paints;
            }
            set
            {
                if (value != this._paints)
                {
                    this._paints = value;
                }
            }
        }

        public List<CategoryWidth> CategoryData
        {
            get
            {
                return this._categoryData;
            }
            set
            {
                if (value != this._categoryData)
                {
                    this._categoryData = value;
                }
            }
        }

        public RazmType() 
        { 
        }

        public RazmType(String lineTypeName, String razmDescription, String razmType, typeOfEntity entType, List<PaintData> paints,
                        List<CategoryWidth> categoryData = null, List<DashSpace> dashSpaceLen = null)
        {
            LineTypeName = lineTypeName;
            Description = razmDescription;
            Type = razmType;
            EntityType = entType;

            Paints = paints;

            CategoryData = categoryData;
            DashSpaceLen = dashSpaceLen;
        }
    }
}
