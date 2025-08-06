namespace CommonUtils.ConfigProviders
{
    /// <summary>
    /// The enumeration of config name.
    /// </summary>
    public enum ConfigName
    {
        /// <summary>
        /// WindowStateMaximized
        /// </summary>
        WindowStateMaximized,

        /// <summary>
        /// Height
        /// </summary>
        Height,

        /// <summary>
        /// Width
        /// </summary>
        Width,

        /// <summary>
        /// Left
        /// </summary>
        Left,

        /// <summary>
        /// Top
        /// </summary>
        Top,

        /// <summary>
        /// CurrentKey 
        /// </summary>
        CurrentKey,

        /// <summary>
        /// SelectedKeys
        /// </summary>
        SelectedKeys,

        /// <summary>
        /// Text
        /// </summary>
        Text,

        /// <summary>
        /// Value
        /// </summary>
        Value,

        /// <summary>
        /// Отчет по умолчанию
        /// </summary>
        DefaultActionReport,

        /// <summary>
        /// SelectedPage
        /// </summary>
        SelectedPage,

        /// <summary>
        /// ExpandedRows
        /// </summary>
        ExpandedRows,

        /// <summary>
        /// PopupHeight
        /// </summary>
        PopupHeight,

        /// <summary>
        /// PopupWidth
        /// </summary>
        PopupWidth,

        /// <summary>
        /// DevExpress layout control.
        /// </summary>
        DXLayoutControl,

        /// <summary>
        /// DevExpress grid control.
        /// </summary>
        DXGridControl,

        /// <summary>
        /// DXCustomizationIsActive
        /// </summary>
        DXCustomizationIsActive,

        /// <summary>
        /// ControlVisualType
        /// </summary>
        ControlVisualType,

        /// <summary>
        /// TransportProviderType
        /// </summary>
        TransportProviderType,

        /// <summary>
        /// LoginServer.
        /// </summary>
        LoginServer,

        /// <summary>
        /// MetadataStorage.
        /// </summary>
        MetadataStorage,

        /// <summary>
        /// Camera name
        /// </summary>
        CamName,

        /// <summary>
        /// CamResolutionX
        /// </summary>
        CamResolutionX,

        /// <summary>
        /// CamResolutionY
        /// </summary>
        CamResolutionY,

        /// <summary>
        /// CamScanFrequency - частота кадров
        /// </summary>
        CamScanFrequency,

        /// <summary>
        /// CamAreaCapturePercent - процент захвата
        /// </summary>
        CamAreaCapturePercent,

        /// <summary>
        /// CamBeepScanner - звуковой сигнал при сканировании
        /// </summary>
        CamBeepScanner,

        /// <summary>
        /// ScannerNamesList
        /// </summary>
        ScannerNamesList,

        /// <summary>
        /// ScannerNamesList
        /// </summary>
        ReceiptNamesList,

        /// <summary>
        /// Document printer.
        /// </summary>
        DocumentPrinter,

        /// <summary>
        /// barcode printer
        /// </summary>
        BarcodePrinter,

        /// <summary>
        /// Подробный учет наличных на машинке 
        /// </summary>
        EnableCashOperations,

        /// <summary>
        /// Тип чекового принтера
        /// </summary>
        PrinterReceiptType,

        /// <summary>
        /// Тип платженого терминала
        /// </summary>
        TerminalType,

        /// <summary>
        /// Текущее id магазина
        /// </summary>
        CurrentIDShop,

        /// <summary>
        /// Текущее id фискального регистратора
        /// </summary>
        CurrentIDFiscalRegister,

        /// <summary>
        /// Текущий тип регистратора RefFRegister
        /// </summary>
        CurrentFiscalRegisterTypeId,

        /// <summary>
        /// Автоматическое обнуление наличности при закрытии смены
        /// </summary>
        CloseDayCashOut,

        /// <summary>
        /// Номер ком-порта сканера
        /// </summary>
        PortNumber,

        /// <summary>
        /// Тип основного сканера 
        /// </summary>
        ScannerType1,

        /// <summary>
        /// Тип резервного сканера 
        /// </summary>
        ScannerType2,

        /// <summary>
        /// СКорость чтения
        /// </summary>
        BaudRate,

        /// <summary>
        /// Задержка чтения 
        /// </summary>
        ReadDelay,

        /// <summary>
        /// Задержка записи
        /// </summary>
        WriteDelay,

        /// <summary>
        /// Использовать сканер в системе
        /// </summary>
        UseScanner,

        /// <summary>
        /// Использовать чековый принтер в системе
        /// </summary>
        UseReceipt,

        /// <summary>
        /// Использовать платежный терминал в системе
        /// </summary>
        UseTerminal,

        /// <summary>
        /// Использовать дисплей покупателя
        /// </summary>
        UseDisplay,


        /// <summary>
        /// Использовать всплывающие сообщения
        /// </summary>
        UseToastNotification,

        /// <summary>
        /// Фискальный режим
        /// </summary>
        IsFiscalMode,

        /// <summary>
        /// Количество товарных чеков
        /// </summary>
        PrintCheckCount,

        /// <summary>
        /// Попозиционная печать 
        /// </summary>
        IsPositionalPrint,

        /// <summary>
        /// Приветственное сообщение
        /// </summary>
        GreetingWords,

        /// <summary>
        /// Наименование фирмы
        /// </summary>
        FirmName,

        /// <summary>
        /// Заменить неверные символы на дисплее покупателя
        /// </summary>
        IsReplace,

        /// <summary>
        /// Приветственная строка для дисплея
        /// </summary>
        CustomerDisplayHello,

        /// <summary>
        /// Название фирмы для дисплея
        /// </summary>
        CustomerDisplayFirm,

        /// <summary>
        /// Заменять символы
        /// </summary>
        CustomerDisplayReplace,

        /// <summary>
        /// Использовать дисплей покупателя
        /// </summary>
        CustomerDisplayIsActive,

        /// <summary>
        /// Длина дисплейя покупателя (в символах)
        /// </summary>
        CustomerDisplayLength,

        /// <summary>
        /// Путь к Output Folder
        /// </summary>
        TerminalOutFolder,

        /// <summary>
        /// Печать слипов на чековом
        /// </summary>
        TerminalPrintReceipt,

        /// <summary>
        /// Использовать платежный терминал
        /// </summary>
        TerminalIsActive,

        /// <summary>
        /// имя пос-принтера
        /// </summary>
        PosPrinterName,

        /// <summary>
        /// количество строк перед отрезкой
        /// </summary>
        LineNumberBeforeCut,

        /// <summary>
        /// текстовое представление конфига
        /// </summary>
        TextRepresenation,

        /// <summary>
        /// Имя e-mail протокола
        /// </summary>
        ProtocolName,

        /// <summary>
        /// Использование учетной записи по умолчанию у e-mail протокола
        /// </summary>
        ProtocolIsUseDefaultCredential,

        /// <summary>
        /// Порт e-mail протокола
        /// </summary>
        ServerPort,

        /// <summary>
        /// Хост e-mail протокола
        /// </summary>
        ServerHost,

        /// <summary>
        /// Адрес поддержки e-mail протокола
        /// </summary>
        EmailSupportAddress,

        /// <summary>
        /// Провайдер 
        /// </summary>
        Provider,

        /// <summary>
        /// Логин для провайдера
        /// </summary>
        ProviderLogin,

        /// <summary>
        /// Пароль для провайдера
        /// </summary>
        ProviderPassword,

        /// <summary>
        /// Отправитель sms провайдера
        /// </summary>
        SenderAddress,

        /// <summary>
        /// Частота, с которой необходимо проеврять наличие обновлений, в минутах
        /// </summary>
        MinutesToUpdate
    }
}