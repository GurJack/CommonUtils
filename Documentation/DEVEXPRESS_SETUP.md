# Настройка DevExpress пакетов для CommonForms

Этот документ описывает как настроить работу с DevExpress пакетами в проекте CommonForms.

## Проблема

DevExpress компоненты - это коммерческие лицензированные пакеты, которые:
- Не доступны в публичных NuGet feed
- Не могут быть включены в Git репозиторий из-за лицензионных ограничений
- Блокируют сборку в GitHub Actions

## Решения

### Решение 1: Локальные пакеты (Рекомендуется для разработки)

1. **Запустите скрипт копирования пакетов:**
   ```powershell
   .\Scripts\copy-devexpress-packages.ps1
   ```

2. **Скрипт автоматически:**
   - Найдет установленные пакеты DevExpress
   - Скопирует их в папку `LocalPackages/`
   - Настроит локальный NuGet feed

3. **Структура после выполнения:**
   ```
   LocalPackages/
   ├── DevExpress.Win.Design.23.2.3.nupkg
   ├── DevExpress.Office.Core.23.2.3.nupkg
   ├── DevExpress.Utils.23.2.3.nupkg
   └── DevExpress.Win.23.2.3.nupkg
   ```

### Решение 2: Условная компиляция (Используется в CI/CD)

Проект CommonForms настроен на условную загрузку DevExpress пакетов:

```xml
<!-- DevExpress только если не CI -->
<ItemGroup Condition="'$(CI)' != 'true'">
  <PackageReference Include="DevExpress.Win.Design" Version="23.2.3" />
  <PackageReference Include="DevExpress.Office.Core" Version="23.2.3" />
  <PackageReference Include="DevExpress.Utils" Version="23.2.3" />
  <PackageReference Include="DevExpress.Win" Version="23.2.3" />
</ItemGroup>
```

**Как работает:**
- При локальной разработке: DevExpress пакеты загружаются из LocalPackages/
- В GitHub Actions: переменная CI=true, DevExpress пакеты игнорируются

### Решение 3: Исключение CommonForms из CI/CD

Если нужно полностью исключить CommonForms из автоматической сборки:

1. **Обновите GitHub Actions workflow:**
   ```yaml
   - name: Pack NuGet packages
     run: |
       $projects = Get-ChildItem -Recurse -Filter *.csproj |
         Where-Object { $_.FullName -notmatch 'test|Test|CommonForms' }
   ```

2. **Создайте отдельный workflow для локальной сборки CommonForms**

## Настройка разработчика

### Первоначальная настройка

1. **Установите DevExpress (если еще не установлен)**

2. **Скопируйте пакеты:**
   ```powershell
   cd d:\Jack\Git\CommonUtils
   .\Scripts\copy-devexpress-packages.ps1
   ```

3. **Проверьте конфигурацию:**
   ```powershell
   dotnet nuget list source
   ```

4. **Соберите проект:**
   ```powershell
   dotnet build CommonUtils\CommonForms\CommonForms.csproj
   ```

### Обновление пакетов DevExpress

При обновлении версии DevExpress:

1. **Обновите версию в скрипте:**
   ```powershell
   .\Scripts\copy-devexpress-packages.ps1 -DevExpressVersion "23.3.1"
   ```

2. **Обновите версии в CommonForms.csproj**

3. **Пересоберите проект**

## GitHub Actions

### Текущее поведение

GitHub Actions автоматически:
- Устанавливает `CI=true`
- Исключает DevExpress пакеты из сборки
- Собирает все остальные проекты
- Публикует пакеты без CommonForms

### Результат в GitHub Packages

Публикуются пакеты:
- ✅ GurJack.CommonUtils
- ✅ GurJack.CommonUtils.BaseData
- ✅ GurJack.CommonUtils.CommonData
- ✅ GurJack.CommonUtils.BaseMSSqlProvider
- ❌ GurJack.CommonUtils.CommonForms (только локально)

## Альтернативные подходы

### 1. Mock пакеты для CI

Создать пустые пакеты DevExpress только для CI:

```xml
<ItemGroup Condition="'$(CI)' == 'true'">
  <PackageReference Include="DevExpress.Win.Design.Mock" Version="1.0.0" />
</ItemGroup>
```

### 2. Условные директивы в коде

```csharp
#if !CI_BUILD
using DevExpress.XtraEditors;
#endif
```

### 3. Separate решение для CI

Создать отдельный `.sln` файл без CommonForms для CI/CD.

## Лицензионные соображения

⚠️ **Важно:**
- DevExpress пакеты защищены лицензией
- Нельзя включать в публичный репозиторий
- Нельзя перераспределять через GitHub Packages
- Каждый разработчик должен иметь собственную лицензию

## Поддержка

При проблемах с DevExpress пакетами:

1. Проверьте установку DevExpress
2. Запустите диагностический скрипт
3. Проверьте лицензию DevExpress
4. Обратитесь к документации DevExpress
