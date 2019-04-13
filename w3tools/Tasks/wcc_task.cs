using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using static w3tools.common.WccExtendedLogger;
using wcc.core.Commands;
using w3tools.common;
using w3tools.Services;
using Ninject;

namespace w3tools
{
    /// <summary>
    /// 
    /// </summary>
    public class WCC_Task
    {
        private IConfigService Config { get; set; }
        private ILoggerService Logger { get; set; }

        [Inject]
        public void SetConfigService(IConfigService configService, ILoggerService loggerService)
        {
            Config = configService;
            Logger = loggerService;
        }

        /// <summary>
        /// runs wcc_lite with specified command
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public WFR RunCommandSync(WCC_Command cmd)
        {
            string args = cmd.CommandLine;
            return RunArgsSync(cmd.Name, args);
        }

        /// <summary>
        /// Runs wcc_lite with specified arguments
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public WFR RunArgsSync(string cmdName, string args)
        {
            string wccPath = Config.GetConfigSetting("WCC_Path");
            var proc = new ProcessStartInfo(wccPath) { WorkingDirectory = Path.GetDirectoryName(wccPath) };

            try
            {
                Logger.LogString($"WCC_TASK: {args}");

                proc.Arguments = args;
                proc.UseShellExecute = false;
                proc.RedirectStandardOutput = true;
                proc.WindowStyle = ProcessWindowStyle.Hidden;
                proc.CreateNoWindow = true;

                using (var process = Process.Start(proc))
                {
                    using (var reader = process.StandardOutput)
                    {
                        while (true)
                        {
                            string result = reader.ReadLine(); //FIXME is this slow?
                            Logger.LogString(result);
                            //_Logger.LogExtended(SystemLogFlag.SLF_Interpretable, cmdName, $"{result}"); //FIXME

                            if (reader.EndOfStream)
                                break;
                        }
                    }
                }

                return WFR.WFR_Finished; //FIXME
            }
            catch (Exception ex)
            {
                Logger.LogString(ex.ToString());
                throw ex;
                return WFR.WFR_Error; //FIXME
                
            }
        }
    }
}
