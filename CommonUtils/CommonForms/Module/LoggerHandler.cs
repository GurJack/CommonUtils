using NLog;
using NLog.Common;
using NLog.Config;
using NLog.Targets.Wrappers;
using NLog.Targets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using DevExpress.XtraEditors;

namespace CommonForms.Module
{
    public static class LoggerHandler
    {
        private static readonly ILogger _logger;
        private static bool _realError { get; set; } = false;
        private static bool _errorAlreadyShown { get; set; } = false;
        public static bool EnableErrorNotifications { get; set; } = false;
        public static bool ShowErrorDetails { get; set; } = false;

        static LoggerHandler()
        {
            // 1. Создаем конфигурацию программно
            SetupNLogConfiguration();

            // Получаем логгер после настройки
            _logger = LogManager.GetCurrentClassLogger();
            
            
        }
        static void SetupNLogConfiguration()
        {
            try
            {
                // Создаем новую конфигурацию
                var config = new LoggingConfiguration();

                // 2. Создаем целевой файл (FileTarget)
                var fileTarget = new FileTarget("fileTarget")
                {
                    // Путь к файлу с датой в имени
                    FileName = Path.Combine("AppData\\logs", "${shortdate}.log"),

                    // Макет сообщения
                    Layout = "${longdate}|${level:uppercase=true}|${logger}|${message}${exception:format=tostring}",

                    // Настройки ротации логов
                    ArchiveFileName = Path.Combine("AppData\\logs\\archives", "{#}.log"),
                    ArchiveEvery = FileArchivePeriod.Day,
                    //ArchiveSuffixFormat = ArchiveNumberingMode.Rolling,
                    MaxArchiveFiles = 14,
                    KeepFileOpen = true,  // Для повышения производительности
                                          //ConcurrentWrites = true
                };

                // 3. Обертываем в асинхронную обертку
                var asyncWrapper = new AsyncTargetWrapper("asyncTarget", fileTarget)
                {
                    // Настройки буферизации
                    QueueLimit = 10000,          // Макс. элементов в очереди
                    OverflowAction = AsyncTargetWrapperOverflowAction.Discard,
                    TimeToSleepBetweenBatches = 0 // Минимальная задержка
                };

                // 4. Добавляем правило для всех логов
                config.AddRule(LogLevel.Trace, LogLevel.Fatal, asyncWrapper);

                // 5. Применяем конфигурацию
                LogManager.Configuration = config;

                //// Опционально: внутреннее логирование NLog для отладки
                //LogManager.ThrowExceptions = true; // Только для отладки!
                //InternalLogger.LogToConsole = true;
                //InternalLogger.LogLevel = LogLevel.Info;
            }
            catch (Exception ex)
            {

                XtraMessageBox.Show($"Ошибка загрузки NLog: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
            
        }

        public static void Info(string contextMessage)
        {
            _logger.Info($"{contextMessage}");
        }
        public static void Warn(string contextMessage)
        {
            _logger.Warn($"{contextMessage}");
        }
        public static void Debug(string contextMessage)
        {
            _logger.Debug($"{contextMessage}");
        }
        public static void Fatal(Exception ex, string contextMessage = "Ошибка в приложении", bool passTheErrorOn = true)
        {
            
            try
            {
                _realError = true;
                // Логирование с дополнительным контекстом
                _logger.Fatal($"{contextMessage}: {ex.GetType().Name}", ex);

                // Отправка уведомлений (при необходимости)
                if (EnableErrorNotifications)
                {
                    SendErrorNotification(ex, contextMessage);
                }

                // Показать пользователю
                if(!_errorAlreadyShown)
                {
                    ShowUserError(contextMessage, ex);
                    _errorAlreadyShown=true;
                }
                
                
            }
            catch (Exception handlerEx)
            {
                _logger.Fatal($"CRITICAL ERROR: {handlerEx.GetType().Name}", handlerEx);
                // Фолбэк на консоль, если всё сломалось
                XtraMessageBox.Show($"CRITICAL ERROR: {handlerEx}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                XtraMessageBox.Show($"Original error: {ex}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            if (_realError && passTheErrorOn)
                throw ex;




        }
        public static void Error(Exception ex, string contextMessage = "Ошибка в приложении",bool showError = true)
        {
            try
            {
                // Логирование с дополнительным контекстом
                _logger.Error($"{contextMessage}: {ex.GetType().Name}", ex);

                // Отправка уведомлений (при необходимости)
                if (EnableErrorNotifications)
                {
                    SendErrorNotification(ex, contextMessage);
                }

                // Показать пользователю
                if(showError && !_errorAlreadyShown)
                    ShowUserError(contextMessage, ex);
                if (_realError)
                    throw ex;
            }
            catch (Exception handlerEx)
            {
                // Фолбэк на консоль, если всё сломалось
                XtraMessageBox.Show($"CRITICAL ERROR: {handlerEx}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                XtraMessageBox.Show($"Original error: {ex}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                
            }
        }

        public static void ResetError()
        {
            _errorAlreadyShown = false;
            _realError = false;

        }

        private static void ShowUserError(string contextMessage, Exception ex)
        {
            if (ShowErrorDetails)
            {
                XtraMessageBox.Show($"{contextMessage}:\n\n{ex}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                XtraMessageBox.Show($"{contextMessage}. Детали смотрите в логах.", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void SendErrorNotification(Exception ex, string contextMessage)
        {
            try
            {
                // Реализация отправки уведомления (email, Slack и т.д.)
                // ...
            }
            catch (Exception notifEx)
            {
                _logger.Error("Ошибка отправки уведомления", notifEx);
            }
        }
    }
}
