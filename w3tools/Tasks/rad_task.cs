using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using Ninject.Infrastructure;
using radish.core.Commands;
using w3tools.common;
using w3tools.Services;

namespace w3tools
{
    
    /// <summary>
    /// 
    /// </summary>
    public class RAD_Task
    {
        public IConfigService Config { get; set; }
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
        public WFR RunCommandSync(RAD_Command cmd)
        {
            string args = cmd.CommandLine;
            return RunArgsSync(cmd.Name, args);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="encoderName"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public WFR RunArgsSync(string encoderName, string args)
        {
            string enoderPath = Path.Combine(Config.GetConfigSetting("RAD_Path"), $"{encoderName}.exe");
            //Logger.LogString($"EnoderPath: {enoderPath}");
            if (!File.Exists(enoderPath))
            {
                Logger.LogString($"Could not find encoder with path: {enoderPath}");
                return WFR.WFR_Error;
            }

            //FIXME check for version?

            var proc = new ProcessStartInfo(enoderPath) { WorkingDirectory = Path.GetDirectoryName(enoderPath) };
            try
            {
                Logger.LogString($"-----------------------------------------------------");
                Logger.LogString($"RAD_TASK: {args}");

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
                            string result = reader.ReadLine();
                            Logger.LogString(result);
                            Logger.LogExtended(SystemLogFlag.SLF_Interpretable, ToolFlag.TLF_Radish, encoderName, $"{result}");

                            if (reader.EndOfStream)
                                break;
                        }
                    }
                }

                //Handle Errors
                if (Logger.ExtendedLog.Any(x => x.Flag == LogFlag.WLF_Error))
                {
                    Logger.LogString("Finished with Errors.");
                    return WFR.WFR_Error;
                }
                else if (Logger.ExtendedLog.Any(x => x.Flag == LogFlag.WLF_Error))
                {
                    Logger.LogString("Finished with Warnings.");
                    return WFR.WFR_Finished;
                }
                else
                {
                    Logger.LogString("Finished without Errors or Warnings.");
                    return WFR.WFR_Finished;
                }
            }
            catch (Exception ex)
            {
                Logger.LogString(ex.ToString());
                throw ex;
            }
        }
    }
        
}
