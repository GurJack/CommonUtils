# Локальные пакеты DevExpress

Эта папка содержит локальные копии пакетов DevExpress, необходимых для сборки проекта CommonForms.

## ⚠️ Важное обновление

**Пакеты DevExpress теперь могут быть использованы в GitHub Actions!**

Это означает:
- ✅ GitHub Actions может собирать CommonForms автоматически
- ✅ Все разработчики получают одинаковые версии пакетов
- ✅ CommonForms публикуется как NuGet пакет
- ⚠️ Убедитесь, что у вас есть права на распространение этих пакетов

## Как добавить пакеты DevExpress

### Вариант 1: Из локальной установки DevExpress

1. Найдите папку с установленными пакетами DevExpress (обычно):
   ```
   C:\Program Files (x86)\DevExpress 23.2\Components\Bin\Framework\
   ```

2. Скопируйте нужные .nupkg файлы в эту папку:
   - DevExpress.Win.Design.23.2.3.nupkg
   - DevExpress.Office.Core.23.2.3.nupkg
   - DevExpress.Utils.23.2.3.nupkg
   - DevExpress.Win.23.2.3.nupkg

### Вариант 2: Скрипт для копирования (рекомендуется)

Запустите скрипт `Scripts/copy-devexpress-packages.ps1` который автоматически найдет и скопирует пакеты.

### Вариант 3: Для GitHub Actions

1. Запустите скрипт `Scripts/encode-devexpress-for-github.ps1`
2. Следуйте инструкциям в `GitHubSecrets/INSTRUCTIONS.md`
3. Добавьте секреты в настройки GitHub репозитория

## Структура папки

```
LocalPackages/
├── DevExpress.Win.Design.23.2.3.nupkg
├── DevExpress.Office.Core.23.2.3.nupkg
├── DevExpress.Utils.23.2.3.nupkg
└── DevExpress.Win.23.2.3.nupkg
```

## Важно для Git

Пакеты DevExpress НЕ должны коммититься в Git из-за лицензионных ограничений.
Добавлены в .gitignore.

## GitHub Actions

Для GitHub Actions создан специальный скрипт который:
1. Кодирует пакеты в base64
2. Подготавливает инструкции для добавления секретов
3. Позволяет GitHub Actions использовать пакеты из секретов

## Альтернативные решения

1. **Исключить CommonForms из CI/CD** - собирать только основные пакеты
2. **Создать mock пакеты** - пустые пакеты с теми же именами для CI
3. **Использовать условную компиляцию** - разные зависимости для разных окружений
