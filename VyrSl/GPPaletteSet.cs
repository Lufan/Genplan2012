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
    

    public class GPPaletteSet : Autodesk.AutoCAD.Windows.PaletteSet
    {
        public Boolean DocumentCreated()
        {
            try
            {
                if (IsEmpty)
                {
                    return false;
                }
                else
                {
                           foreach (var palette in _palettes)
                        {
                            (palette as ILUFSPlug).DocumentCreated();
                        }

                }
                return true;
            }
            catch (System.Exception ex)
            {
                LufsGenplan.AcadApp.AcaEd.WriteMessage("\nERROR: GPPaletteSet.DocumentCreated() " + ex + "\n");
            }
            return false;
        }

        public Boolean DocumentActivated()
        {
            try
            {
                if (IsEmpty)
                {
                    return false;
                }
                else
                {
                    foreach (var palette in _palettes)
                    {
                        (palette as ILUFSPlug).DocumentActivated();
                    }
                }
                return true;
            }
            catch (System.Exception ex)
            {
                LufsGenplan.AcadApp.AcaEd.WriteMessage("\nERROR: GPPaletteSet.DocumentActivated() " + ex + "\n");
            }
            return false;
        }

        public Boolean IsEmpty
        {
            get
            {
                return _palettes.Count == 0;
            }
        }

        public GPPaletteSet()
            : base("*LUFS*2012*Генплан*", null, _guid)
        {
            Initialize();
        }

        private void Initialize()
        {
            try
            {
                this.Style =
                    Autodesk.AutoCAD.Windows.PaletteSetStyles.NameEditable |
                    Autodesk.AutoCAD.Windows.PaletteSetStyles.ShowPropertiesMenu |
                    Autodesk.AutoCAD.Windows.PaletteSetStyles.ShowAutoHideButton |
                    Autodesk.AutoCAD.Windows.PaletteSetStyles.ShowCloseButton;
                this.MinimumSize = new System.Drawing.Size(600, 200);
                this.DockEnabled = Autodesk.AutoCAD.Windows.DockSides.None;

                foreach (var dll in GetPlugins())
                {
                    System.Windows.Forms.UserControl control = LoadPlugin(dll);
                    if (control != null)
                    {
                        if (((LufsGenplan.ILUFSPlug)control).GetTargetApp() == PlugType.Civil3d && AcadApp.isCivilDatabase(AcadApp.AcaDb))
                        {
                            this.AddPalette(CreateControl(((LufsGenplan.ILUFSPlug)control).GetPluginName(), control));
                        }
                        else if (((LufsGenplan.ILUFSPlug)control).GetTargetApp() == PlugType.Autocad)
                        {
                            this.AddPalette(CreateControl(((LufsGenplan.ILUFSPlug)control).GetPluginName(), control));
                        }
                    }
                }
                
                DocumentCreated();
            }
            catch (Autodesk.AutoCAD.Runtime.Exception ex)
            {
                AcadApp.AcaEd.WriteMessage("ERROR: GPPaletteSet.Initialize()\n" + ex + "\n");
            }
        }

        private System.Windows.Forms.UserControl LoadPlugin(String fileName)
        {
            try
            {
                var asm = System.Reflection.Assembly.LoadFrom(fileName);
                Type controltp = null;
                foreach (var tp in asm.GetTypes())
                {
                    if ((tp.BaseType == typeof(System.Windows.Forms.UserControl)) && (tp.GetInterface("ILUFSPlug") ==  typeof(LufsGenplan.ILUFSPlug)))
                    {
                        controltp = tp;
                        break; // Each plugin must contain one UserControl
                    }
                }
                if (controltp != null)
                {
                    return Activator.CreateInstance(controltp) as System.Windows.Forms.UserControl;
                }
                else
                {
                    AcadApp.AcaEd.WriteMessage("WARNING: Plugin( " + new System.IO.FileInfo(fileName).Name + " ) do not have user control.\n");
                }
            }
            catch (Autodesk.AutoCAD.Runtime.Exception ex)
            {
                AcadApp.AcaEd.WriteMessage("ERROR: GPPaletteSet.LoadPlugin( " + fileName + " )\n" + ex + "\n");
            }
            return null;
        }

        private List<String> GetPlugins()
        {
            var files = new List<string>();
            String dir = "";
            try
            {
                var codeBase = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;
                var uri = new System.UriBuilder(codeBase);
                var path = System.Uri.UnescapeDataString(uri.Path);
                dir = System.IO.Path.GetDirectoryName(path);
                dir += "\\plugins";
                foreach (var dll in System.IO.Directory.GetFiles(dir, "*.dll"))
                {
                    AcadApp.AcaEd.WriteMessage("INITIALIZE: plugin( " + new System.IO.FileInfo(dll).Name + ")\n");
                    files.Add(dll);
                }
            }
            catch (Autodesk.AutoCAD.Runtime.Exception ex)
            {
                AcadApp.AcaEd.WriteMessage("WARNING: GPPaletteSet.GetPlugins()\n" + ex + "\n");
            }
            return files;
        }

        private System.Windows.Forms.Control CreateControl(String name, System.Windows.Forms.UserControl uc)
        {
            System.Windows.Forms.Control host = new System.Windows.Forms.Control();
            try
            {
                host = uc;
                host.Name = name;
            }
            catch (Autodesk.AutoCAD.Runtime.Exception ex)
            {
                AcadApp.AcaEd.WriteMessage("ERROR: GPPaletteSet.CreateControl()\n" + ex + "\n");
            }
            return host;
        }

        public void AddPalette(System.Windows.Forms.Control palette)
        {
            try
            {
                bool exists = false;
                foreach (var plt in _palettes)
                {
                    if (plt.Name.ToUpper() == palette.Name.ToUpper())
                    {
                        exists = true;
                        break;
                    }
                }

                if (!exists)
                {
                    this.Add(palette.Name, palette);

                    _palettes.Add(palette);
                }
            }
            catch (Autodesk.AutoCAD.Runtime.Exception ex)
            {
                AcadApp.AcaEd.WriteMessage("ERROR: GPPaletteSet.AddPalette()\n" + ex + "\n");
            }
        }

        public void RemovePalette(string paletteName)
        {
            try
            {
                if (_palettes.Count == 0) return;

                for (int i = 0; i < _palettes.Count; i++)
                {
                    if (_palettes[i].Name.ToUpper() == paletteName.ToUpper())
                    {
                        System.Windows.Forms.Integration.ElementHost ctl = _palettes[i] as System.Windows.Forms.Integration.ElementHost;

                        this.Remove(i);
                        _palettes.RemoveAt(i);

                        ctl.Dispose();

                        if (_palettes.Count == 0) this.Visible = false;
                        return;
                    }
                }
            }
            catch (Autodesk.AutoCAD.Runtime.Exception ex)
            {
                AcadApp.AcaEd.WriteMessage("ERROR: GPPaletteSet.RemovePalette()\n" + ex + "\n");
            }
        }

        public void ActivatePalette(string paletteName)
        {
            if (_palettes.Count == 0) return;

            for (int i = 0; i < _palettes.Count; i++)
            {
                if (_palettes[i].Name.ToUpper() == paletteName.ToUpper())
                {
                    this.Activate(i);
                    return;
                }
            }
        }

        private static Guid _guid = new Guid("0348811E-2B5F-4362-8832-2AC32065BD28");

        private List<System.Windows.Forms.Control> _palettes = new List<System.Windows.Forms.Control>();
    }
}
