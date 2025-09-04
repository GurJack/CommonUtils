using CommonUtils.Settings.Attributes;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static DevExpress.Office.Drawing.LazyGroupBrush;

namespace CommonForms.Views
{
    public partial class ModuleSettingsFormBase : XtraUserControl
    {
        protected object Settings { get; private set; }
        private Dictionary<BaseEdit, PropertyInfo> _controlPropertyMap = new Dictionary<BaseEdit, PropertyInfo>();

        public ModuleSettingsFormBase(object settings)
        {
            Settings = settings ?? throw new ArgumentNullException(nameof(settings));
            Initialize();
            InitializeComponent();
            GenerateForm();
        }

        private void Initialize()
        {
            this.AutoScroll = true;
            this.Dock = DockStyle.Fill;
        }

        private void GenerateForm()
        {
            var properties = Settings.GetType()
                .GetProperties()
                .Where(p => p.CanRead && p.CanWrite)
                .Where(p => p.GetCustomAttribute<DoNotSaveToFileAttribute>() == null)
                .Select(p => new {
                    Property = p,
                    Attribute = p.GetCustomAttribute<DisplaySettingsAttribute>()
                })
                .OrderBy(x => x.Attribute?.Order ?? int.MaxValue)
                .ThenBy(x => x.Property.Name)
                .ToList();

            // Группировка по категориям
            var grouped = properties
                .GroupBy(x => x.Attribute?.Category ?? "General")
                .OrderBy(g => g.Key);

            int yPos = 10;
            const int margin = 10;
            const int labelWidth = 200;
            const int controlWidth = 300;
            const int rowHeight = 30;
            const int groupSpacing = 20;

            foreach (var group in grouped)
            {
                // Добавляем заголовок группы
                if (!string.IsNullOrEmpty(group.Key))
                {
                    var groupLabel = new LabelControl
                    {
                        Text = group.Key,
                        Font = new Font("Tahoma", 10, FontStyle.Bold),
                        Location = new System.Drawing.Point(margin, yPos),
                        AutoSize = true
                    };
                    this.Controls.Add(groupLabel);
                    yPos += 30;
                }

                foreach (var item in group)
                {
                    var prop = item.Property;
                    var attr = item.Attribute;

                    // Создаем метку
                    var label = new LabelControl
                    {
                        Text = attr?.DisplayName ?? GetDisplayName(prop),
                        Location = new System.Drawing.Point(margin + 20, yPos),
                        Width = labelWidth - 20
                    };

                    if (!string.IsNullOrEmpty(attr?.Description))
                    {
                        label.ToolTip = attr.Description;
                    }

                    this.Controls.Add(label);

                    // Создаем элемент управления
                    BaseEdit control = CreateControlForProperty(prop);
                    control.Location = new System.Drawing.Point(margin + labelWidth + margin, yPos);
                    control.Width = controlWidth;

                    if (!string.IsNullOrEmpty(attr?.Description))
                    {
                        control.ToolTip = attr.Description;
                    }

                    // Привязываем значение
                    control.EditValue = prop.GetValue(Settings);
                    control.EditValueChanged += (s, e) => UpdatePropertyValue(control, prop);

                    this.Controls.Add(control);
                    _controlPropertyMap.Add(control, prop);

                    yPos += rowHeight;
                }

                yPos += groupSpacing;
            }
        }

        private string GetDisplayName(PropertyInfo prop)
        {
            var displayName = prop.GetCustomAttribute<DisplayNameAttribute>();
            return displayName?.DisplayName ?? prop.Name;
        }

        private BaseEdit CreateControlForProperty(PropertyInfo prop)
        {
            Type propType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;

            if (propType == typeof(bool))
            {
                return new CheckEdit();
            }
            else if (propType.IsEnum)
            {
                var combo = new ComboBoxEdit();
                combo.Properties.Items.AddRange(Enum.GetNames(propType));
                return combo;
            }
            else if (propType == typeof(int) || propType == typeof(double) || propType == typeof(decimal))
            {
                return new SpinEdit();
            }
            else if (propType == typeof(DateTime))
            {
                return new DateEdit();
            }
            else
            {
                var textEdit = new TextEdit();

                // Для конфиденциальных данных используем маскировку
                var cryptAttr = prop.GetCustomAttribute<CryptAttribute>();
                //if (cryptAttr?.IsCrypt == true)
                //{
                //    textEdit.Properties.PasswordChar = '*';
                //}

                return textEdit;
            }
        }

        private void UpdatePropertyValue(BaseEdit control, PropertyInfo prop)
        {
            try
            {
                object value = control.EditValue;
                Type propType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;

                // Конвертируем значение, если необходимо
                if (value != null && value.GetType() != propType)
                {
                    if (propType.IsEnum)
                    {
                        value = Enum.Parse(propType, value.ToString());
                    }
                    else
                    {
                        value = Convert.ChangeType(value, propType);
                    }
                }

                prop.SetValue(Settings, value);
            }
            catch (Exception ex)
            {
                // Обработка ошибок преобразования
                XtraMessageBox.Show($"Ошибка установки значения: {ex.Message}");
            }
        }

        public void ApplyChanges()
        {
            // Все изменения применяются в реальном времени через EditValueChanged
            // Этот метод можно использовать для дополнительных действий при сохранении
        }
    }
}
