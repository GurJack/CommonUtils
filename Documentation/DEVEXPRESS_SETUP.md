# Настройка DevExpress пакетов для CommonForms

Этот документ описывает как настроить работу с DevExpress пакетами в проекте CommonForms.

## Проблема

DevExpress компоненты - это коммерческие лицензированные пакеты, которые:
- Не доступны в публичных NuGet feed
- Не могут быть включены в Git репозиторий из-за лицензионных ограничений
- Блокируют сборку в GitHub Actions

## Новое решение: Публикация CommonForms как NuGet пакета

Теперь CommonForms может быть опубликован как NuGet пакет в GitHub Packages! Это стало возможно благодаря новому подходу с использованием GitHub Secrets.

### Как это работает:
1. **Локально**: DevExpress пакеты загружаются из LocalPackages/
2. **В GitHub Actions**: Пакеты загружаются из GitHub Secrets и распаковываются во время сборки
3. **Результат**: CommonForms собирается и публикуется как полноценный NuGet пакет

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

### Решение 2: GitHub Secrets (Рекомендуется для CI/CD)

1. **Запустите скрипт кодирования пакетов:**
   ```powershell
   .\Scripts\encode-devexpress-for-github.ps1
   ```

2. **Скрипт автоматически:**
   - Найдет пакеты в `LocalPackages/`
   - Закодирует их в base64
   - Создаст инструкции для GitHub Secrets

3. **Добавьте секреты в GitHub:**
   - Следуйте инструкциям в `Scripts\GitHubSecrets\INSTRUCTIONS.md`
   - Добавьте все секреты в настройки репозитория

### Решение 3: Условная компиляция (Альтернативное)

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
- В GitHub Actions: DevExpress пакеты загружаются из GitHub Secrets

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

### Настройка GitHub Actions

1. **Закодируйте пакеты для GitHub:**
   ```powershell
   .\Scripts\encode-devexpress-for-github.ps1
   ```

2. **Добавьте секреты в GitHub:**
   - Откройте `Scripts\GitHubSecrets\INSTRUCTIONS.md`
   - Следуйте инструкциям для добавления секретов

3. **Запустите GitHub Actions**

### Обновление пакетов DevExpress

При обновлении версии DevExpress:

1. **Обновите версию в скрипте:**
   ```powershell
   .\Scripts\copy-devexpress-packages.ps1 -DevExpressVersion "23.3.1"
   ```

2. **Обновите версии в CommonForms.csproj**

3. **Перекодируйте пакеты для GitHub:**
   ```powershell
   .\Scripts\encode-devexpress-for-github.ps1
   ```

4. **Обновите секреты в GitHub**

## GitHub Actions

### Новое поведение

GitHub Actions теперь автоматически:
- Распаковывает DevExpress пакеты из GitHub Secrets
- Собирает все проекты включая CommonForms
- Упаковывает и публикует все NuGet пакеты
- CommonForms теперь публикуется как полноценный NuGet пакет!

### Результат в GitHub Packages

Публикуются все пакеты:
- ✅ GurJack.CommonUtils
- ✅ GurJack.CommonUtils.BaseData
- ✅ GurJack.CommonUtils.CommonData
- ✅ GurJack.CommonUtils.BaseMSSqlProvider
- ✅ GurJack.CommonUtils.CommonForms

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
- Нельзя перераспределять через GitHub Packages публично
- Каждый разработчик должен иметь собственную лицензию
- При использовании GitHub Secrets убедитесь, что репозиторий приватный

## Поддержка

При проблемах с DevExpress пакетами:

1. Проверьте установку DevExpress
2. Запустите диагностический скрипт
3. Проверьте лицензию DevExpress
4. Обратитесь к документации DevExpress
