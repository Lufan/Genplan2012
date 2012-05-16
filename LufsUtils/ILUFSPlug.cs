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
    public enum PlugType { Autocad, Civil3d };
    public enum EventsType { LayerAdded, LayerModified, LinetypeAdded, LinetypeModified, LayerErased, LinettypeErased };
    public interface ILUFSPlug
    {
        string GetPluginName();
        PlugType GetTargetApp();
        Boolean DocumentCreated();
        Boolean DocumentActivated();
    }
}
