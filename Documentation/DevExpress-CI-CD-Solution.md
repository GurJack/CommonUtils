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

## Примененное решение

### 1. Временное исключение CommonForms из CI/CD

В `.github/workflows/dotnet-desktop.yml` реализован механизм исключения проекта CommonForms:

- **Создается временный solution файл** без ссылок на CommonForms
- **Сборка, тестирование и упаковка** выполняются только для проектов без DevExpress зависимостей
- **CommonForms исключается** из автоматической упаковки в NuGet

### 2. Что собирается в CI/CD

✅ **Включено в CI/CD:**
- `CommonUtils.csproj` - основная библиотека
- `BaseData.csproj` - работа с данными
- `BaseMSSqlProvider.csproj` - SQL Server провайдер
- `CommonData.csproj` - общие классы данных
- `Core.Tests.csproj` - основные тесты
- `Windows.Tests.csproj` - тесты для Windows

❌ **Исключено из CI/CD:**
- `CommonForms.csproj` - требует DevExpress пакеты

### 3. Локальная разработка

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

### Вариант C: Отдельный workflow
Создать отдельный workflow только для core пакетов без DevExpress.

## Рекомендации

1. **Для продакшена:** Рассмотрите включение DevExpress пакетов в репозиторий если лицензия позволяет
2. **Для разработки:** Используйте скрипт `copy-devexpress-packages.ps1` для настройки локального окружения
3. **Для CI/CD:** Текущее решение с исключением CommonForms работает стабильно

## Файлы, затронутые решением

- `.github/workflows/dotnet-desktop.yml` - обновленный CI/CD workflow
- `LocalPackages/README.md` - документация по DevExpress пакетам
- `Scripts/copy-devexpress-packages.ps1` - скрипт копирования пакетов
- `nuget.config` - настройка локального источника пакетов

## Проверка работоспособности

После применения изменений CI/CD должен:
1. ✅ Успешно восстанавливать зависимости
2. ✅ Собирать все проекты кроме CommonForms
3. ✅ Упаковывать и публиковать NuGet пакеты
4. ✅ Проходить все тесты
