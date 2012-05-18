/*
 * Created by Луферов Александр Николаевич
 * 
 * 
 * 
 * Лицензия GNU Lesser General Public License : http://www.gnu.org/copyleft/lesser.html.
 */
namespace LufsGenplan
{	
	using System;
	using System.Collections.Generic;
	using System.Text;
	using System.Runtime.InteropServices;
	
	using Autodesk.AutoCAD.Runtime;
	using Autodesk.AutoCAD.EditorInput;
	using Autodesk.AutoCAD.ApplicationServices;
	using Autodesk.AutoCAD.DatabaseServices;


	/// <summary>
	/// Class contains command metod to invoke in autocad..
	/// </summary>
	public sealed class Commands : IExtensionApplication
	{
		#region IExtensionApplication Members

        private DocumentCollection _dwgManager = null;
        //private static Microsoft.Practices.Prism.Logging.TextLogger _logger = null;

		public void Initialize()
		{
            try
            {
                if (Commands.isRegister() == false)
                {
                    AcadApp.AcaEd.WriteMessage("Приложение не активировано. Выполняется активация...\n");
                    if (Commands.Register() == true)
                    {
                        AcadApp.AcaEd.WriteMessage("Приложение Lufs_ГЕНПЛАН успешно активировано (C) 2012 Aleksandr Luferov\n");
                    }
                    else
                    {
                        AcadApp.AcaEd.WriteMessage("Приложение Lufs_ГЕНПЛАН НЕ активировано (C) 2012 Aleksandr Luferov\n");
                    }
                }
                else
                {
                    AcadApp.AcaEd.WriteMessage("Приложение активировано.\n");
                }
                AcadApp.AcaEd.WriteMessage("Lufs_ГЕНПЛАН успешно загружен (C) 2012 Aleksandr Luferov\n");
                AcadApp.AcaEd.WriteMessage("Создание панели инструментов команда (LUFSSHOWPALETTE).\n");
                AcadApp.AcaEd.WriteMessage("Удаление приложения команда LUFSUNINSTALL.\n");

                //_logger = new Microsoft.Practices.Prism.Logging.TextLogger(new System.IO.StreamWriter(@"D:\log.txt")); ;

                _dwgManager = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager;
                _dwgManager.DocumentDestroyed += new DocumentDestroyedEventHandler(_dwgManager_DocumentDestroyed);
                _dwgManager.DocumentCreated += new DocumentCollectionEventHandler(_dwgManager_DocumentCreated);
                _dwgManager.DocumentActivated += new DocumentCollectionEventHandler(_dwgManager_DocumentActivated);

                LoadPalette();

                //Do update, that take effect with next time autocad execute
                th = new System.Threading.Thread(new System.Threading.ThreadStart(DoUpdate));
                th.Start();
                //DoUpdate();
            }
            catch (System.Exception ex)
            {
                LufsGenplan.AcadApp.AcaEd.WriteMessage("\nERROR: Commands.Initialize() " + ex + "\n");
            }
		}
		public void Terminate()
		{
            try
            {

                //_logger.Log("DEBUG: Application exit\n", Microsoft.Practices.Prism.Logging.Category.Debug, Microsoft.Practices.Prism.Logging.Priority.Low);
                //_logger.Dispose();
                if (_ps != null)
                {
                    _ps.Dispose();
                }
                else
                {
                    LufsGenplan.AcadApp.AcaEd.WriteMessage("\nERROR: Commands.Terminate() Palette is null\n");
                }
                th.Join();
            }
            catch (System.Exception ex)
            {
                LufsGenplan.AcadApp.AcaEd.WriteMessage("\nERROR: Commands.Terminate() " + ex + "\n");
            }
		}
		#endregion

        #region Update

        private void DoUpdate()
        {
            try
            {
                String dir;
                var codeBase = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;
                var uri = new System.UriBuilder(codeBase);
                var path = System.Uri.UnescapeDataString(uri.Path);
                dir = System.IO.Path.GetDirectoryName(path);
                var pluginDir = dir + "\\plugins";

                var currentRootFiles = GetFileList(dir);
                var currentPluginFiles = GetFileList(pluginDir);

                var remoteRootFiles = GetFileList("H:\\Проектир\\БАД\\Credo.usr\\подсчет объемов работ\\genplan2012");
                var remotePluginFiles = GetFileList("H:\\Проектир\\БАД\\Credo.usr\\подсчет объемов работ\\genplan2012" + "\\plugins");

                var filesTobeCopiedFromRoot = new List<System.IO.FileInfo>();

                foreach (var file in remoteRootFiles)
                {
                    if (!AlreadyHaveThisFile(file, currentRootFiles))
                    {
                        //AcadApp.AcaEd.WriteMessage("DEBUG: ROOT TO BE copied " + file.Name + "\n");
                        filesTobeCopiedFromRoot.Add(file);
                    }
                }

                var filesTobeCopiedFromPlugin = new List<System.IO.FileInfo>();

                foreach (var file in remotePluginFiles)
                {
                    if (!AlreadyHaveThisFile(file, currentPluginFiles))
                    {
                        //AcadApp.AcaEd.WriteMessage("DEBUG: PLUGIN TO BE copied " + file.Name + "\n");
                        filesTobeCopiedFromPlugin.Add(file);
                    }
                }

                if (filesTobeCopiedFromRoot.Count != 0)
                {
                    CopyFiles(filesTobeCopiedFromRoot, dir);
                }

                if (filesTobeCopiedFromPlugin.Count != 0)
                {
                    CopyFiles(filesTobeCopiedFromPlugin, pluginDir);
                }

                var filesTobeRemovedFromRoot = new List<System.IO.FileInfo>();

                foreach (var file in currentRootFiles)
                {
                    if (AlreadyDonotHaveThisFile(file, remoteRootFiles))
                    {
                        filesTobeRemovedFromRoot.Add(file);
                    }
                }

                var filesTobeRemovedFromPlugin = new List<System.IO.FileInfo>();

                foreach (var file in currentPluginFiles)
                {
                    if (AlreadyDonotHaveThisFile(file, remotePluginFiles))
                    {
                        filesTobeRemovedFromPlugin.Add(file);
                    }
                }

                if (filesTobeRemovedFromRoot.Count != 0)
                {
                    RemoveFiles(filesTobeRemovedFromRoot);
                }

                if (filesTobeRemovedFromPlugin.Count != 0)
                {
                    RemoveFiles(filesTobeRemovedFromPlugin);
                }
            }
            catch (Autodesk.AutoCAD.Runtime.Exception ex)
            {
                AcadApp.AcaEd.WriteMessage("ERROR: Commands.CheckUpdate()\n" + ex + "\n");
            }
        }

        private Boolean RemoveFiles(List<System.IO.FileInfo> files)
        {
            try
            {
                foreach (var file in files)
                {
                    if (file.Exists)
                    {
                        try
                        {
                            System.IO.File.Delete(file.FullName);
                        }
                        catch (System.Exception)
                        {
                            //AcadApp.AcaEd.WriteMessage("WARNING: Can not delete " + file.Name + " try to rename, and delete at next session.\n");
                            try
                            {
                                System.IO.File.Move(file.FullName, file.FullName.Substring(0, file.FullName.Length - 3) + "old");
                            }
                            catch (System.Exception ex2)
                            {
                                AcadApp.AcaEd.WriteMessage("ERROR: Move " + file.FullName + " -> " + file.FullName.Substring(0, file.FullName.Length - 3) + "old" + "\n" + ex2 + "\n");
                            }
                        }
                    }
                    else
                    {
                        AcadApp.AcaEd.WriteMessage("DEBUG: " + file.FullName + " do not exist.\n");
                    }
                }
                return true;
            }
            catch (System.Exception ex)
            {
                AcadApp.AcaEd.WriteMessage("ERROR: Commands.RemoveFiles()\n" + ex + "\n");
            }
            return false;
        }

        private Boolean CopyFiles(List<System.IO.FileInfo> files, String targetDirectory)
        {
            try
            {
                foreach (var file in files)
                {
                    var targetFilePath = targetDirectory + "\\" + file.Name;
                    var targetFile = new System.IO.FileInfo(targetFilePath);

                    if (targetFile.Exists)
                    {
                        var oldFileName = targetFilePath.Substring(0, targetFilePath.Length - 3) + "old";
                        var oldFile = new System.IO.FileInfo(oldFileName);
                        if (oldFile.Exists)
                        {
                            try
                            {
                                System.IO.File.Delete(oldFile.FullName);
                            }
                            catch (System.Exception ex)
                            {
                                AcadApp.AcaEd.WriteMessage("ERROR: Commands.CopyFiles().Delete " + oldFile.FullName + "\n" + ex + "\n");
                            }
                        }
                        try
                        {
                            System.IO.File.Move(targetFilePath, oldFileName);
                        }
                        catch (System.Exception ex)
                        {
                            AcadApp.AcaEd.WriteMessage("ERROR: Commands.CopyFiles().Move " + targetFilePath + " -> " + oldFileName + "\n" + ex + "\n");
                        }
                    }
                    try
                    {
                        System.IO.File.Copy(file.FullName, targetFilePath);
                    }
                    catch (System.Exception ex)
                    {
                        AcadApp.AcaEd.WriteMessage("ERROR: Commands.CopyFiles().Copy " + file.FullName + " -> " + targetFilePath + "\n" + ex + "\n");
                    }
                }
                return true;
            }
            catch (System.Exception ex)
            {
                AcadApp.AcaEd.WriteMessage("ERROR: Commands.CopyFiles()\n" + ex + "\n");
            }
            return false;
        }

        private Boolean AlreadyHaveThisFile(System.IO.FileInfo file, List<System.IO.FileInfo> currentFiles)
        {
            try
            {
                foreach (var curFile in currentFiles)
                {
                    if ((curFile.Name == file.Name) && (curFile.LastWriteTime >= file.LastWriteTime))
                    {
                        return true;
                    }
                }
            }
            catch (System.Exception ex)
            {
                AcadApp.AcaEd.WriteMessage("ERROR: Commands.AlreadyHaveThisFile " + ex + "\n");
            }
            return false;
        }

        private Boolean AlreadyDonotHaveThisFile(System.IO.FileInfo file, List<System.IO.FileInfo> currentFiles)
        {
            Boolean result = true;
            try
            {
                foreach (var curFile in currentFiles)
                {
                    if ((curFile.Name == file.Name))
                    {
                        result = false;
                    }
                }
            }
            catch (System.Exception ex)
            {
                AcadApp.AcaEd.WriteMessage("ERROR: Commands.AlreadyDonotHaveThisFile " + ex + "\n");
            }
            return result;
        }

        private List<System.IO.FileInfo> GetFileList(String folderPath)
        {
            List<System.IO.FileInfo> files = new List<System.IO.FileInfo>();
            try
            {
                foreach (var dll in System.IO.Directory.GetFiles(folderPath, "*.???"))
                {
                    files.Add(new System.IO.FileInfo(dll));
                }
            }
            catch (Autodesk.AutoCAD.Runtime.Exception ex)
            {
                AcadApp.AcaEd.WriteMessage("ERROR: Commands.GetFileList()\n" + ex + "\n");
            }
            return files;
        }

        #endregion Update

        public void LoadPalette()
        {
            try
            {
                //Create PaletteSet
                CreatePaletteSet(null);
            }
            catch (System.Exception ex)
            {
                LufsGenplan.AcadApp.AcaEd.WriteMessage("\nERROR: Commands.LoadPalette() " + ex + "\n");
            }
        }

        #region Document collection events
        private void _dwgManager_DocumentActivated(object sender, DocumentCollectionEventArgs e)
        {
            try
            {
                if (_ps != null)
                {
                    _ps.DocumentActivated();
                }
            }
            catch (System.Exception ex)
            {
                LufsGenplan.AcadApp.AcaEd.WriteMessage("\nERROR: Commands._dwgManager_DocumentActivated() " + ex + "\n");
            }
        }
        
        private void _dwgManager_DocumentDestroyed(object sender, DocumentDestroyedEventArgs e)
        {
            try
            {
                //_logger.Log("DEBUG: destroyed. not 1 . COUNT = " + Application.DocumentManager.Count, Microsoft.Practices.Prism.Logging.Category.Debug, Microsoft.Practices.Prism.Logging.Priority.Low);
                if (Application.DocumentManager.Count == 1)
                {
                    //_logger.Log("DEBUG: destroyed. Visible = false . COUNT = " + Application.DocumentManager.Count, Microsoft.Practices.Prism.Logging.Category.Debug, Microsoft.Practices.Prism.Logging.Priority.Low);
                    _ps.Visible = false;
                }
            }
            catch (System.Exception ex)
            {
                LufsGenplan.AcadApp.AcaEd.WriteMessage("\nERROR: Commands._dwgManager_DocumentDestroyed() " + ex + "\n");
            }
        }

        private void _dwgManager_DocumentCreated(object sender, DocumentCollectionEventArgs e)
        {
            try
            {
                //_logger.Log("DEBUG: created. CreatePaletteSet() . COUNT = " + Application.DocumentManager.Count, Microsoft.Practices.Prism.Logging.Category.Debug, Microsoft.Practices.Prism.Logging.Priority.Low);
                CreatePaletteSet(new ResultBuffer());
                _ps.DocumentCreated();
            }
            catch (System.Exception ex)
            {
                LufsGenplan.AcadApp.AcaEd.WriteMessage("\nERROR: Commands._dwgManager_DocumentCreated() " + ex + "\n");
            }
        }

        #endregion Document collection events

        private static GPPaletteSet _ps = null;
        private System.Threading.Thread th;

		#region Commands to invoke in autocad
        //[CommandMethod("LUFS", "LUFSSHOWPALETTE", CommandFlags.Transparent)]
        [LispFunction("LUFSSHOWPALETTE")]
        public static void CreatePaletteSet(ResultBuffer rb)
        {
            try 
            {
                if (_ps == null)
                {
                    _ps = new GPPaletteSet();
                }
                if (!_ps.IsEmpty)
                {
                    _ps.Visible = true;
                }
                else
                {
                    AcadApp.AcaEd.WriteMessage("\nLUFSGenplan: Нет доступных расширений для загрузки\n");
                }
            }catch (Autodesk.AutoCAD.Runtime.Exception ex)
            {
                AcadApp.AcaEd.WriteMessage("ERROR: Commands.CreatePaletteSet()\n" + ex + "\n");
            }
        }

        [CommandMethod("LUFS", "LUFSUNINSTALL", CommandFlags.Transparent)]
        public static void RemoveRegInfo()
        {
            try {
                if (Commands.isRegister() == false) {
                    AcadApp.AcaEd.WriteMessage("ERROR: [CommandMethod(\"LUFSUNINSTALL\")].RemoveRegInfo()" + " isRegister() == false\n");
                } else {
                    AcadApp.AcaEd.WriteMessage("Выполняется регистрационных данных приложения.\n");
                    Commands.Uninstall();
                }
            } catch (Autodesk.AutoCAD.Runtime.Exception ex) {
                AcadApp.AcaEd.WriteMessage("ERROR: Commands.RemoveRegInfo()\n" + ex + "\n");
            }
        }

        #endregion
        
        #region Register app for demand loading at startup

        static private bool isRegister()
        {
            try {
                IConfigurationSection con = Application.UserConfigurationManager.OpenGlobalSection();
                IConfigurationSection appsec = con.OpenSubsection("Applications");
                if (appsec.ContainsSubsection(sectionName))
                {
                    System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();
                    string LOADER = asm.Location;

                    Microsoft.Win32.RegistryKey autoc = (Microsoft.Win32.Registry.CurrentUser);
                    Microsoft.Win32.RegistryKey ack = autoc.OpenSubKey(HostApplicationServices.Current.RegistryProductRootKey);
                    Microsoft.Win32.RegistryKey appk = ack.OpenSubKey("Applications", true);
                    Microsoft.Win32.RegistryKey cur_app = appk.OpenSubKey(sectionName, true);
                    if (cur_app.GetValue("LOADER") as String == LOADER) { 
                        return true; 
                    } else {
                        cur_app.SetValue("LOADER", LOADER);
                        AcadApp.AcaEd.WriteMessage("Путь загрузки приложения изменен: " + LOADER);
                    }
                    return true;
                }
            }
            catch (System.Exception ex)
            {
                AcadApp.AcaEd.WriteMessage("ERROR: Commands.isRegister() " + ex + "\n");
                return false;
            }
            return false;
        }

        static private bool Register()
        {
            System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();
            string LOADER = asm.Location;
            try {
                Microsoft.Win32.RegistryKey autoc = (Microsoft.Win32.Registry.CurrentUser);
                Microsoft.Win32.RegistryKey ack = autoc.OpenSubKey(HostApplicationServices.Current.RegistryProductRootKey);
                Microsoft.Win32.RegistryKey appk = ack.OpenSubKey("Applications", true);
                if (appk == null) { appk = ack.CreateSubKey("Applications"); }
                Microsoft.Win32.RegistryKey cur_app = appk.CreateSubKey(sectionName);
                cur_app.SetValue("DESCRIPTION", DESCRIPTION);
                cur_app.SetValue("LOADCTRLS", LOADCTRLS);
                cur_app.SetValue("MANAGED", MANAGED);
                cur_app.SetValue("LOADER", LOADER);
            }
            catch (System.Exception ex) {
                AcadApp.AcaEd.WriteMessage("ERROR: Commands.Register() " + ex + "\n");
                return false;
            }
            return true;
        }

        static private void RemoveRegisterInfo()
        {
            try {
                Microsoft.Win32.RegistryKey autoc = (Microsoft.Win32.Registry.CurrentUser);
                Microsoft.Win32.RegistryKey ack = autoc.OpenSubKey(HostApplicationServices.Current.RegistryProductRootKey);
                Microsoft.Win32.RegistryKey appk = ack.OpenSubKey("Applications", true);
                if (appk.OpenSubKey(sectionName) == null) { 
                    return; 
                }
                appk.DeleteSubKey(sectionName);
            }
            catch (System.Exception ex) {
                AcadApp.AcaEd.WriteMessage("ERROR: Commands.RemoveRegisterInfo() " + ex + "\n");
            }
        }

        static private void Uninstall()
        {
            RemoveRegisterInfo();
            AcadApp.AcaEd.WriteMessage("Приложение деактивировано.\n После перезапуска автокада можно удалить файл приложения.\n");

        }

        private const string sectionName = "LUFS_genplan_2012";
        private const string DESCRIPTION = "(C) Luferov Aleksandr 2012";
        private const int LOADCTRLS = 2;
        private const int MANAGED = 1;
        #endregion
    }
}