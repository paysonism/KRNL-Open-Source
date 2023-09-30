using FluxAPI.Classes;
using System;
using System.Diagnostics;
using System.IO;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Threading;

namespace FluxAPI
{
    public class Flux
    {
        internal static readonly string ProgramData = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
        internal static readonly string PreFlux = Path.Combine(ProgramData, "Fluxus");
        internal static readonly string PostFlux = Path.Combine(PreFlux, "FluxusAPI");
        internal static readonly string ModulePath = Path.Combine(PostFlux, "Module.dll");
        internal static readonly string FluxPath = Path.Combine(PostFlux, "FluxteamAPI.dll");
        internal static readonly string ModuleUrl = "https://github.com/ItzzExcel/LInjectorRedistributables/raw/main/extra/Module.dll";
        internal static readonly string FluxURL = "https://github.com/ItzzExcel/LInjectorRedistributables/raw/main/extra/FluxteamAPI.dll";
        internal static DispatcherTimer timer = new DispatcherTimer();
        internal static string InitString;
        public bool DoAutoAttach = false;

        internal bool IsInitialized;

        public void InitializeAPI(string ExecutorName = "SubZero RBLX")
        {
            _ = CreateDirectories();
            _ = DownloadDLLs();
            try
            {
                _ = CreateDirectories();
                _ = DownloadDLLs();
                FluxusAPI.create_files(ModulePath);
            }
            catch (Exception)
            {
                ThreadBox.MsgThread("Couldn't create files of " + ModulePath);
            }

            if (!IsAdmin())
            {
                ThreadBox.MsgThread("The application must be executed with Administrator privileges.", "SubZero API", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Application.Exit();
            }

            IsInitialized = true;
            SetExecutorName(ExecutorName);
            runAutoAttachTimer();
        }

        internal void SetExecutorName(string executorName)
        {
            InitString =
                $"local a=\"{executorName}\"local b;function Export(c,d)getgenv()[c]=d end;function HookedRequest(e)local f=e.Headers or{{}}f['User-Agent']=a;return b({{Url=e.Url,Method=e.Method or\"GET\",Headers=f,Cookies=e.Cookies or{{}},Body=e.Body or\"\"}})end;b=hookfunction(request,HookedRequest)b=hookfunction(http.request,HookedRequest)b=hookfunction(http_request,HookedRequest)Export(\"identifyexecutor\",function()return a end)Export(\"getexecutorname\",function()return a end)";
        }

        internal void RunInit(object sender, EventArgs e)
        {
            var flag = FluxusAPI.is_injected(FluxusAPI.pid);

            if (flag)
            {
                try
                {
                    FluxusAPI.run_script(FluxusAPI.pid, InitString);
                }
                catch { }
            }

            Task.Delay(200);
        }

        internal Task CreateDirectories()
        {
            if (!Directory.Exists(PreFlux))
            {
                Directory.CreateDirectory(PreFlux);
                if (!Directory.Exists(PostFlux))
                {
                    Directory.CreateDirectory(PostFlux);
                }
            }

            return Task.CompletedTask;
        }

        internal bool IsAdmin()
        {
            bool isElevated;
            using (WindowsIdentity identity = WindowsIdentity.GetCurrent())
            {
                WindowsPrincipal principal = new WindowsPrincipal(identity);
                isElevated = principal.IsInRole(WindowsBuiltInRole.Administrator);
            }

            return isElevated;
        }

        public void RunInternalFunctions()
        {
            timer.Tick += RunInit;
            timer.Interval = TimeSpan.FromSeconds(5);
            timer.Start();
        }

        public async Task DownloadDLLs()
        {
            try
            {
                await Utility.DownloadAsync(ModuleUrl, ModulePath);
                await Utility.DownloadAsync(FluxURL, FluxPath);
            }
            catch (Exception) { }
        }

        public void Inject()
        {
            if (!IsInitialized)
            {
                ThreadBox.MsgThread("Initialize API First!", "SubZero RBLX", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                var flag = !FluxusAPI.is_injected(FluxusAPI.pid);
                if (flag)
                {
                    try
                    {
                        try
                        {
                            FluxusAPI.inject();
                        }
                        catch (Exception ex)
                        {
                            ThreadBox.MsgThread("The API encountered a unrecoverable error" +
                                                "\nDue to Hyperion Byfron, SubZero only suports the UWP (Microsoft Store) version of ROBLOX." +
                                                "\nException:\n"
                                                + ex.Message
                                                + "\nStack Trace:\n"
                                                + ex.StackTrace,
                                "SubZero API | Exception",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                        }

                    }
                    catch (Exception ex)
                    {
                        ThreadBox.MsgThread("Error on inject:\n" + ex.Message
                                                                 + "\nStack Trace:\n" + ex.StackTrace,
                            "SubZero API | Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    ThreadBox.MsgThread("Already injected", "SubZero API",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        public void Execute(string src)
        {
            try
            {
                var flag = FluxusAPI.is_injected(FluxusAPI.pid);
                if (flag)
                {
                    FluxusAPI.run_script(FluxusAPI.pid, $"{InitString}; {src}");
                    Utility.Cw("Executed Script Successfully.");
                }
                else
                {
                    ThreadBox.MsgThread("Inject API before running script.", "SubZero API", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                ThreadBox.MsgThread("We couldn't run the script.", "SubZero API",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                Utility.Cw("Exception from SubZero:\n"
                        + ex.Message
                        + "\nStack Trace:\n"
                        + ex.StackTrace);
            }
        }

        internal void runAutoAttachTimer()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += AttachedDetectorTick;
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Start();
        }

        internal void AttachedDetectorTick(object sender, EventArgs e)
        {
            if (DoAutoAttach == false)
            {
                return;
            }

            var processesByName = Process.GetProcessesByName("Windows10Universal");
            foreach (var Process in processesByName)
            {
                var FilePath = Process.MainModule.FileName;

                if (FilePath.Contains("ROBLOX"))
                {
                    try
                    {
                        var flag = FluxusAPI.is_injected(FluxusAPI.pid);
                        if (flag)
                        {
                            return;
                        }

                        Inject();
                    }
                    catch { }
                }
            }
        }
    }
}