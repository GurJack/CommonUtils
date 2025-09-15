# DevExpress и CI/CD - Решение проблемы

## Проблема
При запуске GitHub Actions возникают ошибки:
```
error NU1101: Unable to find package DevExpress.Win.Design
error NU1101: Unable to find package DevExpress.Utils
error NU1101: Unable to find package DevExpress.Win
```

## Причина
Пакеты DevExpress являются коммерческими и недоступны в публичных источниках NuGet (nuget.org). Они требуют лицензии и обычно устанавливаются локально.

## Новое решение: Публикация CommonForms как NuGet пакета

### 1. Использование секретов GitHub для хранения DevExpress пакетов

В `.github/workflows/dotnet-desktop.yml` реализован механизм:

- **Хранение пакетов как секретов**: DevExpress пакеты хранятся в GitHub Secrets в формате base64
- **Автоматическое распаковывание**: Пакеты распаковываются во время CI/CD процесса
- **Полная сборка**: CommonForms собирается и упаковывается как обычный NuGet пакет
- **Публикация**: Все пакеты, включая CommonForms, публикуются в GitHub Packages

### 2. Что собирается и публикуется

✅ **Включено в CI/CD и публикуется:**
- `CommonUtils.csproj` - основная библиотека
- `BaseData.csproj` - работа с данными
- `BaseMSSqlProvider.csproj` - SQL Server провайдер
- `CommonData.csproj` - общие классы данных
- `CommonForms.csproj` - формы и контролы с DevExpress
- `Core.Tests.csproj` - основные тесты
- `Windows.Tests.csproj` - тесты для Windows

### 3. Настройка секретов GitHub

Для работы решения необходимо:

1. **Подготовить пакеты**:
   ```powershell
   # Запустить скрипт для копирования пакетов
   .\Scripts\copy-devexpress-packages.ps1
   ```

2. **Закодировать пакеты в base64**:
   ```powershell
   # Для каждого пакета из LocalPackages/
   [Convert]::ToBase64String([IO.File]::ReadAllBytes("LocalPackages\DevExpress.Win.Design.23.2.3.nupkg"))
   ```

3. **Добавить секреты в GitHub**:
   - `DEVEXPRESS_WIN_DESIGN_23_2_3_NUPKG` - содержимое DevExpress.Win.Design.23.2.3.nupkg
   - `DEVEXPRESS_OFFICE_CORE_23_2_3_NUPKG` - содержимое DevExpress.Office.Core.23.2.3.nupkg
   - `DEVEXPRESS_UTILS_23_2_3_NUPKG` - содержимое DevExpress.Utils.23.2.3.nupkg
   - `DEVEXPRESS_WIN_23_2_3_NUPKG` - содержимое DevExpress.Win.23.2.3.nupkg

### 4. Локальная разработка

Для локальной разработки с CommonForms:

1. **Установите DevExpress** на локальную машину
2. **Скопируйте пакеты** в папку `LocalPackages/`:
   ```powershell
   .\Scripts\copy-devexpress-packages.ps1
   ```
3. **Собирайте полное решение** локально:
   ```powershell
   dotnet build CommonUtils/CommonUtils.sln
   ```

## Альтернативные решения

### Вариант A: Включение пакетов в репозиторий
```powershell
# Скопировать DevExpress пакеты в LocalPackages и закоммитить их
.\Scripts\copy-devexpress-packages.ps1
git add LocalPackages/*.nupkg
git commit -m "add: DevExpress packages for CI/CD"
```

**⚠️ Внимание:** Проверьте лицензионные ограничения DevExpress!

### Вариант B: Условная компиляция
Изменить `CommonForms.csproj` для использования условных ссылок:

```xml
<PackageReference Include="DevExpress.Win.Design" Version="23.2.3"
                  Condition="'$(CI)' != 'true'" />
```

Проблема: Нужно переписать много кода для работы без DevExpress.

## Рекомендации

1. **Для продакшена:** Используйте решение с секретами GitHub
2. **Для разработки:** Используйте скрипт `copy-devexpress-packages.ps1` для настройки локального окружения
3. **Для CI/CD:** Текущее решение работает стабильно и публикует все пакеты

## Статус NuGet пакетов

После применения изменений в GitHub Packages публикуются:

- ✅ **GurJack.CommonUtils** - основная библиотека
- ✅ **GurJack.CommonUtils.BaseData** - базовые классы данных
- ✅ **GurJack.CommonUtils.CommonData** - общие классы данных
- ✅ **GurJack.CommonUtils.BaseMSSqlProvider** - SQL Server провайдер
- ✅ **GurJack.CommonUtils.CommonForms** - формы и контролы с DevExpress

## Проверка работоспособности

После применения изменений CI/CD должен:
1. ✅ Успешно восстанавливать все зависимости
2. ✅ Собирать все проекты включая CommonForms
3. ✅ Упаковывать и публиковать все NuGet пакеты
4. ✅ Проходить все тесты

## Файлы, затронутые решением

- `.github/workflows/dotnet-desktop.yml` - обновленный CI/CD workflow
- `CommonUtils/CommonForms/CommonForms.csproj` - условные зависимости
- `LocalPackages/README.md` - документация по DevExpress пакетам
- `Scripts/copy-devexpress-packages.ps1` - скрипт копирования пакетов
