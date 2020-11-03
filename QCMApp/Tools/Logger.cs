using log4net;
using System;
using System.Diagnostics;
using System.Text;

namespace QCMApp.Tools
{
    /// <summary>
    /// Gestion des log
    /// </summary>
    public static class Logger
    {
        /// <summary>
        /// Complément au message de log
        /// </summary>
        private static string _strComplement;

        /// <summary>
        /// Constructeur
        /// </summary>
        static Logger()
        {
            var obj = System.Reflection.Assembly.GetAssembly(typeof(Logger));

            if (obj != null)
            {
                //Version
                var strVersion = obj.GetName().Version.ToString();
                var strSignature = "Non signée";

                if (obj.GetName().GetPublicKeyToken().Length > 0)
                {
                    var strBuilder = new StringBuilder();
                    foreach (var c in obj.GetName().GetPublicKeyToken())
                    {
                        strBuilder.AppendFormat("{0:x}", c);
                    }
                    strSignature = strBuilder.ToString();
                }

                Logger._strComplement = string.Format("[{0} - {1}]",
                                                        strVersion,
                                                        strSignature);
            }

            log4net.GlobalContext.Properties["log_who"] = System.Environment.UserName.Trim().Replace(" ", "");
        }

        /// <summary>Niveau d'erreur</summary>
        public enum Niveau
        {
            /// <summary>debug</summary>
            Debug = 0,
            /// <summary>erreur</summary>
            Erreur = 1,
            /// <summary>fatal</summary>
            Fatal = 2,
            /// <summary>info</summary>
            Info = 3,
            /// <summary>warning</summary>
            Warning = 4,

        }

        // Initialisation du logger
        private static ILog _logger;

        /// <summary>Obtient le Logger </summary>
        private static ILog Log
        {
            get
            {
                if (Logger._logger == null) Logger._logger = LogManager.GetLogger("SGT");
                return Logger._logger;
            }
        }



        /// <summary>Détermine si le niveau de trace actuel est égal ou supérieur à Debug</summary>
        /// <returns>Vrai / Faux</returns>
        public static bool IsDebugEnabled { get { return Logger.Log.IsDebugEnabled; } }

        /// <summary>Détermine si le niveau de trace actuel est égal ou supérieur à Info</summary>
        /// <returns>Vrai / Faux</returns>
        public static bool IsInfoEnabled { get { return Logger.Log.IsInfoEnabled; } }

        /// <summary>Détermine si le niveau de trace actuel est égal ou supérieur à Warning</summary>
        /// <returns>Vrai / Faux</returns>
        public static bool IsWarnEnabled { get { return Logger.Log.IsWarnEnabled; } }

        /// <summary>Détermine si le niveau de trace actuel est égal ou supérieur à Error</summary>
        /// <returns>Vrai / Faux</returns>
        public static bool IsErrorEnabled { get { return Logger.Log.IsErrorEnabled; } }

        /// <summary>Détermine si le niveau de trace actuel est égal ou supérieur à Fatal</summary>
        /// <returns>Vrai / Faux</returns>
        public static bool IsFatalEnabled { get { return Logger.Log.IsFatalEnabled; } }
        
        /// <summary>Ecrit le message en fonction de la trace</summary>
        /// <param name="penmNiveau">Le niveau</param>
        /// <param name="pstrMessage">Le message</param>
        /// <remarks> Defaut = Debug </remarks>
        public static void Ecrire(Niveau penmNiveau, string pstrMessage)
        {
            try
            {
                switch (penmNiveau)
                {
                    case Niveau.Debug:
                        if (Logger.Log.IsDebugEnabled) { Logger.Log.Debug(Logger.Completer(pstrMessage)); };
                        break;
                    case Niveau.Erreur:
                        if (Logger.Log.IsErrorEnabled) { Logger.Log.Error(Logger.Completer(pstrMessage)); }
                        break;
                    case Niveau.Fatal:
                        if (Logger.Log.IsFatalEnabled) { Logger.Log.Fatal(Logger.Completer(pstrMessage)); }
                        break;
                    case Niveau.Info:
                        if (Logger.Log.IsInfoEnabled) { Logger.Log.Info(Logger.Completer(pstrMessage)); }
                        break;
                    case Niveau.Warning:
                        if (Logger.Log.IsWarnEnabled) { Logger.Log.Warn(Logger.Completer(pstrMessage)); }
                        break;
                    default:
                        if (Logger.Log.IsDebugEnabled) { Logger.Log.Debug(Logger.Completer(pstrMessage)); }
                        break;
                }
            }
            catch (Exception ex)
            {
                if (penmNiveau == Niveau.Erreur)
                {
                    EventWindowsLog(ex.Message + " " + ex.StackTrace);
                    EventWindowsLog(pstrMessage);
                }
            }
        }

        /// <summary>
        /// Ecriture d'un log avec sérialisation json de l'object
        /// </summary>
        /// <param name="penmNiveau"></param>
        /// <param name="o"></param>
        /// <param name="type"></param>
        public static void Ecrire(Niveau penmNiveau, string message, object o, Type type)
        {
            //sérialisation json de l'object
            if (o != null)
            {
                string strO = JsonHelper.Serialize(o, type);
                Ecrire(penmNiveau, message + strO);
            }
        }

        /// <summary> Ecrit le message en fonction de la trace</summary>
        /// <param name="penmNiveau">Le niveau</param>
        /// <param name="pstrMessage">Le message</param>
        /// <param name="pexcException"> L'exception</param>
        /// <remarks> Defaut = Debug </remarks>
        public static void Ecrire(Niveau penmNiveau, string pstrMessage, Exception pexcException)
        {
            try
            {
                switch (penmNiveau)
                {
                    case Niveau.Debug:
                        if (Logger.Log.IsDebugEnabled) { Logger.Log.Debug(Logger.Completer(pstrMessage), pexcException); }
                        break;
                    case Niveau.Erreur:
                        if (Logger.Log.IsErrorEnabled) { Logger.Log.Error(Logger.Completer(pstrMessage), pexcException); }
                        break;
                    case Niveau.Fatal:
                        if (Logger.Log.IsFatalEnabled) { Logger.Log.Fatal(Logger.Completer(pstrMessage), pexcException); }
                        break;
                    case Niveau.Info:
                        if (Logger.Log.IsInfoEnabled) { Logger.Log.Info(Logger.Completer(pstrMessage), pexcException); }
                        break;
                    case Niveau.Warning:
                        if (Logger.Log.IsWarnEnabled) { Logger.Log.Warn(Logger.Completer(pstrMessage), pexcException); }
                        break;
                    default:
                        if (Logger.Log.IsDebugEnabled) { Logger.Log.Debug(Logger.Completer(pstrMessage), pexcException); }
                        break;
                }
            }
            catch (Exception ex)
            {
                if (penmNiveau == Niveau.Erreur)
                {
                    EventWindowsLog(ex.Message + " " + ex.StackTrace);
                    EventWindowsLog(pstrMessage);
                }
            }
        }

        /// <summary>Ecrit le message en fonction de la trace</summary>
        /// <param name="penmNiveau">Le niveau</param>
        /// <param name="pstrMessage">Le message</param>
        /// <param name="pobjParams"> Tableau de paramètres</param>
        /// <remarks> Defaut = Debug </remarks>
        public static void EcrireFormat(Niveau penmNiveau, string pstrMessage, params object[] pobjParams)
        {
            switch (penmNiveau)
            {
                case Niveau.Debug:
                    if (Logger.Log.IsDebugEnabled) { Logger.Log.DebugFormat(Logger.Completer(pstrMessage), pobjParams); }
                    break;
                case Niveau.Erreur:
                    if (Logger.Log.IsErrorEnabled) { Logger.Log.ErrorFormat(Logger.Completer(pstrMessage), pobjParams); }
                    break;
                case Niveau.Fatal:
                    if (Logger.Log.IsFatalEnabled) { Logger.Log.FatalFormat(Logger.Completer(pstrMessage), pobjParams); }
                    break;
                case Niveau.Info:
                    if (Logger.Log.IsInfoEnabled) { Logger.Log.InfoFormat(Logger.Completer(pstrMessage), pobjParams); }
                    break;
                case Niveau.Warning:
                    if (Logger.Log.IsWarnEnabled) { Logger.Log.WarnFormat(Logger.Completer(pstrMessage), pobjParams); }
                    break;
                default:
                    if (Logger.Log.IsDebugEnabled) { Logger.Log.DebugFormat(Logger.Completer(pstrMessage), pobjParams); }
                    break;
            }

        }

        /// <summary>
        /// Complete le message
        /// </summary>
        /// <param name="pstrMessage"></param>
        /// <returns></returns>
        private static string Completer(string pstrMessage)
        {
            return string.Format("{0} - {1} ", Logger._strComplement, pstrMessage);
        }

        /// <summary>
        /// Ecriture dans les journaux windows
        /// </summary>
        /// <param name="message"></param>
        private static void EventWindowsLog(string message)
        {
            string sSource;
            string sLog;

            sSource = "SGT.Module.Consignes";
            sLog = "Application";

            if (!EventLog.SourceExists(sSource))
                EventLog.CreateEventSource(sSource, sLog);

            EventLog.WriteEntry(sSource, message, EventLogEntryType.Error);
        }
    }
}