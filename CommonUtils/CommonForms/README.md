# CommonForms

Windows Forms библиотека для CommonUtils - содержит базовые формы, контролы и утилиты для разработки WinForms приложений.

## Описание

CommonForms предоставляет набор базовых компонентов и утилит для создания Windows Forms приложений:

- Базовые формы с общим функционалом
- Интеграция с DevExpress компонентами
- Логирование через NLog
- Общие контролы и диалоги

## Зависимости

- .NET 8.0 (Windows)
- CommonUtils библиотека
- DevExpress WinForms компоненты
- NLog для логирования

## Использование

```csharp
using CommonForms.Views;

// Использование базовой формы
public partial class MyForm : BaseForm
{
    public MyForm()
    {
        InitializeComponent();
    }
}
```

## Установка

```bash
dotnet add package GurJack.CommonUtils.CommonForms
```

## Требования

- Windows операционная система
- .NET 8.0 Runtime
- DevExpress лицензия (для использования DevExpress компонентов)
